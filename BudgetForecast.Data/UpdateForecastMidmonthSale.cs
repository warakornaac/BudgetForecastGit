using BudgetForecast.Library;
using BudgetForecast.Model;
using My.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetForecast.Data
{
    public class UpdateForecastMidmonthSale : MsSQL
    {
        public UpdateForecastMidmonthSale() : base(Utils.GetConfig("Lip_ConnectionString"))
        {

        }
        public List<StoreUpdateForecastMidmonthSaleModel> Update(string MONTH_INPUT, string USER, string SEC, string YEAR, string CUSCOD, double INPUT)
        {
            var p = new SqlParameters();
            p.AddParams("@MONTH_INPUT", MONTH_INPUT);
            p.AddParams("@User", USER.ToTrim());
            p.AddParams("@sec", SEC.ToTrim());
            p.AddParams("@year", YEAR.ToTrim());
            p.AddParams("@Cuscod", CUSCOD.ToTrim());
            p.AddParams("@Input", INPUT.ToString().Replace(",", ""));

            p.AddParams("@outGenstatus", "Y");

            var table = GetData(CmdStore("P_Update_NewForecast_Sale_Dev", p));
            return ConvertExtension.ConvertDataTable<StoreUpdateForecastMidmonthSaleModel>(GetData(CmdStore("P_Update_NewForecast_Sale_Dev", p)));
        }
    }
}
