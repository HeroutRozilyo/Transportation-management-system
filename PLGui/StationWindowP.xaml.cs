using BlAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
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
        IEnumerable<BO.Line> temp;
        private ObservableCollection<BO.Station> stations = new ObservableCollection<BO.Station>();
        int oldCode;
        BO.Station addStation = new BO.Station();
        BO.Station stationData = new BO.Station();
      
        public StationWindowP()
        {
            InitializeComponent();
        }

        public StationWindowP(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            RefreshStation();
            NotExist.Visibility = Visibility.Hidden;
         
            //  ListOfStations.ItemsSource = stations;
        }

        public ObservableCollection<T> ConvertList<T>(IEnumerable<T> listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }

        private void RefreshStation()
        {
            stations = ConvertList(bl.GetAllStations());//to make ObservableCollection
            ListOfStations.ItemsSource = stations;
            NOLine.Visibility = Visibility.Hidden;
            LineInStation.Visibility = Visibility.Visible;
            stationExistCheckBox.Visibility = Visibility.Hidden;
            Sexist.Visibility = Visibility.Hidden;



        }

        private void ListOfStations_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            add = false;
            var list = (ListView)sender; //to get the line
            stationData = list.SelectedItem as BO.Station;
            RefreshLineInStation();



        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (numberText != null)
            {
                int Sera = Convert.ToInt32(numberText);
                BO.Station SearchResult = bl.GetStationByCode(Sera);
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
        }
        public void RefreshLineInStation()
        {
            
            StationDataGrid.DataContext = stationData;
           
            if (stationData != null)
            {
                oldCode = stationData.Code;
                temp = bl.GetAllLineIndStation(stationData.Code);
               
            }
            if (temp!=null&&temp.Count() > 0)
                LineInStation.ItemsSource = temp;
            else
            {
                NOLine.Visibility = Visibility.Visible;
                LineInStation.Visibility = Visibility.Hidden;
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
            ListOfStations.ItemsSource = stations;
            textBoxTextBox.Text = null;
        }
        string numberText;
        private void textBoxTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxTextBox.Text != "Search Station here...." && textBoxTextBox.Text != "")
                numberText = textBoxTextBox.Text;
            textBoxTextBox.Text = "Search Station here....";
        }

        private void stationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UpdateStation_Click(object sender, RoutedEventArgs e)//////////
        {
            try
            {
              
                helpaAddStation();
                bl.UpdateStation(addStation,oldCode);
            }
            catch (BO.BadCoordinateException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Sexist.Visibility = Visibility.Visible;
                stationExistCheckBox.Visibility = Visibility.Visible;
                stationExistCheckBox.IsChecked = false;
                add = true;
            }

        }

        private void DeleteStations_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeleteStation(oldCode);
                RefreshStation();
                RefreshLineInStation();
            }
            catch (BO.BadCoordinateException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
               
            }
        }
        bool add = false;
        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            if (!add)
            {
               
                stationData = null;
                temp = null;
                stationExistCheckBox.Visibility = Visibility.Visible;
                Sexist.Visibility = Visibility.Visible;
                add = true;
                RefreshLineInStation();
                
            }
            



        }

        private void helpaAddStation()
        {
            addStation.Address = addressTextBox.Text;
            addStation.Code = Convert.ToInt32(codeTextBox.Text);
            addStation.Coordinate = new GeoCoordinate(double.Parse((latitudeTextBox.Text)), double.Parse(longitudeTextBox.Text));
            addStation.Name = nameTextBox.Text;
            addStation.StationExist = (bool)stationExistCheckBox.IsChecked;
        }

        private void stationExistCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (add == true)
                {
                    helpaAddStation();
                    //addStation = StationDataGrid.DataContext as BO.Station;
                    
                    stationExistCheckBox.Visibility = Visibility.Hidden;
                    Sexist.Visibility = Visibility.Hidden;
                    bl.AddStation(addStation);
                    stationData = addStation;
                    RefreshStation();
                    RefreshLineInStation();
                    add = false;
                }

            }
            catch(BO.BadCoordinateException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Sexist.Visibility = Visibility.Visible;
                stationExistCheckBox.Visibility = Visibility.Visible;
                stationExistCheckBox.IsChecked = false;
                add = true;
            }
            


        }
    }
}
