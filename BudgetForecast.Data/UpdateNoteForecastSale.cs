using My.Data;
using BudgetForecast.Library;
using BudgetForecast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetForecast.Data
{
    public class UpdateNoteForecastSale : MsSQL
    {
        public UpdateNoteForecastSale() : base(Utils.GetConfig("Lip_ConnectionString"))
        {
        }
        public List<StoreUpdateNoteSaleModel> Update(string MONTH, string USER, string SEC, string YEAR, string SLMCOD, string INPUT)
        {

            var p = new SqlParameters();
            p.AddParams("@MONTH_INPUT", MONTH);
            p.AddParams("@User", USER.ToTrim());
            p.AddParams("@sec", SEC.ToTrim());
            p.AddParams("@year", YEAR.ToTrim());
            p.AddParams("@Slmcod", SLMCOD.ToTrim());
            p.AddParams("@Input", INPUT.ToString());

            p.AddParams("@outGenstatus", "Y");

            var table = GetData(CmdStore("P_Update_Note_Sale", p));
            return ConvertExtension.ConvertDataTable<StoreUpdateNoteSaleModel>(GetData(CmdStore("P_Update_Note_Sale", p)));
        }
    }
}
