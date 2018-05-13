using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace htcustomer.web.Controllers
{
    public class DeviceController : Controller
    {
        // GET: Device
        public ActionResult Index(int statusId = 0)
        {
            
            if (statusId == 1)
            {
                return View("FixedDevice"); 
            }
            else if (statusId == 2)
            {
                return View("CannotFixDevice");
            }

            return View("NotFixDevice");
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
    }
}