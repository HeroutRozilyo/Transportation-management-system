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
        
        public MainWindow()
        {
     

        InitializeComponent()
        {
                BusCompany company = new BusCompany();
                List<BusStation> bStation = new List<BusStation>();
                for (int i = 0; i < 40; i++)       //restart stations
                {
                    string name = string.Format("station " + (i + 1));
                    bStation.Add(new BusStation((i + 1), name));
                }

                for (int i = 0; i < 10; i++)       //restart 10 lines with first station and last station
                {
                    company.ComanyBus.Add(new LineBus(bStation[i], bStation[i + 1])); //now we restart 10 buses
                }
                for (int k = 1; k <= 3; k++)       //add stations to the lines
                {

                    int j = 10 * k;
                    for (int i = 0; i < 10; i++)
                    {

                        company.ComanyBus[i].addAtLineBus(bStation[j]);
                        j++;
                    }
                }
                for (int j = 5; j < 15; j++) // now will pass at 10 stations more than 1 line bus.
                {
                    company.ComanyBus[j - 5].addAtLineBus(bStation[j]);
                }
            }

        }
        

        private void tbArea_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
