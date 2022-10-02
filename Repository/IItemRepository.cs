using CustomerManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    interface IItemRepository
    {
        IEnumerable<Item> GetAll();
        Item GetById(int ItemID);
        void Insert(Item item);
        void Update(Item item);
        void Delete(int ItemID);
        void Save();
    }
}
