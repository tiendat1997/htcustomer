using htcustomer.entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace htcustomer.repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly HuyThongDiaryDBEntities _context;
        private readonly IDbSet<T> dbEntity;

        public Repository()
        {
            _context = new HuyThongDiaryDBEntities();
            dbEntity = _context.Set<T>();
        }

        public void Delete(int id)
        {
            T model = dbEntity.Find(id);
            dbEntity.Remove(model);
        }

        public T GetByID(int id)
        {
            return dbEntity.Find(id);
        }

        public IEnumerable<T> Gets()
        {
            return dbEntity.ToList();
        }

        public void Insert(T model)
        {
            dbEntity.Add(model);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Edit(T model)
        {
            _context.Entry<T>(model).State = EntityState.Modified;
        }
    }
}
