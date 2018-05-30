using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.ViewModel.Transaction
{
    public class PriceDetailViewModel
    {
        public int TransactionID { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
