using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using System.Data.SqlClient;

namespace DAL.Repository
{
    public class ProductsRepositorySQL : IProductRepository
    {
        private PcShopContext db;
        public ProductsRepositorySQL(PcShopContext database)
        {
            this.db = database;
        }
        public List<Products> GetList()
        {
            return db.Products.ToList();
        }
        public Products GetItem(int id)
        {
            return db.Products.Find(id);
        }
        public Products Single(int productId)
        {
            return db.Products.Single(id => id.product_id == productId);
        }
        public List<Products> SqlQuery(object[] mass)
        {
            List<Products>
              list = db.Database.SqlQuery<Products>("dbo.Query @PRICE_MIN, @PRICE_MAX, @CATEGORY_FK", mass)
              .ToList();
            return list;
        }
        public void Create(Products products)
        {
            db.Products.Add(products);
            Save();
        }
        public List<Products> Filter(int category_id)
        {
            List<Сategory> allCategories = db.Сategory.ToList();
            Сategory сategory = new Сategory();
            сategory = allCategories.ElementAt(category_id - 1);
            List<Products>
                list = db.Products
                .Where(i => i.category_FK == сategory.category_id)
                .Join(db.Сategory, pr => pr.category_FK, ct => ct.category_id, (pr, ct) => pr)
                .ToList();
            return list;
        }
        public void Update(Products products)
        {
            db.Entry(products).State = EntityState.Modified;
            Save();
        }
        public void Delete(int id)
        {
            Products products = db.Products.Find(id);
            if (products != null)
                db.Products.Remove(products);
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}
