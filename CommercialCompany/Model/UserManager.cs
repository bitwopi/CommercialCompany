using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CommercialCompany.Model
{
    public class UserManager
    {
        public static Window Authorization;
        public dynamic User { get; set; }
        public DataGrid UserGrid { get; set; }
        public List<string> columnsNames = new List<string>();
        public string State { get; set; }
        public string TableState { get; set; }
        public dynamic Table { get; set; }
        public int Index { get; set; }

        private static void Window_Closed(object sender, EventArgs e)
        {
            Authorization.Close();
        }

        public void Sorting()
        {
            ICollectionView view =
            CollectionViewSource.GetDefaultView(UserGrid.ItemsSource);
            view.SortDescriptions.Clear();
            foreach (var i in this.columnsNames)
            {
                SortDescription sortingColumn = new SortDescription(i, ListSortDirection.Ascending);
                view.SortDescriptions.Add(sortingColumn);
            }
            this.columnsNames.Clear();
        }
    }
}
