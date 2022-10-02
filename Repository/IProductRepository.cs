using CustomerManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int ProductID);
        void Insert(Product product);
        void Update(Product product);
        void Delete(int ProductID);
        void Save();
    }
}
