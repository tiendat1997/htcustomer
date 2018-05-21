﻿using htcustomer.service.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.ViewModel.Transaction
{
    public class TransactionViewModel
    {
        public int TransactionID { get; set; }
        public TransactionStatus Status { get; set; }       
        public DateTime? RecieveDate { get; set; }        
        public DateTime? DeliveredDate { get; set; }
        public string Device { get; set; }
        public string Error { get; set; }
        public bool? Delivered { get; set; }
        public double? Price { get; set; }
    }
}
