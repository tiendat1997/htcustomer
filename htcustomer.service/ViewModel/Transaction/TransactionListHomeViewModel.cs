using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.ViewModel.Transaction
{
    public class TransactionListHomeViewModel
    {
        public IEnumerable<TransactionHomeViewModel> NotFixTransactions { get; set; }
        public IEnumerable<TransactionHomeViewModel> FixedTransactions { get; set; }
        public IEnumerable<TransactionHomeViewModel> CannotFixTransactions { get; set; }

    }
}
