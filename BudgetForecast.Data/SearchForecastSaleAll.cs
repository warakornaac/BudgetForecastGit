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
    public class SearchForecastSaleAll : MsSQL
    {
        public SearchForecastSaleAll() : base(Utils.GetConfig("Lip_ConnectionString"))
        {
        }
        public List<StoreSearchForecastSaleModel> GetStoreSearchForecastSaleAll(string slmCode, string year, int flg, int month = 0)
        {
            var p = new SqlParameters();
            p.AddParams("@inSLMCOD", slmCode);
            p.AddParams("@inCOM", "");
            p.AddParams("@inYEAR", year);
            p.AddParams("@Show_Flg", flg);
            p.AddParams("@Show_month", month);

            var table = GetData(CmdStore("P_Search_Forecast_Sale_All_Dev", p));
            return ConvertExtension.ConvertDataTable<StoreSearchForecastSaleModel>(GetData(CmdStore("P_Search_Forecast_Sale_All_Dev", p)));
        }
    }
}
