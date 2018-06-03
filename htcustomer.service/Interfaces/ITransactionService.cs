using htcustomer.service.Enums;
using htcustomer.service.ViewModel;
using htcustomer.service.ViewModel.Contact;
using htcustomer.service.ViewModel.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.Interfaces
{
    public interface ITransactionService
    {        
        ContactDetailsViewModel GetContactDetails(int customerID);
        TransactionListHomeViewModel GetListTransactionHome();
        TransactionListViewModel GetListTransaction(TransactionStatus status, int? month = null, int? year = null, int? categoryId = null);

    }
}
