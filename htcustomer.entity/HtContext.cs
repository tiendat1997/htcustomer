namespace htcustomer.entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HtContext : DbContext
    {
        public HtContext()
            : base("name=HtModel")
        {
        }

        public virtual DbSet<TblAccount> TblAccounts { get; set; }
        public virtual DbSet<TblCategory> TblCategories { get; set; }
        public virtual DbSet<TblCustomer> TblCustomers { get; set; }
        public virtual DbSet<TblDetailPrice> TblDetailPrices { get; set; }
        public virtual DbSet<TblProduct> TblProducts { get; set; }
        public virtual DbSet<TblProductType> TblProductTypes { get; set; }
        public virtual DbSet<TblToDo> TblToDoes { get; set; }
        public virtual DbSet<TblTransaction> TblTransactions { get; set; }
        public virtual DbSet<TblTransactionStatu> TblTransactionStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAccount>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<TblAccount>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<TblCategory>()
                .HasMany(e => e.TblTransactions)
                .WithOptional(e => e.TblCategory)
                .HasForeignKey(e => e.TypeID);

            modelBuilder.Entity<TblCustomer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<TblProduct>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<TblProductType>()
                .HasMany(e => e.TblProducts)
                .WithOptional(e => e.TblProductType)
                .HasForeignKey(e => e.Type);
        }
    }
}
