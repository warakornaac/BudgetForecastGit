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
    public class SearchForecastSale : MsSQL
    {
        public SearchForecastSale() : base(Utils.GetConfig("Lip_ConnectionString"))
        {
        }
        public List<StoreSearchForecastSaleModel> GetStoreSearchForecastSale(string slmCode, string[] cusCode, string[] stkSec, string year, int flg, int month = 0)
        {
            //add all
            if (cusCode == null)
            {
                cusCode = new string[] { "ALL" };
            }
            if (stkSec == null)
            {
                stkSec = new string[] { "ALL" };
            }
            var p = new SqlParameters();
            p.AddParams("@inSLMCOD", slmCode);
            p.AddParams("@inCUSCOD", string.Join(",", cusCode));
            p.AddParams("@inSEC", string.Join(",", stkSec));
            p.AddParams("@inCOM", "TAC");
            p.AddParams("@inYEAR", year);
            p.AddParams("@Show_Flg", flg);
            p.AddParams("@Show_month", month);

            var table = GetData(CmdStore("P_Search_Forecast_Sale_Dev", p));
            return ConvertExtension.ConvertDataTable<StoreSearchForecastSaleModel>(GetData(CmdStore("P_Search_Forecast_Sale_Dev", p)));
        }
    }
}
