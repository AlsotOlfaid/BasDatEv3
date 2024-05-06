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

namespace BasDatEv3.Views
{
    /// <summary>
    /// Interaction logic for UpdateLocationView.xaml
    /// </summary>
    public partial class UpdateLocationView : Window
    {
        private Location _oldLocation;

        private bool _insert = false;

        public bool Insert { get => _insert; set => _insert = value; }
        internal Location OldLocation { get => _oldLocation; set => _oldLocation = value; }

        public UpdateLocationView()
        {
            InitializeComponent();
            CbType.ItemsSource = App.LocationTypes;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Location loc = new Location();

            if (!string.IsNullOrEmpty(CbType.Text) || !string.IsNullOrEmpty(TxtCode.Text))
            {
                loc.LocationCode = CbType.Text + " " + TxtCode.Text;
            }

            try
            {
                string answer = MongoConnection.UpdateLocation(OldLocation, loc);

                if (answer.Length > 0)
                {
                    MessageBox.Show(answer, "Error");
                }
                else
                {
                    MessageBox.Show("La locacion se ha actualizado correctamente", "Information");
                    Insert = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
