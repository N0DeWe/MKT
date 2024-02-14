using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using System.Threading.Tasks;
using BLL;
using BLL.Interfaces;
using BLL.Methods;

namespace MKT.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IChequeService>().To<DbDataOperation>();
            Bind<IChequeCreate>().To<ChequeService>();
            Bind<IProductService>().To<ProductService>();
            Bind<IAuthorizationService>().To<AuthorizationService>();
            Bind<ISupplierService>().To<SupplierService>();
            Bind<IAnalysisSalesService>().To<AnalysisSalesService>();
        }
    }
}
