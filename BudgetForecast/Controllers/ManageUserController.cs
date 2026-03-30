using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BudgetForecast.Models;

namespace BudgetForecast.Controllers
{
    public class ManageUserController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString;

        // GET: /ManageUser/
        public ActionResult Index()
        {
            List<UsrGrp> userList = new List<UsrGrp>();

            try
            {
                SqlConnection Connection = new SqlConnection(connectionString);
                using (Connection)
                {
                    string query = @"SELECT [ID], [UsrID], [UsrName], [Password], [UsrTyp]
                                  ,[Company], [Email], [Slmcod], [Inserted Date], [Inserted By]
                                  ,[Updated Date], [Updated By]
                                   FROM [dbo].[UsrTbl_Budget]";

                    using (SqlCommand cmd = new SqlCommand(query, Connection))
                    {
                        Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UsrGrp user = new UsrGrp
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                    UsrID = reader.IsDBNull(reader.GetOrdinal("UsrID")) ? null : reader.GetString(reader.GetOrdinal("UsrID")),
                                    UsrName = reader.IsDBNull(reader.GetOrdinal("UsrName")) ? null : reader.GetString(reader.GetOrdinal("UsrName")),
                                    Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? null : reader.GetString(reader.GetOrdinal("Password")),
                                    UsrTyp = reader.IsDBNull(reader.GetOrdinal("UsrTyp")) ? 0 : reader.GetInt32(reader.GetOrdinal("UsrTyp")),
                                    Company = reader.IsDBNull(reader.GetOrdinal("Company")) ? null : reader.GetString(reader.GetOrdinal("Company")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                                    Slmcod = reader.IsDBNull(reader.GetOrdinal("Slmcod")) ? null : reader.GetString(reader.GetOrdinal("Slmcod")),
                                    InsertedDate = reader.IsDBNull(reader.GetOrdinal("Inserted Date")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("Inserted Date")),
                                    InsertedBy = reader.IsDBNull(reader.GetOrdinal("Inserted By")) ? null : reader.GetString(reader.GetOrdinal("Inserted By")),
                                    UpdatedDate = reader.IsDBNull(reader.GetOrdinal("Updated Date")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("Updated Date")),
                                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("Updated By")) ? null : reader.GetString(reader.GetOrdinal("Updated By"))
                                };

                                userList.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Something went wrong: " + ex.Message;
            }

            return View(userList);
        }

        // GET: GetUserList for DataTables/jsGrid
        public JsonResult GetUserList()
        {
            List<UsrGrp> userList = new List<UsrGrp>();

            try
            {
                SqlConnection Connection = new SqlConnection(connectionString);
                using (Connection)
                {
                    string query = @"SELECT [ID], [UsrID], [UsrName], [Password], [UsrTyp]
                                  ,[Company], [Email], [Slmcod], [Inserted Date], [Inserted By]
                                  ,[Updated Date], [Updated By]
                                   FROM [dbo].[UsrTbl_Budget]";

                    using (SqlCommand cmd = new SqlCommand(query, Connection))
                    {
                        Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UsrGrp user = new UsrGrp
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                    UsrID = reader.IsDBNull(reader.GetOrdinal("UsrID")) ? null : reader.GetString(reader.GetOrdinal("UsrID")),
                                    UsrName = reader.IsDBNull(reader.GetOrdinal("UsrName")) ? null : reader.GetString(reader.GetOrdinal("UsrName")),
                                    Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? null : reader.GetString(reader.GetOrdinal("Password")),
                                    UsrTyp = reader.IsDBNull(reader.GetOrdinal("UsrTyp")) ? 0 : reader.GetInt32(reader.GetOrdinal("UsrTyp")),
                                    Company = reader.IsDBNull(reader.GetOrdinal("Company")) ? null : reader.GetString(reader.GetOrdinal("Company")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                                    Slmcod = reader.IsDBNull(reader.GetOrdinal("Slmcod")) ? null : reader.GetString(reader.GetOrdinal("Slmcod")),
                                    InsertedDate = reader.IsDBNull(reader.GetOrdinal("Inserted Date")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("Inserted Date")),
                                    InsertedBy = reader.IsDBNull(reader.GetOrdinal("Inserted By")) ? null : reader.GetString(reader.GetOrdinal("Inserted By")),
                                    UpdatedDate = reader.IsDBNull(reader.GetOrdinal("Updated Date")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("Updated Date")),
                                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("Updated By")) ? null : reader.GetString(reader.GetOrdinal("Updated By"))
                                };

                                userList.Add(user);
                            }
                        }
                    }
                }

                return Json(new { success = true, data = userList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Create(UsrGrp user)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("P_Add_UserAD", Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Trim the incoming user id before passing to the stored procedure
                        var trimmedUser = (user?.UsrID ?? string.Empty).Trim();
                        cmd.Parameters.AddWithValue("@inUser", string.IsNullOrEmpty(trimmedUser) ? (object)DBNull.Value : trimmedUser);
                        cmd.Parameters.AddWithValue("@instatus", (object)user.UsrTyp ?? DBNull.Value);

                        SqlParameter outGenStatusParam = new SqlParameter("@outGenstatus", SqlDbType.NVarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outGenStatusParam);

                        Connection.Open();
                        cmd.ExecuteNonQuery();

                        string statusMessage = outGenStatusParam.Value != DBNull.Value
                            ? outGenStatusParam.Value.ToString()
                            : "";

                        if (statusMessage == "Success")
                        {
                            return Json(new { success = true, message = "Added successfully." });
                        }
                        else
                        {
                            return Json(new { success = false, message = statusMessage });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        // GET: Get user by ID
        [HttpGet]
        public JsonResult GetUserById(int id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT [ID], [UsrID], [UsrName], [Password], [UsrTyp]
                                  ,[Company], [Email], [Slmcod], [Inserted Date], [Inserted By]
                                  ,[Updated Date], [Updated By]
                                   FROM [dbo].[UsrTbl_Budget]
                                    WHERE [ID] = @ID";

                    using (SqlCommand cmd = new SqlCommand(query, Connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        Connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UsrGrp user = new UsrGrp
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                    UsrID = reader.IsDBNull(reader.GetOrdinal("UsrID")) ? null : reader.GetString(reader.GetOrdinal("UsrID")),
                                    UsrName = reader.IsDBNull(reader.GetOrdinal("UsrName")) ? null : reader.GetString(reader.GetOrdinal("UsrName")),
                                    Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? null : reader.GetString(reader.GetOrdinal("Password")),
                                    UsrTyp = reader.IsDBNull(reader.GetOrdinal("UsrTyp")) ? 0 : reader.GetInt32(reader.GetOrdinal("UsrTyp")),
                                    Company = reader.IsDBNull(reader.GetOrdinal("Company")) ? null : reader.GetString(reader.GetOrdinal("Company")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                                    Slmcod = reader.IsDBNull(reader.GetOrdinal("Slmcod")) ? null : reader.GetString(reader.GetOrdinal("Slmcod")),
                                    InsertedDate = reader.IsDBNull(reader.GetOrdinal("Inserted Date")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("Inserted Date")),
                                    InsertedBy = reader.IsDBNull(reader.GetOrdinal("Inserted By")) ? null : reader.GetString(reader.GetOrdinal("Inserted By")),
                                    UpdatedDate = reader.IsDBNull(reader.GetOrdinal("Updated Date")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("Updated Date")),
                                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("Updated By")) ? null : reader.GetString(reader.GetOrdinal("Updated By"))
                                };

                                return Json(new { success = true, data = user }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }

                return Json(new { success = false, message = "Not found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Update user
        [HttpPost]
        public JsonResult Update(UsrGrp user)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("P_Update_UserAD", Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // ส่ง UsrID เป็นตัวอ้างอิง และ UsrTyp เป็นค่าที่จะเปลี่ยน
                        cmd.Parameters.AddWithValue("@inUser", user.UsrID);
                        cmd.Parameters.AddWithValue("@instatus", user.UsrTyp);
                        cmd.Parameters.AddWithValue("@Slmcod", (object)user.Slmcod ?? DBNull.Value);

                        SqlParameter outParam = new SqlParameter("@outGenstatus", SqlDbType.NVarChar, 100)
                        { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(outParam);

                        Connection.Open();
                        cmd.ExecuteNonQuery();

                        string result = outParam.Value.ToString();
                        if (result == "Update Success")
                        {
                            return Json(new { success = true, message = "Updated successfully." });
                        }
                        return Json(new { success = false, message = result });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Delete user
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(connectionString);
                using (Connection)
                {
                    string query = @"DELETE FROM [dbo].[UsrTbl_Budget] WHERE [ID] = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, Connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        Connection.Open();
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            return Json(new { success = true, message = "Deleted successfully." });
                        }
                        else
                        {
                            return Json(new { success = false, message = "Deleted failed." });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }
    }
}