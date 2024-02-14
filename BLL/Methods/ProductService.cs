using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL;
using BLL.Interfaces;
using System.Globalization;
using System.IO;
using BLL;

namespace BLL.Methods
{
    public class ProductService : IProductService
    {
        private IDatabaseRepository db;
        public ProductService(IDatabaseRepository database)
        {
            db = database;
        }

        Information_about_sales info = new Information_about_sales();
        List<Information_about_sales> sales;
        Products products = new Products();

        public void infoAboutSales(object[,] chequeData)
        {
            DateTime salesDate = DateTime.Now;
            sales = new List<Information_about_sales>();
            var sale = sales;
            sales = db.InformationAboutSales.GetList();
            int countId = sales.Count();

            for (int i = 0; i < chequeData.GetLength(0); i++)
            {
                info = new Information_about_sales();
                for (int j = 0; j < 6; j++)
                {
                    switch (j)
                    {
                        case 0:
                            info.product_FK = (int)chequeData[i, j];
                            int id = (int)chequeData[i, j];
                            products = db.ProductsRepository.GetItem(id);
                            break;
                        case 1:
                            info.sales_date = salesDate;
                            break;
                        case 3:
                            info.sales_count = Convert.ToInt32(chequeData[i, j]);
                            products.count_of_products = products.count_of_products - (int)chequeData[i, j];
                            db.ProductsRepository.Update(products);
                            break;
                        case 4:
                            info.sales_price = Convert.ToDecimal(chequeData[i, j]);
                            break;
                        case 5:
                            info.Information_about_sales_id = countId;
                            countId = countId + 1;
                            break;
                    }
                }
                sale.Add(info);
            }
            db.InformationAboutSales.CreateList(sale);
        }
    }
}

