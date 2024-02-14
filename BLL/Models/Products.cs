using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class ProductsModel
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string technical_specifications { get; set; }
        public int count_of_products { get; set; }
        public decimal product_price { get; set; }
        public double? discount { get; set; }
        public int category_FK { get; set; }
        public object Сategory { get; set; }

        public ProductsModel()
        {
        }

        public ProductsModel(Products products)
        {
            product_id = products.product_id;
            product_name = products.product_name;
            technical_specifications = products.technical_specifications;
            count_of_products = products.count_of_products;
            product_price = products.product_price;
            discount = products.discount;
            category_FK = products.category_FK;
        }
    }
}
