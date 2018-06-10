using htcustomer.service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using htcustomer.entity;
using htcustomer.service.ViewModel;
using htcustomer.service.Helper;
using htcustomer.service.Enums;

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
        public ActionResult GetAddressBook(string searchCustomer, bool? callFromHome = false)
        {
            var addressBook = contactService.GetAddressBook(searchCustomer);
            if (callFromHome == true)
            { 
                // SEARCH FROM HOME 
                return Json(addressBook.customerList, JsonRequestBehavior.AllowGet);
            }
            // SEARCH FROM CONTACT 
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
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                    return Json(new JsonMessage { Status = JsonResultStatus.Unvalidated, Message = "UnValidate customer", Errors = errors }, JsonRequestBehavior.AllowGet);
                }
                var result = contactService.AddCustomer(customer);
                if (result == -1) return Json(new JsonMessage { Status = JsonResultStatus.Unvalidated, Message = "Customer has existed before !!!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage { Status = JsonResultStatus.Fail, Message = "Error during create customer" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage { Status = JsonResultStatus.Success, Message = "Adding customer successfully" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DisableCustomer(int customerID)
        {
            try
            {
                contactService.DisableCustomer(customerID);                
            } 
            catch(Exception ex)
            {
                return Json(new JsonMessage { Status = JsonResultStatus.Fail, Message = "Error during remove customer" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage { Status = JsonResultStatus.Success, Message = "Removing customer successfully"},JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCustomer(CustomerViewModel customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                    return Json(new JsonMessage { Status = JsonResultStatus.Unvalidated, Message = "Unvalidated customer", Errors = errors }, JsonRequestBehavior.AllowGet);
                }
                contactService.UpdateCustomer(customer);
            }
            catch(Exception ex)
            {
                return Json(new JsonMessage { Status = JsonResultStatus.Fail, Message = "Error during editing customer" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage { Status = JsonResultStatus.Success, Message = "Editing customer successfully" }, JsonRequestBehavior.AllowGet);
        }      
    }
}