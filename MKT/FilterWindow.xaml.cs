using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для FilterWindow.xaml
    /// </summary>
    public partial class FilterWindow : Window
    {
        IChequeService chequeService;

        List<CategoryModel> allCategory;
        List<ProductsModel> allProducts;
        CategoryModel category = new CategoryModel();
        ProductsModel products = new ProductsModel();
        public DataRow row;

        public FilterWindow(IChequeService service)
        {
            chequeService = service;

            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2.0;
            this.Left = (screenWidth - this.Width) / 2.0;

            loadProducts();
            loadCategories();
            loadData();
        }

        private void loadProducts()
        {
            allProducts = chequeService.GetAllProducts();
        }
        private void loadCategories()
        {
            allCategory = chequeService.GetCategories();
            ComboBoxItem comboBoxItem = (ComboBoxItem)comboBox.SelectedItem;
            comboBox.ItemsSource = chequeService.GetCategories().ToList();
            comboBox.DisplayMemberPath = "category_name";
        }

        private void loadData()
        {
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

        private void comboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            category = allCategory.ElementAt(comboBox.SelectedIndex);
            dataGrid.ItemsSource = allProducts
            .Where(i => i.category_FK == category.category_id)
            .Join(allCategory, pr => pr.category_FK, ct => ct.category_id, (pr, ct) => new
            {
                Id = pr.product_id,
                Название = pr.product_name,
                Характеристика = pr.technical_specifications,
                Количество = pr.count_of_products,
                Цена = pr.product_price,
                Скидка = pr.discount,
                Категория = ct.category_name
            })
            .ToList();
        }
    }
}
