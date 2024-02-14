using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Interfaces;
using DAL.Repository;
using BLL;

namespace BLL
{
    public class AnalysisSalesService: IAnalysisSalesService
    {
        IDatabaseRepository db;
        public AnalysisSalesService(IDatabaseRepository database)
        {
            db = database;
        }

        public List<informationAboutSalesModel> AnalisysSales(DateTime startDate, DateTime finishDate)
        {
            return db.Cheques.AnalysisSaLes(startDate, finishDate).Select(i => new informationAboutSalesModel(i)).ToList();
        }
    }
}
