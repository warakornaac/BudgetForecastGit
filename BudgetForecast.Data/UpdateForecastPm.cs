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
    public class UpdateForecastPm : MsSQL
    {
        public UpdateForecastPm() : base(Utils.GetConfig("Lip_ConnectionString")) { 
       
        }
        public List<StoreUpdateForecastPmModel> Update(string USER, string SEC, string YEAR, double FC00, double FC01, double FC02, double FC03, double FC04, double FC05, double FC06, double FC07, double FC08, double FC09, double FC10, double FC11, double FC12, double FC13, double FC14, double FC_GP00, double FC_GP01, double FC_GP02, double FC_GP03, double FC_GP04, double FC_GP05, double FC_GP06, double FC_GP07, double FC_GP08, double FC_GP09, double FC_GP10, double FC_GP11, double FC_GP12, double FC_GP13, double FC_GP14, double TG_INV00, double TG_INV01, double TG_INV02, double TG_INV03, double TG_INV04, double TG_INV05, double TG_INV06, double TG_INV07, double TG_INV08, double TG_INV09, double TG_INV10, double TG_INV11, double TG_INV12, double TG_INV13, double TG_INV14) {
            var p = new SqlParameters();
            p.AddParams("@User", USER.ToTrim());
            p.AddParams("@sec", SEC.ToTrim());
            p.AddParams("@year", YEAR.ToTrim());
            p.AddParams("@FC00", FC00.ToString().Replace(",", ""));
            p.AddParams("@FC01", FC01.ToString().Replace(",", ""));
            p.AddParams("@FC02", FC02.ToString().Replace(",", ""));
            p.AddParams("@FC03", FC03.ToString().Replace(",", ""));
            p.AddParams("@FC04", FC04.ToString().Replace(",", ""));
            p.AddParams("@FC05", FC05.ToString().Replace(",", ""));
            p.AddParams("@FC06", FC06.ToString().Replace(",", ""));
            p.AddParams("@FC07", FC07.ToString().Replace(",", ""));
            p.AddParams("@FC08", FC08.ToString().Replace(",", ""));
            p.AddParams("@FC09", FC09.ToString().Replace(",", ""));
            p.AddParams("@FC10", FC10.ToString().Replace(",", ""));
            p.AddParams("@FC11", FC11.ToString().Replace(",", ""));
            p.AddParams("@FC12", FC12.ToString().Replace(",", ""));
            p.AddParams("@FC13", FC13.ToString().Replace(",", ""));
            p.AddParams("@FC14", FC14.ToString().Replace(",", ""));
            p.AddParams("@FC_GP00", FC_GP00.ToString().Replace(",", ""));
            p.AddParams("@FC_GP01", FC_GP01.ToString().Replace(",", ""));
            p.AddParams("@FC_GP02", FC_GP02.ToString().Replace(",", ""));
            p.AddParams("@FC_GP03", FC_GP03.ToString().Replace(",", ""));
            p.AddParams("@FC_GP04", FC_GP04.ToString().Replace(",", ""));
            p.AddParams("@FC_GP05", FC_GP05.ToString().Replace(",", ""));
            p.AddParams("@FC_GP06", FC_GP06.ToString().Replace(",", ""));
            p.AddParams("@FC_GP07", FC_GP07.ToString().Replace(",", ""));
            p.AddParams("@FC_GP08", FC_GP08.ToString().Replace(",", ""));
            p.AddParams("@FC_GP09", FC_GP09.ToString().Replace(",", ""));
            p.AddParams("@FC_GP10", FC_GP10.ToString().Replace(",", ""));
            p.AddParams("@FC_GP11", FC_GP11.ToString().Replace(",", ""));
            p.AddParams("@FC_GP12", FC_GP12.ToString().Replace(",", ""));
            p.AddParams("@FC_GP13", FC_GP13.ToString().Replace(",", ""));
            p.AddParams("@FC_GP14", FC_GP14.ToString().Replace(",", ""));
            p.AddParams("@TG_INV00", TG_INV00.ToString().Replace(",", ""));
            p.AddParams("@TG_INV01", TG_INV01.ToString().Replace(",", ""));
            p.AddParams("@TG_INV02", TG_INV02.ToString().Replace(",", ""));
            p.AddParams("@TG_INV03", TG_INV03.ToString().Replace(",", ""));
            p.AddParams("@TG_INV04", TG_INV04.ToString().Replace(",", ""));
            p.AddParams("@TG_INV05", TG_INV05.ToString().Replace(",", ""));
            p.AddParams("@TG_INV06", TG_INV06.ToString().Replace(",", ""));
            p.AddParams("@TG_INV07", TG_INV07.ToString().Replace(",", ""));
            p.AddParams("@TG_INV08", TG_INV08.ToString().Replace(",", ""));
            p.AddParams("@TG_INV09", TG_INV09.ToString().Replace(",", ""));
            p.AddParams("@TG_INV10", TG_INV10.ToString().Replace(",", ""));
            p.AddParams("@TG_INV11", TG_INV11.ToString().Replace(",", ""));
            p.AddParams("@TG_INV12", TG_INV12.ToString().Replace(",", ""));
            p.AddParams("@TG_INV13", TG_INV13.ToString().Replace(",", ""));
            p.AddParams("@TG_INV14", TG_INV14.ToString().Replace(",", ""));
            p.AddParams("@outGenstatus", 'Y');

            var table = GetData(CmdStore("P_Update_Forecast_PM", p));
            return ConvertExtension.ConvertDataTable<StoreUpdateForecastPmModel>(GetData(CmdStore("P_Update_Forecast_PM", p)));
        }
    }
}
