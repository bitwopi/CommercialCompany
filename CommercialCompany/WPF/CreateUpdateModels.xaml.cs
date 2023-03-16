using CommercialCompany.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
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
    /// Логика взаимодействия для CreateUpdateModels.xaml
    /// </summary>
    public partial class CreateUpdateModels : Window
    {
        UserManager um;
        CommercialCompanyEntities db = new CommercialCompanyEntities();
        models current_model;

        public CreateUpdateModels(UserManager umo)
        {
            InitializeComponent();

            um = umo;

            if (um.TableState == "New")
            {
                btnAdd.Click += btnAdd_Click;
            }

            else if (um.TableState == "Update")
            {
                try
                {
                    current_model = (from table in db.models where table.id == um.Index select table).First();

                    textBox_id.Text = Convert.ToString(current_model.id);
                    textBox_name.Text = current_model.name;
                    textBox_color_id.Text = Convert.ToString(current_model.color_id);
                    textBox_upholstery_id.Text = Convert.ToString(current_model.upholstery_id);
                    textBox_horsepower.Text = Convert.ToString(current_model.horsepower);
                    textBox_doors_number.Text = Convert.ToString(current_model.doors_number);
                    checkBox_transmission.IsChecked = current_model.transmission;
                    textBox_release_year.Text = Convert.ToString(current_model.release_year);
                    textBox_model_price.Text = Convert.ToString(current_model.model_price);
                    textBox_preparation_price.Text = Convert.ToString(current_model.preparation_price);
                    textBox_transport_price.Text = Convert.ToString(current_model.transport_price);
                    btnAdd.Click += btnAdd_Click;
                }
                catch
                {

                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                models model = new models();
                model.name = textBox_name.Text;
                model.color_id = Convert.ToInt32(textBox_color_id.Text);
                model.upholstery_id = Convert.ToInt32(textBox_upholstery_id.Text);
                model.horsepower = Convert.ToInt32(textBox_horsepower.Text);
                model.doors_number = Convert.ToInt32(textBox_doors_number.Text);
                if(checkBox_transmission.IsChecked == true)
                    model.transmission = true;
                else
                    model.transmission = false;
                if(current_model != null)
                    model.dealers = current_model.dealers;
                else
                    model.dealers = new List<dealers>();
                    
                model.release_year = Convert.ToInt32(textBox_release_year.Text);
                model.model_price = Convert.ToInt32(textBox_model_price.Text);
                model.preparation_price = Convert.ToInt32(textBox_preparation_price.Text);
                model.transport_price = Convert.ToInt32(textBox_transport_price.Text);


                if (um.TableState == "New")
                {
                    db.Entry(model).State = EntityState.Modified;

                    db.models.Add(model);
                    db.SaveChanges();
                    this.Close();
                }
                else if (um.TableState == "Update")
                {
                    model.id = current_model.id;
                    db.Entry(current_model).CurrentValues.SetValues(model);
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
