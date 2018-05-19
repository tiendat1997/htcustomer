using htcustomer.repository;
using htcustomer.service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using htcustomer.entity;
using htcustomer.service.ViewModel;

namespace htcustomer.service.Implements
{
    public class ContactService : IContactService
    {
        private readonly IRepository<TblCustomer> customerRepository;
        public ContactService(IRepository<TblCustomer> _customerRepository)
        {
            customerRepository = _customerRepository;
        }
        public string TestSasuke()
        {
            return "Hello Naruto - I don't want to be master of boruto";
        }

        public IEnumerable<CustomerViewModel> GetAllCustomer()
        {
            var customer = customerRepository.Gets().Select(c => new CustomerViewModel {
                CustomerID = c.CustomerID,
                Name = c.Name,
                Phone = c.Phone,
                Address = c.Address,
                Description = c.Description
            });
            return customer; 
        }

        public bool AddCustomer(TblCustomer customer)
        {
            if (customer != null)
            {
                customerRepository.Insert(customer);
                customerRepository.Save();
                return true;
            }
            return false;
        }
    }
}
