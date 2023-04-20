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
        public ActionResult Index(string slmCode, string[] cusCode, string[] stkGrp, string prodMgr, string[] stkGroup, string year)
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
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    //list slmcode
                    var command = new SqlCommand("P_Search_Name_Budget_Forecast", Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UsrID", user);
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        SlmCodeList.Add(new SelectListItem() { Value = dr["SLMCOD"].ToString(), Text = dr["SLMCOD"].ToString() + "/" + dr["SLMNAM"].ToString() });
                    }
                    dr.Close();
                    dr.Dispose();


                    //list prod
                    command = new SqlCommand("P_Search_Budget_Forecast", Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@inUsrID", user);
                    command.Parameters.AddWithValue("@inType", "PRDNAME");
                    command.Parameters.AddWithValue("@inSearch", "ALL");
                    //command.ExecuteNonQuery();
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

                    command.Dispose();
                    Connection.Close();
                }
            }
            var SearchBudgetSale = new List<StoreSearchBudgetSaleModel>();
            ViewBag.stkGroupList = SearchBudgetSale;
            //stkGroup null
            if (slmCode != null || cusCode != null)
            {
                SearchBudgetSale = new SearchBudgetSale().GetStoreSearchForecastPm(slmCode, cusCode, stkGrp, year);
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
                    CUSTYP = rev_CUSPROV["CUSTYP"].ToString(),
                    AACCrlimit = rev_CUSPROV["AACCRLINE"].ToString(),
                    AACBalance = rev_CUSPROV["AACBAL"].ToString(),
                    TACCrlimit = rev_CUSPROV["TACCRLINE"].ToString(),
                    TACBalance = rev_CUSPROV["TACBAL"].ToString()
                });
            }
            //rev_CUSPROV.Dispose();
            //S20161016
            rev_CUSPROV.Close();
            rev_CUSPROV.Dispose();
            cmd.Dispose();
            //E20161016
            Connection.Close();
            return Json(CUSList, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetSTKGRP(string ProdMRG)
        {
            List<STKGRPList> STKGRPList = new List<STKGRPList>();
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString);
            Connection.Open();
            var command = new SqlCommand("P_Search_Budget_Forecast", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@inUsrID", ProdMRG);
            command.Parameters.AddWithValue("@inType", "STKGRP");
            command.Parameters.AddWithValue("@inSearch", "PROD");

            SqlDataReader rev_CUSPROV = command.ExecuteReader();
            while (rev_CUSPROV.Read())
            {
                STKGRPList.Add(new STKGRPList()
                {
                    STKGRP = rev_CUSPROV["STKGRP"].ToString(),
                    GRPNAM = rev_CUSPROV["GRPNAM"].ToString()
                });
            }
            //rev_CUSPROV.Dispose();
            //S20161016
            rev_CUSPROV.Close();
            rev_CUSPROV.Dispose();
            command.Dispose();
            //E20161016
            Connection.Close();
            return Json(STKGRPList, JsonRequestBehavior.AllowGet);

        }
    }
}