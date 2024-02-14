using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.Entity;
using DAL.Interfaces;

namespace DAL.Repository
{
    public class LegalPersonRepositorySQL : ISuppliersRepository<Legal_person>
    {
        private PcShopContext db;

        public LegalPersonRepositorySQL(PcShopContext pcshopContext)
        {
            this.db = pcshopContext;
        }
        public List<Legal_person> GetList()
        {
            return db.Legal_person.ToList();
        }
        public void Create(Legal_person Sales)
        {
            db.Legal_person.Add(Sales);
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
