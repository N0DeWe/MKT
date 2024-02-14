using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Repository
{
    public class CategoryRepositorySQL : IRepository<Сategory>
    {
        private PcShopContext db;
        public CategoryRepositorySQL(PcShopContext database)
        {
            this.db = database;
        }
        public List<Сategory> GetList()
        {
            return db.Сategory.ToList();
        }
        public Сategory GetItem(int id)
        {
            return db.Сategory.Find(id);
        }
        public void CreateList(List<Сategory> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                db.Сategory.Add(list[i]);
            }
            Save();
        }
        public void Create(Сategory сategory)
        {
            db.Сategory.Add(сategory);
            Save();
        }

        public void Update(Сategory сategory)
        {
            db.Entry(сategory).State = EntityState.Modified;
            Save();
        }

        public void Delete(int id)
        {
            Сategory сategory = db.Сategory.Find(id);
            if (сategory != null)
                db.Сategory.Remove(сategory);
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
