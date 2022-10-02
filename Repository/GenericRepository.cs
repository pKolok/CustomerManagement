using CustomerManagement.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DBEntities dbEntities = null;
        private DbSet<T> table = null;

        public GenericRepository()
        {
            this.dbEntities = new DBEntities();
            table = dbEntities.Set<T>();
        }

        public GenericRepository(DBEntities _context)
        {
            this.dbEntities = _context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            dbEntities.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Save()
        {
            dbEntities.SaveChanges();
        }
    }
}
