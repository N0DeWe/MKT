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
using System.Data.SqlClient;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL;
using BLL.Interfaces;

namespace MKT
{
    /// <summary>
    /// Логика взаимодействия для QueryWindow.xaml
    /// </summary>
    public partial class QueryWindow : Window
    {
        IChequeService chequeService;

        List<CategoryModel> allCategory;
        List<ProductsModel> allProducts;

        public QueryWindow(IChequeService service)
        {
            chequeService = service;
            InitializeComponent();

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

            comboBox.ItemsSource = chequeService.GetCategories().ToList();
            comboBox.DisplayMemberPath = "category_name";

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

        private void showButtonClick(object sender, RoutedEventArgs e)
        {
            object[] mass = new object[3];
            ProductsModel productsModel = new ProductsModel();
            CategoryModel categoryModel = new CategoryModel();

            if (priceMinTextBox.Text == "")
            {
                MessageBox.Show("Вы не указали цену");
                return;
            }
            if (priceMaxTextBox.Text == "")
            {
                MessageBox.Show("Вы не указали цену");
                return;
            }
            if(comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не указали категорию");
                return;
            }
            SqlParameter parameter = new SqlParameter("@PRICE_MIN", Convert.ToDouble(priceMinTextBox.Text));
            SqlParameter parameter1 = new SqlParameter("@PRICE_MAX", Convert.ToDouble(priceMaxTextBox.Text));
            categoryModel = allCategory.ElementAt(comboBox.SelectedIndex);
            SqlParameter parameter2 = new SqlParameter("@CATEGORY_FK", Convert.ToInt32(categoryModel.category_id));
            mass[0] = parameter;
            mass[1] = parameter1;
            mass[2] = parameter2;
            object a = chequeService.querySQl(mass);
            allProducts = (List<ProductsModel>)a;
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

        private void priceMinTextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));

            if(priceMinTextBox.Text.Length > 10)
            {
                e.Handled = true;
            }

            try
            {
                if (priceMinTextBox.Text.Length > 1)
                {
                    Convert.ToInt32(priceMinTextBox.Text + "0");
                }
            }
            catch (System.OverflowException ex)
            {
                e.Handled = true;
            }
        }
        private void priceMaxTextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));

            if (priceMaxTextBox.Text.Length > 10)
            {
                e.Handled = true;
            }

            try
            {
                if (priceMaxTextBox.Text.Length > 1)
                {
                    Convert.ToInt32(priceMaxTextBox.Text + "0");
                }
            }
            catch (System.OverflowException ex)
            {
                e.Handled = true;
            }
        }
    }
}
