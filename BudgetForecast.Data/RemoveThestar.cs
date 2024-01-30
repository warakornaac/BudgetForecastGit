using BudgetForecast.Library;
using BudgetForecast.Model;
using My.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetForecast.Data
{
    public class RemoveThestar : MsSQL
    {
        public RemoveThestar() : base(Utils.GetConfig("Lip_ConnectionString"))
        {
        }
        public List<StoreAddTheStarModel> Remove(string year, string CusKey)
        {

            var p = new SqlParameters();
            p.AddParams("@year", year.ToTrim());
            p.AddParams("@CusKey", CusKey.ToTrim());
            p.AddParams("@outGenstatus", "Y");



            var table = GetData(CmdStore("P_Remove_TheStar", p));
            return ConvertExtension.ConvertDataTable<StoreAddTheStarModel>(GetData(CmdStore("P_Remove_TheStar", p)));
        }
    }
}
