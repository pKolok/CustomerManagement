using CustomerManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(int CustomerID);
        void Insert(Customer customer);
        void Update(Customer customer);
        void Delete(int CustomerID);
        void Save();
    }
}
