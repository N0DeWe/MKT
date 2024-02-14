using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CategoryModel
    {
        public int category_id { get; set; }
        public string category_name { get; set; }

        public CategoryModel() { }

        public CategoryModel(DAL.Сategory сategory)
        {
            category_id = сategory.category_id;
            category_name = сategory.category_name;
        }
    }
}
