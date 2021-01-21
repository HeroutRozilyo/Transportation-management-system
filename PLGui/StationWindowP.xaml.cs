using BlAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PLGui
{
    /// <summary>
    /// Interaction logic for StationWindowP.xaml
    /// </summary>
    public partial class StationWindowP : Page
    {
        #region reset
        private IBL bl;
        IEnumerable<BO.Line> temp;
        private ObservableCollection<BO.Station> stations = new ObservableCollection<BO.Station>();
        int oldCode;
        bool add = false;
        BO.Station addStation = new BO.Station();
        BO.Station stationData = new BO.Station();
        ObservableCollection<BO.AdjacentStations> beforAdj = new ObservableCollection<BO.AdjacentStations>();
        ObservableCollection<BO.AdjacentStations> afterAdj = new ObservableCollection<BO.AdjacentStations>();
        string numberText;
        int CodeNumber;
        #endregion

        #region constructor
        [Obsolete("not using", true)]
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
            updateTS.Visibility = Visibility.Hidden;


        }
        #endregion

        #region refresh
        /// <summary>
        /// refresh List View Station
        /// </summary>
        private void RefreshStation()
        {
            stations = ConvertList(bl.GetAllStations());//to make ObservableCollection
            ListOfStations.ItemsSource = stations;
            stationExistCheckBox.Visibility = Visibility.Hidden;
            Sexist.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// refresh data on the Station
        /// </summary>
        public void RefreshLineInStation()
        {
            beforAdj.Clear();
            afterAdj.Clear();
            stationData = bl.GetStationByCode(CodeNumber);
            StationDataGrid.DataContext = stationData;
            temp = null;
            if (stationData != null)
            {
                oldCode = stationData.Code;
                temp = bl.GetAllLineIndStation(stationData.Code);
                BeforAfter();
                Befor.ItemsSource = beforAdj;
                After.ItemsSource = afterAdj;
            }

            LineInStation.ItemsSource = temp;
            updateTS.Visibility = Visibility.Hidden;

        }
        #endregion

        #region Button Click thd doubleClick
        private void ListOfStations_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            add = false;
            var list = (ListView)sender; //to get the station
            stationData = list.SelectedItem as BO.Station;
            CodeNumber = stationData.Code;
            RefreshLineInStation();

        }

        /// <summary>
        /// to do the search od the station
        /// </summary>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (numberText != null)
            {
                int StationNum = Convert.ToInt32(numberText);
                helpSearch(StationNum);

            }
        }

        /// <summary>
        /// do the search by enter
        /// </summary>
        private void textBoxTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {

                TextBox text = sender as TextBox;
                int lineSteation = int.Parse(text.Text);
                helpSearch(lineSteation);

            }
            if (e.Key == Key.Back)
            {
                NotExist.Visibility = Visibility.Hidden;
                ListOfStations.ItemsSource = stations;
            }
        }


        /// <summary>
        /// to start update time and distance betweenStation
        /// </summary>
        private void UpdataDT_Click(object sender, RoutedEventArgs e)
        {
            updateTS.Visibility = Visibility.Visible;
            var list = sender as FrameworkElement; //to get the line
            BO.AdjacentStations tem = list.DataContext as BO.AdjacentStations;
            updateTS.DataContext = tem;
        }

        /// <summary>
        /// save the change of the detials of Consecutive stations
        /// </summary>
        private void okeyUpdate_Checked(object sender, RoutedEventArgs e)
        {
            try
            {

                MessageBoxResult result = MessageBox.Show("?האם לשמור שינויים", "Update", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            BO.AdjacentStations a = updateTS.DataContext as BO.AdjacentStations;
                            if(a.TimeAverage==0||a.Distance==0)
                            {
                                MessageBox.Show("הערכים בשדות אלו לא יכולים להיות 0","ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                                okeyUpdate.IsChecked = false;
                                RefreshLineInStation();
                                return;

                            }
                            bl.UpdateAdjac(a);//
                            okeyUpdate.IsChecked = true;
                            updateTS.Visibility = Visibility.Hidden;
                            break;
                        }
                    case MessageBoxResult.No:
                        {

                            okeyUpdate.IsChecked = false;
                            

                            break;
                        }
                    case MessageBoxResult.Cancel:
                        {

                            okeyUpdate.IsChecked = false;
                            updateTS.Visibility = Visibility.Hidden;
                            RefreshLineInStation();
                            break;
                        }
                }
            }
            catch (BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                updateTS.Visibility = Visibility.Visible;
                okeyUpdate.IsChecked = false;
                RefreshLineInStation();
            }
        }

        /// <summary>
        /// update data of station
        /// </summary>
        private void UpdateStation_Click(object sender, RoutedEventArgs e)//////////
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("?האם לשמור שינויים\n פעולה זו בלתי הפיכה", "Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            helpaAddStation();
                            bl.UpdateStation(addStation, oldCode);
                            CodeNumber = addStation.Code;
                            RefreshStation();
                            RefreshLineInStation();


                            ListOfStations.SelectedIndex = stations.ToList().FindIndex(b=>b.Code==stationData.Code);
                            break;
                        }
                    case MessageBoxResult.No:
                        {

                            break;
                        }
                }


            }
            catch (BO.BadCoordinateException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Sexist.Visibility = Visibility.Visible;
                stationExistCheckBox.Visibility = Visibility.Visible;
                stationExistCheckBox.IsChecked = false;
                add = true;
            }
            catch (BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Sexist.Visibility = Visibility.Visible;
                stationExistCheckBox.Visibility = Visibility.Visible;
                stationExistCheckBox.IsChecked = false;
                add = true;
            }

        }

        /// <summary>
        /// Delate Station from the System
        /// </summary>
        private void DeleteStations_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("?האם למחוק תחנה זו\n פעולה זו בלתי הפיכה", "Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            bl.DeleteStation(oldCode, stationData.LineAtStation);
                            stationData = null;
                            RefreshStation();
                            RefreshLineInStation();
                            break;
                        }
                    case MessageBoxResult.No:
                        {

                            break;
                        }
                }


            }
            catch (BO.BadCoordinateException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        /// <summary>
        /// Add new station to the line
        /// </summary>
        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            if (!add)
            {

                ListOfStations.SelectedIndex = -1;
                stationData = null;
                temp = null;
                stationExistCheckBox.Visibility = Visibility.Visible;
                Sexist.Visibility = Visibility.Visible;
                add = true;
                RefreshLineInStation();

            }

        }

        /// <summary>
        /// Add finally station to the system
        /// </summary>
        private void stationExistCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {

                if (add)
                {
                    MessageBoxResult result = MessageBox.Show("?האם לשמור שינויים", "Update", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            {
                                helpaAddStation();
                                stationExistCheckBox.Visibility = Visibility.Hidden;
                                Sexist.Visibility = Visibility.Hidden;
                                bl.AddStation(addStation);
                                stationData = addStation;
                                RefreshStation();
                                RefreshLineInStation();
                                add = false;
                                break;
                            }
                        case MessageBoxResult.No:
                            {
                                stationExistCheckBox.IsChecked = false;
                                break;
                            }
                        case MessageBoxResult.Cancel:
                            {
                                StationDataGrid.DataContext = new BO.Station();
                                stationExistCheckBox.IsChecked = false;

                                stationExistCheckBox.Visibility = Visibility.Hidden;
                                Sexist.Visibility = Visibility.Hidden;
                                add = false;
                                break;

                            }
                    }
                }

            }
            catch (BO.BadCoordinateException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Sexist.Visibility = Visibility.Visible;
                stationExistCheckBox.Visibility = Visibility.Visible;
                stationExistCheckBox.IsChecked = false;
                add = true;
            }
            catch (BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Sexist.Visibility = Visibility.Visible;
                stationExistCheckBox.Visibility = Visibility.Visible;
                stationExistCheckBox.IsChecked = false;
                add = true;
            }



        }
        #endregion

        #region fucos searchTextBox
        private void textBoxTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ListOfStations.ItemsSource = stations;
            textBoxTextBox.Text = null;
            ListOfStations.SelectedIndex = -1;
            stationData = null;
            RefreshLineInStation();

        }

        private void textBoxTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxTextBox.Text != "Search Station here...." && textBoxTextBox.Text != "")
                numberText = textBoxTextBox.Text;
            textBoxTextBox.Text = "Search Station here....";//
        }
        #endregion

        #region More func
        public ObservableCollection<T> ConvertList<T>(IEnumerable<T> listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }

        /// <summary>
        /// Divides the list of consecutive stations of the line into a list of stations before and after the current station
        /// </summary>
        private void BeforAfter()
        {
            if(stationData.StationAdjacent!=null)
            foreach (BO.AdjacentStations item in stationData.StationAdjacent)
            {
                if (item.Station1 == stationData.Code)
                {
                    afterAdj.Add(item);
                }
                else beforAdj.Add(item);
            }
        }

        private void textBoxTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x));
        }

        /// <summary>
        /// Does the actual search
        /// </summary>
        /// <param name="numStation"></param>
        public void helpSearch(int numStation)
        {
            BO.Station SearchResult = bl.GetStationByCode(numStation);
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

        /// <summary>
        /// to make all the textBox to station
        /// </summary>
        private void helpaAddStation()
        {
            addStation.Address = addressTextBox.Text;
            addStation.Code = Convert.ToInt32(codeTextBox.Text);
            addStation.Coordinate = new GeoCoordinate(double.Parse((latitudeTextBox.Text)), double.Parse(longitudeTextBox.Text));
            addStation.Name = nameTextBox.Text;
            addStation.StationExist = (bool)stationExistCheckBox.IsChecked;

        }
      
        private void codeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x));
        }

        private void longitudeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x) || '.'.Equals(x));
        }

        private void latitudeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x) || '.'.Equals(x));

        }

        private void timeAverageTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x) || '.'.Equals(x));
        }

        #endregion
    }
}
