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
    public class PhysicalPersonRepositorySQL : ISuppliersRepository<Physical_person>
    {
        private PcShopContext db;

        public PhysicalPersonRepositorySQL(PcShopContext pcshopContext)
        {
            this.db = pcshopContext;
        }
        public List<Physical_person> GetList()
        {
            return db.Physical_person.ToList();
        }

        public void Create(Physical_person Sales)
        {
            db.Physical_person.Add(Sales);
            Save();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
