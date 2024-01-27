using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace BudgetForecast.Library
{
    public class Utils
    {
        public static int CInt(object value)
        {
            if (value == null)
                return (0);

            var data = value.ToString();
            data = data.Replace(",", "");
            var isNum = new System.Text.RegularExpressions.Regex("^([-]|[0-9])[0-9]*$");
            var m = isNum.Match(data);
            return m.Success ? (Convert.ToInt32(data)) : (0);
        }

        public static long CLong(object value)
        {
            if (value == null)
                return (0);

            var data = value.ToString();
            data = data.Replace(",", "");
            var isNum = new System.Text.RegularExpressions.Regex("^([-]|[0-9])[0-9]*$");
            var m = isNum.Match(data);
            return m.Success ? (Convert.ToInt64(data)) : (0);
        }

        public static double CDouble(object value)
        {
            var data = value.ToString();
            data = data.Replace(",", "");
            var isNum = new System.Text.RegularExpressions.Regex("^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$|^([-]|[0-9])[0-9]*$");
            var m = isNum.Match(data);
            return m.Success ? (Convert.ToDouble(data)) : (0.00);
        }

        public static decimal CDecimal(object value)
        {
            var data = value.ToString();
            data = data.ToString(CultureInfo.InvariantCulture).Replace(",", "");
            var isNum = new System.Text.RegularExpressions.Regex("^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$|^([-]|[0-9])[0-9]*$");
            var m = isNum.Match(data);
            return m.Success ? (Convert.ToDecimal(data)) : (Convert.ToDecimal("0.00"));
        }

        public static string GetPathOf(string fullpath)
        {
            var lastIdx = fullpath.LastIndexOf("\\", StringComparison.Ordinal);
            return (fullpath.Substring(0, lastIdx));
        }

        public static void ByteToFile(byte[] buffer, string targetFile)
        {
            try
            {
                var stf = new MemoryStream(buffer);
                var stt = File.Create(targetFile);
                var br = new BinaryReader(stf);
                var bw = new BinaryWriter(stt);
                bw.Write(br.ReadBytes((int)stf.Length));
                bw.Flush();
                bw.Close();
                br.Close();
                stf = null;
                stt = null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static byte[] FileToByte(string sourceFile)
        {
            try
            {
                var b = File.ReadAllBytes(sourceFile);
                return (b);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static string GetConfig(string key)
        {
            /*
            if (key.Equals("CN"))
                return (EncryptData.Decrypt(ConfigurationSettings.AppSettings[key]));
            else
            */

            return (ConfigurationSettings.AppSettings[key]);
        }

        public static string Left(string str, int length)
        {
            return str.Substring(0, Math.Min(length, str.Length));
        }

        public static string Right(string original, int numberCharacters)
        {
            return original.Substring(numberCharacters > original.Length ? 0 : original.Length - numberCharacters);
        }

        public static string ConvertDatetime(string date)
        {
            var stratDate = date.Split('/');
            string newDate = new DateTime(Convert.ToInt32(stratDate[2]) - 543, Convert.ToInt32(stratDate[1]), Convert.ToInt32(stratDate[0])).ToString("yyyy-MM-dd", new CultureInfo("en-US"));

            return newDate;

        }

        public static string ConvertDatetimeToString(DateTime datetime)
        {
            return datetime.Date.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
        }

        public static DateTime ConvertStringToDatetime(string datetime)
        {
            var stratDate = datetime.Split('/');

            return new DateTime(Convert.ToInt32(stratDate[2]) - 543, Convert.ToInt32(stratDate[1]), Convert.ToInt32(stratDate[0]));

            //var stratDate = datetime.Split('-');
            //string newDate = new DateTime(Convert.ToInt32(stratDate[0]) + 543, Convert.ToInt32(stratDate[1]), Convert.ToInt32(stratDate[2])).ToString("dd/MM/yyyy", new CultureInfo("en-TH"));

            //return newDate;
        }

        public static DateTime ConvertStringToDatetimeEn(string datetime)
        {
            var stratDate = datetime.Split('-');

            return new DateTime(Convert.ToInt32(stratDate[0]), Convert.ToInt32(stratDate[1]), Convert.ToInt32(stratDate[2]));

            //var stratDate = datetime.Split('-');
            //string newDate = new DateTime(Convert.ToInt32(stratDate[0]) + 543, Convert.ToInt32(stratDate[1]), Convert.ToInt32(stratDate[2])).ToString("dd/MM/yyyy", new CultureInfo("en-TH"));

            //return newDate;
        }

        public static DateTime ConvertStringToDatetimeEnSlash(DateTime datetime)
        {
            var stratDate = datetime.ToString().Split('/');

            return new DateTime(Convert.ToInt32(stratDate[0]), Convert.ToInt32(stratDate[1]), Convert.ToInt32(stratDate[2]));

        }
        public static string ConvertTimeToString(DateTime datetime)
        {
            return datetime.ToString("H:mm", new CultureInfo("th-TH"));
        }

        public static DateTime ConvertStringToDatetimeEmail(string datetime)
        {
            var stratDate = datetime.Split('/');
            var strYear = stratDate[2].ToString().Split(' ');
            var strTime = strYear[1].ToString().Split(':');

            DateTime dateTime = new DateTime(Convert.ToInt32(strYear[0]) - 543, Convert.ToInt32(stratDate[1]), Convert.ToInt32(stratDate[0]), Convert.ToInt32(strTime[0]) + 7, Convert.ToInt32(strTime[1]), Convert.ToInt32(strTime[2]));
            return dateTime;
        }


        //Implemented based on interface, not part of algorithm
        public static string RemoveAllNamespaces(string xmlDocument)
        {
            XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));

            return xmlDocumentWithoutNs.ToString();
        }

        //Core recursion function
        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    xElement.Add(attribute);

                return xElement;
            }
            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
        }


        //get name month
        public Array ListMonth(string language = "EN", string format = "S")
        {
            var arrayMonth = new string[12];
            if (language == "EN")
            {
                if (format == "S")
                {
                    arrayMonth = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                }
            }
            return (arrayMonth);
        }
        public List<DateTime> getAllDates(int year, int month)
        {
            var ret = new List<DateTime>();
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                ret.Add(new DateTime(year, month, i));
            }
            return ret;
        }
        public static Tuple<string, string, string> GetDateInput(int id, string year)
        {
            string flagInput = "NO";
            string startDate = "";
            string endDate = "";
            if (id != 0 && year != null)
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    //วันที่คีย์ได้ budget pm
                    var dateCurrent = DateTime.Now.ToString("yyy-MM-dd", new CultureInfo("en-US"));
                    var cmdSearch = new SqlCommand("P_Search_Budget_Forecast_Dateinput", Connection);
                    var yearCurrent = DateTime.Now.Year.ToString();

                    cmdSearch.CommandType = CommandType.StoredProcedure;
                    cmdSearch.Parameters.AddWithValue("@inEvent", id);
                    cmdSearch.Parameters.AddWithValue("@inYear", year);
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
                    cmdSearch.Dispose();
                }
            }
            return Tuple.Create(flagInput, startDate, endDate);
        }

        public static Dictionary<string, Tuple<string, bool>> GetdataNote(string year, string[] cuscod)
        {
            Dictionary<string, Tuple<string, bool>> resultDictionary = new Dictionary<string, Tuple<string, bool>>();
            if (year != null && cuscod != null)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Lip_ConnectionString"].ConnectionString))
                {
                    conn.Open();
                    foreach (var cus in cuscod)
                    {
                        var cmdSearch = new SqlCommand("P_Get_Note_TheStar_Dev", conn);

                        cmdSearch.CommandType = CommandType.StoredProcedure;
                        cmdSearch.Parameters.AddWithValue("@cuscod", cus);
                        cmdSearch.Parameters.AddWithValue("@Year", year);
                        SqlParameter p = new SqlParameter("@IsStar", SqlDbType.Bit);
                        SqlParameter p2 = new SqlParameter("@Note", SqlDbType.NVarChar, 1000);
                        p.Direction = ParameterDirection.Output;
                        p2.Direction = ParameterDirection.Output;
                        cmdSearch.Parameters.Add(p);
                        cmdSearch.Parameters.Add(p2);
                        int INSID = cmdSearch.ExecuteNonQuery();
                        bool IsStar = Convert.ToBoolean(cmdSearch.Parameters["@IsStar"].Value);
                        string Note = cmdSearch.Parameters["@Note"].Value.ToString();

                        resultDictionary.Add(cus, Tuple.Create(Note, IsStar));
                        cmdSearch.Dispose();
                    }
                }
            }
            return resultDictionary;
        }
    }
}
