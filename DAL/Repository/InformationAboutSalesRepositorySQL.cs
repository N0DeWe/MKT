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
    public class InformationAboutSalesRepositorySQL : IRepository<Information_about_sales>
    {
        private PcShopContext db;

        public InformationAboutSalesRepositorySQL(PcShopContext pcshopContext)
        {
            this.db = pcshopContext;
        }
        public List<Information_about_sales> GetList()
        {
            return db.Information_about_sales.ToList();
        }
        public Information_about_sales GetItem(int id)
        {
            return db.Information_about_sales.Find(id);
        }
        public void CreateList(List<Information_about_sales> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                db.Information_about_sales.Add(list[i]);
            }
            Save();

        }
        public void Create(Information_about_sales Sales)
        {
            db.Information_about_sales.Add(Sales);
            Save();
        }

        public void Update(Information_about_sales Sales)
        {
            db.Entry(Sales).State = EntityState.Modified;
            Save();
        }

        public void Delete(int id)
        {
            Information_about_sales Sales = db.Information_about_sales.Find(id);
            if (Sales != null)
                db.Information_about_sales.Remove(Sales);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
