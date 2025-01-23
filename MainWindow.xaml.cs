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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public string path { get; set; } = "C:\\Users\\" + Environment.UserName + "\\Documents";
        public string path { get; set; } = "C:\\Users\\" + Environment.UserName + "\\Documents\\Restaurant_gestion";
        public string filePath { get; set; } = "C:\\Users\\" + Environment.UserName + "\\Documents\\Restaurant_gestion\\restaurant.txt";

        public string Name { get; set; }
        public string Place { get; set; }
        public string XXX { get; set; }

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
            bool lineXXX = false;

            using (StreamReader sr = new StreamReader(fileChoice()))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
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
                    if (line.Contains("XXX :"))
                    {
                        lineXXX = true;
                        XXX = line.Split(":")[1];
                    }

                    if (lineRestaurent && lineName && linePlace && lineXXX)
                    {
                        restaurantName.Content = Name;
                        restaurantPlace.Content = Place;
                    }
                }
            }
        }

        public void open_restaurant_automatically(string name, string place, string xxx)
        {
            restaurantName.Content = name;
            restaurantPlace.Content = place;
        }

        private void etendu_stocks(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = new StackPanel();

            TextBlock textBlock1 = new() { Text = "Étendu des stocks : ", FontSize = 24, Padding = new Thickness(10)};
            TextBlock textBlock2 = new() { Text = "1) Viande Haché - 4kg\n2) Blanc de poulet - 5kg\n3) Poivrons - 500g", Padding = new Thickness(10,0,0,0) };

            stackPanel.Children.Add(textBlock1);
            stackPanel.Children.Add(textBlock2);

            etendu_stocks_control.Content = stackPanel;
        }

        

        private void achats(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = new StackPanel();

            TextBlock textBlock1 = new() { Text = "Achats : ", FontSize = 24, Padding = new Thickness(10) };
            TextBlock textBlock2 = new() { Text = "Nourriture :", FontSize = 15, Padding = new Thickness(10, 0, 0, 2) };
            TextBlock textBlock3 = new() { Text = "- 15 kg Riz - 50CHF\n- 4kg Tomates - 20CHF\n- 6 kg Steak Cheval - 90CHF", Padding = new Thickness(18, 0, 0, 0) };

            stackPanel.Children.Add(textBlock1);
            stackPanel.Children.Add(textBlock2);
            stackPanel.Children.Add(textBlock3);

            etendu_stocks_control.Content = stackPanel;
        }

        private void finances(object sender, RoutedEventArgs e)
        {
            ScrollViewer scrollViewer = new();
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            StackPanel stackPanel = new StackPanel();

            TextBlock textBlock1 = new() { Text = "Finances : ", FontSize = 24, Padding = new Thickness(10) };
            TextBlock textBlock2 = new() { Text = "Dépenses du mois :", FontSize = 15, Padding = new Thickness(10, 0, 0, 2) };
            TextBlock textBlock3 = new() { Text = "- Nourriture : 560CHF\n- Employés : 15'000CHF\n- Infrastructure : 3'600CHF", Padding = new Thickness(18, 0, 0, 0) };

            TextBlock textBlock4 = new() { Text = "Revenus du mois :", FontSize = 15, Padding = new Thickness(10, 25, 0, 0) };
            TextBlock textBlock5 = new() { Text = "- 01.01.2025 : 560CHF\n- 02.01.2025 : 570CHF\n- 03.01.2025 : 360CHF\n- 04.01.2025 : 560CHF\n- 05.01.2025 : 560CHF\n- 06.01.2025 : 560CHF\n- 07.01.2025 : 560CHF\n- 08.01.2025 : 560CHF\n- 09.01.2025 : 560CHF\n- 10.01.2025 : 560CHF\n- 11.01.2025 : 560CHF\n- 12.01.2025 : 560CHF\n- 13.01.2025 : 560CHF\n- 14.01.2025 : 560CHF\n- 15.01.2025 : 560CHF\n- 16.01.2025 : 560CHF\n- 17.01.2025 : 560CHF\n- 18.01.2025 : 560CHF\n- 19.01.2025 : 560CHF\n", Padding = new Thickness(18, 2, 0, 0) };

            TextBlock textBlock6 = new() { Text = "Total :", FontSize = 15, Padding = new Thickness(10, 25, 0, 0)};
            TextBlock textBlock7 = new() { Text = "5678CHF", Padding = new Thickness(18,0, 0, 10) };



            stackPanel.Children.Add(textBlock1);
            stackPanel.Children.Add(textBlock2);
            stackPanel.Children.Add(textBlock3);
            stackPanel.Children.Add(textBlock4);
            stackPanel.Children.Add(textBlock5);
            stackPanel.Children.Add(textBlock6);
            stackPanel.Children.Add(textBlock7);

            scrollViewer.Content = stackPanel;

            etendu_stocks_control.Content = scrollViewer;
        }

        private void liste_employes(object sender, RoutedEventArgs e)
        {
            ScrollViewer scrollViewer = new();
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            StackPanel stackPanel = new StackPanel();

            TextBlock textBlock1 = new() { Text = "Liste des employés : ", FontSize = 24, Padding = new Thickness(10) };

            People = new ObservableCollection<Person>
            {
                new Person {Nom = "Alice", Prenom = "Bob", Poste = "Cuisinière"},
                new Person {Nom = "Achille", Prenom = "Gollay", Poste = "Serveur"},
                new Person {Nom = "Mafille", Prenom = "Quentin", Poste = "Femme de ménage"},
            };

            DataGrid dataGrid = new DataGrid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                AutoGenerateColumns = true,
                ItemsSource = People
            };


            stackPanel.Children.Add(textBlock1);
            stackPanel.Children.Add(dataGrid);
            
            scrollViewer.Content = stackPanel;

            etendu_stocks_control.Content = scrollViewer;
        }
    }
}