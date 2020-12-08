using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;


namespace doNet5781_03B_4789_9647
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>




    ///

    public partial class MainWindow : Window
    {
        //List<Bus> egged = new List<Bus>();  //A list that will contain all the buses
        static Random r = new Random(DateTime.Now.Millisecond);
        addWindow wnd;


        public Bus temp
        {
            get; private set;
        }


        private ObservableCollection<Bus> egged = new ObservableCollection<Bus>();




        public MainWindow()
        {
            try
            {
                initBuses(egged);
                InitializeComponent();
                allbuses.ItemsSource = egged;
            }
            catch (Exception)
            {
                MessageBox.Show("a");
            }



        }


        //private void initBuses(List<Bus> egged)    //List<Bus> egged)
        private void initBuses(ObservableCollection<Bus> egged)
        {
            int license1;
            DateTime date1;


            for (int i = 0; i < 10; i++) ///restart 10 buses 
            {
                date1 = new DateTime(r.Next(1990, DateTime.Today.Year + 1), r.Next(1, 13), r.Next(1, 29));//,r.Next(1,25),r.Next(0,60),r.Next(0,60));
                //int a = date1.Year;
                do
                {
                    if ((date1.Year < 2018))
                    {
                        license1 = r.Next(1000000, 10000000); //to random number with 7 digite
                    }
                    else
                    {
                        license1 = r.Next(10000000, 100000000); //to random number with 8 digite
                    }

                } while (findBuse(egged, license1)); //check if the license exsis

                try
                {
                    Bus temp = new Bus(license1, date1);
                    egged.Add(temp);
                }

                catch (Exception)
                {
                    i--;
                }


            }


            egged[2].lastTreat = (egged[2].lastTreat.AddYears(-1));
            egged[1].lastTreat = DateTime.Now.AddDays(-1);
            egged[3].newKm_from_LastTreatment = 19900;
            egged[4].Fuel = 50;





        }




        //private static bool findBuse(List<Bus> buses, int num)  //function that check if the require bus exist
        private static bool findBuse(ObservableCollection<Bus> buses, int num)
        {
            try
            {
                int temp1;


                foreach (Bus item in buses) //move on the list buses
                {
                    string temp = item.License;

                    temp = temp.Replace("-", string.Empty); //To remove the hyphens from our license number
                    int.TryParse(temp, out temp1);

                    if (temp1 == num) //check if the licenes equal
                    {
                        return true;
                    }

                }
                return false;
            }
            catch (Exception)
            {

                MessageBox.Show("b");
                return false;
            }



        }



        private void addBus_Click(object sender, RoutedEventArgs e) //to add bus to our company
        {

            try
            {
                wnd = new addWindow();
                // Bus temp = new Bus();
                //wnd = new addWindow(ref temp);

                bool? result = wnd.ShowDialog();
                if (result == true)
                {

                    

                    egged.Add(wnd.myBus);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The new licence is not valid,\n please nter again number licence with 8 digite", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }



        }




        private void Button_Click(object sender, RoutedEventArgs e) //to send the bus to refulling
        {
            try
            {
                var fxElt = sender as FrameworkElement;
                Bus lineData = fxElt.DataContext as Bus;
                MessageBoxResult result = MessageBox.Show(lineData.fuelString(), "FUELIING", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.Yes);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            ((sender as Button).DataContext as Bus).Refuelling(); //gp to refulling the bus
                          //  ((sender as Button).DataContext as Bus).enable = false;
                           
                            allbuses.Items.Refresh();
                            
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }

            }
            catch (Exception)
            {

                MessageBox.Show("a");
            }

           
        }



        private void Pick_Button_Click(object sender, RoutedEventArgs e) //to starting travel
        {
            StartingTravel smalla = new StartingTravel((sender as Button).DataContext as Bus);
            smalla.ShowDialog();
        }





        private void doubleflick(object sender, RoutedEventArgs e) //in order to shoe details on the bus
        {


            var list = (ListView)sender;
            object item = list.SelectedItem;
            Bus_Data temp = new Bus_Data(item);
            temp.ShowDialog();




        }





        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void allbuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

