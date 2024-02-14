using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{

    public class informationAboutSalesModel
    {

        public int sales_count { get; set; }

        public decimal sales_price { get; set; }

        public DateTime sales_date { get; set; }

        public int product_FK { get; set; }
        public informationAboutSalesModel() { }
        public informationAboutSalesModel(Information_about_sales sales)
        {
            sales_count = sales.sales_count;
            sales_price = sales.sales_price;
            sales_date = sales.sales_date;
            product_FK = sales.product_FK;
        }
    }
}
