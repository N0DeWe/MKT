using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{

    public  class InformationAboutSuppliersModel
    {
        public int Information_about_supplie_id { get; set; }

        public int supplies_count { get; set; }

        public decimal supplies_price { get; set; }
        public DateTime supplies_date { get; set; }
        public int supplier_FK { get; set; }
        public int product_FK { get; set; }
        public InformationAboutSuppliersModel() { }

        public InformationAboutSuppliersModel(Information_about_suppliers informationAboutSuppliers)
        {
            Information_about_supplie_id = informationAboutSuppliers.Information_about_supplie_id;
            supplies_count = informationAboutSuppliers.supplies_count;
            supplies_price = informationAboutSuppliers.supplies_price;
            supplies_date = informationAboutSuppliers.supplies_date;
            supplier_FK = informationAboutSuppliers.supplier_FK;
            product_FK = informationAboutSuppliers.product_FK;
        }

    }
}
