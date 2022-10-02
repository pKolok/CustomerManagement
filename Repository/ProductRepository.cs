using CustomerManagement.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    class ProductRepository : IProductRepository
    {
        private readonly DBEntities dBEntities;
        private bool disposed;

        public ProductRepository()
        {
            dBEntities = new DBEntities();
        }

        public ProductRepository(DBEntities _dBEntities)
        {
            dBEntities = _dBEntities;
        }

        IEnumerable<Product> IProductRepository.GetAll()
        {
            return dBEntities.Products.ToList();
        }

        Product IProductRepository.GetById(int ProductID)
        {
            return dBEntities.Products.Find(ProductID);
        }

        void IProductRepository.Insert(Product product)
        {
            dBEntities.Products.Add(product);
        }

        void IProductRepository.Update(Product product)
        {
            dBEntities.Entry(product).State = EntityState.Modified;
        }

        void IProductRepository.Delete(int ProductID)
        {
            Product product = dBEntities.Products.Find(ProductID);
            dBEntities.Products.Remove(product);
        }

        void IProductRepository.Save()
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
