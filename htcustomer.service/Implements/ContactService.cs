using htcustomer.repository;
using htcustomer.service.Interfaces;
using htcustomer.entity;
using System.Collections.Generic;
using System.Linq;
using htcustomer.service.ViewModel;
using htcustomer.service.ViewModel.Contact;
using System;
using htcustomer.repository.UnitOfWork;

namespace htcustomer.service.Implements
{
    public class ContactService : IContactService
    {
        private readonly IRepository<TblCustomer> customerRepository;
        private readonly IUnitOfWork unitOfWork;
        public ContactService(IUnitOfWork _unitOfWork,IRepository<TblCustomer> _customerRepository)
        {
            unitOfWork = _unitOfWork;
            customerRepository = _customerRepository;
        }        
        public IEnumerable<CustomerViewModel> GetAllCustomer()
        {
            var customer = customerRepository.Gets().Select(c => new CustomerViewModel
            {
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
                .Where(e => (searchValue == null) || ((searchValue != null) && ((e.Name + " " + e.Description).ToUpper().Contains(searchValue.ToUpper()))))
                .Where(e => e.Disable != true)
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

        public void AddCustomer(CustomerViewModel customer)
        {
            if (customer == null) throw new ArgumentNullException("Null Argument");
            customer.Disable = false;
            customerRepository.Insert(new TblCustomer
            {
                Name = customer.Name,
                Description = customer.Description,
                Phone = customer.Phone,
                Address = customer.Address,
                Disable = false
            });
            unitOfWork.Save();
        }

        public void DisableCustomer(int customerID)
        {
            var customer = customerRepository.GetByID(customerID);
            customer.Disable = true;
            customerRepository.Edit(new TblCustomer()
            {
                CustomerID = customer.CustomerID,
                Name = customer.Name,
                Description = customer.Description,
                Phone = customer.Phone,
                Address = customer.Address,
                Disable = customer.Disable
            });
            unitOfWork.Save();
        }

        public void UpdateCustomer(CustomerViewModel customer)
        {
            customerRepository.Edit(new TblCustomer()
            {
                CustomerID = customer.CustomerID,
                Name = customer.Name,
                Description = customer.Description,
                Phone = customer.Phone,
                Address = customer.Address,
                Disable = customer.Disable
            });
            unitOfWork.Save();
        }
    }
}
