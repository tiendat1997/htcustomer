using htcustomer.service.Enums;
using htcustomer.service.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.ViewModel.Transaction
{
    public class TransactionHomeViewModel
    {
        public int TransactionID { get; set; }
        public CategoryViewModel Category { get; set; }
        public CustomerViewModel Customer { get; set; }
        public string DeviceDescription { get; set; }
        public TransactionStatus Status { get; set; }
        public double Price { get; set; }
        public string CannotFixNote { get; set; }
        public string Error { get; set; }
        public IEnumerable<PriceDetailViewModel> ListPriceDetail { get; set; }

    }
}
