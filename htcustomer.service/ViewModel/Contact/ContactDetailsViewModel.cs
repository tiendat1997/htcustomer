using htcustomer.service.Enums;
using htcustomer.service.ViewModel.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.ViewModel.Contact
{
    public class ContactDetailsViewModel
    {
        public CustomerViewModel Customer { get; set; }
        public IEnumerable<TransactionViewModel> Transactions { get; set; }
        public Dictionary<string, IEnumerable<TransactionViewModel>> WaitingDeliver
        {
            get
            {
                var waitingQueue = new Dictionary<string, IEnumerable<TransactionViewModel>>();
                var fixedTransaction = Transactions.Where(t => (t.Delivered == false) && (t.Status == TransactionStatus.Fixed)).ToList();
                var cannotFixTransaction = Transactions.Where(t => (t.Delivered == false) && (t.Status == TransactionStatus.CannotFix)).ToList();
                waitingQueue.Add("FIXED", fixedTransaction);
                waitingQueue.Add("CANNOTFIX",cannotFixTransaction);

                return waitingQueue;    
            }            
        }       
        public IEnumerable<TransactionViewModel> NotFixTransaction
        {
            get
            {
                return Transactions.Where(t => (t.Delivered == false) && (t.Status == TransactionStatus.NotFix));
            }
        }
        public IEnumerable<TransactionViewModel> DeliveredTransaction
        {
            get
            {
                return Transactions.Where(t => (t.Delivered == true));
            }
        }
    }
}
