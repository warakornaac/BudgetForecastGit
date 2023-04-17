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
using System.Globalization;

namespace BudgetForecast.Controllers
{
    public class BudgetPmController : Controller
    {
        // GET: BudgetPm
        public ActionResult Index(string prodMgr, string year, string[] stkGroup)
        {
            string flagInput = "NO";
            string startDate = "";
            string endDate = "";
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
                List<SelectListItem> GroupStkGrp = new List<SelectListItem>();
                List<SelectListItem> PRODList = new List<SelectListItem>();
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString))
                {
                    Connection.Open();

                    var command = new SqlCommand("P_Search_Budget_Forecast", Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@inUsrID", user);
                    command.Parameters.AddWithValue("@inType", "STKGRP");
                    SqlDataReader dr2 = command.ExecuteReader();
                    while (dr2.Read())
                    {
                        GroupStkGrp.Add(new SelectListItem() { Value = dr2["STKGRP"].ToString(), Text = dr2["STKGRP"].ToString() + "/" + dr2["GRPNAM"].ToString() });

                    }
                    ViewBag.StkGrp = GroupStkGrp;
                    //S20161016
                    dr2.Close();
                    dr2.Dispose();
                    command.Dispose();

                    command = new SqlCommand("P_Search_Budget_Forecast", Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@inUsrID", user);
                    command.Parameters.AddWithValue("@inType", "PRDNAME");
                    //command.ExecuteNonQuery();
                    SqlDataReader dr3 = command.ExecuteReader();
                    while (dr3.Read())
                    {
                        PRODList.Add(new SelectListItem() { Value = dr3["PROD"].ToString(), Text = dr3["PROD"].ToString() + "/" + dr3["PRODNAM"].ToString() });
                    }
                    dr3.Close();
                    dr3.Dispose();

                    if (stkGroup != null)
                    {
                        //วันที่คีย์ได้ budget pm
                        var dateCurrent = DateTime.Now.ToString("yyy-MM-dd", new CultureInfo("en-US"));
                        var yearCurrent = DateTime.Now.Year.ToString();
                        var cmdSearch = new SqlCommand("P_Search_Budget_Forecast_Dateinput", Connection);
                        cmdSearch.CommandType = CommandType.StoredProcedure;
                        cmdSearch.Parameters.AddWithValue("@inEvent", 1);
                        cmdSearch.Parameters.AddWithValue("@inYear", yearCurrent);
                        SqlParameter p = new SqlParameter("@outResult", SqlDbType.NVarChar, 1000);
                        SqlParameter p1 = new SqlParameter("@outStartDate", SqlDbType.NVarChar, 1000);
                        SqlParameter p2 = new SqlParameter("@outEndDate", SqlDbType.NVarChar, 1000);
                        p.Direction = ParameterDirection.Output;
                        p1.Direction = ParameterDirection.Output;
                        p2.Direction = ParameterDirection.Output;
                        cmdSearch.Parameters.Add(p);
                        cmdSearch.Parameters.Add(p1);
                        cmdSearch.Parameters.Add(p2);
                        int INSID = cmdSearch.ExecuteNonQuery();
                        flagInput = cmdSearch.Parameters["@outResult"].Value.ToString();
                        startDate = cmdSearch.Parameters["@outStartDate"].Value.ToString();
                        endDate = cmdSearch.Parameters["@outEndDate"].Value.ToString();

                        //ViewBag.flagInput = flagInput;
                        //ViewBag.startDate = startDate;
                        ViewBag.endDate = endDate;
                        cmdSearch.Dispose();
                    }

                    ViewBag.PRODList = PRODList;
                    //เอา userId เป็น default Prod name
                    ViewBag.prodMgr = prodMgr == null ? slmCodeDefault : prodMgr;
                    ViewBag.stkGroup = stkGroup == null ? "[]" : "[\"" + string.Join("\",\"", stkGroup.Select(x => x.ToString()).ToArray()) + "\"]";
                    ViewBag.year = year == null ? DateTime.Now.Year.ToString() : year;
                  
                    command.Dispose();
                    Connection.Close();
                }
            }
            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;
            var arrMonth = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var SearchBudgetPm = new List<StoreSearchBudgetPmModel>();
            //stkGroup null
            if (stkGroup != null)
            {
                SearchBudgetPm = new SearchBudgetPm().GetStoreSearchBudgetPm(prodMgr, year, stkGroup);
            }

            ViewBag.stkGroupList = SearchBudgetPm;
            ViewBag.monthList = arrMonth;

            return View();
        }
        [HttpPost]
        public ActionResult SaveBudget(List<StoreUpdateBudgetPmModel> request)
        {
            var UpdateBudgetPm = new List<StoreUpdateBudgetPmModel>();
            foreach (var listData in (List<StoreUpdateBudgetPmModel>)request)
            {
                UpdateBudgetPm = new UpdateBudgetPm().Update(listData.USER, listData.STKGRP, listData.BUD00, listData.BUD01, listData.BUD02, listData.BUD03, listData.BUD04, listData.BUD05, listData.BUD06, listData.BUD07, listData.BUD08, listData.BUD09, listData.BUD10, listData.BUD11, listData.BUD12, listData.GP00, listData.GP01, listData.GP02, listData.GP03, listData.GP04, listData.GP05, listData.GP06, listData.GP07, listData.GP08, listData.GP09, listData.GP10, listData.GP11, listData.GP12);
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult getStkgrp(string prodCode)
        {
            List<STKGRPList> STKGRPList = new List<STKGRPList>();
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString);
            Connection.Open();
            var command = new SqlCommand("P_Search_Budget_Forecast", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@inUsrID", prodCode);
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