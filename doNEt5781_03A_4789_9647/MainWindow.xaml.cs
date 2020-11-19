
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
        static Random lineRandom = new Random(DateTime.Now.Millisecond);
        BusCompany egged = new BusCompany();
        List<BusStation> stations = new List<BusStation>();
            
        

        public MainWindow()
        {
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
        private LineBus currentDisplayBusLine = new LineBus();

        private void initComboBox()
        {
            cbBusLines.ItemsSource = stations;
            cbBusLines.DisplayMemberPath = " NumberID ";
            cbBusLines.SelectedIndex = 0;
        }

        private void tbArea_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as LineBus).NumberID);
        }

        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = egged[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.BusStations;
        }
    }


}
