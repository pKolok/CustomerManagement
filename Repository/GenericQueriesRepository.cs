using CustomerManagement.Data;
using CustomerManagement.UnitOfWork;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CustomerManagement.Repository
{
    public class GenericQueriesRepository<T> 
        : IGenericQueriesRepository<T> where T : class
    {
        private IDbSet<T> _entities;

        public GenericQueriesRepository(IUnitOfWork<DBEntities> unitOfWork)
            : this(unitOfWork.Context)
        {
        }

        public GenericQueriesRepository(DBEntities _dBEntities)
        {
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

        public IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }

        public T GetById(object id)
        {
            return Entities.Find(id);
        }

        public IEnumerable<Order> GetByCustmerIDByOrderDate(int id)
        {
            var query = from order in dBEntities.Orders.AsEnumerable()
                        where order.CustomerID == id
                        orderby order.OrderDate
                        select order;

            return query;
        }
    }
}
