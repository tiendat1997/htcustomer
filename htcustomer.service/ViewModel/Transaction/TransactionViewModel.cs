using htcustomer.service.Enums;
using htcustomer.service.ViewModel.Category;
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
        public DateTime? RecievedDate { get; set; }        
        public DateTime? DeliveredDate { get; set; }
        public CategoryViewModel Category { get; set; }
        public string Description { get; set; }
        public string Error { get; set; }
        public bool? Delivered { get; set; }
        public double? Price { get; set; }        
        public string Reason { get; set; }
        public CustomerViewModel Customer { get; set; }
        public IEnumerable<PriceDetailViewModel> ListPriceDetail { get; set; }
    }
}
