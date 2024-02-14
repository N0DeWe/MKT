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
using System.Security.Cryptography;
using BLL.Interfaces;
using BLL;
using System.Text.RegularExpressions;

namespace MKT
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class CreateAccountWindow : Window
    {
        IAuthorizationService authorizationService;
        bool isRegistration;
        public CreateAccountWindow(IAuthorizationService service, bool isRegistration)
        {
            authorizationService = service;
            this.isRegistration = isRegistration;
            InitializeComponent();

            if (!isRegistration)
            {
                createButton.Content = "Войти";
            }

            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2.0;
            this.Left = (screenWidth - this.Width) / 2.0;

        }

        private void createButtonClick(object sender, RoutedEventArgs e)
        {
            UsersModel usersModel = new UsersModel();
            UsersRolesModel usersRolesModel = new UsersRolesModel();

            string hash;
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
           
            if (loginTextBox.Text == "")
            {
                MessageBox.Show("Вы не ввели логин");
                return;
            }
            if (!regex.IsMatch(loginTextBox.Text))
            {
                MessageBox.Show("Вы ввели не корректный адрес электронной почты");
                return;
            }
            if (isRegistration)
            {
                if (authorizationService.isUserEmailExists(loginTextBox.Text))
                {
                    MessageBox.Show("Адрес электронной почты уже используется");
                    return;
                }
            }
            if (passwordBox.Password == "")
            {
                MessageBox.Show("Вы не ввели пароль");
                return;
            }
            if(passwordBox.Password.Length < 6)
            {
                MessageBox.Show("Пароль слишком короткий");
                return;
            }

            hash = Hash(passwordBox.Password.ToString());
            usersModel.user_login = loginTextBox.Text;
            usersModel.user_password = hash;

            if (!isRegistration)
            {
                if (!authorizationService.isUserEmailExists(usersModel.user_login))
                {
                    MessageBox.Show("Неверный логин!");
                    return;
                }
                if (!authorizationService.isLoginDataValid(usersModel.user_password, usersModel.user_login))
                {
                    MessageBox.Show("Неверный пароль!");
                    return;
                }
            }
            else
            {
                usersModel.user_id = authorizationService.GetUsersList().Count;
                authorizationService.AddAccount(usersModel);
            }

            DialogResult = true;
            usersModel.user_id = authorizationService.GetUsersList().Where(i => i.user_login.Equals(usersModel.user_login)).First().user_id;
            List<UsersRolesModel> usersRolesModelsList = new List<UsersRolesModel>();
            usersRolesModelsList = authorizationService.GetAllUsersRoles();
            usersRolesModel = usersRolesModelsList.Where(i => i.user_id_FK == usersModel.user_id).First();
            ShopWindowMenu shopWindowMenu = new ShopWindowMenu(usersRolesModel.role_id_FK);
            shopWindowMenu.Show();
            this.Close();
        }

        private string Hash(string ha)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                string base64string = Convert.ToBase64String(Encoding.UTF8.GetBytes(ha));
                byte[] buffer = Convert.FromBase64String(base64string);

                var sb = new StringBuilder();
                for (var i = 0; i < buffer.Length; i++)
                {
                    sb.Append(buffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }


    }
}
