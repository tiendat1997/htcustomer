using htcustomer.service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.Interfaces
{
    public interface ITransactionService
    {
        IEnumerable<CustomerTransactionViewModel> GetCustomerTransaction(int customerID);
    }
}
