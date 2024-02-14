using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL;

namespace DAL.Repository
{
    public class DatabaseChequeRepositorySQL : IDatabaseRepository
    {
        private PcShopContext db;
        private InformationAboutSalesRepositorySQL SalesSQL;
        private ChequeRepositorySQL ChequeSQL;
        private ProductsRepositorySQL repositorySQL;
        private CategoryRepositorySQL CategoryRepositorySQL;
        private AuthorizationRepositorySQL authorizationRepositorySQL;
        private LegalPersonRepositorySQL legalPersonRepositorySQL;
        private PhysicalPersonRepositorySQL physicalPersonRepositorySQl;

        public DatabaseChequeRepositorySQL()
        {
            db = new PcShopContext();
        }
        public IRepository<Сategory> CategoryRepository
        {
            get
            {
                if (CategoryRepositorySQL == null)
                    CategoryRepositorySQL = new CategoryRepositorySQL(db);
                return CategoryRepositorySQL;
            }
        }

        public IProductRepository ProductsRepository
        {
            get
            {
                if (repositorySQL == null)
                    repositorySQL = new ProductsRepositorySQL(db);
                return repositorySQL;
            }
        }

        public IAuthorizationRepository AuthorizationRepository
        {
            get
            {
                if (authorizationRepositorySQL == null)
                    authorizationRepositorySQL = new AuthorizationRepositorySQL(db);
                return authorizationRepositorySQL;
            }
        }

        public IRepository<Information_about_sales> InformationAboutSales
        {
            get
            {
                if (SalesSQL == null)
                    SalesSQL = new InformationAboutSalesRepositorySQL(db);
                return SalesSQL;
            }
        }
        public ISuppliersRepository<Legal_person> LegalPersonRepository
        {
            get
            {
                if (legalPersonRepositorySQL == null)
                    legalPersonRepositorySQL = new LegalPersonRepositorySQL(db);
                return legalPersonRepositorySQL;
            }
        }
        public ISuppliersRepository<Physical_person> PhysicalPersonRepository
        {
            get
            {
                if (physicalPersonRepositorySQl == null)
                    physicalPersonRepositorySQl = new PhysicalPersonRepositorySQL(db);
                return physicalPersonRepositorySQl;
            }
        }
        public IChequeRepository Cheques
        {
            get
            {
                if (ChequeSQL == null)
                    ChequeSQL = new ChequeRepositorySQL(db);
                return ChequeSQL;
            }
        }
        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
