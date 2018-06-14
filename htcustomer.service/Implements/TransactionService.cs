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

        public bool Add(TransactionViewModel transaction)
        {
            if (transaction != null)
            {
                transactionRepository.Insert(new TblTransaction() {
                    CustomerID = transaction.Customer.CustomerID,
                    Delivered = false,
                    Description = transaction.Description,
                    Error = transaction.Error,
                    RecievedDate = DateTime.Now,
                    StatusID = (int)TransactionStatus.NotFix,
                    TypeID = transaction.Category.CategoryID
                });
                transactionRepository.Save();
                return true;
            }
            return false;
        }

        public bool CannotFixTransaction(int transactionID, string reason = "")
        {
            var transaction = transactionRepository.GetByID(transactionID);
            if (transaction != null)
            {
                transaction.StatusID = (int)TransactionStatus.CannotFix;
                transaction.Reason = reason;
                transactionRepository.Edit(transaction);
                transactionRepository.Save();
                return true;
            }
            return false;
        }

        public bool DeliverTransaction(int transactionID)
        {
            var transaction = transactionRepository.GetByID(transactionID);
            if (transaction != null)
            {
                transaction.Delivered = true;
                transaction.DeliverDate = DateTime.Now;
                transactionRepository.Edit(transaction);
                transactionRepository.Save();
                return true;
            }
            return false;
        }

        public bool FixedTransaction(int transactionID, IEnumerable<PriceDetailViewModel> priceDetails)
        {
            var transaction = transactionRepository.GetByID(transactionID);
            if (transaction != null)
            {
                if (priceDetails != null || priceDetails.Count() > 0)
                {
                    foreach (var priceDetail in priceDetails)
                    {
                        priceDetailRepository.Insert(new TblDetailPrice()
                        {
                            Description = priceDetail.Description,
                            Price = priceDetail.Price,
                            TransactionID = priceDetail.TransactionID
                        });
                        transaction.Price = priceDetails.Sum(x => x.Price);
                    }
                }
                transaction.StatusID = (int)TransactionStatus.Fixed;
                transactionRepository.Edit(transaction);
                transactionRepository.Save();
                return true;
            }
            
            return false;
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

        public TransactionListViewModel GetListTransaction(TransactionStatus? status = null, int? month = null, int? year = null, int? categoryId = null)
        {
            var transactions = transactionRepository.Gets();
            if(status != null)
            {
                if(status != TransactionStatus.Delivered)
                {
                    transactions = transactions.Where(t => t.StatusID == (int)status && t.Delivered == false);
                }
                else
                {
                    transactions = transactions.Where(t => t.Delivered == true);
                }
            }
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
            
            var result = new TransactionListViewModel
            {
                Status = status,
                Transactions = transactions.Select(t => new TransactionViewModel()
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
                    Category = new CategoryViewModel
                    {
                        CategoryID = t.TblCategory.CategoryID,
                        Name = t.TblCategory.Name
                    },
                    Customer = new CustomerViewModel
                    {
                        CustomerID = t.TblCustomer.CustomerID,
                        Name = t.TblCustomer.Name,
                        Phone = t.TblCustomer.Phone
                    },
                    ListPriceDetail = t.TblDetailPrices.Select(p => new PriceDetailViewModel()
                    {
                        TransactionID = p.TransactionID.Value,
                        Description = p.Description,
                        Price = p.Price.Value
                    })
                })
            };

            return result;
        }

        public TransactionListHomeViewModel GetListTransactionHome()
        {
            var transactions = transactionRepository.Gets()
                                                    //.Where(t => t.RecievedDate.Value.Month == DateTime.Now.Month && t.RecievedDate.Value.Year == DateTime.Now.Year
                                                    //              && !t.Delivered.Value)
                                                    .Where(t => t.Delivered.HasValue ? t.Delivered.Value == false : true)
                                                    .Select(t => new TransactionHomeViewModel()
                                                    {
                                                        TransactionID = t.TransactionID,
                                                        Category = new CategoryViewModel()
                                                        {
                                                            CategoryID = t.TblCategory.CategoryID,
                                                            Name = t.TblCategory.Name
                                                        },
                                                        Customer = new CustomerViewModel()
                                                        {
                                                            CustomerID = t.TblCustomer.CustomerID,
                                                            Name = t.TblCustomer.Name,
                                                            Phone = t.TblCustomer.Phone
                                                        },
                                                        DeviceDescription = t.Description != null ? t.Description : "No description for this device",
                                                        Price = t.Price != null ? t.Price.Value : 0,
                                                        Error = t.Error != null ? t.Error : "",
                                                        Status = (TransactionStatus)t.StatusID.Value,
                                                        CannotFixNote = t.Reason != null ? t.Reason : "No reason for this transaction",
                                                        ListPriceDetail = t.TblDetailPrices.Select(p => new PriceDetailViewModel()
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
