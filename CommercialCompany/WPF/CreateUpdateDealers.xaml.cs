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
using static System.Net.Mime.MediaTypeNames;

namespace CommercialCompany.WPF
{
    /// <summary>
    /// Логика взаимодействия для CreateUpdateDealers.xaml
    /// </summary>
    public partial class CreateUpdateDealers : Window
    {
        UserManager um;
        CommercialCompanyEntities db = new CommercialCompanyEntities();
        dealers current_dealer;

        public CreateUpdateDealers(UserManager umo)
        {
            InitializeComponent();

            um = umo;

            if (um.TableState == "New")
            {
                btnAdd.Click += btnAdd_Click;
            }

            else if (um.TableState == "Update")
            {
                current_dealer = (from table in db.dealers where table.id == um.Index select table).First();

                textBox_id.Text = Convert.ToString(current_dealer.id);
                textBox_name.Text = current_dealer.name;
                textBox_phone.Text = current_dealer.phone;
                textBox_email.Text = current_dealer.email;
                textBox_url.Text = current_dealer.url;
                List <string> model_list = new List<string>();
                foreach (var item in current_dealer.models)
                    model_list.Add($"{item.name}({item.id})");
                comboBoxListTab.ItemsSource = model_list;
                btnAdd.Click += btnAdd_Click;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dealers dealer = new dealers();
                dealer.name = textBox_name.Text;
                dealer.phone = textBox_phone.Text;
                dealer.email = textBox_email.Text;
                dealer.url = textBox_url.Text;
                dealer.models = ConvertModelList();
                if(um.TableState == "New")
                {
                    db.Entry(dealer).State = EntityState.Modified;

                    db.dealers.Add(dealer);
                    db.SaveChanges();
                    this.Close();
                }
                else if(um.TableState == "Update") 
                {
                    dealer.id = current_dealer.id;
                    db.Entry(current_dealer).CurrentValues.SetValues(dealer);
                    db.SaveChanges();
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Неккоректный ввод данных", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btnAdd_Model_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(textBox_model.Text);
            var model = (from mod in db.models where mod.id == id select mod).First();
            if (model != null)
            {
                MessageBox.Show(model.ToString());
                List<string> list = (List<string>)comboBoxListTab.ItemsSource;
                list.Add($"{model.name}({model.id})");
                comboBoxListTab.ItemsSource = list;
                comboBoxListTab.Items.Refresh();
            }
            else
                MessageBox.Show("null");
            
        }

        private void comboBoxListTab_SelectionChaged(object sender, RoutedEventArgs e)
        {
            string text = comboBoxListTab.SelectedItem.ToString();
            text = text.Substring(text.IndexOf("(") + 1);
            int id = Convert.ToInt32(text.Substring(0, text.IndexOf(")")));
            MessageBox.Show($"{id}");
            textBox_model.Text = $"{id}";
        }

        private List<models> ConvertModelList()
        {
            try
            {
                List<models> models_list = new List<models>();
                foreach (var item in comboBoxListTab.Items)
                {
                    string text = item.ToString();
                    text = text.Substring(text.IndexOf("(") + 1);
                    int id = Convert.ToInt32(text.Substring(0, text.IndexOf(")")));

                    var model = (from mod in db.models where mod.id == id select mod).First();
                    models_list.Add(model);
                }
                return models_list;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
