using My.Data;
using BudgetForecast.Model;
using BudgetForecast.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetForecast.Data
{
    public class SearchForecastPm : MsSQL
    {
        public SearchForecastPm() : base(Utils.GetConfig("Lip_ConnectionString"))
        {

        }
        public List<StoreSearchForecastPmModel> GetStoreSearchForecastPm(string User, string Year, string[] Sec)
        {
            var p = new SqlParameters();
            p.AddParams("@User", User.ToTrim());
            p.AddParams("@Year", Year.ToTrim());
            p.AddParams("@Sec", string.Join(",", Sec));

            var table = GetData(CmdStore("P_Search_Forecast_PM", p));
            return ConvertExtension.ConvertDataTable<StoreSearchForecastPmModel>(GetData(CmdStore("P_Search_Forecast_PM", p)));
        }
    }
}
