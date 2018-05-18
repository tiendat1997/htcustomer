using htcustomer.service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.Interfaces
{
    public interface IContactService
    {
        string TestSasuke();
        IEnumerable<CustomerViewModel> GetAllCustomer();
    }
    



}
