using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BudgetForecast.Model;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace BudgetForecast.Controllers
{
    public class ManagerUserADController : Controller
    {
        // GET: ManagerUserAD
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index()
        {
            try
            {
                string companyName = "TRU";
                string accountName = "warakorn.pra";
                string searchBy = "";
                List<UserAD> lstADUsers = new List<UserAD>();
                string DomainPath = "LDAP://ADSRV2016-01/dc=Automotive,dc=com";
                DirectoryEntry searchRoot = new DirectoryEntry(DomainPath);
                DirectorySearcher search = new DirectorySearcher(searchRoot);
                //search.Filter = "(&(objectClass=user)(objectCategory=person))";
                string query = $"(&(objectClass=user)(objectCategory=person))";
                //if (accountName != null) { 
                //  query = $"(&(objectClass=user)(objectCategory=person)(sAMAccountName={accountName}))";
                //}
                //if (companyName != null)
                //{
                //    query += $"(&(objectClass=user)(objectCategory=person)(sAMAccountName={accountName})(company={companyName}))";
                //}

                search.Filter = query;
                search.PropertiesToLoad.Add("samaccountname");
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("usergroup");
                search.PropertiesToLoad.Add("displayname");
                search.PropertiesToLoad.Add("department");
                search.PropertiesToLoad.Add("company");
                SearchResult result;
                SearchResultCollection resultCol = search.FindAll();
                if (resultCol != null)
                {
                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {
                        string UserNameEmailString = string.Empty;
                        result = resultCol[counter];
                        if (result.Properties.Contains("samaccountname") && result.Properties.Contains("mail") && result.Properties.Contains("displayname") && result.Properties.Contains("department") && result.Properties.Contains("company"))
                        {
                            UserAD objSurveyUsers = new UserAD();
                            objSurveyUsers.Email = (String)result.Properties["mail"][0];// + "^" + (String)result.Properties["displayname"][0];
                            objSurveyUsers.UserName = (String)result.Properties["samaccountname"][0];
                            objSurveyUsers.DisplayName = (String)result.Properties["displayname"][0];
                            objSurveyUsers.Department = (String)result.Properties["department"][0];
                            objSurveyUsers.Company = (String)result.Properties["company"][0];
                            lstADUsers.Add(objSurveyUsers);
                        }
                    }
                }

               ViewBag.listADUsers = lstADUsers;
              // return View();

            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public ActionResult ChangeMyPassword(string userName, string currentPassword, string newPassword)
        {
            userName = "Deploy";
            currentPassword = "Happy3000";
            newPassword = "happy4000";
            string messageSave = "Password updated";
            string statusSave = "success";
            try
            {
                //string ldapPath = "LDAP://ADSRV2016-01/dc=Automotive,dc=com";
                //DirectoryEntry directionEntry = new DirectoryEntry(ldapPath, domainName + "\\" + userName, currentPassword);
                DirectoryEntry directionEntry = new DirectoryEntry("LDAP://automotive.com", userName, currentPassword);
                if (directionEntry != null)
                {
                    DirectorySearcher search = new DirectorySearcher(directionEntry);
                    search.Filter = "(SAMAccountName=" + userName + ")";
                    SearchResult result = search.FindOne();
                    if (result != null)
                    {
                        DirectoryEntry userEntry = result.GetDirectoryEntry();
                        if (userEntry != null)
                        {
                            userEntry.Invoke("ChangePassword", new object[] { currentPassword, newPassword });
                           // userEntry.Invoke("SetPassword", new object[] { newPassword });
                            userEntry.CommitChanges();
                            userEntry.Close();
                            //return Json(new { status = statusSave, message = messageSave });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                //Exception has been thrown by the target of an invocation / passwprd new ไม่ผ่าน
                 statusSave = "error";
                messageSave = ex.Message.ToString();//ex.Message.ToString();
            }
            return Json(new { status = statusSave, message = messageSave }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ResetPassword(string Samaccountname)
        {
            //i get the user by its SamaccountName to change his password
            PrincipalContext context = new PrincipalContext (ContextType.Domain, "MBS", "OU=DevOU,DC=MBS,DC=com");
            UserPrincipal user = UserPrincipal.FindByIdentity (context, IdentityType.SamAccountName, Samaccountname);
            //Enable Account if it is disabled
            user.Enabled = true;
            //Reset User Password
            string newPassword = "P@ssw0rd";
            user.SetPassword(newPassword);
            //Force user to change password at next logon dh optional
            user.ExpirePasswordNow();
            user.Save();
            TempData["msg"] = "<script>alert('Password Changed Successfully');</script>";
            return RedirectToAction("GetAllUsers");
        }
    }
}