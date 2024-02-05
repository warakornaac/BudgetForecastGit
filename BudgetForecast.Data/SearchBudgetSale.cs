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
    public class SearchBudgetSale : MsSQL
    {
        public SearchBudgetSale() : base(Utils.GetConfig("Lip_ConnectionString"))
        {

        }
        public List<StoreSearchBudgetSaleModel> GetStoreSearchBudgetSale(string slmCode, string[] cusCode, string[] stkSec, string[] stkGroup, int flgBudget = 1)
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
            if (stkGroup == null)
            {
                stkGroup = new string[] { "ALL" };
            }
            var p = new SqlParameters();
            p.AddParams("@Slmcode", slmCode);
            p.AddParams("@Cuskey", string.Join(",", cusCode));
            p.AddParams("@Stksec", string.Join(",", stkSec));
            p.AddParams("@StkGrp", string.Join(",", stkGroup));
            p.AddParams("@User", "");
            p.AddParams("@Year", "2024");
            p.AddParams("@Figure_Flg", flgBudget);

            var table = GetData(CmdStore("P_Search_Budget_Sale", p));
            return ConvertExtension.ConvertDataTable<StoreSearchBudgetSaleModel>(GetData(CmdStore("P_Search_Budget_Sale", p)));
        }
    }
}
