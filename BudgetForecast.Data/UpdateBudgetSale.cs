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
    public class UpdateBudgetSale : MsSQL
    {
        public UpdateBudgetSale() : base(Utils.GetConfig("Lip_ConnectionString"))
        {

        }
        public List<StoreUpdateBudgetSaleModel> Update(string USER, string CUSCODE, string STKGRP, double F_SLM)
        {
            var p = new SqlParameters();
            p.AddParams("@Cuscode", CUSCODE.ToTrim());
            p.AddParams("@Stkgrp", STKGRP.ToTrim());
            p.AddParams("@User", USER.ToTrim());
            p.AddParams("@F_slm", F_SLM.ToString().Replace(",", ""));
            p.AddParams("@Year", "");
            p.AddParams("@outGenstatus", 'Y');

            var table = GetData(CmdStore("P_Update_Budget_Sale_Dev", p));
            return ConvertExtension.ConvertDataTable<StoreUpdateBudgetSaleModel>(GetData(CmdStore("P_Update_Budget_Sale_Dev", p)));
        }
    }
}
