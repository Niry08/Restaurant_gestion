using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
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

namespace Restaurant_gestion
{
    public partial class MainWindow : Window
    {
        public string path { get; set; } = "C:\\Users\\" + Environment.UserName + "\\Documents\\Restaurant_gestion";
        public string filePath { get; set; } = "C:\\Users\\" + Environment.UserName + "\\Documents\\Restaurant_gestion\\restaurant.txt";

        public string Name { get; set; }
        public string Place { get; set; }
        public string Type { get; set; }

        List<Stocks> stocks = new();
        List<Nourriture> nourritures = new();
        List<Fourniture> fournitures = new();
        List<Depense> depenses = new();
        List<Person> persons = new();
        List<Revenus> revenus = new();
        List<Commandes> commandes = new();

        public ObservableCollection<Person> People { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void create_restaurent(object sender, RoutedEventArgs e)
        {
            try // Création dossier
            {
                Directory.CreateDirectory(path);
            }
            catch
            {
                Console.WriteLine("Error : creation of the directory");
            }

            try // Création fichier
            {
                string filePath = System.IO.Path.Combine(path, "restaurant.txt");
                using (StreamWriter sw = new StreamWriter(filePath)) { }
            }
            catch
            {
                Console.WriteLine("Error : creation of the file");
            }

            Pop_Up_addRestaurant popUpNewRestaurant = new Pop_Up_addRestaurant();

            bool? result = popUpNewRestaurant.ShowDialog();


            if (result == true)
            {
                string name = popUpNewRestaurant.Name;
                string place = popUpNewRestaurant.Place;
                string xxx = popUpNewRestaurant.XXX;

                newRestaurantSerialize(name, place, xxx);
            }
        }

        private void newRestaurantSerialize(string name, string place, string xxx) // ca écrase le fichier
        {
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine("Restaurant : \n" + "\tName : " + name + "\n\tEmplacement : " + place + "\n\tXXX : " + xxx);
            }

            open_restaurant_automatically(name, place, xxx);
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        public string fileChoice()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
            }

            return filePath;
        }

        public void open_restaurant(object sender, RoutedEventArgs e)
        {
            bool lineRestaurent = false;
            bool lineName = false;
            bool linePlace = false;
            bool lineType = false;

            using (StreamReader sr = new StreamReader(fileChoice()))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // Restaurant :
                    if (line.Contains("Restaurant :"))
                    {
                        lineRestaurent = true;
                    }
                    if (line.Contains("Name :"))
                    {
                        lineName = true;
                        Name = line.Split(":")[1];
                    }
                    if (line.Contains("Emplacement :"))
                    {
                        linePlace = true;
                        Place = line.Split(":")[1];
                    }
                    if (line.Contains("Type :"))
                    {
                        lineType = true;
                        Type = line.Split(":")[1];
                    }

                    if (lineRestaurent && lineName && linePlace && lineType)
                    {
                        StackPanel stackPanel = new StackPanel();

                        TextBlock textBlock1 = new TextBlock() { Text = Name, FontSize = 24, HorizontalAlignment = HorizontalAlignment.Center };
                        TextBlock textBlock2 = new TextBlock() { Text = Place, FontSize = 12, HorizontalAlignment = HorizontalAlignment.Right, Padding= new Thickness(0,0,20,0) };

                        stackPanel.Children.Add(textBlock1);
                        stackPanel.Children.Add(textBlock2);

                        data.Content = stackPanel;
                    }

                    if (line.Contains("Stock : "))
                    {
                        string[] parts = line.Split(new string[] { " : ", " - " }, StringSplitOptions.None);
                        string name = parts[1];
                        string poids = parts[2];
                        stocks.Add(new Stocks { Nom = name, QuantitePoids = poids });
                    }
                    if (line.Contains("Nourriture :"))
                    {
                        string[] parts = line.Split(new string[] { " : ", " - " }, StringSplitOptions.None);
                        string name = parts[1];
                        string poids = parts[2];
                        string prix = parts[3];
                        nourritures.Add(new Nourriture { Nom = name, QuantitePoids = poids, Prix = prix });
                    }
                    if (line.Contains("Fourniture :"))
                    {
                        string[] parts = line.Split(new string[] { " : ", " - " }, StringSplitOptions.None);
                        string name = parts[1];
                        string prix = parts[2];
                        fournitures.Add(new Fourniture { Nom = name, Prix = prix });
                    }
                    if (line.Contains("Employés :"))
                    {
                        string[] parts = line.Split(new string[] { " : ", "\t" }, StringSplitOptions.None);
                        string name = parts[3];
                        string prenom = parts[5];
                        string adresse = parts[7];
                        string telephone = parts[9];
                        string email = parts[11];
                        string dateNaissance = parts[13];
                        string poste = parts[15];
                        string salaire = parts[17];
                        persons.Add(new Person { Nom = name, Prenom = prenom, Adresse = adresse, Telephone = telephone, Email = email, DateNaissance = dateNaissance, Poste = poste, Salaire = double.Parse(salaire) });
                    }
                    if (line.Contains("Dépense :")) {
                        string[] parts = line.Split(new string[] { " : ", "-" }, StringSplitOptions.None);
                        string date = parts[1];
                        string name = parts[2];
                        string prix = parts[3];
                        depenses.Add(new Depense { date = DateTime.Parse(date), Nom = name, Prix = prix });
                    }
                    if (line.Contains("Revenus du mois :"))
                    {
                        string[] parts = line.Split(new string[] { " : ", "-" }, StringSplitOptions.None);
                        string date = parts[1];
                        string valeur = parts[2];
                        revenus.Add(new Revenus { date = DateTime.Parse(date), Revenu = double.Parse(valeur) });
                    }
                }
            }
        }

        public void open_restaurant_automatically(string name, string place, string xxx)
        {
            data.Content = name;
            data.Content = place;
        }

        private void etendu_stocks(object sender, RoutedEventArgs e)
        {
            ScrollViewer scrollViewer = new();
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            StackPanel stackPanel = new StackPanel();

            TextBlock textBlock = new() { Text = "Étendu des stocks : ", FontSize = 24, Padding = new Thickness(10) };

            stackPanel.Children.Add(textBlock);

            foreach (Stocks stock in stocks)
            {
                string text = stock.Nom + " - " + stock.QuantitePoids;
                TextBlock textBlockStocks = new() { Text = text, Padding = new Thickness(10, 0, 0, 0) };
                stackPanel.Children.Add(textBlockStocks);
            }

            scrollViewer.Content = stackPanel;

            data.Content = scrollViewer;
        }        

        private void achats(object sender, RoutedEventArgs e)
        {
            ScrollViewer scrollViewer = new();
            StackPanel stackPanel = new StackPanel();

            TextBlock textBlock1 = new() { Text = "Achats : ", FontSize = 24, Padding = new Thickness(10) };
            TextBlock textBlock2 = new() { Text = "Nourriture :", FontSize = 18, FontWeight= FontWeights.Bold, Padding = new Thickness(10, 0, 0, 5) };

            stackPanel.Children.Add(textBlock1);
            stackPanel.Children.Add(textBlock2);

            foreach (Nourriture nourriture in nourritures)
            {
                string text = nourriture.Nom + " - " + nourriture.Prix + " - " + nourriture.QuantitePoids;
                TextBlock textBlockStocks = new() { Text = text, Padding = new Thickness(10, 0, 0, 0) };
                stackPanel.Children.Add(textBlockStocks);
            }

            TextBlock textBlock3 = new() { Text = "Fournitures :", FontSize = 18, FontWeight= FontWeights.Bold, Padding = new Thickness(10, 20, 0, 5) };
            stackPanel.Children.Add(textBlock3);

            foreach (Fourniture fourniture in fournitures)
            {
                string text = fourniture.Nom + " - " + fourniture.Prix;
                TextBlock textBlockStocks = new() { Text = text, Padding = new Thickness(10, 0, 0, 0) };
                stackPanel.Children.Add(textBlockStocks);
            }

            scrollViewer.Content = stackPanel;
            data.Content = scrollViewer;
        }

        private void finances(object sender, RoutedEventArgs e)
        {
            ScrollViewer scrollViewer = new();
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            StackPanel stackPanel = new StackPanel();

            List<DateTime> dates = new();
            double total = 0.0;

            foreach (Revenus r in revenus)
            {
                dates.Add(r.date);
                dates.Sort();
            }

            TextBlock textBlock1 = new() { Text = "Finances : ", FontSize = 24, Padding = new Thickness(10) };
            TextBlock textBlock2 = new() { Text = "Dépenses du mois :", FontSize = 15, Padding = new Thickness(10, 0, 0, 2) };
            stackPanel.Children.Add(textBlock1);
            stackPanel.Children.Add(textBlock2);

            foreach (Depense d in depenses)
            {
                TextBlock textBlock = new() { Text = d.Nom + " : " + d.Prix + "CHF" + " --> " + d.date.ToString("d"), Padding = new Thickness(18, 0, 0, 0) };
                stackPanel.Children.Add(textBlock);
            }

            TextBlock textBlock4 = new() { Text = "Revenus du mois :", FontSize = 15, Padding = new Thickness(10, 25, 0, 0) };
            stackPanel.Children.Add(textBlock4);

            foreach (DateTime d in dates)
            {
                foreach (Revenus r in revenus)
                {
                    if (r.date == d)
                    {
                        TextBlock textBlock = new() { Text = "- " + d.ToString("d") + " : " + r.Revenu + "CHF", Padding = new Thickness(18, 0, 0, 0) };
                        stackPanel.Children.Add(textBlock);
                        total += r.Revenu;
                    }
                }
            }

            TextBlock textBlock6 = new() { Text = "Total :", FontSize = 15, Padding = new Thickness(10, 25, 0, 0)};
            TextBlock textBlock7 = new() { Text = total.ToString() + "CHF", Padding = new Thickness(18,0, 0, 10) };

            stackPanel.Children.Add(textBlock6);
            stackPanel.Children.Add(textBlock7);

            scrollViewer.Content = stackPanel;

            data.Content = scrollViewer;
        }

        private void liste_employes(object sender, RoutedEventArgs e)
        {
            ScrollViewer scrollViewer = new();
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            StackPanel stackPanel = new StackPanel();

            TextBlock textBlock1 = new() { Text = "Liste des employés : ", FontSize = 24, Padding = new Thickness(10) };


            DataGrid dataGrid = new DataGrid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                AutoGenerateColumns = true,
                ItemsSource = persons
            };


            stackPanel.Children.Add(textBlock1);
            stackPanel.Children.Add(dataGrid);
            
            scrollViewer.Content = stackPanel;

            data.Content = scrollViewer;
        }

        private void commandesListe(object sender, RoutedEventArgs e)
        {
            ScrollViewer scrollViewer = new();
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            StackPanel stackPanel = new StackPanel();

            TextBlock textBlock1 = new() { Text = "Liste des commandes : ", FontSize = 24, Padding = new Thickness(10) };


            DataGrid dataGrid = new DataGrid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                AutoGenerateColumns = true,
                ItemsSource = commandes
            };

            stackPanel.Children.Add(textBlock1);
            stackPanel.Children.Add(dataGrid);

            scrollViewer.Content = stackPanel;

            data.Content = scrollViewer;
        }

        private void addCommande(object sender, RoutedEventArgs e)
        {
            Pop_Up_addCommande popUpNewCommande = new Pop_Up_addCommande();

            bool? result = popUpNewCommande.ShowDialog();

            if (result == true)
            {
                string date = popUpNewCommande.Date;
                string heure = popUpNewCommande.Heure;
                string plat = popUpNewCommande.Plat;
                int table = popUpNewCommande.Table;
                int quantite = popUpNewCommande.Quantite;
                string type = popUpNewCommande.Type;
                string specification = popUpNewCommande.Specification;

                commandes.Add(new Commandes { Date = date, Heure = heure, Plat = plat, Table = table, Quantite = quantite, Type = type, Specification = specification });
            
                commandesListe(sender, e);
            }
        }

        private void enregistrer_sous(object sender, RoutedEventArgs e)
        {
            string fichierSource = filePath;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Enregistrer sous",
                Filter = "Fichiers texte (*.txt)|*.txt|Tous les fichiers (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string fichierDestination = saveFileDialog.FileName;

                try
                {
                    File.Copy(fichierSource, fichierDestination, true); // Copie avec remplacement si besoin
                    Console.WriteLine("Fichier enregistré sous : " + fichierDestination);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Erreur : Aucun fichier sélectionné");
            }

        }
    }
}