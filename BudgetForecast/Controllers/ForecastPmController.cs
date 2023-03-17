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

namespace BudgetForecast.Controllers
{
    public class ForecastPmController : Controller
    {
        // GET: ForecastPm
        public ActionResult Index(string prodMgr, string year, string[] sec)
        {

            //Check login
            if (this.Session["UserType"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            else
            {
                if (this.Session["UserType"] == null)
                {
                    return RedirectToAction("LogIn", "Home");
                }
                else
                {
                    string usre = Session["UserID"].ToString();
                    List<SLM> SlmList = new List<SLM>();
                    List<SelectListItem> GroupStkGrp = new List<SelectListItem>();
                    List<SelectListItem> PRODList = new List<SelectListItem>();
                    using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString))
                    {
                        Connection.Open();
                        //list stkGrp
                        var command = new SqlCommand("P_Search_Budget_Forecast", Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@inUsrID", usre);
                        command.Parameters.AddWithValue("@inType", "SEC");
                        //command.ExecuteNonQuery();
                        SqlDataReader dr2 = command.ExecuteReader();
                        while (dr2.Read())
                        {
                            GroupStkGrp.Add(new SelectListItem() { Value = dr2["SEC"].ToString(), Text = dr2["SEC"].ToString() + "/" + dr2["SECNAM"].ToString() });

                        }
                        ViewBag.StkGrp = GroupStkGrp;
                        dr2.Close();
                        dr2.Dispose();
                        command.Dispose();

                        //list product
                        command = new SqlCommand("P_Search_Budget_Forecast", Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@inUsrID", usre);
                        command.Parameters.AddWithValue("@inType", "PRDNAME");
                        SqlDataReader dr3 = command.ExecuteReader();
                        while (dr3.Read())
                        {
                            PRODList.Add(new SelectListItem() { Value = dr3["PROD"].ToString(), Text = dr3["PROD"].ToString() + "/" + dr3["PRODNAM"].ToString() });

                        }
                        ViewBag.PRODList = PRODList;
                        //เอา userId เป็น default Prod name
                        ViewBag.prodMgr = prodMgr == null ? usre : prodMgr;
                        ViewBag.stkGroup = sec == null ? "[]" : "[\"" + string.Join("\",\"", sec.Select(x => x.ToString()).ToArray()) + "\"]";
                        ViewBag.year = year == null ? DateTime.Now.Year.ToString() : year;
                        dr3.Close();
                        dr3.Dispose();
                        command.Dispose();
                        Connection.Close();
                    }
                }
            }

            var arrMonth = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var countMonth = arrMonth.Count();

            ViewBag.monthList = arrMonth;
            var SearchForecastPm = new List<StoreSearchForecastPmModel>();

            ViewBag.stkGroupList = SearchForecastPm;
            //stkGroup null
            if (sec != null)
            {
                SearchForecastPm = new SearchForecastPm().GetStoreSearchForecastPm(prodMgr, year, sec);
                ViewBag.stkGroupList = SearchForecastPm;
            }
            ViewBag.monthList = arrMonth;

            return View();
        }
        [HttpPost]
        public ActionResult SaveForecast(List<StoreUpdateForecastPmModel> request)
        {
            var UpdateForecastPm = new List<StoreUpdateForecastPmModel>();
            foreach (var listData in (List<StoreUpdateForecastPmModel>)request)
            {
                UpdateForecastPm = new UpdateForecastPm().Update(listData.USER, listData.SEC, listData.YEAR, listData.FC00, listData.FC01, listData.FC02, listData.FC03, listData.FC04, listData.FC05, listData.FC06, listData.FC07, listData.FC08, listData.FC09, listData.FC10, listData.FC11, listData.FC12, listData.FC_GP00, listData.FC_GP01, listData.FC_GP02, listData.FC_GP03, listData.FC_GP04, listData.FC_GP05, listData.FC_GP06, listData.FC_GP07, listData.FC_GP08, listData.FC_GP09, listData.FC_GP10, listData.FC_GP11, listData.FC_GP12);
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}