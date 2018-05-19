using htcustomer.repository;
using htcustomer.service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using htcustomer.entity;
using htcustomer.service.ViewModel;
using htcustomer.service.ViewModel.Contact;

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
        public AddressBookViewModel GetAddressBook(string searchValue)
        {
            var customerList = customerRepository.Gets()
                .Where(e => (searchValue == null) || ((searchValue != null) && ((e.Name + " " + e.Description).ToUpper().Contains(searchValue.ToUpper()))) &&
                            (e.Disable == true || e.Disable == null))
                .Select(c => new CustomerViewModel
                {
                    CustomerID = c.CustomerID,
                    Name = c.Name,
                    Phone = c.Phone,
                    Address = c.Address,
                    Description = c.Description,                   
                    Disable = c.Disable
                })
                .OrderBy(c => c.Name)
                .ThenBy(c => c.Description)
                .Take(20);

            var addressBook = new AddressBookViewModel { customerList = customerList };
            return addressBook;
        }

        public bool AddCustomer(TblCustomer customer)
        {
            throw new NotImplementedException();
        }
    }
}
