using CustomerManagement.Data;
using CustomerManagement.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    public class GenericQueriesRepository<T> 
        : IGenericQueriesRepository<T> where T : class
    {
        private IDbSet<T> _entities;
        //private string _errorMessage = string.Empty;
        //private bool _isDisposed;

        public GenericQueriesRepository(IUnitOfWork<DBEntities> unitOfWork)
            : this(unitOfWork.Context)
        {
        }

        public GenericQueriesRepository(DBEntities _dBEntities)
        {
            //_isDisposed = false;
            dBEntities = _dBEntities;
        }

        public DBEntities dBEntities { get; set; }

        public virtual IQueryable<T> Table
        {
            get { return Entities; }
        }

        protected virtual IDbSet<T> Entities
        {
            get { return _entities ?? (_entities = dBEntities.Set<T>()); }
        }

        //public void Dispose()
        //{
        //    if (dBEntities != null)
        //        dBEntities.Dispose();
        //    //_isDisposed = true;
        //}

        public IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }

        public T GetById(object id)
        {
            int count = Entities.ToList().Count();   // TODO temp

            return Entities.Find(id);
        }

    }
}
