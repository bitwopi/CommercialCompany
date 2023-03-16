using CommercialCompany.Model;
using CommercialCompany.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CommercialCompany.WPF
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        CommercialCompanyEntities db = new CommercialCompanyEntities();

        public UserManager um;
        private int Index;
        private string type;
        public Profile(UserManager uma)
        {
            InitializeComponent();
            Closing += Window_Closed;
            um = uma;
            try
            {
                BitmapImage photo = new BitmapImage();
                photo.BeginInit();
                string currentDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(currentDirectory).Parent.FullName;
                photo.UriSource = new Uri($"{projectDirectory}\\Images\\{um.User.type}.jpg");
                photo.EndInit();
                profilePhoto.Source = photo;
                profileName.Content = um.User.GetFullName();
                profileType.Content = um.User.type;

                if (um.User.type == "manager")
                {
                    tableComboBox.Items.Add("clients");
                    tableComboBox.Items.Add("dealers");
                    tableComboBox.Items.Add("contracts");
                }

                else if (um.User.type == "consultant")
                {
                    tableComboBox.Items.Add("models");
                }

                else if (um.User.type == "trader")
                {
                    tableComboBox.Items.Add("models");
                    tableComboBox.Items.Add("colors");
                    tableComboBox.Items.Add("upholstery");
                    tableComboBox.Items.Add("dealers");
                }
                else if (um.User.type == "admin")
                {
                    tableComboBox.Items.Add("clients");
                    tableComboBox.Items.Add("models");
                    tableComboBox.Items.Add("colors");
                    tableComboBox.Items.Add("upholstery");
                    tableComboBox.Items.Add("dealers");
                    tableComboBox.Items.Add("users");
                }
                tableComboBox.SelectedIndex = 0;
            }
            catch (Exception)
            {

            }
        }

        private void UpdateData()
        {
            db = new CommercialCompanyEntities();

            switch (tableComboBox.SelectedItem)
            {
                case "models":
                    db.models.Load();
                    dataGrid.ItemsSource = db.models.Local.Select(x => new
                    {
                        ID = x.id,
                        КаробкаПередач = x.transmission,
                        Мощность = x.horsepower,
                        Цвет = x.colors.name,
                        Обивка = x.upholstery.name,
                        ГодВыпуска = x.release_year,
                        КоличествоДверей = x.doors_number,
                        ЦенаМодели = x.model_price,
                        ЦенаПриготовлений = x.preparation_price,
                        ЦенаТранспортировки = x.transport_price,
                    }
                    ).ToList();
                    dataGrid.Items.Refresh();
                    break;

                case "clients":
                    db.clients.Load();
                    dataGrid.ItemsSource = db.clients.Local.Select(x => new
                    {
                        ID = x.id,
                        Фамилия = x.surname,
                        Имя = x.name,
                        Отчество = x.patronimyc,
                        Город = x.city,
                        Улица = x.street,
                        Дом = x.house,
                        Оффис = x.office,                       
                        Телефон = x.phone,
                    }
                    ).ToList();
                    dataGrid.Items.Refresh();
                    break;

                case "contracts":
                    db.clients.Load();
                    dataGrid.ItemsSource = db.contracts.Local.Select(x => new
                    {
                        НомерКонтракта = x.contract_number,
                        Клиент = x.clients.GetFullName(),
                        Модель = x.models.name,
                        ДатаПокупки = x.purchase_date,
                        Статус = x.status,
                    }
                    ).ToList();
                    dataGrid.Items.Refresh();
                    break;

                case "dealers":
                    db.dealers.Load();
                    dataGrid.ItemsSource = db.dealers.Local.Select(x => new
                    {
                        ID = x.id,
                        Наименование = x.name,
                        Email = x.email,
                        Телефон = x.phone,
                        Сайт = x.url,
                    }
                    ).ToList();
                    dataGrid.Items.Refresh();
                    break;

                case "users":
                    db.users.Load();
                    dataGrid.ItemsSource = db.users.Local.Select(x => new
                    {
                        ID = x.id,
                        Фамилия = x.surname,
                        Имя = x.name,
                        Отчество = x.patronymic,
                        Пароль = x.password,
                        Email = x.email,
                    }
                    ).ToList();
                    dataGrid.Items.Refresh();
                    break;

                case "colors":
                    db.colors.Load();
                    dataGrid.ItemsSource = db.colors.Local.Select(x => new
                    {
                        ID = x.id,
                        Наименование = x.name,
                    }
                    ).ToList();
                    dataGrid.Items.Refresh();
                    break;

                case "upholstery":
                    db.upholstery.Load();
                    dataGrid.ItemsSource = db.upholstery.Local.Select(x => new
                    {
                        ID = x.id,
                        Наименование = x.name,
                    }
                    ).ToList();
                    dataGrid.Items.Refresh();
                    break;
            }
        }

        private void comboBoxListTab_SelectionChaged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void butBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            UserManager.Authorization.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            UserManager.Authorization.Show();
        }

        private void butSort_Click(object sender, RoutedEventArgs e)
        {
            um.columnsNames.Clear();
            foreach (var item in dataGrid.Columns)
            {
                um.columnsNames.Add(item.Header.ToString());
            }
        }

        private void butAddNew_Click(object sender, RoutedEventArgs e)
        {
            um.TableState = "New";
            callWindows();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (tableComboBox.SelectedItem)
                {
                    case "clients":
                        var table_cli = db.clients;
                        Delete(table_cli);
                        break;
                    case "colors":
                        var table_col = db.colors;
                        Delete(table_col);
                        break;
                    case "upholstery":
                        var table_uph = db.upholstery;
                        Delete(table_uph);
                        break;
                    case "dealers":
                        var table_deal = db.dealers;
                        Delete(table_deal);
                        break;
                    case "models":
                        var table_mod = db.models;
                        Delete(table_mod);
                        break;
                    case "contracts":
                        var table_cont = db.contracts;
                        Delete(table_cont);
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Неккоректный ввод данных", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete(DbSet table)
        {
            table.Remove(table.Find(Index));
            db.SaveChanges();
            UpdateData();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            um.Index = Index;
            um.TableState = "Update";
            callWindows();
        }

        private void callWindows()
        {
            switch (tableComboBox.SelectedItem)
            {
                case "models":
                    CreateUpdateModels model_window = new CreateUpdateModels(um);
                    if (model_window.ShowDialog() == true) UpdateData();
                    break;

                case "clients":
                    CreateUpdateClients client_window = new CreateUpdateClients(um);
                    if (client_window.ShowDialog() == true) UpdateData();
                    break;

                case "contracts":
                    CreateUpdateContract contract_window = new CreateUpdateContract(um);
                    if (contract_window.ShowDialog() == true) UpdateData();
                    break;

                case "dealers":
                    CreateUpdateDealers dealer_window = new CreateUpdateDealers(um);
                    if (dealer_window.ShowDialog() == true) UpdateData();
                    break;

                case "users":
                    CreateUpdateUsers user_window = new CreateUpdateUsers(um);
                    if (user_window.ShowDialog() == true) UpdateData();
                    break;

                case "colors":
                    CreateUpdateColors color_window = new CreateUpdateColors(um);
                    if (color_window.ShowDialog() == true) UpdateData();
                    break;

                case "upholstery":
                    CreateUpdateUpholstery upholstery_window = new CreateUpdateUpholstery(um);
                    if (upholstery_window.ShowDialog() == true) UpdateData();
                    break;
            }
        }
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchString = searchTextBox.Text;
            var surnames = db.users;
            if (!String.IsNullOrEmpty(searchString))
            {
                var hsurnames = db.users.Where(s => s.surname.Contains(searchString)).ToList();
                dataGrid.ItemsSource = hsurnames;
            }
        }
        public static DataGridCell GetCell(DataGrid grid, int row, int column)
        {
            DataGridRow rowContainer = GetRow(grid, row);
            return GetCell(grid, rowContainer, column);
        }

        public static DataGridRow GetRow(DataGrid grid, int index)
        {
            DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                grid.UpdateLayout();
                grid.ScrollIntoView(grid.Items[index]);
                row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }
        public static DataGridCell GetCell(DataGrid grid, DataGridRow row, int column)
        {
            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }

        private static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        private void DataGridRow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var row = dataGrid.SelectedItem.ToString();
                string temp_row = row.Substring(row.IndexOf("ID") + 5);
                int id = Convert.ToInt32((temp_row.Substring(0, temp_row.IndexOf(","))));
                Index = id;
            }
        }
    }
}
