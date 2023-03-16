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
    /// Логика взаимодействия для CreateUpdateContract.xaml
    /// </summary>
    public partial class CreateUpdateContract : Window
    {
        UserManager um;
        CommercialCompanyEntities db = new CommercialCompanyEntities();
        contracts current_contract;

        public CreateUpdateContract(UserManager umo)
        {
            InitializeComponent();

            um = umo;

            if (um.TableState == "New")
            {
                btnAdd.Click += btnAdd_Click;
            }

            else if (um.TableState == "Update")
            {
                current_contract = (from table in db.contracts where table.contract_number == um.Index select table).First();

                textBox_contract_number.Text = Convert.ToString(current_contract.contract_number);
                textBox_client_id.Text = Convert.ToString(current_contract.client_id);
                textBox_model_id.Text = Convert.ToString(current_contract.model_id);
                checkBox_status.IsChecked = current_contract.status;
                btnAdd.Click += btnAdd_Click;
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                contracts contract = new contracts();
                contract.contract_number = Convert.ToInt32(textBox_contract_number.Text);
                contract.client_id = Convert.ToInt32(textBox_client_id.Text);
                contract.model_id = Convert.ToInt32(textBox_model_id.Text);
                if (checkBox_status.IsChecked == true)
                    contract.status = true;
                else
                    contract.status = false;

                if (um.TableState == "New")
                {
                    db.Entry(contract).State = EntityState.Modified;

                    db.contracts.Add(contract);
                    db.SaveChanges();
                    this.Close();
                }
                else if (um.TableState == "Update")
                {
                    contract.contract_number = current_contract.contract_number;
                    db.Entry(current_contract).CurrentValues.SetValues(contract);
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
