using htcustomer.entity;
using htcustomer.service.ViewModel.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.ViewModel.Device
{
    public class PriceTransactionViewModel
    {
        public int TransactionID { get; set; }
        public IEnumerable<PriceDetailViewModel> PriceList { get; set; }
        //public static readonly Expression<Func<TblTransaction, PriceTransactionViewModel>> LamdaConstructor
        //    = tran => new PriceTransactionViewModel
        //    {
        //        TransactionID = tran.TransactionID,
        //        PriceList = tran.TblDetailPrices.Select(item => new PriceDetailViewModel { Price = item.Price ?? 0, Description = item.Description })
        //    };

    }
}
