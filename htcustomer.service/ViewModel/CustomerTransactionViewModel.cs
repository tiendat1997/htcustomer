using htcustomer.service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace htcustomer.service.ViewModel
{
    public class CustomerTransactionViewModel
    {
        public int TransactionID { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public string Device { get; set; }
        public string Error { get; set; }
        public bool? Delivered { get; set; }
    }
}