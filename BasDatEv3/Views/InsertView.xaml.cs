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
using System.Windows.Shapes;
using ThirdParty.BouncyCastle.Asn1;

namespace BasDatEv3.Views
{
    /// <summary>
    /// Interaction logic for InsertView.xaml
    /// </summary>
    public partial class InsertView : Window
    {
        private bool _insert = false;

        public bool Insert { get => _insert; set => _insert = value; }

        public InsertView()
        {
            InitializeComponent();
            CbType.ItemsSource = App.PiecesTypes;
            CbLocations.ItemsSource = MongoConnection.GetLocationsNames();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            Piece piece = new Piece();
            piece.Name = TxtName.Text;
            piece.Cost = double.Parse(TxtCost.Text);
            piece.Type = CbType.Text;

            List<Location> data = MongoConnection.GetCollectionData<Location>("Locations");
            Location loc = data.Where(l => l.LocationCode == CbLocations.Text).Select(l => l).Single();

            piece.Location = loc;
            piece.Stock = int.Parse( TxtStock.Text);
            piece.Date = DateTime.Now;

            try
            {
                string answer = MongoConnection.InsertPiece(piece);

                if (answer.Length > 0)
                {
                    MessageBox.Show(answer, "Error");
                }
                else
                {
                    MessageBox.Show("El elemento se ha insertado con exito", "Information");
                    Insert = true;
                    this.Close();
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information");
            }       
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
