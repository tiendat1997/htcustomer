using htcustomer.service.Enums;
using htcustomer.service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace htcustomer.web.Controllers
{
    public class DeviceController : Controller
    {
        private readonly ITransactionService transactionService;

        public DeviceController(ITransactionService _transactionService)
        {
            this.transactionService = _transactionService;
        }


        // GET: Device
        public ActionResult Index(int statusId = (int)TransactionStatus.NotFix)
        {
            var transactions = transactionService.GetListTransaction((TransactionStatus)statusId);
          
            if (statusId == (int)TransactionStatus.Fixed)
            {
                return View("FixedDevice", transactions); 
            }
            else if (statusId == (int)TransactionStatus.CannotFix)
            {
                return View("CannotFixDevice",transactions);
            }
            return View("NotFixDevice", transactions);

        }

        // Render Partial View for Device Tabs
        public ActionResult PartialTabs()
        {
            return PartialView("_DeviceTab");
        }

        // Render Partial View for Filters
        public ActionResult PartialFilter()
        {
            return PartialView("_DeviceFilter");
        }

        public ActionResult AddToBillForm()
        {
            return PartialView("_AddBillForm");
        }
    }
}