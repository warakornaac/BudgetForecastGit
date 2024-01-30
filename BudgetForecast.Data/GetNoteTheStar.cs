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
    public class GetNoteTheStar : MsSQL
    {
        public GetNoteTheStar() : base(Utils.GetConfig("Lip_ConnectionString"))
        {
        }
        public List<StoreSearchTheStarForecastNote> GetStoreSearchTheStarForecastNotes(string CUSKEY, string Year)
        {
            var p = new SqlParameters();
            p.AddParams("@cuscod", CUSKEY);
            p.AddParams("@Year", Year);


            var table = GetData(CmdStore("P_Get_Note_TheStar", p));
            return ConvertExtension.ConvertDataTable<StoreSearchTheStarForecastNote>(GetData(CmdStore("P_Get_Note_TheStar", p)));
        }
    }
}
