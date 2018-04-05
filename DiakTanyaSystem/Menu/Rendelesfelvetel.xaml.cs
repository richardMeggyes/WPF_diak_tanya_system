using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DiakTanyaSystem
{
    public partial class Rendelesfelvetel : UserControl
    {
        SQLiteConnection conn = new SQLiteConnection("Data Source=DiakTanyaAdatbazis.db");
        string rendeles = "";
        int OrderNumberOfItem = 1;
        string cOrderNumberOfItem = "";
        string cOrderType = "";
        string cOrderNumber = "";
        string cOrderSize = "";
        string cOrderSub3 = "";
        string cOrderSub4 = "";
        string cOrderPlus = "";
        string cOrderMinus = "";

        List<String> GombSzinLista = new List<String>();
        List<String> RendelesLista = new List<String>();

        public Rendelesfelvetel()
        {
            string selectedPizzaID = "999";
            string selectedTable = "Pizza_Étel";
            byte RED = 0;
            byte GREEN = 0;
            byte BLUE = 0;


            InitializeComponent();

            double UIScaleSetting = Properties.Settings.Default.UIScaleVar;
            ScaleTransform scale = new ScaleTransform(UIScaleSetting, UIScaleSetting);
            ScrollViewer.LayoutTransform = scale;

            var command = conn.CreateCommand();
            conn.Open();



            command.CommandText = "SELECT * FROM GombSzín";
            SQLiteDataReader sdr3 = command.ExecuteReader();

            while (sdr3.Read())
            {
                GombSzinLista.Add(sdr3.GetString(1) + "," + sdr3.GetInt32(2).ToString() + "," + sdr3.GetInt32(3).ToString() + "," + sdr3.GetInt32(4).ToString());
            }
            sdr3.Close();





            command.CommandText = "SELECT* FROM sqlite_master where type = 'table'";
            SQLiteDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                Debug.WriteLine(sdr.GetString(1));
                Button newBtn = new Button();



                if (sdr.GetString(1).Contains("_Étel"))
                {
                    string str = sdr.GetString(1);

                    string[] sdrSplit = str.Split('_');

                    double myMargin = 5;
                    myMargin = Math.Round(myMargin / 2, 0);
                    //  newBtn.Margin = new Thickness(myMargin);
                    LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 255), new Point(0.5, 0.6), new Point(0.5, 1));

                    RED = 0;
                    GREEN = 0;
                    BLUE = 0;

                    for (int i = 0; i < GombSzinLista.Count; i++)
                    {
                        Debug.WriteLine("Lista: " + GombSzinLista[i]);

                        string[] splitlist = GombSzinLista[i].Split(',');
                        if (splitlist[0] == sdrSplit[0])
                        {
                            Debug.WriteLine("Lista: " + splitlist[0] + "," + splitlist[1]);
                            try
                            {
                                int REDInt = Int32.Parse(splitlist[1]);
                                int GREENInt = Int32.Parse(splitlist[2]);
                                int BLUEInt = Int32.Parse(splitlist[3]);

                                RED = (byte)(REDInt);
                                GREEN = (byte)(GREENInt);
                                BLUE = (byte)(BLUEInt);
                            }
                            catch
                            {

                            }

                        }
                    }

                    RadialGradientBrush RadGrad = new RadialGradientBrush(Color.FromRgb(RED, GREEN, BLUE), Color.FromRgb(0, 0, 0));
                    RadGrad.Center = new Point(0.5, 1);
                    RadGrad.GradientOrigin = new Point(0.5, 1);
                    RadGrad.RadiusX = 1;
                    RadGrad.RadiusY = 0.5;
                    newBtn.Background = RadGrad;

                    newBtn.Foreground = new SolidColorBrush(Colors.White);
                    newBtn.Content = sdrSplit[0];
                    newBtn.Name = sdr.GetString(1);
                    newBtn.Width = 100;
                    newBtn.Height = 100;
                    newBtn.FontSize = 16;
                    newBtn.FontWeight = FontWeights.Bold;
                    newBtn.Click += (sender, args) =>
                    {
                        resetRendeles();

                        Console.WriteLine("Pressed " + (sender as Button).Content);
                        //     if ((sender as Button).Content.ToString() == "Pizza")
                        //    {
                        Console.WriteLine("Pressed " + (sender as Button).Content);
                        cOrderType = (sender as Button).Content.ToString();
                        updateCurrentOrder();
                        subMenu1((sender as Button).Name, sdrSplit[0]);
                        /*
                    }else
                    {
                        Console.WriteLine("Shit isnt working " + (sender as Button).Content);
                    }
                    */
                    };
                    mainWrapPanel.Children.Add(newBtn);

                }
            }
            sdr.Close();
            conn.Close();

        }
        public void subMenu1(string selectedTable, string typename)
        {
            Console.WriteLine("subMenu1 typename " + typename);
            WrapPanel2.Children.Clear();
            

            //  Console.WriteLine("subMenu1 selectedTable " + selectedTable);

            conn.Open();
            var command = conn.CreateCommand();
            //Read from table
            command.CommandText = string.Format("SELECT * FROM {0} ORDER BY id", selectedTable);
            SQLiteDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                Button newBtn = new Button();
                int spaceToInsert = sdr.GetString(1).Length - 1;
                // Debug.WriteLine("SpacesToInsert " + spaceToInsert);
                string spaces = "";

                for (int i = 0; i < spaceToInsert; i++)
                {
                    spaces += " ";
                }

                byte RED = 0;
                byte GREEN = 0;
                byte BLUE = 0;

                for (int i = 0; i < GombSzinLista.Count; i++)
                {
                    // Debug.WriteLine("Lista: " + GombSzinLista[i]);

                    string[] splitlist = GombSzinLista[i].Split(',');

                    Debug.WriteLine("Lista: " + splitlist[0] + " " + "typename " + typename);

                    if (splitlist[0] == typename)
                    {
                        Debug.WriteLine("szín set " + splitlist[0] + " " + "typename " + typename);
                        try
                        {
                            int REDInt = Int32.Parse(splitlist[1]);
                            int GREENInt = Int32.Parse(splitlist[2]);
                            int BLUEInt = Int32.Parse(splitlist[3]);

                            RED = (byte)(REDInt);
                            GREEN = (byte)(GREENInt);
                            BLUE = (byte)(BLUEInt);
                        }
                        catch
                        {

                        }

                    }
                }

                RadialGradientBrush RadGrad = new RadialGradientBrush(Color.FromRgb(RED, GREEN, BLUE), Color.FromRgb(0, 0, 0));
                RadGrad.Center = new Point(0.5, 1);
                RadGrad.GradientOrigin = new Point(0.5, 1);
                RadGrad.RadiusX = 1;
                RadGrad.RadiusY = 0.5;
                newBtn.Background = RadGrad;

                newBtn.Content = spaces + sdr.GetInt32(0) + "\n" + sdr.GetString(1);
                newBtn.Foreground = new SolidColorBrush(Colors.White);
                newBtn.Name = "button_" + sdr.GetInt32(0).ToString();
                newBtn.FontSize = 13;
                newBtn.FontWeight = FontWeights.Bold;
                newBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
                newBtn.VerticalContentAlignment = VerticalAlignment.Center;
                newBtn.Width = 120;
                newBtn.Height = 75;
                newBtn.Click += (sender, args) =>
                {
                    // Console.WriteLine("Pressed " + (sender as Button).Name);

                    conn.Open();
                    string[] ID = (sender as Button).Name.Split('_');
                    command.CommandText = string.Format("SELECT * FROM {0} WHERE id = {1}", selectedTable, ID[1]);
                    SQLiteDataReader sdr2 = command.ExecuteReader();

                    while (sdr2.Read())
                    {
                        Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        //ID Név UNIXTIME Típus

                    }
                    
                    sdr2.Close();
                    conn.Close();

                    string[] buttonName = newBtn.Name.Split('_');
                    string pizzaNumber = buttonName[1];

                    cOrderNumber = pizzaNumber.ToString();
                    updateCurrentOrder();
                    subMenu2();
                };
                WrapPanel2.Children.Add(newBtn);
            }
            sdr.Close();
            conn.Close();
        }
        public void subMenu2()
        {
            WrapPanel2.Children.Clear();
            Button newBtn = new Button();
            Button newBtn2 = new Button();
            Button newBtn3 = new Button();

            // Kicsi gomb
            RadialGradientBrush     RadGrad = new RadialGradientBrush(Color.FromRgb(255, 0, 0), Color.FromRgb(0, 0, 0));
            RadGrad.Center = new Point(0.5, 1);
            RadGrad.GradientOrigin = new Point(0.5, 1);
            RadGrad.RadiusX = 1;
            RadGrad.RadiusY = 0.5;
            newBtn.Background = RadGrad;

            newBtn.Content = "Kicsi (22)";
            newBtn.Foreground = new SolidColorBrush(Colors.White);
            newBtn.Name = "Kicsi";
            newBtn.FontSize = 13;
            newBtn.FontWeight = FontWeights.Bold;
            newBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
            newBtn.VerticalContentAlignment = VerticalAlignment.Center;
            newBtn.Width = 120;
            newBtn.Height = 75;
            newBtn.Click += (sender, args) =>
            {
                cOrderSize = newBtn.Name;
                updateCurrentOrder();
                subMenuPlus();
            };
            WrapPanel2.Children.Add(newBtn);

            // Normál gomb
            newBtn2.Background = RadGrad;
            newBtn2.Content = "Közepes (30)";
            newBtn2.Foreground = new SolidColorBrush(Colors.White);
            newBtn2.Name = "Közepes";
            newBtn2.FontSize = 13;
            newBtn2.FontWeight = FontWeights.Bold;
            newBtn2.HorizontalContentAlignment = HorizontalAlignment.Center;
            newBtn2.VerticalContentAlignment = VerticalAlignment.Center;
            newBtn2.Width = 120;
            newBtn2.Height = 75;
            newBtn2.Click += (sender, args) =>
            {
                cOrderSize = newBtn2.Name;
                updateCurrentOrder();
                subMenuPlus();
            };
            WrapPanel2.Children.Add(newBtn2);

            // Nagy gomb
            newBtn3.Background = RadGrad;
            newBtn3.Content = "Nagy (50)";
            newBtn3.Foreground = new SolidColorBrush(Colors.White);
            newBtn3.Name = "Nagy";
            newBtn3.FontSize = 13;
            newBtn3.FontWeight = FontWeights.Bold;
            newBtn3.HorizontalContentAlignment = HorizontalAlignment.Center;
            newBtn3.VerticalContentAlignment = VerticalAlignment.Center;
            newBtn3.Width = 120;
            newBtn3.Height = 75;
            newBtn3.Click += (sender, args) =>
            {
                cOrderSize = newBtn3.Name;
                updateCurrentOrder();
                subMenuPlus();
            };
            WrapPanel2.Children.Add(newBtn3);
        }

        private void subMenuPlus()
        {
            WrapPanel2.Children.Clear();

            stackPanel2.Children.Clear();
            TextBlock txt = new TextBlock();
            txt.Text = "Plusz feltét";
            txt.Width = 200;
            txt.Height = 25;
            txt.FontSize = 20;
            txt.HorizontalAlignment = HorizontalAlignment.Center;
            txt.Foreground = new SolidColorBrush(Colors.White);
            //  txt.Background = new SolidColorBrush(Colors.Yellow);
            stackPanel2.Children.Add(txt);

            Button newBtn2 = new Button();
            Button newBtn3 = new Button();
            Button newBtn4 = new Button();

            // Befejez gomb
            RadialGradientBrush RadGrad = new RadialGradientBrush(Color.FromRgb(255, 0, 0), Color.FromRgb(0, 0, 0));
            RadGrad.Center = new Point(0.5, 1);
            RadGrad.GradientOrigin = new Point(0.5, 1);
            RadGrad.RadiusX = 1;
            RadGrad.RadiusY = 0.5;
            newBtn4.Background = RadGrad;

            newBtn4.Content = "Befejez";
            newBtn4.Foreground = new SolidColorBrush(Colors.White);
            newBtn4.Name = "Befejez";
            newBtn4.FontSize = 13;
            newBtn4.FontWeight = FontWeights.Bold;
            newBtn4.HorizontalContentAlignment = HorizontalAlignment.Center;
            newBtn4.VerticalContentAlignment = VerticalAlignment.Center;
            newBtn4.Width = 120;
            newBtn4.Height = 75;
            newBtn4.Click += (sender, args) =>
            {

             //   listBox.Items.Add(rendeles);
                RendelesLista.Add(rendeles);
                resetRendeles();
                updateCurrentOrder();
                fullPrice();
                WrapPanel2.Children.Clear();
            };
            WrapPanel2.Children.Add(newBtn4);

            newBtn3.Content = "Befejez, több pizza";
            newBtn3.Foreground = new SolidColorBrush(Colors.White);
            newBtn3.Name = "BefejezM";
            newBtn3.FontSize = 13;
            newBtn3.FontWeight = FontWeights.Bold;
            newBtn3.HorizontalContentAlignment = HorizontalAlignment.Center;
            newBtn3.VerticalContentAlignment = VerticalAlignment.Center;
            newBtn3.Width = 120;
            newBtn3.Height = 75;
            newBtn3.Click += (sender, args) =>
            {
                WrapPanel2.Children.Clear();
                stackPanel2.Children.Clear();
                orderMultiplySubMenu();
            };
            WrapPanel2.Children.Add(newBtn3);

            // Minus gomb
            RadGrad.Center = new Point(0.5, 1);
            RadGrad.GradientOrigin = new Point(0.5, 1);
            RadGrad.RadiusX = 1;
            RadGrad.RadiusY = 0.5;
            newBtn2.Background = RadGrad;

            newBtn2.Content = "Minusz";
            newBtn2.Foreground = new SolidColorBrush(Colors.White);
            newBtn2.Name = "minus";
            newBtn2.FontSize = 13;
            newBtn2.FontWeight = FontWeights.Bold;
            newBtn2.HorizontalContentAlignment = HorizontalAlignment.Center;
            newBtn2.VerticalContentAlignment = VerticalAlignment.Center;
            newBtn2.Width = 120;
            newBtn2.Height = 75;
            newBtn2.Click += (sender, args) =>
            {
                subMenuMinus();
                updateCurrentOrder();
            };
            WrapPanel2.Children.Add(newBtn2);

            conn.Open();
            var command = conn.CreateCommand();
            //Read from table
            command.CommandText = string.Format("SELECT * FROM {0} ORDER BY id", "Feltétek");
            SQLiteDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                Button newBtn = new Button();
                int spaceToInsert = sdr.GetString(1).Length - 1;
                // Debug.WriteLine("SpacesToInsert " + spaceToInsert);
                string spaces = "";

                for (int i = 0; i < spaceToInsert; i++)
                {
                    spaces += " ";
                }

                byte RED = 255;
                byte GREEN = 0;
                byte BLUE = 0;

                RadGrad.Center = new Point(0.5, 1);
                RadGrad.GradientOrigin = new Point(0.5, 1);
                RadGrad.RadiusX = 1;
                RadGrad.RadiusY = 0.5;
                newBtn.Background = RadGrad;

                newBtn.Content = spaces + sdr.GetInt32(0) + "\n" + sdr.GetString(1);
                newBtn.Foreground = new SolidColorBrush(Colors.White);
                newBtn.Name = "button_" + sdr.GetInt32(0).ToString();
                newBtn.FontSize = 13;
                newBtn.FontWeight = FontWeights.Bold;
                newBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
                newBtn.VerticalContentAlignment = VerticalAlignment.Center;
                newBtn.Width = 120;
                newBtn.Height = 75;
                newBtn.Click += (sender, args) =>
                {
                    conn.Open();
                    string[] ID = (sender as Button).Name.Split('_');
                    command.CommandText = string.Format("SELECT * FROM {0} WHERE id = {1} ORDER BY name", "Feltétek", ID[1]);
                    SQLiteDataReader sdr2 = command.ExecuteReader();

                    while (sdr2.Read())
                    {
                        if (cOrderSize == "Kicsi")
                        {
                            cOrderPlus += sdr2.GetString(1) + ",";
                            updatePrice(sdr2.GetString(2), "0");
                        }
                        else if (cOrderSize == "Közepes")
                        {
                            cOrderPlus += sdr2.GetString(1) + ",";
                            updatePrice(sdr2.GetString(3), "0");
                        }
                        else if (cOrderSize == "Nagy")
                        {
                            cOrderPlus += sdr2.GetString(1) + ",";
                            updatePrice(sdr2.GetString(4), "0");
                        }
                    }
                    sdr2.Close();
                    conn.Close();
                    updateCurrentOrder();
                };
                WrapPanel2.Children.Add(newBtn);
            }
            sdr.Close();
            conn.Close();

        }

        private void subMenuMinus()
        {// új koli porta 0682 505929
            WrapPanel2.Children.Clear();

            stackPanel2.Children.Clear();
            TextBlock txt = new TextBlock();
            txt.Text = "Minusz feltét";
            txt.Width = 200;
            txt.Height = 25;
            txt.FontSize = 20;
            txt.HorizontalAlignment = HorizontalAlignment.Center;
            txt.Foreground = new SolidColorBrush(Colors.White);
            //  txt.Background = new SolidColorBrush(Colors.Yellow);
            stackPanel2.Children.Add(txt);

            Button newBtn2 = new Button();
            Button newBtn3 = new Button();

            // Minus gomb
            RadialGradientBrush RadGrad = new RadialGradientBrush(Color.FromRgb(255, 0, 0), Color.FromRgb(0, 0, 0));
            RadGrad.Center = new Point(0.5, 1);
            RadGrad.GradientOrigin = new Point(0.5, 1);
            RadGrad.RadiusX = 1;
            RadGrad.RadiusY = 0.5;

            newBtn2.Background = RadGrad;
            newBtn2.Content = "Befejez";
            newBtn2.Foreground = new SolidColorBrush(Colors.White);
            newBtn2.Name = "Befejez";
            newBtn2.FontSize = 13;
            newBtn2.FontWeight = FontWeights.Bold;
            newBtn2.HorizontalContentAlignment = HorizontalAlignment.Center;
            newBtn2.VerticalContentAlignment = VerticalAlignment.Center;
            newBtn2.Width = 120;
            newBtn2.Height = 75;
            newBtn2.Click += (sender, args) =>
            {
                txt.Text = "";
                listBox.Items.Add(rendeles);
                resetRendeles();
                updateCurrentOrder();
                fullPrice();
                WrapPanel2.Children.Clear();
            };
            WrapPanel2.Children.Add(newBtn2);

            newBtn3.Background = RadGrad;
            newBtn3.Content = "Befejez, több pizza";
            newBtn3.Foreground = new SolidColorBrush(Colors.White);
            newBtn3.Name = "BefejezM";
            newBtn3.FontSize = 13;
            newBtn3.FontWeight = FontWeights.Bold;
            newBtn3.HorizontalContentAlignment = HorizontalAlignment.Center;
            newBtn3.VerticalContentAlignment = VerticalAlignment.Center;
            newBtn3.Width = 120;
            newBtn3.Height = 75;
            newBtn3.Click += (sender, args) =>
            {
                WrapPanel2.Children.Clear();
                stackPanel2.Children.Clear();
                orderMultiplySubMenu();
            };
            WrapPanel2.Children.Add(newBtn3);

            


            conn.Open();
            var command = conn.CreateCommand();
            //Read from table
            command.CommandText = string.Format("SELECT * FROM {0} ORDER BY id", "Feltétek");
            SQLiteDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                Button newBtn = new Button();
                int spaceToInsert = sdr.GetString(1).Length - 1;
                // Debug.WriteLine("SpacesToInsert " + spaceToInsert);
                string spaces = "";

                for (int i = 0; i < spaceToInsert; i++)
                {
                    spaces += " ";
                }

                byte RED = 255;
                byte GREEN = 0;
                byte BLUE = 0;

                RadGrad.Center = new Point(0.5, 1);
                RadGrad.GradientOrigin = new Point(0.5, 1);
                RadGrad.RadiusX = 1;
                RadGrad.RadiusY = 0.5;
                newBtn.Background = RadGrad;

                newBtn.Content = spaces + sdr.GetInt32(0) + "\n" + sdr.GetString(1);
                newBtn.Foreground = new SolidColorBrush(Colors.White);
                newBtn.Name = "button_" + sdr.GetInt32(0).ToString();
                newBtn.FontSize = 13;
                newBtn.FontWeight = FontWeights.Bold;
                newBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
                newBtn.VerticalContentAlignment = VerticalAlignment.Center;
                newBtn.Width = 120;
                newBtn.Height = 75;
                newBtn.Click += (sender, args) =>
                {
                    conn.Open();
                    string[] ID = (sender as Button).Name.Split('_');
                    command.CommandText = string.Format("SELECT * FROM {0} WHERE id = {1} ORDER BY name", "Feltétek", ID[1]);
                    SQLiteDataReader sdr2 = command.ExecuteReader();

                    while (sdr2.Read())
                    {
                        if (cOrderSize == "Kicsi")
                        {
                            cOrderMinus += sdr2.GetString(1) + ",";
                            updatePrice("0", sdr2.GetString(2));
                        }
                        else if (cOrderSize == "Közepes")
                        {
                            cOrderMinus += sdr2.GetString(1) + ",";
                            updatePrice("0", sdr2.GetString(3));
                        }
                        else if (cOrderSize == "Nagy")
                        {
                            cOrderMinus += sdr2.GetString(1) + ",";
                            updatePrice("0", sdr2.GetString(4));
                        }
                    }

                    sdr2.Close();
                    conn.Close();


                    updateCurrentOrder();

                };
                WrapPanel2.Children.Add(newBtn);
            }
            sdr.Close();
            conn.Close();

        }

        private void orderMultiplySubMenu()
        {
            Button newBtn = new Button();
            Button newBtn2 = new Button();
            Button newBtn3 = new Button();
            TextBlock txtNumber = new TextBlock();

            RadialGradientBrush RadGrad = new RadialGradientBrush(Color.FromRgb(255, 0, 0), Color.FromRgb(0, 0, 0));
            RadGrad.Center = new Point(0.5, 1);
            RadGrad.GradientOrigin = new Point(0.5, 1);
            RadGrad.RadiusX = 1;
            RadGrad.RadiusY = 0.5;

            newBtn2.Background = RadGrad;
            newBtn2.Content = "Befejez";
            newBtn2.Foreground = new SolidColorBrush(Colors.White);
            newBtn2.Name = "Befejez";
            newBtn2.FontSize = 13;
            newBtn2.FontWeight = FontWeights.Bold;
            newBtn2.HorizontalContentAlignment = HorizontalAlignment.Center;
            newBtn2.VerticalContentAlignment = VerticalAlignment.Center;
            newBtn2.Width = 120;
            newBtn2.Height = 75;
            newBtn2.Click += (sender, args) =>
            {
                listBox.Items.Add(rendeles);
                resetRendeles();
                updateCurrentOrder();
                fullPrice();
                WrapPanel2.Children.Clear();
                OrderNumberOfItem = 1;
            };
            WrapPanel2.Children.Add(newBtn2);

            newBtn.Background = RadGrad;
            newBtn.Content = "+";
            newBtn.Foreground = new SolidColorBrush(Colors.White);
            newBtn.Name = "plus";
            newBtn.FontSize = 13;
            newBtn.FontWeight = FontWeights.Bold;
            newBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
            newBtn.VerticalContentAlignment = VerticalAlignment.Center;
            newBtn.Width = 120;
            newBtn.Height = 75;
            newBtn.Click += (sender, args) =>
            {
                OrderNumberOfItem = OrderNumberOfItem + 1;
                txtNumber.Text = OrderNumberOfItem.ToString();
                updateCurrentOrder();
            };
            WrapPanel2.Children.Add(newBtn);

            newBtn3.Background = RadGrad;
            newBtn3.Content = "-";
            newBtn3.Foreground = new SolidColorBrush(Colors.White);
            newBtn3.Name = "minus";
            newBtn3.FontSize = 13;
            newBtn3.FontWeight = FontWeights.Bold;
            newBtn3.HorizontalContentAlignment = HorizontalAlignment.Center;
            newBtn3.VerticalContentAlignment = VerticalAlignment.Center;
            newBtn3.Width = 120;
            newBtn3.Height = 75;
            newBtn3.Click += (sender, args) =>
            {
                OrderNumberOfItem = OrderNumberOfItem - 1;
                txtNumber.Text = OrderNumberOfItem.ToString();
                updateCurrentOrder();
            };
            WrapPanel2.Children.Add(newBtn3);



            txtNumber.Name = "asd";
            txtNumber.FontSize = 20;
            txtNumber.Height = 75;
            txtNumber.Text = OrderNumberOfItem.ToString();
            WrapPanel2.Children.Add(txtNumber);

        }

        private void updateCurrentOrder()
        {
            Debug.WriteLine("updateCurrentOrder " + cOrderSize);
            if (cOrderSub3 == "")
            {
                if (cOrderSize == "Kicsi")
                {
                    Debug.WriteLine("cOrderSize == ");
                    var command = conn.CreateCommand();
                    conn.Open();
                    command.CommandText = string.Format("SELECT * FROM Pizza_Étel");
                    SQLiteDataReader sdr = command.ExecuteReader();

                    while (sdr.Read())
                    {
                        if (sdr.GetInt32(0) == Int32.Parse(cOrderNumber))
                        {
                            cOrderSub3 = sdr.GetString(2);
                        }

                    }
                    sdr.Close();
                    conn.Close();
                }
                else if (cOrderSize == "Közepes")
                {
                    Debug.WriteLine("cOrderSize == ");
                    var command = conn.CreateCommand();
                    conn.Open();
                    command.CommandText = string.Format("SELECT * FROM Pizza_Étel");
                    SQLiteDataReader sdr = command.ExecuteReader();

                    while (sdr.Read())
                    {
                        if (sdr.GetInt32(0) == Int32.Parse(cOrderNumber))
                        {
                            cOrderSub3 += sdr.GetString(3);
                        }

                    }
                    sdr.Close();
                    conn.Close();
                }
                else if (cOrderSize == "Nagy")
                {
                    Debug.WriteLine("cOrderSize == ");
                    var command = conn.CreateCommand();
                    conn.Open();
                    command.CommandText = string.Format("SELECT * FROM Pizza_Étel");
                    SQLiteDataReader sdr = command.ExecuteReader();

                    while (sdr.Read())
                    {
                        if (sdr.GetInt32(0) == Int32.Parse(cOrderNumber))
                        {
                            cOrderSub3 = sdr.GetString(4);
                        }
                    }
                    sdr.Close();
                    conn.Close();
                }
            }

            string dividerPlus = "";
            if (cOrderPlus != "")
            {
                dividerPlus = ",+";
            }
            else
            {
                dividerPlus = "";
            }

            string dividerMinus = "";
            if (cOrderMinus != "")
            {
                dividerMinus = "-";
            }
            else
            {
                dividerMinus = "";
            }
            if (cOrderPlus == "" && cOrderMinus != "")
            {
                dividerMinus = ",-";
            }

            string dividerType = "";
            if (cOrderType != "")
            {
                dividerType = ",";
            }
            else
            {
                dividerType = "";
            }

            string dividerNumber = "";
            if (cOrderNumber != "")
            {
                dividerNumber = ",";
            }
            else
            {
                dividerNumber = "";
            }

            string dividerSize = "";
            if (cOrderSize != "")
            {
                dividerSize = ",";
            }
            else
            {
                dividerSize = "";
            }

                rendeles = OrderNumberOfItem.ToString() + " db," + cOrderType + dividerType + cOrderNumber + dividerNumber + cOrderSize + dividerSize + cOrderSub3 + dividerPlus + cOrderPlus + dividerMinus + cOrderMinus;
            currentOrder.Text = "Aktuális rendelés: " + rendeles;

        }

        private void updatePrice(string plus, string minus)
        {
            bool noUnderDefPriceB = Properties.Settings.Default.noUnderDefPrice;
            bool noMinusPriceB = Properties.Settings.Default.noMinusPrice;

            int cOrderSub3Int = Int32.Parse(cOrderSub3);

            if (noMinusPriceB == true)
            {
                minus = "0";
            }

            cOrderSub3Int = cOrderSub3Int + Int32.Parse(plus) - Int32.Parse(minus);

            if (noUnderDefPriceB == true)
            {
                var command = conn.CreateCommand();
                //  conn.Open();
                command.CommandText = string.Format("SELECT * FROM Pizza_Étel");
                SQLiteDataReader sdr = command.ExecuteReader();

                while (sdr.Read())
                {
                    Debug.WriteLine("updatePrice");
                    if (Int32.Parse(cOrderNumber) == sdr.GetInt32(0))
                    {
                        Debug.WriteLine("updatePrice" + "1");
                        if (cOrderSize == "Kicsi")
                        {
                            Debug.WriteLine("updatePrice" + "2");
                            if (cOrderSub3Int < Int32.Parse(sdr.GetString(2)))
                            {
                                Debug.WriteLine("updatePrice" + "3 " + cOrderSub3Int + " " + Int32.Parse(sdr.GetString(3)));
                                cOrderSub3 = sdr.GetString(2);
                            }
                            else
                            {
                                cOrderSub3 = cOrderSub3Int.ToString();
                            }
                        }else if (cOrderSize == "Közepes")
                        {
                            Debug.WriteLine("updatePrice" + "2");
                            if (cOrderSub3Int < Int32.Parse(sdr.GetString(3)))
                            {
                                Debug.WriteLine("updatePrice" + "3 " + cOrderSub3Int + " " + Int32.Parse(sdr.GetString(3)));
                                cOrderSub3 = sdr.GetString(3);
                            }
                            else
                            {
                                cOrderSub3 = cOrderSub3Int.ToString();
                            }
                        } else if (cOrderSize == "Nagy")
                        {
                            Debug.WriteLine("updatePrice" + "2");
                            if (cOrderSub3Int < Int32.Parse(sdr.GetString(4)))
                            {
                                Debug.WriteLine("updatePrice" + "3 " + cOrderSub3Int + " " + Int32.Parse(sdr.GetString(3)));
                                cOrderSub3 = sdr.GetString(4);
                            }
                            else
                            {
                                cOrderSub3 = cOrderSub3Int.ToString();
                            }
                        }
                    }
                }
                sdr.Close();
                // conn.Close();
            }
            else
            {
                cOrderSub3 = cOrderSub3Int.ToString();
            }
        }
        private void resetRendeles()
        {
            stackPanel2.Children.Clear();
            cOrderType = "";
            cOrderNumber = "";
            cOrderSize = "";
            cOrderSub3 = "";
            cOrderSub4 = "";
            cOrderPlus = "";
            cOrderMinus = "";
        }

        private void fullPrice()
        {
            for (int i = 0; i < listBox.Items.Count; i++)
            {

                //listBox.Items.Add();
              //  Debug.WriteLine("ListboxItem: " + obj);
            }
        }
    }
}