using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISuppliersRepository<T> where T : class
    {
        List<T> GetList();
        void Create(T item);
        void Save();
    }
}
