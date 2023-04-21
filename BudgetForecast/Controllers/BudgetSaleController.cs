using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BudgetForecast.Model;
using BudgetForecast.Data;
using BudgetForecast.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.DirectoryServices;
using BudgetForecast.Library;

namespace BudgetForecast.Controllers
{
    public class BudgetSaleController : Controller
    {
        // GET: BudgetSale
        public ActionResult Index(string slmCode, string[] cusCode, string[] stkSec, string prodMgr, string[] stkGroup, string year)
        {
            //Check login
            if (this.Session["UserType"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            else
            {
                string user = Session["UserID"].ToString();
                string slmCodeDefault = Session["SLMCOD"].ToString();
                List<SLM> SlmList = new List<SLM>();
                List<SelectListItem> SlmCodeList = new List<SelectListItem>();
                List<SelectListItem> ProdList = new List<SelectListItem>();
                List<SelectListItem> stkSecList = new List<SelectListItem>();
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    //list slmcode
                    var command = new SqlCommand("P_Search_Name_Budget_Forecast", Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UsrID", user);
                    command.Parameters.AddWithValue("@Flag", "sale");
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        SlmCodeList.Add(new SelectListItem() { Value = dr["SLMCOD"].ToString(), Text = dr["SLMCOD"].ToString() + "/" + dr["SLMNAM"].ToString() });
                    }
                    dr.Close();
                    dr.Dispose();
                    //list stkGrp
                    command = new SqlCommand("P_Search_Budget_Forecast", Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@inUsrID", slmCodeDefault);
                    command.Parameters.AddWithValue("@inType", "SEC");
                    //command.ExecuteNonQuery();
                    SqlDataReader dr2 = command.ExecuteReader();
                    while (dr2.Read())
                    {
                        stkSecList.Add(new SelectListItem() { Value = dr2["SEC"].ToString(), Text = dr2["SEC"].ToString() + "/" + dr2["SECNAM"].ToString() });
                    }
                    ViewBag.stkSecList = stkSecList;
                    dr2.Close();
                    dr2.Dispose();
                    command.Dispose();

                    //list prod
                    command = new SqlCommand("P_Search_Name_Budget_Forecast", Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UsrID", "thanaphan.s");
                    SqlDataReader dr3 = command.ExecuteReader();
                    while (dr3.Read())
                    {
                        ProdList.Add(new SelectListItem() { Value = dr3["PROD"].ToString(), Text = dr3["PROD"].ToString() + "/" + dr3["PRODNAM"].ToString() });

                    }
                    ViewBag.ProdList = ProdList;
                    //เอา userId เป็น default Prod name
                    ViewBag.prodMgr = prodMgr == null ? "[]" : prodMgr;
                    ViewBag.stkGroup = stkGroup == null ? "[]" : "[\"" + string.Join("\",\"", stkGroup.Select(x => x.ToString()).ToArray()) + "\"]";
                    dr3.Close();
                    dr3.Dispose();
                    command.Dispose();

                    if (slmCode != null)
                    {
                        //วันที่คีย์ได้ budget pm
                        var yearCurrent = DateTime.Now.Year.ToString();
                        var getDateInput = Utils.GetDateInput(3, yearCurrent);

                        ViewBag.flagInput = getDateInput.Item1;
                        ViewBag.startDate = getDateInput.Item2;
                        ViewBag.endDate = getDateInput.Item3;
                    }

                    ViewBag.SlmCodeList = SlmCodeList;
                    ViewBag.slmCode = slmCode == null ? slmCodeDefault : slmCode;
                    ViewBag.cusCode = cusCode == null ? "[]" : "[\"" + string.Join("\",\"", cusCode.Select(x => x.ToString()).ToArray()) + "\"]";
                    ViewBag.year = year == null ? DateTime.Now.Year.ToString() : year;
                    ViewBag.stkSec = stkSec == null ? "[]" : "[\"" + string.Join("\",\"", stkSec.Select(x => x.ToString()).ToArray()) + "\"]";

                    command.Dispose();
                    Connection.Close();
                }
            }
            var SearchBudgetSale = new List<StoreSearchBudgetSaleModel>();
            ViewBag.stkGroupList = SearchBudgetSale;
            //stkGroup null
            if (slmCode != null || cusCode != null)
            {
                SearchBudgetSale = new SearchBudgetSale().GetStoreSearchForecastPm(slmCode, cusCode, stkSec, year);
                ViewBag.stkGroupList = SearchBudgetSale;
            }

            return View();
        }
        [HttpPost]
        public ActionResult SaveBudget(List<StoreUpdateBudgetSaleModel> request)
        {
            var UpdateBudgetSale = new List<StoreUpdateBudgetSaleModel>();
            foreach (var listData in (List<StoreUpdateBudgetSaleModel>)request) 
            {
                UpdateBudgetSale = new UpdateBudgetSale().Update(listData.USER, listData.CUSCODE, listData.STKGRP, listData.F_SLM, listData.YEAR);
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult Getdatabyslm(string slmCode)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString;
            SqlConnection Connection = new SqlConnection(connectionString);
            Connection.Open();
            List<CUS> CUSList = new List<CUS>();
            SqlCommand cmd = new SqlCommand("select * from CUSPROV where SLMCOD =N'" + slmCode + "' order by SLMCOD", Connection);
            SqlDataReader rev_CUSPROV = cmd.ExecuteReader();
            while (rev_CUSPROV.Read())
            {
                CUSList.Add(new CUS()
                {
                    CUSCOD = rev_CUSPROV["CUSCOD"].ToString(),
                    CUSNAM = rev_CUSPROV["CUSNAM"].ToString(),
                    PRO = rev_CUSPROV["PRO"].ToString(),
                    ADDR_01 = rev_CUSPROV["ADDR_01"].ToString(),
                    CUSTYP = rev_CUSPROV["CUSTYP"].ToString()
                });
            }
            //add new slmcode
            CUSList.Add(new CUS() { CUSCOD = "NEW--" + slmCode, CUSNAM = "NEW--" + slmCode
            });
            rev_CUSPROV.Close();
            rev_CUSPROV.Dispose();
            cmd.Dispose();
            Connection.Close();
            return Json(CUSList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSTKGRP(string ProdMRG)
        {
            List<SECList> STKGRPList = new List<SECList>();
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString);
            Connection.Open();
            var command = new SqlCommand("P_Search_Budget_Forecast", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@inUsrID", ProdMRG);
            command.Parameters.AddWithValue("@inType", "SEC");

            SqlDataReader rev_CUSPROV = command.ExecuteReader();
            while (rev_CUSPROV.Read())
            {
                STKGRPList.Add(new SECList()
                {
                    SEC = rev_CUSPROV["SEC"].ToString(),
                    SECNAM = rev_CUSPROV["SECNAM"].ToString()
                });
            }
            rev_CUSPROV.Close();
            rev_CUSPROV.Dispose();
            command.Dispose();
            //E20161016
            Connection.Close();
            return Json(STKGRPList, JsonRequestBehavior.AllowGet);
        }
    }
}