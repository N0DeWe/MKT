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

namespace MKT
{
    /// <summary>
    /// Логика взаимодействия для ListProductWindow.xaml
    /// </summary>
    public partial class ListProductWindow : Window
    {
        IChequeService chequeService;
        List<CategoryModel> allCategory;
        List<ProductsModel> allProducts;
        public ListProductWindow(IChequeService service, int role)
        {

            InitializeComponent();

            if(role == 2)
            {
                addProductButton.Visibility = Visibility.Collapsed;
            }

            chequeService = service;
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2.0;
            this.Left = (screenWidth - this.Width) / 2.0;

            loadData();
        }

        
        private void loadData()
        {
            allCategory = chequeService.GetCategories();
            allProducts = chequeService.GetAllProducts();
            dataGrid.ItemsSource = allProducts
            .Join(allCategory, pr => pr.category_FK, ct => ct.category_id, (pr, ct) => new
            {
                Id = pr.product_id,
                Название = pr.product_name,
                Характеристика = pr.technical_specifications,
                Количество = pr.count_of_products,
                Цена = pr.product_price,
                Скидка = pr.discount,
                Категория = ct.category_name
            }).ToList();
        }

        private void nameProductTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            var products = allProducts
             .Join(allCategory, pr => pr.category_FK, ct => ct.category_id, (pr, ct) => new
             {
                 Id = pr.product_id,
                 Product_name = pr.product_name,
                 Technical_specifications = pr.technical_specifications,
                 Count_of_products = pr.count_of_products,
                 Product_price = pr.product_price,
                 Discount = pr.discount,
                 Сategory = ct.category_name
             })
             .Where(name => name.Product_name.ToLower().StartsWith(nameProductTextBox.Text.ToLower())).ToList();
            dataGrid.ItemsSource = products;

            dataGrid.Columns[0].Header = "Id";
            dataGrid.Columns[1].Header = "Название";
            dataGrid.Columns[2].Header = "Характеристика";
            dataGrid.Columns[3].Header = "Количество";
            dataGrid.Columns[4].Header = "Цена";
            dataGrid.Columns[5].Header = "Скидка";
            dataGrid.Columns[6].Header = "Категория";
        }

        private void addProductButtonClick(object sender, RoutedEventArgs e)
        {
            AddProductInDatabaseWindow addProductInDatabaseWindow = new AddProductInDatabaseWindow(chequeService);
            addProductInDatabaseWindow.ShowDialog();
        }
    }
}
