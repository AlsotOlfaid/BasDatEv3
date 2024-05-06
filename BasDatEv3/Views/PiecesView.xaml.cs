using BasDatEv3.DBActions;
using BasDatEv3.Entities;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasDatEv3.Views
{
    /// <summary>
    /// Interaction logic for PiecesView.xaml
    /// </summary>
    public partial class PiecesView : Page
    {
        List<Piece> pieces = new List<Piece>();
        public PiecesView()
        {
            InitializeComponent();
            SetDataBase();
        }

        private void SetDataBase()
        {
            pieces = MongoConnection.GetCollectionData<Piece>("Pieces");
            MainDataGrid.ItemsSource = pieces;
        }

        private void InsertPiece_Click(object sender, RoutedEventArgs e)
        {
            InsertView ins = new InsertView();
            ins.ShowDialog();

            if (ins.Insert == true)
            {
                SetDataBase();
            }
        }

        private void BtnDeleter_Click(object sender, EventArgs e)
        {
            Piece piece = MainDataGrid.SelectedItem as Piece;

            MongoConnection.DeletePiece("Pieces", piece);
            SetDataBase();
        }

        private void MainSearchBar_TextChanged(object sender, EventArgs e)
        {
            string filter = MainSearchBar.Text.ToLower();

            if (filter != "buscar")
            {

                if (!string.IsNullOrEmpty(filter))
                {
                    var filteredData = pieces.Where(u =>

                        u.Name.ToLower().Contains(filter) ||
                        u.Type.ToLower().Contains(filter) ||
                        u.Cost.ToString().ToLower().Contains(filter) ||
                        u.Stock.ToString().ToLower().Contains(filter) ||
                        u.Location.LocationCode.ToLower().Contains(filter)

                        ).ToList();

                    MainDataGrid.ItemsSource = filteredData;
                }
                else
                {
                    MainDataGrid.ItemsSource = pieces;
                }
            }
        }
    }
}
