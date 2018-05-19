using htcustomer.entity;
using htcustomer.repository;
using htcustomer.service.Enums;
using htcustomer.service.Interfaces;
using htcustomer.service.ViewModel;
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

        public TransactionService(IRepository<TblTransaction> _transactionRepository)
        {
            transactionRepository = _transactionRepository;
        }

        public IEnumerable<CustomerTransactionViewModel> GetCustomerTransaction(int customerID)
        {
            return transactionRepository.Gets()
                .Where(t => t.CustomerID == customerID)
                .Select(t => new CustomerTransactionViewModel() {
                    TransactionID = t.TransactionID,
                    Status = (TransactionStatus)t.StatusID,
                    DeliveredDate = t.DeliverDate,
                    Device = t.Device,
                    Error = t.Error,
                    Delivered = t.Delivered
                });
        }
    }
}
