using htcustomer.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.repository.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private HtContext dbContext;
        public UnitOfWork(HtContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public int Save()
        {
            return dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbContext != null)
                {
                    dbContext.Dispose();
                    dbContext = null; 
                }
            }
        }
    }
}
