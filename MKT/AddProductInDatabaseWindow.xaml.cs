using BLL.Interfaces;
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

namespace MKT
{
    /// <summary>
    /// Логика взаимодействия для AddProductInDatabaseWindow.xaml
    /// </summary>
    public partial class AddProductInDatabaseWindow : Window
    {
        IChequeService chequeService;
        List<ProductsModel> allProducts;
        List<CategoryModel> allCategory;
        public AddProductInDatabaseWindow(IChequeService cheque)
        {
            chequeService = cheque;
            InitializeComponent();

            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2.0;
            this.Left = (screenWidth - this.Width) / 2.0;

            loadData();
        }

        private void acceptButtonClick(object sender, RoutedEventArgs e)
        {
            if(Convert.ToDouble(discontTextBox.Text)< 0.0)
            {
                MessageBox.Show("Вы ввели скидку меньше,чем 0");
                return;
            }
            if (Convert.ToDouble(discontTextBox.Text) > 100.0)
            {
                MessageBox.Show("Скидка не может быть больше, чем 100");
                return;
            }

            if ((nameProductTextBox.Text != "") && (texnicalspecificationTextBox.Text != "") && (countOfProductTextBox.Text != "") && (priceProductTextBox.Text != "")&&(discontTextBox.Text != ""))
            {
                ProductsModel productModel = new ProductsModel();
                if (allProducts.Count == 0)
                {

                    productModel.product_id = 1;
                }
                else
                {
                    productModel.product_id = allProducts.Last().product_id + 1;
                }
                productModel.product_name = nameProductTextBox.Text;
                productModel.technical_specifications = texnicalspecificationTextBox.Text;
                productModel.count_of_products = Convert.ToInt32(countOfProductTextBox.Text);
                productModel.product_price = Convert.ToDecimal(priceProductTextBox.Text);
                productModel.discount = Convert.ToDouble(discontTextBox.Text);
                productModel.category_FK = categoryComboBox.SelectedIndex + 1;
                chequeService.AddProduct(productModel);
                nameProductTextBox.Text = "";
                texnicalspecificationTextBox.Text = "";
                countOfProductTextBox.Text = "";
                priceProductTextBox.Text = "";
                MessageBox.Show("Товар успешно добавлен");
     
            }
            else
            {
                MessageBox.Show("Поля незаполненны");
                return;
            }
        }

        private void loadData()
        {
            allProducts = chequeService.GetAllProducts();
            allCategory = chequeService.GetCategories();
            ComboBoxItem comboBoxItem = (ComboBoxItem)categoryComboBox.SelectedItem;
            categoryComboBox.ItemsSource = chequeService.GetCategories().ToList();
            categoryComboBox.DisplayMemberPath = "category_name";
        }

        private void discontTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
            try
            {
                if (discontTextBox.Text.Length > 1)
                {
                    Convert.ToInt32(discontTextBox.Text + "0");
                }
            }
            catch (System.OverflowException ex)
            {
                e.Handled = true;
            }
        }

        private void priceProductTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
            try
            {
                if (priceProductTextBox.Text.Length > 1)
                {
                    Convert.ToInt32(priceProductTextBox.Text + "0");
                }
            }
            catch (System.OverflowException ex)
            {
                e.Handled = true;
            }
        }

        private void countOfProductTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
            try
            {
                if (priceProductTextBox.Text.Length > 1)
                {
                    Convert.ToInt32(priceProductTextBox.Text + "0");
                }
            }
            catch (System.OverflowException ex)
            {
                e.Handled = true;
            }
        }
    }
}
