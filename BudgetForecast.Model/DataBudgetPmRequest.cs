using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetForecast.Model
{
    public class DataBudgetPmRequest
    {
        public string USER { get; set; }
        public string STKGRP { get; set; }
        public List<StoreUpdateBudgetPmModel> DETAILS { get; set; }
    }
}
