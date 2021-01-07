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
        BO.Station stationData = new BO.Station();
        public StationWindowP()
        {
            InitializeComponent();
        }

        public StationWindowP(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            RefreshLine();
            NotExist.Visibility = Visibility.Hidden;
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
          //  stationData=bl.gets


        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            
            TextBox text = sender as TextBox;
            int lineSteation = int.Parse(text.Text);
            BO.Station SearchResult = bl.GetStationByCode(lineSteation);
            if (SearchResult != null)
            {
                ObservableCollection<BO.Station> a = new ObservableCollection<BO.Station>();
                a.Add(SearchResult);
                ListOfStations.ItemsSource = a;

            }
            else
            {
                ListOfStations.ItemsSource = stations;
                NotExist.Visibility = Visibility.Visible;
            }
        }

        private void textBoxTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                
                TextBox text = sender as TextBox;
                int lineSteation = int.Parse(text.Text) ;
               BO.Station SearchResult= bl.GetStationByCode(lineSteation);
                if (SearchResult != null)
                {
                    ObservableCollection<BO.Station> a = new ObservableCollection<BO.Station>();
                    a.Add(SearchResult);
                    ListOfStations.ItemsSource = a;

                }
                else
                {
                    ListOfStations.ItemsSource = stations;
                    NotExist.Visibility = Visibility.Visible;
                }
               

            }
            if(e.Key==Key.Back)
            {
                NotExist.Visibility = Visibility.Hidden;
                ListOfStations.ItemsSource = stations;
            }
        }

        private void textBoxTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x));
        }

      
        

        private void textBoxTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            textBoxTextBox.Text = null;
        }

        private void textBoxTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            textBoxTextBox.Text = "Search Station here....";
        }
    }
}
