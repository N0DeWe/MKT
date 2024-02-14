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
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        IChequeService chequeService;

        List<CategoryModel> allCategory;
        List<ProductsModel> allProducts;
        public ProductsModel selectedProduct = new ProductsModel();
        public List<ProductsModel> selectedProducts;
        public AddProductWindow(IChequeService service, List<ProductsModel> selectedProducts)
        {
            chequeService = service;
            InitializeComponent();

            this.selectedProducts = selectedProducts;
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2.0;
            this.Left = (screenWidth - this.Width) / 2.0;

            loadData();
        }

        private void countOfProductTextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
            try
            {
                if (countOfProductTextBox.Text.Length > 1)
                {
                    Convert.ToInt32(countOfProductTextBox.Text + "0");
                }
            }
            catch(System.OverflowException ex)
            {
                e.Handled = true;
            }
        }

        private void acceptButtonClick(object sender, RoutedEventArgs e)
        {
            object product = 0;
            int id = 0;

            if (dataGrid.SelectedItems.Count > 0)
            {
                product = dataGrid.SelectedItem;
            }
            else
            {
                MessageBox.Show("Вы не выбрали товар");
                return;
            }
            id = Convert.ToInt32(product.GetType().GetProperty("Id").GetValue(product, null));
            selectedProduct = chequeService.GetProduct(id);

            if (Convert.ToInt32(countOfProductTextBox.Text) > Convert.ToInt32(selectedProduct.count_of_products))
            {
                MessageBox.Show("Вы не можете выбрать количество большее, чем есть на складе");
            }
            else
            {
                selectedProduct.count_of_products = Convert.ToInt32(countOfProductTextBox.Text);

                this.Close();
            }
        }

        private void loadData()
        {
            allCategory = chequeService.GetCategories();
            allProducts = chequeService.GetAllProducts();
            for (int i = 0; i < selectedProducts.Count; i++)
            {
                allProducts.Single(pr => pr.product_id == selectedProducts[i].product_id).count_of_products -= selectedProducts[i].count_of_products;
            }

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
        }

        private void countOfProductTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
