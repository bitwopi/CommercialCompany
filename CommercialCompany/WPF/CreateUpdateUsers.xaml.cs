using CommercialCompany.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для CreateUpdateUsers.xaml
    /// </summary>
    public partial class CreateUpdateUsers : Window
    {
        UserManager um;
        CommercialCompanyEntities db = new CommercialCompanyEntities();
        users current_user;
        public CreateUpdateUsers(UserManager umo)
        {
            InitializeComponent();

            um = umo;

            if (um.TableState == "New")
            {
                btnAdd.Click += btnAdd_Click;
            }

            else if (um.TableState == "Update")
            {
                current_user = (from table in db.users where table.id == um.Index select table).First();

                textBox_id.Text = Convert.ToString(current_user.id);
                textBox_name.Text = current_user.name;
                textBox_surname.Text = current_user.surname;
                textBox_patronimyc.Text = current_user.patronymic;
                textBox_email.Text = current_user.email;
                textBox_password.Text = current_user.password;
                textBox_type.Text = current_user.type;

                btnAdd.Click += btnAdd_Click;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                users user = new users();
                user.name = textBox_name.Text;
                user.surname = textBox_surname.Text;
                user.patronymic = textBox_patronimyc.Text;
                user.email = textBox_email.Text;
                user.password = textBox_password.Text;
                user.type = textBox_type.Text;

                if (um.TableState == "New")
                {
                    db.Entry(user).State = EntityState.Modified;

                    db.users.Add(user);
                    db.SaveChanges();
                    this.Close();
                }
                else if (um.TableState == "Update")
                {
                    user.id = current_user.id;
                    db.Entry(current_user).CurrentValues.SetValues(user);
                    db.SaveChanges();
                    this.Close();
                }
            }

            catch
            {
                MessageBox.Show("Неккоректный ввод данных", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
