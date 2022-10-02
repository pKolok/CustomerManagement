using CustomerManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order GetById(int OrderID);
        void Insert(Order order);
        void Update(Order order);
        void Delete(int OrderID);
        void Save();
    }
}
