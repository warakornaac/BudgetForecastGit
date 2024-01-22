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
    public class UpdateForecastSale : MsSQL
    {
        public UpdateForecastSale() : base(Utils.GetConfig("Lip_ConnectionString"))
        {
        }
        public List<StoreUpdateForecastSaleModel> Update(string MONTH_INPUT, string USER, string SEC, string YEAR, string CUSCOD, double INPUT)
        {
            var p = new SqlParameters();
            p.AddParams("@MONTH_INPUT", MONTH_INPUT);
            p.AddParams("@User", USER.ToTrim());
            p.AddParams("@Sec", SEC.ToTrim());
            p.AddParams("@Year", YEAR.ToTrim());
            p.AddParams("@Cuscod", CUSCOD.ToTrim());
            p.AddParams("@Input", INPUT.ToString().Replace(",", ""));

            p.AddParams("@outGenstatus", 'Y');

            var table = GetData(CmdStore("P_Update_Forecast_Sale_Dev", p));
            return ConvertExtension.ConvertDataTable<StoreUpdateForecastSaleModel>(GetData(CmdStore("P_Update_Forecast_Sale_Dev", p)));
        }
    }
}
