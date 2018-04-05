using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;

namespace DiakTanyaSystem
{
	public partial class Settings : System.Windows.Controls.UserControl
    {
        string selectedPizzaID = "999";
        string selectedTable = "Pizza_Étel";
        SQLiteConnection conn = new SQLiteConnection("Data Source=DiakTanyaAdatbazis.db");
        public Settings()
		{
            double UIScaleSetting = Properties.Settings.Default.UIScaleVar;
			InitializeComponent();

            bool windowAutoMax = Properties.Settings.Default.windowAutoMax;
            autoMAximize.IsChecked = windowAutoMax;


            bool noUnderDefPriceB = Properties.Settings.Default.noUnderDefPrice;
            noUnderDefPrice.IsChecked = noUnderDefPriceB;

            bool noMinusPriceB = Properties.Settings.Default.noMinusPrice;
            noMinusPrice.IsChecked = noMinusPriceB;


            Debug.WriteLine("UIScaleVar before " + Properties.Settings.Default.UIScaleVar);

               UIScaleSlider.Value = UIScaleSetting;

            // UI méret beállítás config fájlból
            /*
            double UIScaleSetting = Properties.Settings.Default.UIScaleVar;
            ScaleTransform scale = new ScaleTransform(UIScaleSetting, UIScaleSetting);
            ScrollViewer.LayoutTransform = scale;
             */

            var command = conn.CreateCommand();
            conn.Open();
            command.CommandText = "SELECT* FROM sqlite_master where type = 'table'";
            SQLiteDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                Debug.WriteLine(sdr.GetString(1));

                    RadioButton rb = new RadioButton() { Content = sdr.GetString(1)};
                    rb.Checked += (sender, args) =>
                    {
                        Console.WriteLine("Pressed " + (sender as RadioButton).Tag);
                        selectedTable = (sender as RadioButton).Tag.ToString();
                        refreshListBox(selectedTable);

                        if (selectedTable == "Pizza_Étel"
                        || selectedTable == "Saláta_Étel"
                        || selectedTable == "Hamburger_Étel"
                        || selectedTable == "Hotdog_Étel"
                        || selectedTable == "Gyros_Étel"
                        || selectedTable == "Desszert_Étel"
                        || selectedTable == "Egyéb_Étel")
                        {
                            tableInfoTB.Text = "Sorszám, Név, Kicsi ár, Közepes ár, Nagy ár Feltét 1, Feltét 2... (Max14 feltét)";
                        }else if (selectedTable == "Saláta_Étel")
                        {
                            tableInfoTB.Text = "Sorszám, Név, Kicsi ár, Közepes ár, Nagy ár Feltét 1, Feltét 2... (Max14 feltét)";
                        }



                    };
                    rb.Unchecked += (sender, args) => { /* Do stuff */ };
                rb.Foreground = new SolidColorBrush(Colors.White);
                rb.Tag = sdr.GetString(1);
                RadioButtonWrapPanel.Children.Add(rb);
            }
            sdr.Close();


            conn.Close();

        }

        private void newPizza_Click(object sender, RoutedEventArgs e)
        {
            newItem();
        }

        private void readPizzak_Click(object sender, RoutedEventArgs e)
        {
            refreshListBox(selectedTable);


        }
        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void deletePizzak_Click(object sender, RoutedEventArgs e)
        {
            deleteItem();
        }
        void selectedPizza(object sender, SelectionChangedEventArgs args)
        {
            System.Windows.Controls.ListBox lstbox = sender as System.Windows.Controls.ListBox;

            string selectedPizzaStr = lstbox.SelectedItem as string;

            if (selectedPizzaStr != null)
            {
                itemTextBox.Text = selectedPizzaStr;

                string[] words = selectedPizzaStr.Split(',');

                selectedPizzaID = words[0];
                Debug.WriteLine("selectedPizzaID "+selectedPizzaID);
            }
            statusTextBlock.Text = "Kiválasztott pizza: " + selectedPizzaStr;
        }

        private void editPizza_Click(object sender, RoutedEventArgs e)
        {
            deleteItem();
            newItem();
        }

        private void UIScaleSliderValueChanged(object sender, RoutedEventArgs e)
        {
           

            Properties.Settings.Default.UIScaleVar = UIScaleSlider.Value;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            Debug.WriteLine("UIScale sliderchange " + UIScaleSlider.Value);

            ScaleTransform scale = new ScaleTransform(UIScaleSlider.Value, UIScaleSlider.Value);
            ScrollViewer.LayoutTransform = scale;

        }

        private void refreshListBox(string table)
        {
            pizzaListBox.Items.Clear();
            conn.Open();
            var command = conn.CreateCommand();
            //Read from table
            Debug.WriteLine("RefreshListBox" + string.Format("SELECT * FROM {0} ORDER BY id", table));
                command.CommandText = string.Format("SELECT * FROM {0} ORDER BY id", table);


            SQLiteDataReader sdr = command.ExecuteReader();
            while (sdr.Read())
            {
                int count = sdr.FieldCount;
                string listBoxItem = "";
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        if (sdr.GetString(i) != "none")
                        {
                            listBoxItem += sdr.GetString(i) + ",";
                        }
                    }
                    catch (Exception e)
                    {                    }
                    try
                    {
                        listBoxItem += sdr.GetInt32(i) + ",";
                    }
                    catch (Exception e)
                    {                    }
                }
                listBoxItem = listBoxItem.Remove(listBoxItem.Length - 1);
                pizzaListBox.Items.Add(listBoxItem);
            }
             sdr.Close();
             conn.Close();
        }

        public void newItem()
        {
            conn.Open();
            var command = conn.CreateCommand();
            int columnCount = 0;

            command.CommandText = string.Format("PRAGMA table_info(" + selectedTable + ")");
            SQLiteDataReader sdr2 = command.ExecuteReader();

            while (sdr2.Read())
            {
                columnCount += 1;
            }
            sdr2.Close();

            command.CommandText = string.Format("SELECT * FROM {0}", selectedTable);
            SQLiteDataReader sdr1 = command.ExecuteReader();

            while (sdr1.Read())
            {
                //  columnCount = sdr1.FieldCount;
            }
            Debug.WriteLine("COUNT " + columnCount);
            sdr1.Close();


            string[] splitText = itemTextBox.Text.Split(',');
            string formattedText = "";

            for (var i = 0; i < splitText.Length - 1; i++)
            {
                if (i == 0)
                {
                    formattedText += splitText[i] + ", ";
                }
                else
                {
                    formattedText += "'" + splitText[i] + "', ";
                }

            }
            formattedText += "'" + splitText[splitText.Length - 1] + "'";

            string[] formattedTextSplit = formattedText.Split(',');
            int length = formattedTextSplit.Length;

            Debug.WriteLine("LenghtofShit: " + length);

            if (length < columnCount)
            {
                for (var i = 0; i < columnCount - length; i++)
                    formattedText += ",'none'";
            }

            if (length > columnCount)
            {
                NotificationHandler.sendToast("Túl sok adat a bevitelhez.", " ");
            }
            else
            {

                Debug.WriteLine(formattedText);
                //(id, name, priceS, priceM, priceL, feltet1, feltet2, feltet3, feltet4, feltet5, feltet6, feltet7, feltet8, feltet9, feltet10, feltet11, feltet12, feltet13, feltet14)
                //Inserting data
                string DataS = string.Format("INSERT INTO {0} values ({1})",
                    selectedTable, formattedText);


                Debug.WriteLine("DataS " + DataS);

                //Inserting data
                command.CommandText = DataS;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception exc)
                {
                    Debug.WriteLine("SQLEXCEPTION :" + exc);
                    statusTextBlock.Text = "Új pizza felvétele nem sikerült! Sorszám már létezik?";
                    NotificationHandler.sendToast("Új pizza felvétele nem sikerült!", "A sorszám már létezik? " + exc.ToString());
                }
            }
            conn.Close();
            refreshListBox(selectedTable);
        }
        public void deleteItem()
        {
            conn.Open();
            var command = conn.CreateCommand();

                command.CommandText = string.Format("DELETE FROM {0} WHERE id = {1}", selectedTable, selectedPizzaID);
                command.ExecuteNonQuery();
 
            
            conn.Close();
            refreshListBox(selectedTable);
        }
        void Handle(CheckBox autoMAximize)
        {
            bool flag = autoMAximize.IsChecked.Value;
            Properties.Settings.Default.windowAutoMax = flag;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
        private void autoMAximize_Checked(object sender, RoutedEventArgs e)
        {
            Handle(sender as CheckBox);
        }

        private void autoMAximize_Unchecked(object sender, RoutedEventArgs e)
        {
            Handle(sender as CheckBox);
        }
        private void newLineKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                newItem();
                itemTextBox.Text = "";
            }
        }

        void HandlenoUnderDefPrice(CheckBox noUnderDefPrice)
        {
            bool flag = noUnderDefPrice.IsChecked.Value;
            Properties.Settings.Default.noUnderDefPrice = flag;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
        private void noUnderDefPrice_Checked(object sender, RoutedEventArgs e)
        {
            HandlenoUnderDefPrice(sender as CheckBox);
        }

        private void noUnderDefPrice_Unchecked(object sender, RoutedEventArgs e)
        {
            HandlenoUnderDefPrice(sender as CheckBox);
        }

        void HandlenoMinusPrice(CheckBox noUnderDefPrice)
        {
            bool flag = noMinusPrice.IsChecked.Value;
            Properties.Settings.Default.noMinusPrice = flag;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
        private void noMinusPrice_Checked(object sender, RoutedEventArgs e)
        {
            HandlenoMinusPrice(sender as CheckBox);
        }

        private void noMinusPrice_Unchecked(object sender, RoutedEventArgs e)
        {
            HandlenoMinusPrice(sender as CheckBox);
        }

    }
}