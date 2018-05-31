using htcustomer.entity;
using htcustomer.repository;
using htcustomer.service.Enums;
using htcustomer.service.Interfaces;
using htcustomer.service.ViewModel;
using htcustomer.service.ViewModel.Category;
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
        private readonly IRepository<TblCategory> categoryRepository;
        private readonly IRepository<TblDetailPrice> priceDetailRepository;


        public TransactionService(
            IRepository<TblTransaction> _transactionRepository,
            IRepository<TblCustomer> _customerRepository,
            IRepository<TblCategory> _categoryRepository,
            IRepository<TblDetailPrice> _priceDetailRepository)
        {
            transactionRepository = _transactionRepository;
            customerRepository = _customerRepository;
            categoryRepository = _categoryRepository;
            priceDetailRepository = _priceDetailRepository;
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
                                RecievedDate = t.RecievedDate,
                                DeliveredDate = t.DeliverDate,
                                Category = categoryRepository.Gets().Where(c => c.CategoryID == t.TypeID).Select(c => new CategoryViewModel()
                                {
                                    CategoryID = c.CategoryID,
                                    Name = c.Name
                                }).SingleOrDefault(),
                                Description = t.Description,
                                Error = t.Error,
                                Delivered = t.Delivered,
                                Price = t.Price,
                                Reason = t.Reason
                            }),
            };
            return model;
        }

        public IEnumerable<TransactionViewModel> GetListTransaction(TransactionStatus status, int? month = null, int? year = null, int? categoryId = null)
        {
            var transactions = transactionRepository.Gets()
                                                    .Where(t => (TransactionStatus)t.StatusID.Value == status)
                                                    ; 
            if (month != null && year != null)
            {
               transactions = transactions.Where(t => t.RecievedDate.Value.Month == month && t.RecievedDate.Value.Year == year);
            }
            else 
            {
                transactions = transactions.Where(t => t.RecievedDate.Value.Month == DateTime.Now.Month && t.RecievedDate.Value.Year == DateTime.Now.Year);

            }

            if (categoryId != null)
            {
                transactions = transactions.Where(t => t.TypeID == categoryId);
            }


            return transactions.Select(t => new TransactionViewModel()
            {
                TransactionID = t.TransactionID,
                DeliveredDate = t.DeliverDate,
                Description = t.Description,
                Error = t.Error,
                Price = t.Price,
                Reason = t.Reason,
                Delivered = t.Delivered,
                RecievedDate = t.RecievedDate,
                Status = (TransactionStatus)t.StatusID,
                Category = categoryRepository.Gets().Where(c => c.CategoryID == t.TypeID).Select(c => new CategoryViewModel()
                {
                    CategoryID = c.CategoryID,
                    Name = c.Name
                }).SingleOrDefault(),
                Customer = customerRepository.Gets().Where(c => c.CustomerID == t.CustomerID).Select(c => new CustomerViewModel()
                {
                    CustomerID = c.CustomerID,
                    Name = c.Name,
                    Phone = c.Phone
                }).SingleOrDefault(),
                ListPriceDetail = priceDetailRepository.Gets().Where(p => p.TransactionID == t.TransactionID).Select(p => new PriceDetailViewModel()
                {
                    TransactionID = p.TransactionID.Value,
                    Description = p.Description,
                    Price = p.Price.Value
                })
            }).ToList();
        }

        public TransactionListHomeViewModel GetListTransactionHome()
        {
            var transactions = transactionRepository.Gets()
                                                    //.Where(t => t.RecievedDate.Value.Month == DateTime.Now.Month && t.RecievedDate.Value.Year == DateTime.Now.Year
                                                    //              && !t.Delivered.Value)
                                                    .Where(t => t.Delivered.HasValue ? t.Delivered.Value == true : false)
                                                    .Select(t => new TransactionHomeViewModel()
                                                    {
                                                        TransactionID = t.TransactionID,
                                                        Category = categoryRepository.Gets().Where(c => c.CategoryID == t.TypeID).Select(c => new CategoryViewModel()
                                                        {
                                                            CategoryID = c.CategoryID,
                                                            Name = c.Name
                                                        }).SingleOrDefault(),
                                                        Customer = customerRepository.Gets().Where(c => c.CustomerID == t.CustomerID).Select(c => new CustomerViewModel()
                                                        {
                                                            CustomerID = c.CustomerID,
                                                            Name = c.Name,
                                                            Phone = c.Phone
                                                        }).SingleOrDefault(),
                                                        DeviceDescription = t.Description,
                                                        Price = t.Price.Value,
                                                        Error = t.Error,
                                                        Status = (TransactionStatus)t.StatusID.Value,
                                                        CannotFixNote = t.Reason,
                                                        ListPriceDetail = priceDetailRepository.Gets().Where(p => p.TransactionID == t.TransactionID).Select(p => new PriceDetailViewModel()
                                                        {
                                                            TransactionID = p.TransactionID.Value,
                                                            Description = p.Description,
                                                            Price = p.Price.Value
                                                        })
                                                    });
                                                    
            return new TransactionListHomeViewModel()
            {
                CannotFixTransactions = transactions.Where(t => t.Status == TransactionStatus.CannotFix).ToList(),
                FixedTransactions = transactions.Where(t => t.Status == TransactionStatus.Fixed).ToList(),
                NotFixTransactions = transactions.Where(t => t.Status == TransactionStatus.NotFix).ToList()
            };
        }
    }
}
