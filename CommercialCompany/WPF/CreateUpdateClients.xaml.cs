using CommercialCompany.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// Логика взаимодействия для CreateUpdateClients.xaml
    /// </summary>
    public partial class CreateUpdateClients : Window
    {
        UserManager um;
        CommercialCompanyEntities db = new CommercialCompanyEntities();
        clients current_client;
        public CreateUpdateClients(UserManager umo)
        {
            InitializeComponent();
            um = umo;

            if (um.TableState == "New")
            {
                btnAdd.Click += btnAdd_Click;
            }

            else if (um.TableState == "Update")
            {
                current_client = (from table in db.clients where table.id == um.Index select table).First();

                textBox_id.Text = Convert.ToString(current_client.id);
                textBox_name.Text = current_client.name;
                textBox_surname.Text = current_client.surname;
                textBox_patronimyc.Text = current_client.patronimyc;
                textBox_phone.Text = current_client.phone;
                textBox_city.Text = current_client.city;
                textBox_street.Text = current_client.street;
                textBox_house.Text = Convert.ToString(current_client.house);
                textBox_office.Text = Convert.ToString(current_client.office);

                btnAdd.Click += btnAdd_Click;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clients client = new clients();
                client.name = textBox_name.Text;
                client.surname = textBox_surname.Text;
                client.patronimyc = textBox_patronimyc.Text;
                client.phone = textBox_phone.Text;
                client.city = textBox_city.Text;
                client.street = textBox_street.Text;
                client.house = Convert.ToInt32(textBox_house.Text);
                client.office = Convert.ToInt32(textBox_office.Text);
                if(um.TableState == "New")
                {
                    db.Entry(client).State = EntityState.Modified;

                    db.clients.Add(client);
                    db.SaveChanges();
                    this.Close();
                }
                else if (um.TableState == "Update")
                {
                    client.id = current_client.id;
                    db.Entry(current_client).CurrentValues.SetValues(client);
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
