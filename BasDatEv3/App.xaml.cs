using BasDatEv3.DBActions;
using System.Configuration;
using System.Data;
using System.Windows;

namespace BasDatEv3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static List<string> _piecesTypes = new List<string> { "Consumible", "Refaccion", "Quimico"};
        private static List<string> _locationTypes = new List<string> { "Rack", "ElevadorA1", "ElevadorA2", "Locker"};

        public static List<string> PiecesTypes { get => _piecesTypes; set => _piecesTypes = value; }
        public static List<string> LocationTypes { get => _locationTypes; set => _locationTypes = value; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            StartConnection();
        }

        private void StartConnection()
        {
            MongoConnection.OpenConnection();
            MongoConnection.DropIfExists("evidence3");
            MongoConnection.CreateCollection("Pieces");
            MongoConnection.CreateCollection("Locations");
        }
    }

}
