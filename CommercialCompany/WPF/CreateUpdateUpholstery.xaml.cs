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
    /// Логика взаимодействия для CreateUpdateUpholstery.xaml
    /// </summary>
    public partial class CreateUpdateUpholstery : Window
    {
        CommercialCompanyEntities db = new CommercialCompanyEntities();
        UserManager um;
        upholstery current_upholstery;

        public CreateUpdateUpholstery(UserManager umo)
        {
            InitializeComponent();

            um = umo;

            if (um.TableState == "New")
            {
                btnAdd.Click += btnAdd_Click;
            }

            else if (um.TableState == "Update")
            {
                current_upholstery = (from table in db.upholstery where table.id == um.Index select table).First();

                textBox_id.Text = Convert.ToString(current_upholstery.id);
                textBox_name.Text = current_upholstery.name;

                btnAdd.Click += btnAdd_Click;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                upholstery upholstery = new upholstery();
                upholstery.name = textBox_name.Text;

                if (um.TableState == "New")
                {
                    db.Entry(upholstery).State = EntityState.Modified;

                    db.upholstery.Add(upholstery);
                    db.SaveChanges();
                    this.Close();
                }
                else if (um.TableState == "Update")
                {
                    upholstery.id = current_upholstery.id;
                    db.Entry(current_upholstery).CurrentValues.SetValues(upholstery);
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
