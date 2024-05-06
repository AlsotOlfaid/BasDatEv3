using BasDatEv3.DBActions;
using BasDatEv3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for LocationsView.xaml
    /// </summary>
    public partial class LocationsView : Page
    {
        public LocationsView()
        {
            InitializeComponent();
            SetDataBase();
        }

        private void SetDataBase()
        {
            List<Location> locationsList = MongoConnection.GetCollectionData<Location>("Locations");
            MainDatagrid.ItemsSource = locationsList;
        }

        private void InsertLocation_Click(object sender, RoutedEventArgs e)
        {
            InsertLocationView ins = new InsertLocationView();
            ins.ShowDialog();

            if (ins.Insert == true)
            {
                SetDataBase();
            }
        }

        private void BtnDeleter_Click(object sender, EventArgs e)
        {
            Location loc = MainDatagrid.SelectedItem as Location;

            MongoConnection.DeleteLocation("Locations", loc);
            SetDataBase();
        }

        private void BtnUpdater_Click(object sender, EventArgs e)
        {
            UpdateLocationView ins = new UpdateLocationView();
            Location loc = MainDatagrid.SelectedItem as Location;

            ins.OldLocation = loc;
            ins.CbType.Text = loc.LocationCode.Split(' ')[0];
            ins.TxtCode.Text = loc.LocationCode.Split(' ')[1];
            ins.ShowDialog();

            if (ins.Insert == true)
            {
                Location newLoc = new Location();
                newLoc.LocationCode = ins.CbType.Text + " " + ins.TxtCode.Text;

                MongoConnection.UpdateLocation(loc, newLoc);
                SetDataBase();
            }
        }
    }
}
