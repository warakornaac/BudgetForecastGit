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
    public class AddTheStar : MsSQL
    {
        public AddTheStar() : base(Utils.GetConfig("Lip_ConnectionString"))
        {
        }
        public List<StoreAddTheStarModel> Add(string year, string CusKey, string USER)
        {

            var p = new SqlParameters();
            p.AddParams("@year", year.ToTrim());
            p.AddParams("@CusKey", CusKey.ToTrim());
            p.AddParams("@User", USER.ToTrim());
            p.AddParams("@outGenstatus", "Y");



            var table = GetData(CmdStore("P_Add_TheStar_Dev", p));
            return ConvertExtension.ConvertDataTable<StoreAddTheStarModel>(GetData(CmdStore("P_Add_TheStar_Dev", p)));
        }
    }
}
