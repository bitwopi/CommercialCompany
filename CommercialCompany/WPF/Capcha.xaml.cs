using CommercialCompany.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Логика взаимодействия для Capcha.xaml
    /// </summary>
    public partial class Capcha : Window
    {
        string capcha_text = "ifhkfp";
        int attempts = 0;
        UserManager um;
        public Capcha(UserManager uma)
        {
            InitializeComponent();
            um = uma;
            Closing += WindowClose;
            BitmapImage photo = new BitmapImage();
            photo.BeginInit();
            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.FullName;
            photo.UriSource = new Uri($"{projectDirectory}\\Images\\capcha\\capcha1.jpg");
            photo.EndInit();
            capchaPhoto.Source = photo;
        }

        private void WindowClose(object sender, CancelEventArgs e)
        {
            if(attempts > 2) 
                UserManager.Authorization.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text == capcha_text)
                this.Close();
            else
            {
                if(attempts > 2)
                    this.Close();
                MessageBox.Show("Неверный ввод, повторите попытку", "Capcha", MessageBoxButton.OK, MessageBoxImage.Error);
                attempts++;
            }
                
        }


    }
}
