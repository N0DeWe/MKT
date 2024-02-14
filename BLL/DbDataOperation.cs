using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Interfaces;
using System.Data.Entity;
using System.ComponentModel;
using BLL.Interfaces;
using System.Collections.ObjectModel;

namespace BLL
{
    public class DbDataOperation : IChequeService
    {
        IDatabaseRepository db;
        public DbDataOperation(IDatabaseRepository database)
        {
            db = database;
        }

        #region Products
        public List<ProductsModel> GetAllProducts()
        {
            return db.ProductsRepository.GetList().Select(i => new ProductsModel(i)).ToList();
        }
        public ProductsModel GetProduct(int id)
        {
            return new ProductsModel(db.ProductsRepository.GetItem(id));
        }

        public void DeleteProducts(int id)
        {
            Products pr = db.ProductsRepository.GetItem(id);
            if (pr != null)
            {
                db.ProductsRepository.Delete(pr.product_id);
                Save();
            }
        }
        public void cancleProduct(int productId, int countOfProducts)
        {
            db.ProductsRepository.Single(productId).count_of_products = countOfProducts;
            Save();
        }
        public List<ProductsModel> Filter(int category_id)
        {
            var list = db.ProductsRepository.Filter(category_id)
                .Select(i => new ProductsModel()
                {
                    product_id = i.product_id,
                    product_name = i.product_name,
                    technical_specifications = i.technical_specifications,
                    count_of_products = i.count_of_products,
                    discount = i.discount,
                    product_price = i.product_price,
                    category_FK = i.category_FK
                })
                .ToList();
            return list;
        }
        public List<ProductsModel> querySQl(object[] mass)
        {

            var list = db.ProductsRepository.SqlQuery(mass)
                .Select(i => new ProductsModel()
                {
                    product_id = i.product_id,
                    product_name = i.product_name,
                    technical_specifications = i.technical_specifications,
                    count_of_products = i.count_of_products,
                    discount = i.discount,
                    product_price = i.product_price,
                    category_FK = i.category_FK
                })
                .ToList();
            return list;
        }
        public void UpdateProduct(ProductsModel productsmodel)
        {
            Products pr = db.ProductsRepository.GetItem(productsmodel.product_id);
            pr.product_name = productsmodel.product_name;
            pr.product_price = productsmodel.product_price;
            pr.technical_specifications = productsmodel.technical_specifications;
            pr.count_of_products = productsmodel.count_of_products;
            pr.category_FK = productsmodel.category_FK;
            pr.discount = productsmodel.discount;
        }
        public void AddProduct(ProductsModel productsModel)
        {
            db.ProductsRepository.Create(new Products()
            {
                product_id = productsModel.product_id,
                product_name = productsModel.product_name,
                product_price = productsModel.product_price,
                technical_specifications = productsModel.technical_specifications,
                count_of_products = productsModel.count_of_products,
                category_FK = productsModel.category_FK
            });
            Save();
        }
        #endregion

        public List<CategoryModel> GetCategories()
        {
            return db.CategoryRepository.GetList().Select(i => new CategoryModel(i)).ToList();
        }
        public List<informationAboutSalesModel> GetInfoAboutSales()
        {
            return db.InformationAboutSales.GetList().Select(i => new informationAboutSalesModel(i)).ToList();
        }
        public void updateDataBase(object[,] chequeData)
        {
            ProductsModel productsModel = new ProductsModel();
            for (int i = 0; i < chequeData.Length / 7 - 1; i++)
            {
                productsModel.product_id = Convert.ToInt32(chequeData[i, 0]);
                db.ProductsRepository.Single(productsModel.product_id).count_of_products -= (int)chequeData[i, 3];
            }
            Save();
        }
        public int Savebd()
        {
            return db.Save();
        }
        public bool Save()
        {
            if (db.Save() > 0) return true;
            return false;
        }
    }

}

