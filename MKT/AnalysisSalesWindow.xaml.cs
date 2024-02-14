using BLL.Interfaces;
using BLL;
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

namespace MKT
{
    /// <summary>
    /// Логика взаимодействия для AnalysisSalesWindow.xaml
    /// </summary>
    public partial class AnalysisSalesWindow : Window
    {
        IAnalysisSalesService analysisSalesService;
        IChequeService chequeService;
        List<informationAboutSalesModel> informationAboutSalesModels = new List<informationAboutSalesModel>();
        List<ProductsModel> productsModels = new List<ProductsModel>();
        // List<CategoryModel> categoryModelsList = new List<CategoryModel>();
        public AnalysisSalesWindow(IAnalysisSalesService analysisSales, IChequeService cheque)
        {
            analysisSalesService = analysisSales;
            chequeService = cheque;

            InitializeComponent();

            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2.0;
            this.Left = (screenWidth - this.Width) / 2.0;

            dataGrid.Visibility = Visibility.Hidden;
            startDataPicker.SelectedDate = DateTime.Now;
            finishDataPicker.SelectedDate = DateTime.Now;
        }

        private void searchButtonClick(object sender, RoutedEventArgs e)
        {
            dataGrid.Visibility = Visibility.Visible;
            double sum = 0.0;
            int[] massCategory;
            int countOfCategories = chequeService.GetCategories().Count;

            DateTime startDate = Convert.ToDateTime(startDataPicker.SelectedDate);
            DateTime finishDate = Convert.ToDateTime(finishDataPicker.SelectedDate);

            informationAboutSalesModels = analysisSalesService.AnalisysSales(startDate, finishDate);

            if (informationAboutSalesModels.Count == 0)
            {
                return;
            }

            massCategory = new int[countOfCategories];
            productsModels = chequeService.GetAllProducts();
            dataGrid.ItemsSource = informationAboutSalesModels
                .Join(productsModels, sales => sales.product_FK, pr => pr.product_id, (sales, pr) => new
                {
                    Продукт = pr.product_name,
                    Количество = sales.sales_count,
                    Цена = sales.sales_price,
                    Дата = sales.sales_date
                }).ToList();

            foreach (informationAboutSalesModel salesModel in informationAboutSalesModels)
            {
                sum += Convert.ToDouble(salesModel.sales_price);
                massCategory[productsModels.First(i => i.product_id == salesModel.product_FK).category_FK] += salesModel.sales_count;
            }

            int maximalCount = 0;
            int idCategory = -1;

            for (int i = 0; i < massCategory.Length; i++)
            {
                if (maximalCount < massCategory[i])
                {
                    maximalCount = massCategory[i];
                    idCategory = i;
                }
            }

            priceTextBox.Text = Convert.ToString(sum);
            bestSellingCategoryTextBox.Text = Convert.ToString(chequeService.GetCategories().First(i => i.category_id == idCategory).category_name);
        }
    }
}
