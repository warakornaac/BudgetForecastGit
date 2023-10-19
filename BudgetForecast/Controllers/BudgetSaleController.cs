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
        public ActionResult Index(string slmCode, string[] cusCode, string[] stkSec, string prodMgr, string[] stkGroup, string year, int flgBudget = 0)
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
                string flagSup = string.Empty;
                List<SLM> SlmList = new List<SLM>();
                List<SelectListItem> SlmCodeList = new List<SelectListItem>();
                List<SelectListItem> ProdList = new List<SelectListItem>();
                List<SelectListItem> stkSecList = new List<SelectListItem>();
                List<SelectListItem> GroupStkGrp = new List<SelectListItem>();
                List<SelectListItem> STKGRPList = new List<SelectListItem>();

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
                    command.Parameters.AddWithValue("@inUsrID", "");
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

                    if (slmCodeDefault != null) {
                        SqlCommand cmd1 = new SqlCommand("select TOP 1 * From v_SLMTAB_SM_Userrid where SUP = N'" + slmCodeDefault + "'", Connection);
                        SqlDataReader rev = cmd1.ExecuteReader();
                        while (rev.Read())
                        {
                            flagSup = rev["SUP"].ToString();
                        }
                        rev.Close();
                        rev.Dispose();
                        cmd1.Dispose();
                    }
                    SqlCommand cmd2 = new SqlCommand("SELECT STKGRP,  GRPNAM FROM MST_STKGRP WHERE GRPNAM <> '' GROUP BY STKGRP,  GRPNAM", Connection);
                    SqlDataReader rev_stk = cmd2.ExecuteReader();
                    while (rev_stk.Read())
                    {
                        STKGRPList.Add(new SelectListItem() { Value = rev_stk["STKGRP"].ToString(), Text = rev_stk["STKGRP"].ToString() + "/" + rev_stk["GRPNAM"].ToString() });
                    }
                    rev_stk.Close();
                    rev_stk.Dispose();
                    cmd2.Dispose();
                    Connection.Close();

                    ViewBag.StkGrp = STKGRPList;
                    ViewBag.SlmCodeList = SlmCodeList;
                    ViewBag.slmCode = slmCode == null ? slmCodeDefault : slmCode;
                    ViewBag.cusCode = cusCode == null ? "[]" : "[\"" + string.Join("\",\"", cusCode.Select(x => x.ToString()).ToArray()) + "\"]";
                    ViewBag.year = year == null ? DateTime.Now.AddYears(1).Year.ToString() : year;
                    ViewBag.stkSec = stkSec == null ? "[]" : "[\"" + string.Join("\",\"", stkSec.Select(x => x.ToString()).ToArray()) + "\"]";
                    ViewBag.stkGroup = stkGroup == null ? "[]" : "[\"" + string.Join("\",\"", stkGroup.Select(x => x.ToString()).ToArray()) + "\"]";
                    ViewBag.flagSup = flagSup;

                }
            }
            var SearchBudgetSale = new List<StoreSearchBudgetSaleModel>();
            ViewBag.stkGroupList = SearchBudgetSale;
            //stkGroup null
            if (slmCode != null || cusCode != null)
            {
                SearchBudgetSale = new SearchBudgetSale().GetStoreSearchBudgetSale(slmCode, cusCode, stkSec, stkGroup, flgBudget);
                ViewBag.stkGroupList = SearchBudgetSale;
            }

            return View();
        }
        [HttpPost]
        public ActionResult SearchBudget(string slmCode, string[] cusCode, string[] stkSec, string prodMgr, string[] stkGroup, int flgBudget)
        {
            string user = Session["UserID"].ToString();
            string slmCodeDefault = Session["SLMCOD"].ToString();
            List<SLM> SlmList = new List<SLM>();
            List<SelectListItem> SlmCodeList = new List<SelectListItem>();
            List<SelectListItem> ProdList = new List<SelectListItem>();
            List<SelectListItem> stkSecList = new List<SelectListItem>();
            List<SelectListItem> GroupStkGrp = new List<SelectListItem>();
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
                ViewBag.stkSec = stkSec == null ? "[]" : "[\"" + string.Join("\",\"", stkSec.Select(x => x.ToString()).ToArray()) + "\"]";

                command.Dispose();
                Connection.Close();
            }
            var SearchBudgetSale = new List<StoreSearchBudgetSaleModel>();
            ViewBag.stkGroupList = SearchBudgetSale;
            //stkGroup null
            if (slmCode != null || cusCode != null)
            {
                var stkGroup2 = new string[] { "" };
                if (stkGroup[0] == "ALL")
                {
                    stkGroup2[0] = "ALL";
                } else {
                    stkGroup2 = stkGroup;
                }
                SearchBudgetSale = new SearchBudgetSale().GetStoreSearchBudgetSale(slmCode, cusCode, stkSec, stkGroup2, flgBudget);
                ViewBag.stkGroupList = SearchBudgetSale;
            }

            return PartialView("_listDataBudgetSale", new
            {
                @ViewBag.stkGroupList,
                @ViewBag.SlmCodeList,
                @ViewBag.slmCode,
                @ViewBag.cusCode,
                @ViewBag.year,
                @ViewBag.stkSec,
                @ViewBag.stkSecList,
                @ViewBag.secList,
            });
        }
        [HttpPost]
        public ActionResult SaveBudget(string USER, string CUSCODE, string STKGRP, double F_SLM)
        {
            var UpdateBudgetSale = new List<StoreUpdateBudgetSaleModel>();
            try
            {
                UpdateBudgetSale = new UpdateBudgetSale().Update(USER, CUSCODE, STKGRP, F_SLM);
                return Json(new { status = "success", message = "budgetSale updated" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = ex.Message });
            }
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
        public JsonResult GetSection(string ProdMRG)
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
        public JsonResult GetStkgrp(string[] prodCode, string[] secCode)
        {
            string[] prodCodeArr; 
            string secCodeArr = string.Empty; 
            List<STKGRPList> STKGRPList = new List<STKGRPList>();
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString);
            Connection.Open();
            if ((prodCode != null && prodCode[0] == "ALL") || prodCode == null)
            {
                prodCode[0] = "ALL";
            }
            if ((secCode != null && secCode[0] == "ALL") || (secCode == null))
            {
                secCode[0] = "ALL";
            }
            var command = new SqlCommand("P_Search_StockGroup", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@prodCode", string.Join(",", prodCode));
            command.Parameters.AddWithValue("@secCode", string.Join(",", secCode));

            SqlDataReader rec = command.ExecuteReader();
            while (rec.Read())
            {
                STKGRPList.Add(new STKGRPList()
                {
                    STKGRP = rec["STKGRP"].ToString(),
                    GRPNAM = rec["GRPNAM"].ToString()
                });
            }
            rec.Close();
            rec.Dispose();
            command.Dispose();
            Connection.Close();
            return Json(STKGRPList, JsonRequestBehavior.AllowGet);
        }
    }
}