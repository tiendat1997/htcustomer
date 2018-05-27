using htcustomer.entity;
using htcustomer.repository;
using htcustomer.service.Enums;
using htcustomer.service.Interfaces;
using htcustomer.service.ViewModel;
using htcustomer.service.ViewModel.Contact;
using htcustomer.service.ViewModel.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.Implements
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<TblTransaction> transactionRepository;
        private readonly IRepository<TblCustomer> customerRepository;

        public TransactionService(
            IRepository<TblTransaction> _transactionRepository,
            IRepository<TblCustomer> _customerRepository)
        {
            transactionRepository = _transactionRepository;
            customerRepository = _customerRepository;
        }
        public ContactDetailsViewModel GetContactDetails(int customerID)
        {
            ContactDetailsViewModel model = new ContactDetailsViewModel
            {
                Customer = customerRepository.Gets()
                            .Where(c => c.CustomerID == customerID && c.Disable != true)
                            .Select(c => new CustomerViewModel
                            {
                                CustomerID = c.CustomerID,
                                Name = c.Name,
                                Address = c.Address,
                                Description = c.Description,
                                Disable = c.Disable,
                                Phone = c.Phone
                            }).FirstOrDefault(),
                Transactions = transactionRepository.Gets().Where(t => t.CustomerID == customerID)
                            .Select(t => new TransactionViewModel
                            {
                                TransactionID = t.TransactionID,
                                Status = (TransactionStatus)t.StatusID,
                                RecieveDate = t.RecievedDate,
                                DeliveredDate = t.DeliverDate,
                                Category = t.TblCategory.Name,
                                Description = t.Description,
                                Error = t.Error,
                                Delivered = t.Delivered,
                                Price = t.Price,
                                Reason = t.Reason
                            }),
            };
            return model;
        }     
    }
}
