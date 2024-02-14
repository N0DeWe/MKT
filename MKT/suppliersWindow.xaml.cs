using BLL;
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

namespace MKT
{
    /// <summary>
    /// Логика взаимодействия для suppliersWindow.xaml
    /// </summary>
    public partial class suppliersWindow : Window
    {
        IChequeService chequeService;
        ISupplierService supplierService;

        List<PhysicalPersonModel> physicalPersonModels;
        List<LegalPersonModel> legalPersonModels;
        PhysicalPersonModel physicalPerson = new PhysicalPersonModel();
        LegalPersonModel legalPerson = new LegalPersonModel();
        ComboBoxItem comboBoxItem;
        List<InformationAboutSuppliersModel> informationAboutSuppliersModels;
        public suppliersWindow(IChequeService service, ISupplierService supplier)
        {
            chequeService = service;
            supplierService = supplier;
            InitializeComponent();

            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2.0;
            this.Left = (screenWidth - this.Width) / 2.0;

            loadData();
        }

        private void personComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (personComboBox.SelectedIndex == 0)
            {

                dataGrid.ItemsSource = physicalPersonModels
                .ToList();
                dataGrid.Columns[0].Header = "Id";
                dataGrid.Columns[0].Width = 110;
                dataGrid.Columns[1].Header = "ФИО";
                dataGrid.Columns[1].Width = 200;
                dataGrid.Columns[2].Header = "Серия и номер паспорта";
                dataGrid.Columns[2].Width = 300;
                dataGrid.Columns[3].Header = "ИНН";
                dataGrid.Columns[3].Width = 150;
            }
            else if (personComboBox.SelectedIndex == 1)
            {

                dataGrid.ItemsSource = legalPersonModels
                .ToList();
                dataGrid.Columns[0].Header = "Id";
                dataGrid.Columns[0].Width = 110;
                dataGrid.Columns[1].Header = "ИНН";
                dataGrid.Columns[1].Width = 200;
                dataGrid.Columns[2].Header = "КПП"; //Код причины постановки
                dataGrid.Columns[2].Width = 300;
                dataGrid.Columns[3].Header = "ОГРН"; //Основной государственный регистрационный номер
                dataGrid.Columns[3].Width = 150;
            }
        }

        private void loadData()
        {
            legalPersonModels = supplierService.GetLegalPersonList();
            physicalPersonModels = supplierService.GetPhysicalPersonList();

            comboBoxItem = (ComboBoxItem)personComboBox.SelectedItem;
        }

        private void addSupplierButtonClick(object sender, RoutedEventArgs e)
        {
            AddSupplierWindow addSupplierWindow = new AddSupplierWindow(supplierService);
            addSupplierWindow.ShowDialog();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
