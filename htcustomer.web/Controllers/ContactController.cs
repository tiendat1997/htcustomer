using htcustomer.service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using htcustomer.entity;
using htcustomer.service.ViewModel;

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
        [HttpGet]
        public ActionResult AddCustomer()
        {
            return PartialView("_NewCustomer", new CustomerViewModel());
        }
        [HttpPost]
        public ActionResult AddCustomer(CustomerViewModel customer)
        {           
            try
            {
                contactService.AddCustomer(customer);
                
            } catch(Exception ex)
            {
                
            }
            return RedirectToAction("Index");
        }
        public ActionResult DisableCustomer(int customerID)
        {
            contactService.DisableCustomer(customerID);
            return RedirectToAction("Index", new { customerID });
        }
        public ActionResult UpdateCustomer(CustomerViewModel customer)
        {
            contactService.UpdateCustomer(customer);
            return RedirectToAction("Index", new { customerID = customer.CustomerID });
        }
    }
}