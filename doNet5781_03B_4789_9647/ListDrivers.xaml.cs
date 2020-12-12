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
using System.Windows.Shapes;


namespace doNet5781_03B_4789_9647
{
    /// <summary>
    /// Interaction logic for ListDrivers.xaml
    /// </summary>
    
    public partial class ListDrivers : Window
    {
        private ObservableCollection<Drivers> driverBus= new ObservableCollection<Drivers>() ;

        private ObservableCollection<Bus> buses = new ObservableCollection<Bus>();
      

        public ListDrivers() //defult constructor
        {
            InitializeComponent();        
            allDriver.Items.Refresh();

        }


        public ListDrivers(ObservableCollection<Drivers> drivers ,ObservableCollection<Bus>egged)  //constructor that get the list of driver and the list of buses
        {
            InitializeComponent();
            allDriver.ItemsSource = drivers;
            buses = egged;
            driverBus = drivers;
            check(); //go to find which driver is at travel
            allDriver.Items.Refresh();
        }

        /// <summary>
        ///  when we open this window we want to show prograss bar on driver that been at travel.
        ///  so in order to do this we need find which driver been at travel when the customer want to open this window.
        ///  we move on the driver list and buses list to check if someone been at travle.
        ///  if we find driver at travel we send him to func that begin to us a prograss bar. this func get the current time that this bus have to finish the travel.
        /// </summary>

        public void check()//find who the drivers for each bus
        {
            
            for(int i=0;i<buses.Count;i++)
            {
                allDriver.Items.Refresh();
                for (int j=0;j<driverBus.Count;j++)
                {
                    allDriver.Items.Refresh();
                    if (buses[i].NameDriver == driverBus[j].Name1)
                    {


                        int num = buses[i].TimeToEndWork;
                        driverBus[j].help(num);
                         
                        
                      
                        allDriver.Items.Refresh();
                    }
                }
                allDriver.Items.Refresh();
            }
            allDriver.Items.Refresh();
        }





        private void allDriver_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            allDriver.Items.Refresh();
        }

        private void TakeAbreak_Click(object sender, RoutedEventArgs e) //send the driver to break
        {
            ((sender as Button).DataContext as Drivers).TakeBreak();
           
            allDriver.Items.Refresh();
        }

        private void adddriver_Click(object sender, RoutedEventArgs e) //add driver to our company
        {

            try
            {
                 Add_driver wnd = new Add_driver();


                bool? result = wnd.ShowDialog();
                if (result == true)
                {

                    int a = wnd.driver.Id;

                    if (!findriverID(driverBus,a)) //find if the ID already exsis
                    {
                        driverBus.Add(wnd.driver);
                        allDriver.Items.Refresh();
                    }

                    this.allDriver.Items.Refresh();
                    
                }
                allDriver.Items.Refresh();
            }

            catch (ArgumentException messege)
            {


                MessageBox.Show(messege.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }



        }

        private void close_Click(object sender, RoutedEventArgs e) //close the window
        {
            Visibility = Visibility.Hidden;
            
            this.Close();

        }



        private static bool findriverID(ObservableCollection<Drivers> driver, int num) //if the new ID is allready exsis so throw messege
        {

            for (int i = 0; i < driver.Count; i++)
            {
                if (driver[i].Id == num)
                    throw new ArgumentException(String.Format("{0} Number Id allready exsis", num));
              
            }
            
            return false;
           
                
            
        }

        private void allDriver_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            allDriver.Items.Refresh();
        }
    }
}
