using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Restaurant_gestion
{
    public partial class Pop_Up_addCommande : Window
    {
        public string Date { get; set; }
        public string Heure { get; set; }
        public string Plat { get; set; }
        public int Table { get; set; }
        public int Quantite { get; set; }
        public string Type { get; set; }
        public string Specification { get; set; }

        public Pop_Up_addCommande()
        {
            InitializeComponent();
        }

        private void buttonConfirm_click(object sender, RoutedEventArgs e)
        {
            bool status = true;

            if (string.IsNullOrEmpty(textBoxDate.Text))
            {
                Date = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                Date = textBoxDate.Text;
            }
            if (string.IsNullOrEmpty(textBoxHeure.Text))
            {
                Heure = DateTime.Now.ToString("HH:mm");
            }
            else
            {
                Heure = textBoxHeure.Text;
            }
            if (string.IsNullOrEmpty(textBoxPlat.Text))
            {
                MessageBox.Show("Please enter a dish");
                status = false;
            }
            else
            {
                Plat = textBoxPlat.Text;
            }
            if (string.IsNullOrEmpty(textBoxTable.Text))
            {
                MessageBox.Show("Please enter a table");
                status = false;
            }
            else
            {
                Table = int.Parse(textBoxTable.Text);
            }
            if (string.IsNullOrEmpty(textBoxQuantite.Text))
            {
                MessageBox.Show("Please enter a quantity");
                status = false;
            }
            else
            {
                Quantite = int.Parse(textBoxQuantite.Text);
            }
            if (string.IsNullOrEmpty(textBoxType.Text))
            {
                MessageBox.Show("Please enter a type");
                status = false;
            }
            else
            {
                Type = textBoxType.Text;
            }
            if (string.IsNullOrEmpty(textBoxSpecification.Text))
            {
                Specification = "None";
            }
            else
            {
                Specification = textBoxSpecification.Text;
            }

            if (status)
            {
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
