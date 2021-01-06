using BlAPI;
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

namespace PLGui
{
    /// <summary>
    /// Interaction logic for StationWindowP.xaml
    /// </summary>
    public partial class StationWindowP : Page
    {
        private IBL bl;
        private ObservableCollection<BO.Station> stations = new ObservableCollection<BO.Station>();
        public StationWindowP()
        {
            InitializeComponent();
        }

        public StationWindowP(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            RefreshLine();
            //  ListOfStations.ItemsSource = stations;
        }

        public ObservableCollection<T> Convert<T>(IEnumerable<T> listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }

        private void RefreshLine()
        {
            stations = Convert(bl.GetAllStations());//to make ObservableCollection
            ListOfStations.ItemsSource = stations;
        }

        private void ListOfStations_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {



        }
    }
}
