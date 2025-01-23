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

namespace Restaurant_gestion
{
    /// <summary>
    /// Interaction logic for Pop_Up_addRestaurant.xaml
    /// </summary>
    public partial class Pop_Up_addRestaurant : Window
    {
        public string Name { get; set; }
        public string Place { get; set; }
        public string XXX { get; set; }

        public Pop_Up_addRestaurant()
        {
            InitializeComponent();
        }

        private void buttonConfirm_click(object sender, RoutedEventArgs e)
        {
            bool status = true;

            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Please enter a name");
                status = false;
            }
            if (string.IsNullOrEmpty(textBoxPlace.Text))
            {
                MessageBox.Show("Please enter a name");
                status = false;
            }
            if (string.IsNullOrEmpty(textBoxXXX.Text))
            {
                MessageBox.Show("Please enter a name");
                status = false;
            }

            Name = textBoxName.Text;
            Place = textBoxPlace.Text;
            XXX = textBoxXXX.Text;

            if (status)
            {
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
