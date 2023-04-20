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
        public List<StoreSearchBudgetSaleModel> GetStoreSearchForecastPm(string slmCode, string[] cusCode, string[] stkGrp, string year)
        {
            //add all
            if (cusCode == null) {
                cusCode = new string[] { "ALL" };
            }
            var p = new SqlParameters();
            p.AddParams("@Slmcode", slmCode);
            p.AddParams("@Cuskey", string.Join(",", cusCode));
            p.AddParams("@Stkgrp", string.Join(",", stkGrp));
            p.AddParams("@User", "");
            p.AddParams("@Year", year);

            var table = GetData(CmdStore("P_Search_Budget_Sale", p));
            return ConvertExtension.ConvertDataTable<StoreSearchBudgetSaleModel>(GetData(CmdStore("P_Search_Budget_Sale", p)));
        }
    }
}
