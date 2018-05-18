﻿using htcustomer.service.Interfaces;
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllCustomer()
        {
            var allCustomer = contactService.GetAllCustomer();            
            return Json(allCustomer, JsonRequestBehavior.AllowGet);
        }
    }
}