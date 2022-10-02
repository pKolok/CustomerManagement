using CustomerManagement.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    class CustomerRepository : ICustomerRepository
    {
        private readonly DBEntities dBEntities;
        private bool disposed;

        public CustomerRepository()
        {
            dBEntities = new DBEntities();
        }

        public CustomerRepository(DBEntities _dBEntities)
        {
            dBEntities = _dBEntities;
        }

        IEnumerable<Customer> ICustomerRepository.GetAll()
        {
            return dBEntities.Customers.ToList();
        }

        Customer ICustomerRepository.GetById(int CustomerID)
        {
            return dBEntities.Customers.Find(CustomerID);
        }

        void ICustomerRepository.Insert(Customer customer)
        {
            dBEntities.Customers.Add(customer);
        }

        void ICustomerRepository.Update(Customer customer)
        {
            dBEntities.Entry(customer).State = EntityState.Modified;
        }

        void ICustomerRepository.Delete(int CustomerID)
        {
            Customer customer = dBEntities.Customers.Find(CustomerID);
            dBEntities.Customers.Remove(customer);
        }

        void ICustomerRepository.Save()
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
