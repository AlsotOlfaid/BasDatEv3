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

namespace BasDatEv3.UIElements
{
    /// <summary>
    /// Interaction logic for Editer.xaml
    /// </summary>
    public partial class Editer : System.Windows.Controls.UserControl
    {
        public event EventHandler Click;
        public Editer()
        {
            InitializeComponent();
            mainButton.Click += ClickHandler;
        }

        private void ClickHandler(object sender, RoutedEventArgs e)
        {
            OnClick();
        }

        private void OnClick()
        {
            Click?.Invoke(this, EventArgs.Empty);
        }
    }
}
