using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL;

namespace DAL.Repository
{
    public class ChequeRepositorySQL : IChequeRepository
    {
        private PcShopContext db;
        private class chequeResult
        {
            public string nameProduct { get; set; }
            public int salesCount { get; set; }

            public decimal salesPrice { get; set; }

            public DateTime salesDate { get; set; }

        }
        public ChequeRepositorySQL(PcShopContext pcshopContext)
        {
            this.db = pcshopContext;
        }

        public List<Information_about_sales> AnalysisSaLes(DateTime startDate, DateTime finishDate)
        {
            var info = db.Information_about_sales
                .Where(i => i.sales_date >= startDate.Date && i.sales_date <= finishDate.Date)
                .ToList();
            return info;
        }
    }
}
