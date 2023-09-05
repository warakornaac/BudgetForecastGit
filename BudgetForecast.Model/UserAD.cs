using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetForecast.Model
{
    public class UserAD
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Department { get; set; }
        public string DisplayName { get; set; }
        public string Company { get; set; }
        public bool isMapped { get; set; }
    }
}
