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
    public class ChequeService : IChequeCreate
    {
        private IDatabaseRepository db;
        public ChequeService(IDatabaseRepository database)
        {
            db = database;
        }

        List<Information_about_sales> sales;
        Information_about_sales info = new Information_about_sales();

        public void printCheque(object[,] chequeData)
        {
            sales = db.InformationAboutSales.GetList();
            DateTime salesDate = DateTime.Now;
            double[] discount = new double[chequeData.Length / 7];
            string[] lines = new string[5];
            double price = 0.0;
            string s1 = "Товар: ";
            string s2 = "Количество: ";
            string s3 = "Цена: ";
            string s4 = "Дата: ";
            string path = "../../../BLL/Cheque/";
            string[] AllFiles = Directory.GetFiles(path, ".", SearchOption.AllDirectories);
            int count = AllFiles.Count() +1;

            if (AllFiles.Count() != 0)
            {
                path = path.Insert(path.Length, "Cheque");
                path = path.Insert(path.Length, Convert.ToString(count));
                path = path.Insert(path.Length, ".txt");

            }
            else
            {
                path = path.Insert(path.Length, "Cheque1.txt");
            }

            for (int i = 0; i < chequeData.Length / 7 ; i++)
            {
                discount[i] = Convert.ToDouble(chequeData[i, 5]);
                info.sales_count = Convert.ToInt32(chequeData[i, 3]);
                info.sales_date = salesDate;
                info.sales_price = Convert.ToDecimal(chequeData[i, 4]);
                info.product_FK = Convert.ToInt32(chequeData[i, 0]);

                Products product = db.ProductsRepository.GetItem(info.product_FK);
                var productsSales = sales
                .Where(info => info.product_FK == product.product_id).ToList();
                lines[0] = s1.Insert(s1.Length, Convert.ToString(product.product_name));
                lines[1] = s2.Insert(s2.Length, Convert.ToString(info.sales_count));
                lines[2] = s3.Insert(s3.Length, Convert.ToString(info.sales_price));
                lines[3] = s4.Insert(s4.Length, Convert.ToString(info.sales_date));
                lines[4] = "";

                if(discount[i] != 0)
                {
                    price += (Convert.ToDouble(info.sales_price) - ((Convert.ToDouble(info.sales_price) * discount[i] )/ 100)) * info.sales_count;
                }
                else
                {
                    price += Convert.ToDouble(info.sales_price * info.sales_count);
                }
                File.AppendAllLines(path, lines);
            }
                File.AppendAllText(path, $"Сумма: {price} рублей"); 

        }
    }
}
