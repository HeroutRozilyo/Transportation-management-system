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
      

        public ListDrivers()
        {
            InitializeComponent();
           
            allDriver.Items.Refresh();

        }


        public ListDrivers(ObservableCollection<Drivers> drivers ,ObservableCollection<Bus>egged)
        {
            InitializeComponent();
            allDriver.ItemsSource = drivers;
            buses = egged;
            driverBus = drivers;
            check();
            allDriver.Items.Refresh();
        }



        public void check()//find who the drivers for each bus
        {
            
            for(int i=0;i<buses.Count;i++)
            {
                for(int j=0;j<driverBus.Count;j++)
                {
                    if (buses[i].NameDriver == driverBus[j].Name1)
                    {


                        int num = buses[i].helptime;
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

        private void TakeAbreak_Click(object sender, RoutedEventArgs e)
        {
            ((sender as Button).DataContext as Drivers).TakeBreak();
           
            allDriver.Items.Refresh();
        }

        private void adddriver_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                 Add_driver wnd = new Add_driver();


                bool? result = wnd.ShowDialog();
                if (result == true)
                {

                    int a = wnd.driver.Id;

                    if (!findriverID(driverBus,a))
                    {
                        driverBus.Add(wnd.driver);
                    }

                    this.allDriver.Items.Refresh();

                }
            }

            catch (ArgumentException messege)
            {


                MessageBox.Show(messege.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }



        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }



        private static bool findriverID(ObservableCollection<Drivers> driver, int num)
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
