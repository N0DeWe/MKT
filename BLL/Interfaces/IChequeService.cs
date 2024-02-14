using System.Collections.Generic;


namespace BLL.Interfaces
{
    public interface IChequeService
    {
        List<ProductsModel> GetAllProducts();
        List<CategoryModel> GetCategories();
        List<ProductsModel> Filter(int category_id);
        ProductsModel GetProduct(int productId);
        void AddProduct(ProductsModel productsModel);
        int Savebd();
        void DeleteProducts(int id);
        List<ProductsModel> querySQl(object[] mass);
        void cancleProduct(int productId, int countOfProducts);
        void UpdateProduct(ProductsModel productsmodel);


    }
}
