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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasDatEv3.UIElements
{
    /// <summary>
    /// Interaction logic for Deleter.xaml
    /// </summary>
    public partial class Deleter : System.Windows.Controls.UserControl
    {
        public event EventHandler Click;

        public Deleter()
        {
            InitializeComponent();
            mainButton.Click += MainButton_Click;
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            OnClick();
        }

        private void OnClick()
        {
            Click?.Invoke(this, EventArgs.Empty);
        }
    }
}
