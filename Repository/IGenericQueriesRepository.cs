using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    public interface IGenericQueriesRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
    }
}
