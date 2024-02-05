using My.Data;
using BudgetForecast.Library;
using BudgetForecast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BudgetForecast.Data
{
    public class UpdateNoteForecastSale : MsSQL
    {
        // private string dev;
        public UpdateNoteForecastSale() : base(Utils.GetConfig("Lip_ConnectionString"))
        {
        }
        public List<StoreUpdateNoteSaleModel> Update(string MONTH, string USER, string SEC, string YEAR, string SLMCOD, string INPUT)
        {
            //dev = GetDevFromUrl();
            var p = new SqlParameters();
            p.AddParams("@MONTH_INPUT", MONTH);
            p.AddParams("@User", USER.ToTrim());
            p.AddParams("@sec", SEC.ToTrim());
            p.AddParams("@year", YEAR.ToTrim());
            p.AddParams("@Slmcod", SLMCOD.ToTrim());
            p.AddParams("@Input", INPUT.ToString());

            p.AddParams("@outGenstatus", "Y");

            //var storeProcedureName = "";

            //if (dev.Count() > 0)
            //{
            //    storeProcedureName = "P_Update_Note_Sale_" + dev;
            //}
            //else
            //{
            //    storeProcedureName = "P_Update_Note_Sale";

            //}

            var table = GetData(CmdStore("P_Update_Note_Sale", p));
            return ConvertExtension.ConvertDataTable<StoreUpdateNoteSaleModel>(GetData(CmdStore("P_Update_Note_Sale", p)));
        }
        private string GetDevFromUrl()
        {
            string[] segments = HttpContext.Current.Request.Url.Segments;
            for (int i = 0; i < segments.Length; i++)
            {
                if (segments[i].Trim('/').Equals("NB2023_Test", StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 2 < segments.Length && segments[i + 1].Trim('/').Contains("Controller") && segments[i + 2].Trim('/').Contains("Index"))
                    {
                        return "Dev";
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return null;
        }
    }
}
