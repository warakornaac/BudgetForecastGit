using BudgetForecast.Library;
using BudgetForecast.Model;
using My.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BudgetForecast.Data
{
    public class UpdateTheStarNote : MsSQL
    {
        public UpdateTheStarNote() : base(Utils.GetConfig("Lip_ConnectionString"))
        {
        }
        public List<StoreUpdateTheStarNote> Update(string User, string Year, string CusKey, string Input)
        {

            var p = new SqlParameters();
            p.AddParams("@User", User.ToTrim());
            p.AddParams("@year", Year.ToTrim());
            p.AddParams("@CusKey", CusKey.ToTrim());
            p.AddParams("@Input", Input.ToString());

            p.AddParams("@outGenstatus", "Y");

            var table = GetData(CmdStore("P_Update_Note_TheStar_Dev", p));
            return ConvertExtension.ConvertDataTable<StoreUpdateTheStarNote>(GetData(CmdStore("P_Update_Note_TheStar_Dev", p)));
        }
    }
}
