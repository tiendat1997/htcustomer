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
    
    public partial class TblCustomer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblCustomer()
        {
            this.TblTransactions = new HashSet<TblTransaction>();
        }
    
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public Nullable<long> Phone { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public Nullable<bool> Disable { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblTransaction> TblTransactions { get; set; }
    }
}
