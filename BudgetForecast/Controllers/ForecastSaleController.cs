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
using BudgetForecast.Library;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using My.Data;
using System.Security.Cryptography;

namespace BudgetForecast.Controllers
{
    public class ForecastSaleController : Controller
    {
        public ActionResult Index(string slmCode, string[] cusCode, string[] stkSec, string prodMgr, string[] stkGroup, string year, int flg = 1, int flgBudget = 0)
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
                List<SelectListItem> SlmCodeList = new List<SelectListItem>();
                List<SelectListItem> ProdList = new List<SelectListItem>();
                List<SelectListItem> SecList = new List<SelectListItem>();
                List<SelectListItem> MonthYearList = new List<SelectListItem>();
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
                        SecList.Add(new SelectListItem() { Value = dr2["SEC"].ToString(), Text = dr2["SEC"].ToString() + "/" + dr2["SECNAM"].ToString() });
                    }
                    ViewBag.SecList = SecList;
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
                    dr3.Close();
                    dr3.Dispose();
                    command.Dispose();

                    //list month search
                    command = new SqlCommand("P_Get_Month_Current", Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr4 = command.ExecuteReader();
                    while (dr4.Read())
                    {
                        MonthYearList.Add(new SelectListItem() { Value = dr4["months"].ToString(), Text = dr4["months"].ToString() });
                    }
                    ViewBag.MonthYearList = MonthYearList;
                    dr4.Close();
                    dr4.Dispose();
                    command.Dispose();

                    //เอา userId เป็น default Prod name
                    ViewBag.prodMgr = prodMgr == null ? "[]" : prodMgr;
                    ViewBag.stkGroup = stkGroup == null ? "[]" : "[\"" + string.Join("\",\"", stkGroup.Select(x => x.ToString()).ToArray()) + "\"]";

                    if (slmCode != null)
                    {
                        //วันที่คีย์ได้ budget pm
                        var yearCurrent = DateTime.Now.Year.ToString();
                        var getDateInput = Utils.GetDateInput(4, yearCurrent);

                        ViewBag.flagInput = getDateInput.Item1;
                        ViewBag.startDate = getDateInput.Item2;
                        ViewBag.endDate = getDateInput.Item3;
                    }
                    if (cusCode != null)
                    {
                        string cusCodArr = String.Join(",", cusCode.Select(s => "'" + s + "'"));
                        List<CusomerListKey> cusList = new List<CusomerListKey>();
                        SqlCommand cmd = new SqlCommand("select cus.CUSKEY, cus.CUSNAM from v_Fore_AllCusProvCuskey as cus inner join Fore_Month_slm forecast on forecast.CUSCOD = cus.CUSCOD where forecast.CUSCOD in (" + cusCodArr + ") and forecast.SLMCOD = '" + slmCode + "' group by cus.CUSKEY, cus.CUSNAM order by cus.CUSKEY", Connection);
                        SqlDataReader revCusList = cmd.ExecuteReader();
                        while (revCusList.Read())
                        {
                            cusList.Add(new CusomerListKey()
                            {
                                CUSKEY = revCusList["CUSKEY"].ToString(),
                                CUSNAM = revCusList["CUSNAM"].ToString(),
                            });
                        }
                        revCusList.Close();
                        revCusList.Dispose();
                        ViewBag.cusNameList = cusList;
                    }
                    if (slmCodeDefault != null)
                    {
                        SqlCommand cmd = new SqlCommand("select TOP 1 * From v_SLMTAB_SM_Userrid where SUP = N'" + slmCodeDefault + "'", Connection);
                        SqlDataReader rev = cmd.ExecuteReader();
                        while (rev.Read())
                        {
                            flagSup = rev["SUP"].ToString();
                        }
                        rev.Close();
                        rev.Dispose();
                        cmd.Dispose();
                        Connection.Close();
                    }
                    ViewBag.secAllArray = "";
                    ViewBag.SlmCodeList = SlmCodeList;
                    ViewBag.slmCode = slmCode == null ? slmCodeDefault : slmCode;
                    ViewBag.cusCode = cusCode == null ? "[]" : "[\"" + string.Join("\",\"", cusCode.Select(x => x.ToString()).ToArray()) + "\"]";
                    ViewBag.year = year == null ? DateTime.Now.Year.ToString() : year;
                    ViewBag.stkSec = stkSec == null ? "[]" : "[\"" + string.Join("\",\"", stkSec.Select(x => x.ToString()).ToArray()) + "\"]";
                    ViewBag.flagSup = flagSup;
                    command.Dispose();
                    Connection.Close();
                }
            }
            var arrMonth = new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            var SearchForecastSale = new List<StoreSearchForecastSaleModel>();
            ViewBag.stkGroupList = SearchForecastSale;
            //stkGroup null
            if (slmCode != null || cusCode != null)
            {
                SearchForecastSale = new SearchForecastSale().GetStoreSearchForecastSale(slmCode, cusCode, stkSec, year, flg, 0, flgBudget);

            }

            ViewBag.cusCodeList = cusCode == null ? null : cusCode;
            ViewBag.secArray = stkSec == null ? null : stkSec;
            ViewBag.stkGroupList = SearchForecastSale;
            ViewBag.monthList = arrMonth;

            return View();
        }
        //search
        [HttpPost]
        public ActionResult SearchForecast(string slmCode, string[] cusCode, string[] stkSec, string prodMgr, string[] stkGroup, string year, int flg, int flgBudget)
        {
            List<SelectListItem> SlmCodeList = new List<SelectListItem>();
            List<SelectListItem> ProdList = new List<SelectListItem>();
            List<SelectListItem> SecList = new List<SelectListItem>();
            List<SelectListItem> MonthYearList = new List<SelectListItem>();
            List<CusomerListKey> cusList = new List<CusomerListKey>();
            var Listtest = new string[] { };
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString))
            {
                Connection.Open();
                if (cusCode != null)
                {

                    string cusCodArr = String.Join(",", cusCode.Select(s => "'" + s + "'"));

                    List<StarNoteViewModel> starnoteList = new List<StarNoteViewModel>();

                    var TheStarNote = Utils.GetdataNote(year, cusCode);
                    foreach (var entry in TheStarNote)
                    {
                        string cus = entry.Key;
                        string note = entry.Value.Item1;
                        bool isStar = entry.Value.Item2;

                        // เพิ่มข้อมูลลงใน List<StarNoteViewModel>
                        starnoteList.Add(new StarNoteViewModel { Cus = cus, Note = note, IsStar = isStar });
                    }
                    ViewBag.Starnote = starnoteList;
                    //SqlCommand cmd = new SqlCommand("select cus.CUSKEY, cus.CUSNAM from v_Fore_AllCusProvCuskey as cus where cus.CUSCOD in (" + cusCodArr + ") and cus.SLMCOD = '" + slmCode + "' group by cus.CUSKEY, cus.CUSNAM order by cus.CUSKEY", Connection);

                    //string txtSqlCus = string.Empty;
                    //txtSqlCus = "select cus.CUSKEY, cus.CUSNAM " +
                    //    "from v_Fore_AllCusProvCuskey as cus " +
                    //    "inner join Fore_Month_slm forecast on forecast.CUSCOD = cus.CUSCOD " +
                    //    "left join v_Budget_CusGrp_Month budget on budget.CUSKEY = cus.CUSCOD " +
                    //    "where forecast.CUSCOD in (" + cusCodArr + ") and forecast.SLMCOD = '" + slmCode + "' " +
                    //    "group by cus.CUSKEY, cus.CUSNAM";
                    //if (flgBudget == 1) {
                    //    txtSqlCus += " Having sum(isnull(budget.Budget,0)) > 0 OR ";
                    //}
                    //txtSqlCus += " order by cus.CUSKEY ";
                    //SqlCommand cmd = new SqlCommand(txtSqlCus, Connection);
                    //SqlCommand cmd = new SqlCommand("select CUSKEY, CUSKEYNAM from CUSPROV where SLMCOD =N'" + slmCode + "' group by CUSKEY, CUSKEYNAM order by CUSKEY", Connection);
                    //SqlDataReader revCusList = cmd.ExecuteReader();
                    //while (revCusList.Read())
                    //{
                    //cusList.Add(new CusomerListKey()
                    //{
                    //    CUSKEY = revCusList["CUSKEY"].ToString(),
                    //    CUSNAM = revCusList["CUSNAM"].ToString(),
                    //});
                    //}
                    //revCusList.Close();
                    //revCusList.Dispose();
                }
                //sec list
                if (stkSec != null)
                {
                    string secArr;
                    string txtSql = string.Empty;
                    string whSec = string.Empty;
                    List<SecListKey> secListAll = new List<SecListKey>();
                    if (stkSec[0] != "ALL"/* || (stkSec[0] == "ALL" && stkSec[1] != "")*/) //เลือกบาง sec
                    {
                        secArr = String.Join(",", stkSec.Select(s => "'" + s + "'"));
                        //whSec = " where SEC in (" + secArr + ")";
                        txtSql = "select SEC, SECNAM from MST_SECTION_COMP where SEC in (" + secArr + ") order by SEC";
                    }
                    else
                    { //เลือกทั้งหมด
                        if (prodMgr != null && prodMgr != "ALL")
                        { //เลือก Prod มา
                            txtSql = "SELECT stk.SEC, sec.SECNAM" +
                               " FROM [MST_STKGRP_PRD] prd" +
                               " inner join [MST_STKGRP] stk on stk.STKGRP = prd.STKGRP" +
                               " inner join [MST_SECTION] sec on sec.SEC = stk.SEC" +
                               " where prd.PROD = '" + prodMgr + "' and prd.PROD <> ''" +
                               " and stk.SEC not in ('m00', 'm10', 'm20', 'm310', 'm99', 'o000', 't00', 't10', 't20', 't30', 't99', 'z00') " +
                               " group by stk.SEC, sec.SECNAM" +
                               " order by stk.SEC";
                        }
                        else
                        {
                            txtSql = "SELECT section.SEC, section.SECNAM " +
                                " FROM Fore_Month_slm forecast" +
                                " inner join [MST_SECTION] section on forecast.SEC = section.SEC" +
                                " where forecast.SLMCOD = '" + slmCode + "' " +
                                " and section.SEC not in ('m00', 'm10', 'm20', 'm310', 'm99', 'o000', 't00', 't10', 't20', 't30', 't99', 'z00') " +
                                " group by section.SEC, section.SECNAM " +
                                " order by section.SEC";
                        }
                    }
                    SqlCommand cmd2 = new SqlCommand(txtSql, Connection);
                    SqlDataReader revSecList = cmd2.ExecuteReader();
                    while (revSecList.Read())
                    {
                        secListAll.Add(new SecListKey()
                        {
                            SEC = revSecList["SEC"].ToString(),
                            SEC_NAME = revSecList["SECNAM"].ToString(),
                        });
                    }
                    revSecList.Close();
                    revSecList.Dispose();
                    ViewBag.secAllArray = secListAll;
                }
                //list month search
                var command = new SqlCommand("P_Get_Month_Current", Connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr4 = command.ExecuteReader();
                while (dr4.Read())
                {
                    MonthYearList.Add(new SelectListItem() { Value = dr4["months"].ToString(), Text = dr4["months"].ToString() });
                }
                ViewBag.MonthYearList = MonthYearList;
                dr4.Close();
                dr4.Dispose();
                command.Dispose();
            }

            var arrMonth = new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            var SearchForecastSale = new List<StoreSearchForecastSaleModel>();
            ViewBag.stkGroupList = SearchForecastSale;
            //stkGroup null
            if (slmCode != null || cusCode != null)
            {
                string flagSelectCustomerAll = Array.Find(cusCode, element => element.StartsWith("ALLCUS"));
                string flagSelectSecAll = Array.Find(stkSec, element => element.StartsWith("ALLSEC"));
                if (flagSelectCustomerAll != null)
                {
                    cusCode = new string[] { "ALL" };
                }
                if (flagSelectSecAll != null)
                {
                    stkSec = new string[] { "ALL" };
                }
                SearchForecastSale = new SearchForecastSale().GetStoreSearchForecastSale(slmCode, cusCode, stkSec, year, flg, 0, flgBudget);
            }
            //ดึงข้อมูลร้านค้า
            if (SearchForecastSale != null)
            {
                foreach (var dataItem in (List<StoreSearchForecastSaleModel>)SearchForecastSale)
                {
                    if (dataItem.CUSKEY != null && dataItem.CUSNAM != null)
                    {
                        cusList.Add(new CusomerListKey()
                        {
                            CUSKEY = dataItem.CUSKEY.ToString(),
                            CUSNAM = dataItem.CUSNAM.ToString(),
                        });
                    }
                }
            }

            ViewBag.cusNameList = cusList.GroupBy(x => x.CUSKEY).Select(x => x.First()).ToList();

            ViewBag.secArray = stkSec == null ? null : stkSec;
            ViewBag.stkGroupList = SearchForecastSale;
            ViewBag.slmCode = slmCode;
            ViewBag.monthList = arrMonth;
            ViewBag.cusCodeList = cusCode == null ? null : cusCode;

            return PartialView("_listDataForecastSale", new
            {
                @ViewBag.stkGroupList,
                @ViewBag.slmCode,
                @ViewBag.monthList,
                @ViewBag.cusCodeList,
                @ViewBag.secList,
                @ViewBag.MonthYearList,
                @ViewBag.cusNameList,
                @ViewBag.secAllArray,
                @ViewBag.Starnote
            });
        }
        //search by month
        [HttpPost]
        public ActionResult SearchForecastByAllCustomer(string slmCode, string year, int flg, int month)
        {
            List<SelectListItem> SlmCodeList = new List<SelectListItem>();
            List<SelectListItem> ProdList = new List<SelectListItem>();
            List<SelectListItem> SecList = new List<SelectListItem>();
            List<SelectListItem> MonthYearList = new List<SelectListItem>();
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString))
            {
                Connection.Open();
                //list stkGrp
                var command = new SqlCommand("P_Search_Budget_Forecast", Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@inUsrID", slmCode);
                command.Parameters.AddWithValue("@inType", "SEC");
                //command.ExecuteNonQuery();
                SqlDataReader dr2 = command.ExecuteReader();
                while (dr2.Read())
                {
                    SecList.Add(new SelectListItem() { Value = dr2["SEC"].ToString(), Text = dr2["SEC"].ToString() + "/" + dr2["SECNAM"].ToString() });
                }
                ViewBag.SecList = SecList;
                dr2.Close();
                dr2.Dispose();
                command.Dispose();

                if (slmCode != null)
                {
                    //วันที่คีย์ได้ budget pm
                    var yearCurrent = DateTime.Now.Year.ToString();
                    var getDateInput = Utils.GetDateInput(4, yearCurrent);

                    ViewBag.flagInput = getDateInput.Item1;
                    ViewBag.startDate = getDateInput.Item2;
                    ViewBag.endDate = getDateInput.Item3;
                }

                //list month search
                command = new SqlCommand("P_Get_Month_Current", Connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr4 = command.ExecuteReader();
                while (dr4.Read())
                {
                    MonthYearList.Add(new SelectListItem() { Value = dr4["months"].ToString(), Text = dr4["months"].ToString() });
                }
                ViewBag.MonthYearList = MonthYearList;
                dr4.Close();
                dr4.Dispose();
                command.Dispose();
            }

            var arrMonth = new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            var SearchForecastSaleAll = new List<StoreSearchForecastSaleModel>();
            ViewBag.stkGroupList = SearchForecastSaleAll;
            //stkGroup null
            if (slmCode != null)
            {
                SearchForecastSaleAll = new SearchForecastSaleAll().GetStoreSearchForecastSaleAll(slmCode, year, flg, month);
            }

            ViewBag.sumByMonth = SearchForecastSaleAll;
            ViewBag.slmCode = slmCode;
            ViewBag.monthList = arrMonth;

            string renderPage = "_listDataForecastSaleByMonthAll";//by month all
            if (flg == 3)
            { //by sec all
                renderPage = "_listDataForecastSaleBySecAll";
            }

            return PartialView(renderPage, new
            {
                @ViewBag.sumByMonth,
                @ViewBag.slmCode,
                @ViewBag.monthList,
                @ViewBag.MonthYearList,
                @ViewBag.cusNameList,
                //@ViewBag.secAllArray
            });
        }
        //save
        [HttpPost]
        public ActionResult SaveForecast(string MONTH_INPUT, string USER, string SEC, string YEAR, string CUSCOD, double INPUT)
        {
            var UpdateForecastSale = new List<StoreUpdateForecastSaleModel>();
            try
            {
<<<<<<< HEAD
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand("P_Update_Forecast_Sale_Dev", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MONTH_INPUT", MONTH_INPUT);
                    cmd.Parameters.AddWithValue("@User", USER.ToTrim());
                    cmd.Parameters.AddWithValue("@Sec", SEC.ToTrim());
                    cmd.Parameters.AddWithValue("@Year", YEAR.ToTrim());
                    cmd.Parameters.AddWithValue("@Cuscod", CUSCOD.ToTrim());
                    cmd.Parameters.AddWithValue("@Input", INPUT.ToString().Replace(",", ""));


                    int INSID = cmd.ExecuteNonQuery();
                    check_sta = INSID.ToString();
                    if (INSID > 0)
                    {
                        sta = "success";
                    }
                    else
                    {
                        sta = "unsuccess";
                    }
                }
                return Json(new { status = sta, message = "forecastSale updated" });

=======
                UpdateForecastSale = new UpdateForecastSale().Update(MONTH_INPUT, USER, SEC, YEAR, CUSCOD, INPUT);
                return Json(new { status = "success", message = "forecastSale updated" });
>>>>>>> parent of 2a90efc ([bin & controller & view data] SaveForecast Test New Logic)
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
            SqlCommand cmd = new SqlCommand("select CUSKEY, CUSKEYNAM from CUSPROV where SLMCOD =N'" + slmCode + "' group by CUSKEY, CUSKEYNAM order by CUSKEY", Connection);
            SqlDataReader rev_CUSPROV = cmd.ExecuteReader();
            while (rev_CUSPROV.Read())
            {
                CUSList.Add(new CUS()
                {
                    CUSCOD = rev_CUSPROV["CUSKEY"].ToString(),
                    CUSNAM = rev_CUSPROV["CUSKEYNAM"].ToString(),
                });
            }
            //add new slmcode
            CUSList.Add(new CUS()
            {
                CUSCOD = "NEW--" + slmCode,
                CUSNAM = "NEW--" + slmCode
            });
            rev_CUSPROV.Close();
            rev_CUSPROV.Dispose();
            cmd.Dispose();
            Connection.Close();
            return Json(CUSList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveTheStarNote(string USER, string YEAR, string CusKey, string Input)
        {
            var UpdateNoteTheStar = new List<StoreUpdateTheStarNote>();
            try
            {
                UpdateNoteTheStar = new UpdateTheStarNote().Update(USER, YEAR, CusKey, Input);
                return Json(new { status = "success", message = "Note updated" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult AddTheStar(string Year, string CusKey)
        {
            var Addthestar = new List<StoreAddTheStarModel>();
            try
            {
                Addthestar = new AddTheStar().Add(Year, CusKey);
                return Json(new { status = "success", message = "The Star updated" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = ex.Message });
            }

        }
        [HttpPost]
        public ActionResult RemoveTheStar(string Year, string CusKey)
        {
            var Addthestar = new List<StoreAddTheStarModel>();
            try
            {
                Addthestar = new RemoveThestar().Remove(Year, CusKey);
                return Json(new { status = "success", message = "The Star updated" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = ex.Message });
            }

        }

    }
}