using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services.Protocols;
using System.Security.Principal;
using System.Web.Security;
using System.Runtime.InteropServices;
using BudgetForecast.Models;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using BudgetForecast.Library;

namespace BudgetForecast.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (this.Session["UserType"] == null)
            {
                this.Session["UserType"] = "";
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string User, string Password)
        {
            string Userlog = string.Empty;
            string Usertype = string.Empty;
            //string DepartmentAd = string.Empty;
            this.Session["UserAD"] = null;
            this.Session["UserType"] = null;
            this.Session["UserID"] = User.ToTrim();
            this.Session["UserPassword"] = Password.ToTrim();
            this.Session["SLMCOD"] = "";
            string UserType = string.Empty;
            string sessionId = Request["http_cookie"];
            //if (User != null && Password != null)
            //{
            var connectionString = ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString;
            SqlConnection Connection = new SqlConnection(connectionString);
            try//มีใน AD
            {
                //check user&pass ใน AD
                DirectoryEntry entry = new DirectoryEntry("LDAP://ADSRV2016-01/dc=Automotive,dc=com", User.ToTrim(), Password.ToTrim());
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + User.ToTrim() + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                var userSearch = search.FindOne();
                DirectoryEntry dirEntry = (DirectoryEntry)userSearch.GetDirectoryEntry();
                string DepartmentAd = dirEntry.Properties["Department"].Value.ToString();

                Connection.Open();
                if (result == null) {
                    if (IsValid(User.ToTrim(), Password.ToTrim()))
                    {
                        FormsAuthentication.SetAuthCookie(User.ToTrim(), false);
                        return RedirectToAction("LogIn", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Login details are wrong.");
                    }
                } else {
                    this.Session["UserAD"] = "YES";
                    string txtSql = "";
                    txtSql = "SELECT Usr.UsrTyp, Ad.Department, Ad.SLMCOD " +
                        "FROM UsrTbl_Budget Usr INNER JOIN v_ADUser Ad ON Ad.LogInName = Usr.UsrID " +
                        "WHERE UsrID =N'" + User.ToTrim() + "'";
                    SqlCommand cmdcus = new SqlCommand(txtSql, Connection);
                    SqlDataReader revcus = cmdcus.ExecuteReader();
                    //มีใน UsrTbl_Budget
                    if (revcus.HasRows)
                    {
                        while (revcus.Read())
                        {
                            this.Session["UserType"] = revcus["UsrTyp"].ToString();
                            this.Session["Department"] = "";// revcus["Department"].ToString();
                            UserType = Session["UserType"].ToString();
                            sessionId = sessionId.Substring(sessionId.Length - 24);
                            this.Session["ID"] = sessionId;
                            this.Session["SLMCOD"] = revcus["SLMCOD"].ToString();
                        }
                    }
                    else //ไม่มีใน UsrTbl_Budget
                    {
                        //ถ้าเป็น It default admin
                        if (DepartmentAd == "MIS")
                        {
                            this.Session["UserType"] = 1; //
                            UserType = Session["UserType"].ToString();
                            sessionId = sessionId.Substring(sessionId.Length - 24);
                            this.Session["ID"] = sessionId;
                        }
                        else 
                        {
                            ModelState.AddModelError("", "You don't have permission, Please contact admin");
                        }
                    }
                    revcus.Close();
                    revcus.Dispose();
                    cmdcus.Dispose();
                    FormsAuthentication.SetAuthCookie(User.ToTrim(), false);
                    if (UserType == "1" || UserType == "2")//admin pm
                    {
                        return RedirectToAction("Index", "BudgetPm");
                    }
                    else if (UserType == "3")//sale
                    {
                        return RedirectToAction("Index", "BudgetSale");
                    }
                }
                ModelState.AddModelError("", "Login details are wrong.");
            }
            catch (COMException ex) //ไม่มีใน AD เอามาจาก UsrTbl_Budget
            {
                Connection.Open();
                string txtSql = "";
                txtSql = "SELECT Usr.UsrTyp, Ad.Department, Ad.SLMCOD " +
                    "FROM UsrTbl_Budget Usr INNER JOIN v_ADUser Ad ON Ad.LogInName = Usr.UsrID " +
                    "WHERE UsrID =N'" + User.ToTrim() + "'and [dbo].F_decrypt([Password])='" + Password + "'";
                SqlCommand cmdcus = new SqlCommand(txtSql, Connection);
                SqlDataReader revcus = cmdcus.ExecuteReader();
                while (revcus.Read())
                {
                    this.Session["UserAD"] = "NO";
                    this.Session["UserType"] = revcus["UsrTyp"].ToString();
                    this.Session["Department"] = "";// revcus["Department"].ToString();
                    UserType = Session["UserType"].ToString();
                    sessionId = sessionId.Substring(sessionId.Length - 24);
                    this.Session["ID"] = sessionId;
                    this.Session["SLMCOD"] = revcus["SLMCOD"].ToString();
                }
                revcus.Close();
                revcus.Dispose();
                cmdcus.Dispose();
                FormsAuthentication.SetAuthCookie(User.ToTrim(), false);

                if (UserType == "1" || UserType == "2")//admin pm
                {
                    return RedirectToAction("Index", "BudgetPm");
                }
                else if (UserType == "3")//sale
                {
                    return RedirectToAction("Index", "BudgetSale");
                }

                ModelState.AddModelError("", "Login details are wrong.");
            }
            Connection.Close();

            return View();
        }
        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return Json(new { status = "success" });
        }
        private bool IsValid(string user, string Password)
        {
            bool IsValid = false;
            if (user == null || Password == null)
            {
                IsValid = false;
            }
            return IsValid;
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}