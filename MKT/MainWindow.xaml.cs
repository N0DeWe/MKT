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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using Ninject;
using Ninject.Modules;
using BLL;
using MKT.Util;

namespace MKT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        IAuthorizationService authorizationService;

        public MainWindow()
        {
            string connection = ConfigurationManager.ConnectionStrings["PcshopContext"].ConnectionString;
            var kernel = new StandardKernel(new NinjectRegistrations(), new ServiceModule(connection));
            authorizationService = kernel.Get<IAuthorizationService>();

            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2.0;
            this.Left = (screenWidth - this.Width) / 2.0;
        }
        private void exitButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void enterButtonClick(object sender, RoutedEventArgs e)
        {
            CreateAccountWindow createAccountWindow = new CreateAccountWindow(authorizationService, false);

            if(createAccountWindow.ShowDialog() == true)
            {
                this.Close();
            }
        }

        private void createButtonClick(object sender, RoutedEventArgs e)
        {
            CreateAccountWindow createAccountWindow = new CreateAccountWindow(authorizationService, true);

            if (createAccountWindow.ShowDialog() == true)
            {
                this.Close();
            }
        }
    }
}
