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
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : System.Windows.Controls.UserControl    
    {
        private string _text;
        public string Text { get => _text = txtSearch.Text; set => _text = value; }

        public event EventHandler TextChanged;

        public SearchBar()
        {
            InitializeComponent();

            txtSearch.TextChanged += TextChangedHandler;
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                // Si el TextBox esta vacio
                if (textBox.Text.Trim() == "")
                {
                    textBox.Text = "Buscar";
                }
            }
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                textBox.Text = "";
            }
        }

        private void TextChangedHandler(object sender, RoutedEventArgs e)
        {
            OnChange();
        }

        private void OnChange()
        {
            TextChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
