using CommercialCompany.Model;
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

namespace CommercialCompany.WPF
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        CommercialCompanyEntities db = new CommercialCompanyEntities();
        UserManager um = new UserManager();
        int attempts = 0;
        public Authorization()
        {
            InitializeComponent();
            UserManager.Authorization = this;
        }

        private void butEnter_Click(object sender, RoutedEventArgs e)
        {
            var users = from user in db.users where user.email == loginTextBox.Text && user.password == passwordHideBox.Password select user;
            if (users.Count() != 0)
            {
                um.User = users.First();

                Profile profile = new Profile(um);
                this.Hide();
                profile.Show();
            }
            else
            {
                if(attempts > 2)
                {
                    attempts = 0;
                    Capcha capcha_window = new Capcha(um);
                    capcha_window.Show();
                }
                MessageBox.Show("Неверный пароль или логин", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                attempts++;
            }

        }

        private void ShowPassword(object sender, RoutedEventArgs e)
        {
            showHidePassword.Content = "Скрыть пароль";
            passwordShowTextBox.Text = passwordHideBox.Password;
            passwordShowTextBox.Visibility = Visibility.Visible;
            passwordHideBox.Visibility = Visibility.Hidden;
        }
        private void HidePassword(object sender, RoutedEventArgs e)
        {
            showHidePassword.Content = "Показать пароль";
            passwordHideBox.Password = passwordShowTextBox.Text;
            passwordShowTextBox.Visibility = Visibility.Hidden;
            passwordHideBox.Visibility = Visibility.Visible;
        }

        private void TextChanged(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Неверный пароль или логин", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
