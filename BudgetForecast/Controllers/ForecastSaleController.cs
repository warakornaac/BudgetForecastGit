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

namespace BudgetForecast.Controllers
{
    public class ForecastSaleController
    {
        //public ActionResult Index(string slmCode, string[] cusCode, string[] stkGrp, string year)
        //{
        //    //Check login
        //    if (this.Session["UserType"] == null)
        //    {
        //        return RedirectToAction("LogIn", "Home");
        //    }
        //    else
        //    {
        //        string usre = Session["UserID"].ToString();
        //        string slmCodeDefault = Session["SLMCOD"].ToString();
        //        List<SLM> SlmList = new List<SLM>();
        //        List<SelectListItem> SlmCodeList = new List<SelectListItem>();
        //        using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString))
        //        {
        //            Connection.Open();
        //            //list slmcode
        //            var command = new SqlCommand("P_Chk_user", Connection);
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@UsrID", usre);
        //            command.Parameters.AddWithValue("@Password", "");
        //            SqlDataReader dr = command.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                SlmCodeList.Add(new SelectListItem() { Value = dr["SLMCOD"].ToString(), Text = dr["SLMCOD"].ToString() + "/" + dr["SLMNAM"].ToString() });
        //            }
        //            dr.Close();
        //            dr.Dispose();


        //            ////list stkGrp
        //            //command = new SqlCommand("P_Search_Budget_Forecast", Connection);
        //            //command.CommandType = CommandType.StoredProcedure;
        //            //command.Parameters.AddWithValue("@inUsrID", usre);
        //            //command.Parameters.AddWithValue("@inType", "SEC");
        //            ////command.ExecuteNonQuery();
        //            //SqlDataReader dr2 = command.ExecuteReader();
        //            //while (dr2.Read())
        //            //{
        //            //    GroupStkGrp.Add(new SelectListItem() { Value = dr2["SEC"].ToString(), Text = dr2["SEC"].ToString() + "/" + dr2["SECNAM"].ToString() });

        //            //}
        //            //ViewBag.StkGrp = GroupStkGrp;
        //            //dr2.Close();
        //            //dr2.Dispose();
        //            //command.Dispose();

        //            ViewBag.SlmCodeList = SlmCodeList;

        //            ViewBag.slmCode = slmCode == null ? slmCodeDefault : slmCode;
        //            ViewBag.cusCode = cusCode == null ? "[]" : "[\"" + string.Join("\",\"", cusCode.Select(x => x.ToString()).ToArray()) + "\"]";
        //            ViewBag.stkGroup = stkGrp == null ? "[]" : "[\"" + string.Join("\",\"", stkGrp.Select(x => x.ToString()).ToArray()) + "\"]";
        //            command.Dispose();
        //            Connection.Close();
        //        }
        //    }
        //    var SearchBudgetSale = new List<StoreSearchBudgetSaleModel>();
        //    ViewBag.stkGroupList = SearchBudgetSale;
        //    //stkGroup null
        //    if (slmCode != null || cusCode != null)
        //    {
        //        SearchBudgetSale = new SearchBudgetSale().GetStoreSearchForecastPm(slmCode, cusCode, stkGrp, year);
        //        ViewBag.stkGroupList = SearchBudgetSale;
        //    }

        //    return View();
        //}
    }
}