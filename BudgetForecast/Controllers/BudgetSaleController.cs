using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BudgetForecast.Model;
using BudgetForecast.Data;
using BudgetForecast.Models;

namespace BudgetForecast.Controllers
{
    public class BudgetSaleController : Controller
    {
        // GET: BudgetSale
        public ActionResult Index()
        {
            return View();
        }
    }
}