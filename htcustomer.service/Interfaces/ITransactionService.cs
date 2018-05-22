using htcustomer.service.ViewModel;
using htcustomer.service.ViewModel.Contact;
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
    }
}
