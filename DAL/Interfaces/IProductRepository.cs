using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProductRepository
    {
        List<Products> SqlQuery(object[] mass);
        Products Single(int productId);
        List<Products> Filter(int category_id);
        List<Products> GetList();
        Products GetItem(int id);
        void Create(Products item);
        void Update(Products item);
        void Delete(int id);
        void Save();
    }
}
