using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
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

namespace DiakTanyaSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            bool windowAutoMax = Properties.Settings.Default.windowAutoMax;
            Debug.WriteLine("windowAutoMax " + windowAutoMax);

            if (windowAutoMax == true)
            {
                this.WindowState = WindowState.Maximized;
            }
            

            mainFrame.Source = new Uri("/Menu/MainMenu.xaml", UriKind.Relative);
            
            SQLiteConnection conn = new SQLiteConnection("Data Source=DiakTanyaAdatbazis.db");
            conn.Open();
            var command = conn.CreateCommand();
            //table Create
            command.CommandText = "CREATE TABLE IF NOT EXISTS Rendeles(id int, type Varchar(30), RID int, unixTime int, size Varchar(30), orderName Varchar(30), price Varchar(30), name Varchar(30), address Varchar(30), extra1 Varchar(30), extra2 Varchar(30), extra3 Varchar(30), extra4 Varchar(30), extra5 Varchar(30), extra6 Varchar(30), extra7 Varchar(30), extra8 Varchar(30), extra9 Varchar(30), extra10 Varchar(30))";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Pizza_Étel(id int PRIMARY KEY, name Varchar(30), priceS Varchar(30), priceM Varchar(30), priceL Varchar(30), feltet1 Varchar(30), feltet2 Varchar(30), feltet3 Varchar(30), feltet4 Varchar(30), feltet5 Varchar(30), feltet6 Varchar(30), feltet7 Varchar(30), feltet8 Varchar(30), feltet9 Varchar(30), feltet10 Varchar(30), feltet11 Varchar(30), feltet12 Varchar(30), feltet13 Varchar(30), feltet14 Varchar(30))";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Saláta_Étel(id int PRIMARY KEY, name Varchar(30), price Varchar(30), feltet1 Varchar(30), feltet2 Varchar(30), feltet3 Varchar(30), feltet4 Varchar(30), feltet5 Varchar(30), feltet6 Varchar(30), feltet7 Varchar(30), feltet8 Varchar(30), feltet9 Varchar(30), feltet10 Varchar(30), feltet11 Varchar(30), feltet12 Varchar(30), feltet13 Varchar(30), feltet14 Varchar(30))";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Hamburger_Étel(id int PRIMARY KEY, name Varchar(30), price Varchar(30), feltet1 Varchar(30), feltet2 Varchar(30), feltet3 Varchar(30), feltet4 Varchar(30), feltet5 Varchar(30), feltet6 Varchar(30), feltet7 Varchar(30), feltet8 Varchar(30), feltet9 Varchar(30), feltet10 Varchar(30), feltet11 Varchar(30), feltet12 Varchar(30), feltet13 Varchar(30), feltet14 Varchar(30))";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Gyros_Étel(id int PRIMARY KEY, name Varchar(30), price Varchar(30), feltet1 Varchar(30), feltet2 Varchar(30), feltet3 Varchar(30), feltet4 Varchar(30), feltet5 Varchar(30), feltet6 Varchar(30), feltet7 Varchar(30), feltet8 Varchar(30), feltet9 Varchar(30), feltet10 Varchar(30), feltet11 Varchar(30), feltet12 Varchar(30), feltet13 Varchar(30), feltet14 Varchar(30))";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Hotdog_Étel(id int PRIMARY KEY, name Varchar(30), price Varchar(30), feltet1 Varchar(30), feltet2 Varchar(30), feltet3 Varchar(30), feltet4 Varchar(30), feltet5 Varchar(30), feltet6 Varchar(30), feltet7 Varchar(30), feltet8 Varchar(30), feltet9 Varchar(30), feltet10 Varchar(30), feltet11 Varchar(30), feltet12 Varchar(30), feltet13 Varchar(30), feltet14 Varchar(30))";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Desszert_Étel(id int PRIMARY KEY, name Varchar(30), price Varchar(30), feltet1 Varchar(30), feltet2 Varchar(30), feltet3 Varchar(30), feltet4 Varchar(30), feltet5 Varchar(30), feltet6 Varchar(30), feltet7 Varchar(30), feltet8 Varchar(30), feltet9 Varchar(30), feltet10 Varchar(30), feltet11 Varchar(30), feltet12 Varchar(30), feltet13 Varchar(30), feltet14 Varchar(30))";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Egyéb_Étel(id int PRIMARY KEY, name Varchar(30), price Varchar(30), feltet1 Varchar(30), feltet2 Varchar(30), feltet3 Varchar(30), feltet4 Varchar(30), feltet5 Varchar(30), feltet6 Varchar(30), feltet7 Varchar(30), feltet8 Varchar(30), feltet9 Varchar(30), feltet10 Varchar(30), feltet11 Varchar(30), feltet12 Varchar(30), feltet13 Varchar(30), feltet14 Varchar(30))";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Ital(id int PRIMARY KEY, name Varchar(30), price Varchar(30), feltet1 Varchar(30), feltet2 Varchar(30), feltet3 Varchar(30), feltet4 Varchar(30), feltet5 Varchar(30), feltet6 Varchar(30), feltet7 Varchar(30), feltet8 Varchar(30), feltet9 Varchar(30), feltet10 Varchar(30), feltet11 Varchar(30), feltet12 Varchar(30), feltet13 Varchar(30), feltet14 Varchar(30))";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Öntet(id int PRIMARY KEY, name Varchar(30), price Varchar(30))";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Szósz(id int PRIMARY KEY, name Varchar(30), price Varchar(30))";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS GombSzín(id int PRIMARY KEY, name Varchar(30), RED int, GREEN int, BLUE int)";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Feltétek(id int PRIMARY KEY, name Varchar(30), priceS Varchar(30), priceM Varchar(30), priceL Varchar(30))";
            command.ExecuteNonQuery();
            conn.Close();

        }
        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }
        public void searchselected(object sender, RoutedEventArgs e)
        {
            mainFrame.Source = new Uri("/Menu/Rendelesfelvetel.xaml", UriKind.Relative);
        }
        public void settingsSelected(object sender, RoutedEventArgs e)
        {
            mainFrame.Source = new Uri("/Menu/Settings.xaml", UriKind.Relative);
        }
        public void ordersSelected(object sender, RoutedEventArgs e)
        {
            mainFrame.Source = new Uri("/Menu/Orders.xaml", UriKind.Relative);
        }
    }
}
