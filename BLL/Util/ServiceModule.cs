using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Repository;
using Ninject.Modules;
using System.Threading.Tasks;

namespace BLL
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IDatabaseRepository>().To<DatabaseChequeRepositorySQL>().InSingletonScope();
        }
    }
}
