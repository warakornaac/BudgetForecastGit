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
    public class UpdateBudgetPm : MsSQL
    {
        public UpdateBudgetPm() : base(Utils.GetConfig("Lip_ConnectionString"))
        {

        }
        public List<StoreUpdateBudgetPmModel> Update(string USER, string STKGRP, double BUD00, double BUD01, double BUD02, double BUD03, double BUD04, double BUD05, double BUD06, double BUD07, double BUD08, double BUD09, double BUD10, double BUD11, double BUD12, double GP01, double GP02, double GP03, double GP04, double GP05, double GP06, double GP07, double GP08, double GP09, double GP10, double GP11, double GP12)
        {
            var p = new SqlParameters();
            p.AddParams("@User", USER.ToTrim());
            p.AddParams("@stkgrp", STKGRP.ToTrim());
            p.AddParams("@BUD00", BUD00.ToString().Replace(",", ""));
            p.AddParams("@BUD01", BUD01.ToString().Replace(",", ""));
            p.AddParams("@BUD02", BUD02.ToString().Replace(",", ""));
            p.AddParams("@BUD03", BUD03.ToString().Replace(",", ""));
            p.AddParams("@BUD04", BUD04.ToString().Replace(",", ""));
            p.AddParams("@BUD05", BUD05.ToString().Replace(",", ""));
            p.AddParams("@BUD06", BUD06.ToString().Replace(",", ""));
            p.AddParams("@BUD07", BUD07.ToString().Replace(",", ""));
            p.AddParams("@BUD08", BUD08.ToString().Replace(",", ""));
            p.AddParams("@BUD09", BUD09.ToString().Replace(",", ""));
            p.AddParams("@BUD10", BUD10.ToString().Replace(",", ""));
            p.AddParams("@BUD11", BUD11.ToString().Replace(",", ""));
            p.AddParams("@BUD12", BUD12.ToString().Replace(",", ""));

            p.AddParams("@GP01", GP01.ToString().Replace(",", ""));
            p.AddParams("@GP02", GP02.ToString().Replace(",", ""));
            p.AddParams("@GP03", GP03.ToString().Replace(",", ""));
            p.AddParams("@GP04", GP04.ToString().Replace(",", ""));
            p.AddParams("@GP05", GP05.ToString().Replace(",", ""));
            p.AddParams("@GP06", GP06.ToString().Replace(",", ""));
            p.AddParams("@GP07", GP07.ToString().Replace(",", ""));
            p.AddParams("@GP08", GP08.ToString().Replace(",", ""));
            p.AddParams("@GP09", GP09.ToString().Replace(",", ""));
            p.AddParams("@GP10", GP10.ToString().Replace(",", ""));
            p.AddParams("@GP11", GP11.ToString().Replace(",", ""));
            p.AddParams("@GP12", GP12.ToString().Replace(",", ""));
            p.AddParams("@outGenstatus", 'Y');

            var table = GetData(CmdStore("P_Update_Budget_PM", p));
            return ConvertExtension.ConvertDataTable<StoreUpdateBudgetPmModel>(GetData(CmdStore("P_Update_Budget_PM", p)));
        }
    }
}
