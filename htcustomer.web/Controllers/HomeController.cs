using htcustomer.service.Enums;
using htcustomer.service.Helper;
using htcustomer.service.Interfaces;
using htcustomer.service.ViewModel.Transaction;
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
        private readonly IContactService contactService;

        public HomeController(ITransactionService _transactionService, IContactService _contactService)
        {
            this.transactionService = _transactionService;
            this.contactService = _contactService;
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
        public ActionResult InitTransaction()
        {
            var transaction = new TransactionViewModel();
            return PartialView("_NewTransaction", transaction);
        }
        public ActionResult AddTransaction(TransactionViewModel transaction)
        {
            try
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Where(x => !x.ErrorMessage.Contains("Phone")).Select(x => x.ErrorMessage).ToList();
                if (errors.Count > 0) 
                    return Json(new JsonMessage { Status = JsonResultStatus.Unvalidated, Message = "Transaction is invalid", Errors = errors }, JsonRequestBehavior.AllowGet);
                var result = transactionService.Add(transaction);
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage { Status = JsonResultStatus.Fail, Message = "Error during add transaction" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage { Status = JsonResultStatus.Success, Message = "Add Transaction Successfully" });
        }
        public ActionResult AddCustomerInfo(TransactionViewModel transaction)
        {
            return PartialView("_AddCustomer", transaction);
        }
        [HttpPost]
        public ActionResult AddCustomer(TransactionViewModel transaction)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                    return Json(new JsonMessage { Status = JsonResultStatus.Unvalidated, Message = "UnValidate customer", Errors = errors }, JsonRequestBehavior.AllowGet);
                }
                var result = contactService.AddCustomer(transaction.Customer);
                if (result == -1) return Json(new JsonMessage { Status = JsonResultStatus.Unvalidated, Message = "Customer has existed before !!!" }, JsonRequestBehavior.AllowGet);

                transaction.Customer.CustomerID = result;
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage { Status = JsonResultStatus.Fail, Message = "Error during create customer" }, JsonRequestBehavior.AllowGet);
            }

            return PartialView("_NewTransaction", transaction);
        }
    }
}