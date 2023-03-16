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
    /// Логика взаимодействия для Colors.xaml
    /// </summary>
    public partial class CreateUpdateColors : Window
    {
        CommercialCompanyEntities db = new CommercialCompanyEntities();
        UserManager um;
        colors current_color;
        public CreateUpdateColors(UserManager umo)
        {
            InitializeComponent();
            um = umo;

            if (um.TableState == "New")
            {
                btnAdd.Click += btnAdd_Click;
            }

            else if (um.TableState == "Update")
            {
                current_color = (from table in db.colors where table.id == um.Index select table).First();

                textBox_id.Text = Convert.ToString(current_color.id);
                textBox_name.Text = current_color.name;

                btnAdd.Click += btnAdd_Click;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                colors color = new colors();
                color.name = textBox_name.Text;

                if (um.TableState == "New")
                {
                    db.Entry(color).State = EntityState.Modified;

                    db.colors.Add(color);
                    db.SaveChanges();
                    this.Close();
                }
                else if (um.TableState == "Update")
                {
                    color.id = current_color.id;
                    db.Entry(current_color).CurrentValues.SetValues(color);
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
