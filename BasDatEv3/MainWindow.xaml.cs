using BasDatEv3.DBActions;
using BasDatEv3.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BasDatEv3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button btn = new Button();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new PiecesView());
        }

        private void PiecesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!MainFrame.Content.Equals(new PiecesView()))
            {
                MainFrame.Navigate(new PiecesView());
            }
            
        }

        private void LocationsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new LocationsView();
        }
    }
}