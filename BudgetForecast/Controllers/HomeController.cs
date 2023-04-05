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
            string dateexpire = string.Empty;
            string UsrClmStaff = string.Empty;
            int intdateexpire = 0;
            //if (User != null && Password != null)
            //{
            var connectionString = ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString;
            SqlConnection Connection = new SqlConnection(connectionString);
            Connection.Open();
            try
            {
                this.Session["UserType"] = null;
                this.Session["UserID"] = User;
                this.Session["UserPassword"] = Password;
                this.Session["UsrGrpspecial"] = 0;
                this.Session["DatetoExpire"] = "..";
                this.Session["UsrClmStaff"] = "0";
                this.Session["SLMCOD"] = "";
                string UserType = string.Empty;
                string sessionId = Request["http_cookie"];
                string txtSql = "";
                txtSql = "SELECT Usr.UsrTyp, Ad.Department, Ad.SLMCOD  FROM UsrTbl_Budget Usr INNER JOIN v_ADUser Ad ON Ad.LogInName = Usr.UsrID WHERE UsrID =N'" + User + "'and [dbo].F_decrypt([Password])='" + Password + "'";
                SqlCommand cmdcus = new SqlCommand(txtSql, Connection);
                SqlDataReader revcus = cmdcus.ExecuteReader();
                while (revcus.Read())
                {
                    this.Session["UserType"] = revcus["UsrTyp"].ToString();
                    this.Session["Department"] = "";// revcus["Department"].ToString();
                    this.Session["CUSCOD"] = "";// revcus["CUSCOD"].ToString();
                    UserType = Session["UserType"].ToString();
                    this.Session["UsrClmStaff"] = "";// revcus["UsrClmStaff"].ToString();

                    sessionId = sessionId.Substring(sessionId.Length - 24);
                    this.Session["ID"] = sessionId;
                    this.Session["SLMCOD"] = revcus["SLMCOD"].ToString();
                }
                revcus.Close();
                revcus.Dispose();
                cmdcus.Dispose();
                FormsAuthentication.SetAuthCookie(User, false);

                if (UserType == null)
                {
                    //ADSRV01
                    DirectoryEntry entry = new DirectoryEntry("LDAP://ADSRV2016-01/dc=Automotive,dc=com", User, Password);
                    DirectorySearcher search = new DirectorySearcher(entry);
                    search.Filter = "(SAMAccountName=" + User + ")";
                    search.PropertiesToLoad.Add("cn");

                    SearchResult result = search.FindOne();
                    //result.GetDirectoryEntry();
                    // Connection.Open();
                    if (null == result)
                    {
                        if (IsValid(User, Password))
                        {

                        }
                        else
                        {
                            ModelState.AddModelError("", "Login details are wrong.");
                        }
                        //throw new SoapException("Error authenticating ",SoapException.ClientFaultCode);
                    }
                    else
                    {
                        this.Session["UserID"] = User;
                        this.Session["UserPassword"] = Password;
                        this.Session["UsrGrpspecial"] = 0;
                        SqlCommand cmd = new SqlCommand("select * From v_UsrTbl where UsrID =N'" + User + "'", Connection);
                        SqlDataReader rev = cmd.ExecuteReader();
                        while (rev.Read())
                        {
                            dateexpire = rev["Date to Expire"].ToString();
                            //dateexpire = "2";
                            this.Session["UserType"] = rev["UsrTyp"].ToString();
                            this.Session["CUSCOD"] = "";
                            this.Session["Department"] = "";// rev["Department"].ToString();
                            this.Session["SLMCOD"] = rev["SLMCOD"].ToString();
                        }
                        rev.Close();
                        rev.Dispose();
                        cmd.Dispose();

                        intdateexpire = Convert.ToInt32(dateexpire);
                        this.Session["expdatecal"] = intdateexpire;
                        if (intdateexpire <= 15)
                        {
                            this.Session["DatetoExpire"] = "Passwords expire '" + intdateexpire + "' days";
                        }
                        else if (intdateexpire == 0)
                        {
                            this.Session["DatetoExpire"] = "The user's password must be changed password  Changed password on Citrix";
                        }
                        else
                        {
                            this.Session["DatetoExpire"] = "..";
                        }
                        FormsAuthentication.SetAuthCookie(User, false);
                        //return RedirectToAction("Index", "SeleScrCustomer");
                        UserType = Session["UserType"].ToString();
                        if (UserType == "5")
                        {
                            // return RedirectToAction("Index", "Home");
                            return RedirectToAction("Index", "PriceApproval");
                        }
                        else
                        {
                            return RedirectToAction("dashboard", "SeleScrCustomer");
                        }
                    }

                }
                else if (UserType == "")
                {
                    //ADSRV01
                    DirectoryEntry entry = new DirectoryEntry("LDAP://ADSRV2016-01/dc=Automotive,dc=com", User, Password);
                    DirectorySearcher search = new DirectorySearcher(entry);
                    search.Filter = "(SAMAccountName=" + User + ")";
                    search.PropertiesToLoad.Add("cn");

                    SearchResult result = search.FindOne();
                    if (null == result)
                    {
                        if (IsValid(User, Password))
                        {

                        }
                        else
                        {
                            ModelState.AddModelError("", "Login details are wrong.");
                        }
                        //throw new SoapException("Error authenticating ",SoapException.ClientFaultCode);
                    }
                    else
                    {
                        this.Session["UserID"] = User;
                        this.Session["UserPassword"] = Password;
                        this.Session["UsrGrpspecial"] = 0;
                        SqlCommand cmd = new SqlCommand("select * From v_UsrTbl where UsrID =N'" + User + "'", Connection);
                        SqlDataReader rev = cmd.ExecuteReader();
                        while (rev.Read())
                        {

                            dateexpire = rev["Date to Expire"].ToString();
                            //dateexpire = "2";
                            this.Session["UserType"] = rev["UsrTyp"].ToString();
                            this.Session["CUSCOD"] = "";
                            this.Session["Department"] = "";// rev["Department"].ToString();
                            this.Session["SLMCOD"] = rev["SLMCOD"].ToString();
                        }
                        rev.Close();
                        rev.Dispose();
                        cmd.Dispose();

                        intdateexpire = Convert.ToInt32(dateexpire);
                        this.Session["expdatecal"] = intdateexpire;
                        if (intdateexpire <= 15)
                        {
                            this.Session["DatetoExpire"] = "Passwords expire '" + intdateexpire + "' days";
                        }
                        else if (intdateexpire == 0)
                        {
                            this.Session["DatetoExpire"] = "The user's password must be changed password  Changed password on Citrix";
                        }
                        else
                        {

                            this.Session["DatetoExpire"] = "..";
                        }

                        FormsAuthentication.SetAuthCookie(User, false);

                        //return RedirectToAction("Index", "SeleScrCustomer");
                        UserType = Session["UserType"].ToString();
                        if (UserType == "5")
                        {
                            // return RedirectToAction("Index", "Home");
                            return RedirectToAction("Index", "PriceApproval");
                        }
                        else
                        {
                           return RedirectToAction("Index", "BudgetPm");
                        }
                    }
                    // ModelState.AddModelError("", "Login details are wrong.");
                }
                else if (UserType == "1" || UserType == "2")//sale, salesco
                {
                    //return RedirectToAction("dashboard", "SeleScrCustomer");
                    return RedirectToAction("Index", "BudgetSale");

                }
                else if (UserType == "5")//pm
                {
                    return RedirectToAction("Index", "BudgetPm");
                }
                else if (UserType == "6") //customer
                {
                    var command = new SqlCommand("P_logSingin", Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.AddWithValue("@pCusCod", strcustome);
                    command.Parameters.AddWithValue("@UsrID", User);
                    command.Parameters.AddWithValue("@SessionId", sessionId);
                    command.ExecuteReader();
                    command.Dispose();
                    //return RedirectToAction("Index", "Home");
                    return RedirectToAction("Index", "BudgetPm");
                }
            }
            catch (COMException ex)
            {
                ModelState.AddModelError("", "Login details are wrong.");
            }
            Connection.Close();
            //}

            return View();
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