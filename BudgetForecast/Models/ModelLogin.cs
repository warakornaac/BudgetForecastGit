using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BudgetForecast.Models
{
    public class ModelLogin
    {
        public class LoginUserViewModels
        {
            //[Required]
            //[StringLength(150, MinimumLength = 10)]
            //[Display(Name = "User: ")]
            public string User { get; set; }

            //[Required]
            //[DataType(DataType.Password)]
            //[StringLength(150, MinimumLength = 2)]
            //[Display(Name = "Password: ")]
            public string Password { get; set; }
        }
    }
}