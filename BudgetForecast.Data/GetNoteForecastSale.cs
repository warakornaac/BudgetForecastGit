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
    public class GetNoteForecastSale : MsSQL
    {
        public GetNoteForecastSale() : base(Utils.GetConfig("Lip_ConnectionString"))
        {
        }
        public List<GetNoteForecastSaleModel> GetNoteSale(string SLMCOD, string MONTH_INPUT, string YEAR, string SEC)
        {
            var p = new SqlParameters();
            p.AddParams("@Slmcode", SLMCOD);
            p.AddParams("@Month", MONTH_INPUT);
            p.AddParams("@Year", YEAR);
            p.AddParams("@Sec", SEC);

            var table = GetData(CmdStore("P_Get_Note_Sale_De", p));
            return ConvertExtension.ConvertDataTable<GetNoteForecastSaleModel>(GetData(CmdStore("P_Get_Note_Sale_Dev", p)));

        }
    }
}
