using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    public interface IGenericCommandsRepository<T> where T : class
    {
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
