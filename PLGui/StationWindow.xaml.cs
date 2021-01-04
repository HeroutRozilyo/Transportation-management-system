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
using System.Windows.Shapes;

namespace PLGui
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        private IBL bl;
        private ObservableCollection<BO.Station> stations = new ObservableCollection<BO.Station>();
        public StationWindow()
        {
            InitializeComponent();
        }

        public StationWindow(IBL bl)
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




















        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource stationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // stationViewSource.Source = [generic data source]
        }

        private void stationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListOfStations_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
