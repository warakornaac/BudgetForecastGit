using BudgetForecast.Library;
using BudgetForecast.Model;
using My.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BudgetForecast.Data
{
    public class SearchBudgetPm : MsSQL
    {
        public SearchBudgetPm() : base(Utils.GetConfig("Lip_ConnectionString")) 
        {
            
        }
        public List<StoreSearchBudgetPmModel> GetStoreSearchBudgetPm(string User, string Year, string[] StockGroup) 
        {
            var p = new SqlParameters();
            p.AddParams("@User", User.ToTrim());
            p.AddParams("@Year", Year.ToTrim());
            p.AddParams("@StockGroup", string.Join(",", StockGroup));

            var table = GetData(CmdStore("P_Search_Budget_PM_NxtYr", p));
            return ConvertExtension.ConvertDataTable<StoreSearchBudgetPmModel>(GetData(CmdStore("P_Search_Budget_PM_NxtYr", p)));
        }
    }
}
