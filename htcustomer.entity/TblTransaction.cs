//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace htcustomer.entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblTransaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblTransaction()
        {
            this.TblDetailPrices = new HashSet<TblDetailPrice>();
        }
    
        public int TransactionID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<System.DateTime> RecievedDate { get; set; }
        public Nullable<System.DateTime> DeliverDate { get; set; }
        public Nullable<int> TypeID { get; set; }
    
        public virtual TblCategory TblCategory { get; set; }
        public virtual TblCustomer TblCustomer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblDetailPrice> TblDetailPrices { get; set; }
        public virtual TblTransactionStatu TblTransactionStatu { get; set; }
    }
}
