using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.ViewModel.Transaction
{
    public class TransactionCreateViewModel
    {
        public int CustomerID { get; set; }
        public int CategoryID { get; set; }
        public string Description { get; set; }
        public string Error { get; set; }
    }
}
