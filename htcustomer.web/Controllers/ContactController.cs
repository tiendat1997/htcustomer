using htcustomer.service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace htcustomer.web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService contactService;
        private readonly ITransactionService transactionService;
        public ContactController(IContactService contactService, ITransactionService transactionService)
        {
            this.contactService = contactService;
            this.transactionService = transactionService;
        }
        public string TestStatus()
        {
            return contactService.TestSasuke();
        }
        // GET: Contact
        public ActionResult Index(int? customerID = null)
        {

            if (customerID != null)
            {
                // Get Details of A customer 
                var contactDetails = transactionService.GetContactDetails((int)customerID);
                return View(contactDetails);
            }
            else
            {
        
            }
            return View();
        }
        public ActionResult GetAddressBook(string searchCustomer)
        {
            var addressBook = contactService.GetAddressBook(searchCustomer);
            TempData["reload-url"] = "htcustomer/Contact/";

            return PartialView("_AddressBook", addressBook);
        }        
    }
}