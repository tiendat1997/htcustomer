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
        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
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
            }            
            return View();
        }     
        public ActionResult GetAddressBook(string searchCustomer)
        {
            var addressBook = contactService.GetAddressBook(searchCustomer);

            return PartialView("_AddressBook", addressBook);
        }


    }
}