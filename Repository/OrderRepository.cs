using CustomerManagement.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    class OrderRepository : IOrderRepository
    {
        private readonly DBEntities dBEntities;
        private bool disposed;

        public OrderRepository()
        {
            dBEntities = new DBEntities();
        }

        public OrderRepository(DBEntities _dBEntities)
        {
            dBEntities = _dBEntities;
        }

        IEnumerable<Order> IOrderRepository.GetAll()
        {
            return dBEntities.Orders.ToList();
        }

        Order IOrderRepository.GetById(int OrderID)
        {
            return dBEntities.Orders.Find(OrderID);
        }

        void IOrderRepository.Insert(Order order)
        {
            dBEntities.Orders.Add(order);
        }

        void IOrderRepository.Update(Order order)
        {
            dBEntities.Entry(order).State = EntityState.Modified;
        }

        void IOrderRepository.Delete(int OrderID)
        {
            Order order = dBEntities.Orders.Find(OrderID);
            dBEntities.Orders.Remove(order);
        }

        void IOrderRepository.Save()
        {
            dBEntities.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dBEntities.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
