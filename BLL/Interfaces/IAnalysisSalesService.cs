using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAnalysisSalesService
    {
        List<informationAboutSalesModel> AnalisysSales(DateTime startDate, DateTime finishDate);
    }
}
