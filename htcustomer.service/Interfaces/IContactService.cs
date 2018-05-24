﻿using htcustomer.entity;
using htcustomer.service.ViewModel;
using htcustomer.service.ViewModel.Contact;
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
        AddressBookViewModel GetAddressBook(string searchValue);
        void AddCustomer(CustomerViewModel customer);
    }
    



}
