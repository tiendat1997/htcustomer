using htcustomer.service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.ViewModel.Transaction
{
    public class TransactionListViewModel
    {
        public TransactionStatus? Status { get; set; }
        public IEnumerable<TransactionViewModel> Transactions { get; set; }
    }
}
