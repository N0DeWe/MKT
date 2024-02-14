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
    /// Логика взаимодействия для AddSupplierWindow.xaml
    /// </summary>
    public partial class AddSupplierWindow : Window
    {
        ISupplierService supplierService;

        List<PhysicalPersonModel> physicalPersonModelsList = new List<PhysicalPersonModel>();
        List<LegalPersonModel> legalPersonModelsList = new List<LegalPersonModel>();
        PhysicalPersonModel physicalPerson = new PhysicalPersonModel();
        LegalPersonModel legalPerson = new LegalPersonModel();

        public AddSupplierWindow(ISupplierService supplier)
        {
            supplierService = supplier;

            InitializeComponent();

            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2.0;
            this.Left = (screenWidth - this.Width) / 2.0;

            physicalPersonModelsList = supplierService.GetPhysicalPersonList();
            legalPersonModelsList = supplierService.GetLegalPersonList();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            if (supplierComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали тип поставщика");
                return;
            }

            if ((nameTextBox.Text != "") && (pasportNumberTextBox.Text != "") && (TINTextBox.Text != ""))
            {
                if (supplierComboBox.SelectedIndex == 0)
                {
                    if (physicalPersonModelsList.Count == 0)
                    {

                        physicalPerson.physical_person_id = 1;
                    }
                    else
                    {
                        physicalPerson.physical_person_id = physicalPersonModelsList.Last().physical_person_id + 1;
                    }

                    physicalPerson.physical_person_name = nameTextBox.Text;
                    physicalPerson.physical_person_pasport_number = pasportNumberTextBox.Text;
                    physicalPerson.physical_person_TIN = TINTextBox.Text;
                    supplierService.AddPhysicalPerson(physicalPerson);
                    MessageBox.Show("Поставщик успешно добавлен");
                }
                else if (supplierComboBox.SelectedIndex == 1)
                {

                    if (legalPersonModelsList.Count == 0)
                    {

                        legalPerson.legal_person_id = 1;
                    }
                    else
                    {
                        legalPerson.legal_person_id = legalPersonModelsList.Last().legal_person_id + 1;
                    }

                    legalPerson.Legal_person_CRS = nameTextBox.Text;
                    legalPerson.Legal_person_MSRN = pasportNumberTextBox.Text;
                    physicalPerson.physical_person_TIN = TINTextBox.Text;
                    supplierService.AddLegalPerson(legalPerson);
                    MessageBox.Show("Поставщик успешно добавлен");
                }
            }
            else
            {
                MessageBox.Show("Поля незаполненны");
                return;
            }
        }

        private void supplierComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (supplierComboBox.SelectedIndex == 0)
            {
                label1.Content = "Введите ФИО";
                label2.Content = "Ведите серию и номер паспорта";
                label3.Content = "Введите ИНН";
            }
            else if (supplierComboBox.SelectedIndex == 1)
            {
                label1.Content = "Введите КПП";
                label2.Content = "Введите ОГРН";
                label3.Content = "Введите ИНН";
            }
        }

        private void TINTextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));

            if (TINTextBox.Text.Length > 11)
            {
                e.Handled = true;
            }
        }

        private void pasportNumberTextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
            //if (pasportNumberTextBox.Text.Length / 4 == 1)
            //{
            //    pasportNumberTextBox.Text = pasportNumberTextBox.Text.Insert(pasportNumberTextBox.Text.Length, " ");
            //}
            if (pasportNumberTextBox.Text.Length > 10)
            {
                e.Handled = true;
            }
        }
    }
}
