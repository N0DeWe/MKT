using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL;
using BLL.Interfaces;
using System.Collections.ObjectModel;

namespace MKT
{
    /// <summary>
    /// Логика взаимодействия для PurchaseWindow.xaml
    /// </summary>
    public partial class PurchaseWindow : Window
    {
        IChequeService chequeService;
        IChequeCreate chequeCreate;
        IProductService productService;


        List<CategoryModel> allCategory;
        List<ProductsModel> allProducts;
        public ObservableCollection<ProductsModel> selectedProducts { get; set; }
        public object[] mass = new object[1];

        public PurchaseWindow(IChequeService service, IChequeCreate cheque, IProductService productSer)
        {
            chequeService = service;
            chequeCreate = cheque;
            productService = productSer;

            InitializeComponent();
            selectedProducts = new ObservableCollection<ProductsModel>();

            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2.0;
            this.Left = (screenWidth - this.Width) / 2.0;
            loadData();
        }

        private void cancleButtonClick(object sender, RoutedEventArgs e)
        {
            object product = 0;
            int index = 0;
            if (dataGrid.SelectedItems.Count > 0)
            {
                product = dataGrid.SelectedItem;
            }
            else
            {
                MessageBox.Show("Вы не выбрали товар");
                return;
            }
            index = dataGrid.SelectedIndex;
            deletePurchase(product, index);
        }

        private void AddProductButtonClick(object sender, RoutedEventArgs e)
        {
            string[] item = new string[7];
            AddProductWindow addProductWindow = new AddProductWindow(chequeService, selectedProducts.ToList());
            addProductWindow.ShowDialog();
            if ((addProductWindow.selectedProduct != null) && (addProductWindow.selectedProduct.count_of_products != 0))
            {
                ProductsModel productsModel = new ProductsModel();
                productsModel.product_id = addProductWindow.selectedProduct.product_id;
                productsModel.product_name = addProductWindow.selectedProduct.product_name;
                productsModel.technical_specifications = addProductWindow.selectedProduct.technical_specifications;
                productsModel.count_of_products = addProductWindow.selectedProduct.count_of_products;
                productsModel.product_price = addProductWindow.selectedProduct.product_price;
                productsModel.discount = addProductWindow.selectedProduct.discount;
                productsModel.category_FK = addProductWindow.selectedProduct.category_FK;
                selectedProducts.Add(productsModel);
                dataGrid.ItemsSource = selectedProducts;
            }
        }

        private void PrintChequeButtonClick(object sender, RoutedEventArgs e)
        {
            if(dataGrid.Items.Count == 0)
            {
                MessageBox.Show("Список пуст");
                return;
            }
            object[,] chequeData = new object[dataGrid.Items.Count-1, dataGrid.Columns.Count];
            ProductsModel productsModel = new ProductsModel();
   
            for (int i = 0; i < dataGrid.Items.Count-1; i++)
            {
                productsModel = (ProductsModel)dataGrid.Items[i];
                chequeData[i, 0] = productsModel.product_id;
                chequeData[i, 1] = productsModel.product_name;
                chequeData[i, 2] = productsModel.technical_specifications;
                chequeData[i, 3] = productsModel.count_of_products;
                chequeData[i, 4] = productsModel.product_price;
                chequeData[i, 5] = productsModel.discount;
                chequeData[i, 6] = productsModel.category_FK;
            }
            chequeCreate.printCheque(chequeData);
            productService.infoAboutSales(chequeData);
            MessageBox.Show("Чек успешно сформирован");
        }

        private void loadData()
        {
            allProducts = chequeService.GetAllProducts();
            allCategory = chequeService.GetCategories();

        }

        public void deletePurchase(object product, int index)
        {
            int selectedCount = Convert.ToInt32(product.GetType().GetProperty("count_of_products").GetValue(product, null));
            int selectedId = Convert.ToInt32(product.GetType().GetProperty("product_id").GetValue(product, null));
            int countOfProducts = allProducts.Single(id => id.product_id == selectedId).count_of_products;
            countOfProducts += selectedCount;
            object mass = countOfProducts;
            chequeService.cancleProduct(selectedId, countOfProducts);
            chequeService.UpdateProduct(allProducts.Single(id => id.product_id == selectedId));
            chequeService.Savebd();
            selectedProducts.RemoveAt(index);
        }
    }
}
