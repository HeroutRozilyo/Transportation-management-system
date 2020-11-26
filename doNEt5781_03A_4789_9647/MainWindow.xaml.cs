
using dotNet5781_02_4789_9647;
using dotNet5781_02_4789_9647.Properties;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace doNEt5781_03A_4789_9647
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
     
    public partial class MainWindow : Window
    {
   
        private static Random r = new Random(DateTime.Now.Second); //static random
        BusCompany egged; //company of the bus
      List<BusStation> stations; /// <summary>
      /// list of station
      /// </summary>

      private LineBus currentDisplayBusLine = new LineBus(); // a varieble that get a line from the compay bus.
        public MainWindow()
        {
            egged = new BusCompany();
            stations = new List<BusStation>();

            /// restart the buses and the station at the program     

            for (int i = 0; i < 40; i++)//restart stations
            {
                string name = string.Format("station " + (i + 1));
                stations.Add(new BusStation((i + 1), name));
            }

            for (int i = 0; i < 10; i++) //restart 10 lines with first station and last station. the constructor at lineBus random the number line.
            {
                egged.ComanyBus.Add(new LineBus(stations[i], stations[i + 1])); //now we restart 10 buses
            }
            for (int k = 1; k <= 3; k++)       //add stations to the lines
            {

                int j = 10 * k;
                for (int i = 0; i < 10; i++)
                {

                    egged.ComanyBus[i].addAtLineBus(stations[j]);
                    j++;
                }
            }

            InitializeComponent();
            initComboBox();
         

        }
        /// <summary>
        /// print at him all the buses numberID
        /// </summary>
        private void initComboBox()
        {
            cbBusLines.ItemsSource = egged.ComanyBus;
            cbBusLines.DisplayMemberPath = " NumberID ";
            cbBusLines.SelectedIndex = 0;
            ShowBusLine(((LineBus)cbBusLines.SelectedItem).NumberID);
            
        }

        private void tbArea_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        /// <summary>
        /// help func to show data on specific line
        /// </summary>
       
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as LineBus).NumberID);
        }

        /// <summary>
        /// to print a data on line incordding to his numberID
        /// </summary>
  
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = egged[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.BusStations;

            
        }

        private void lbBusLineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }


}
