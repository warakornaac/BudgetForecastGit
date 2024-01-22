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
    public class GetNoteSale : MsSQL
    {

        public GetNoteSale() : base(Utils.GetConfig("Lip_ConnectionString"))
        {
        }

        public List<StoredGetNoteSale> GetStoredGetNoteSales(string Slmcod, string Month, string Year, string Sec)
        {
            var p = new SqlParameters();
            p.AddParams("@Slmcode", Slmcod.ToTrim());
            p.AddParams("@Month", Month.ToTrim());
            p.AddParams("@Year", Year.ToTrim());
            p.AddParams("@Sec", Sec.ToTrim());


            var table = GetData(CmdStore("P_Get_Note_Sale_Dev", p));
            return ConvertExtension.ConvertDataTable<StoredGetNoteSale>(GetData(CmdStore("P_Get_Note_Sale_Dev", p)));
        }
    }
}
