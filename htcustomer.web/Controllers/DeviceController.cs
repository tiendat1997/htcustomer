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
        public ActionResult Index(int statusId = 0)
        {
            var transactions = transactionService.GetListTransaction((TransactionStatus)statusId);
            if (statusId == 1)
            {
                return View("FixedDevice", transactions); 
            }
            else if (statusId == 2)
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