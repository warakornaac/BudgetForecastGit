using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BudgetForecast.Model
{
    public class StoreUpdateTheStarNote
    {
        public string User { get; set; }
        public string year { get; set; }
        public string CusKey { get; set; }
        public string Input { get; set; }
    }
}