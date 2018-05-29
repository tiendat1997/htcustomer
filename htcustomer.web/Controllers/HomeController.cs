using htcustomer.service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace htcustomer.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransactionService transactionService;

        public HomeController(ITransactionService _transactionService)
        {
            this.transactionService = _transactionService;
        }

        public ActionResult Index()
        {
            var model = transactionService.GetListTransactionHome();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}