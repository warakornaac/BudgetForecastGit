using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetForecast.Model
{
    public class StoreSearchBudgetSaleModel
    {
       // public string[][] arrData { get; set; }
        public string CUSGRP { get; set; }
        public string CUSCOD { get; set; }
        public string CUSKEY { get; set; }
        public string PEOPLE { get; set; }
        public string CUSNAM { get; set; }
        public string PRO { get; set; }
        public string STKGRP { get; set; }
        public string SEC { get; set; }
        public string GRPNAM { get; set; }
        public string CUSTYP { get; set; }
        public string SLMCOD { get; set; }
        public double Last2Yr { get; set; }
        public double LastYr { get; set; }
        public double Ytd { get; set; }
        public double Quota { get; set; }
        public double Quota_Nxt { get; set; }
        public double F_Slm { get; set; }
        public double F_Prod { get; set; }
        public double F_Diff { get; set; }
        public string STYR { get; set; }
        public string StQYr { get; set; }
        public DateTime AsOf { get; set; }
        public string UpdateBy { get; set; }
        public DateTime Updatedate { get; set; }

    }
}
