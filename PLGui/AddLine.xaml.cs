using BlAPI;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PLGui
{
    /// <summary>
    /// Add New lIne Window
    /// </summary>
    public partial class AddLine : Window
    {
        #region reset
        private IBL bl;
        private ObservableCollection<BO.Station> GetStations = new ObservableCollection<BO.Station>();//list that change
        private ObservableCollection<BO.Station> allStations = new ObservableCollection<BO.Station>();// list that not change
        private ObservableCollection<BO.LineStation> GetLineStations = new ObservableCollection<BO.LineStation>();
        BO.LineStation convertStation = new BO.LineStation();
        static int add;
        int idLineFromDS = 0;
        int keepLineID = 0;
        BO.Line newLine = new BO.Line();
        private BO.Line line;
        #endregion

        #region to return
        /// <summary>
        /// to Return the new Line and is Station List  to LineWindow
        /// </summary>
        public BO.Line NewLine
        {

            get
            {
                return newLine;
            }

        }

        public List<BO.LineStation> StationList
        {

            get
            {
                return GetLineStations.ToList();
            }

        }
        #endregion

        #region Constructors
        [Obsolete("not using", true)]
        public AddLine()
        {
            InitializeComponent();
        }

        /// <summary>
        /// for new Line
        /// </summary>
        public AddLine(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            try
            {
                GetStations = ConvertList(bl.GetAllStations());
                allStations = ConvertList(bl.GetAllStations());
                RefreshStation();
                RefreshLineStation();
                add = 0;
                AreaComboBox.ItemsSource = Enum.GetValues(typeof(BO.AREA));
                this.DataContext = newLine;
                GridDataLine.Visibility = Visibility.Visible;
                FinishAddLine.Visibility = Visibility.Visible;
                updataStationLine.Visibility = Visibility.Hidden;
            }
            catch (BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }

        /// <summary>
        /// for update line Station
        /// </summary>
        public AddLine(BO.Line line, IBL bl)
        {
            try
            {
                InitializeComponent();
                this.line = line;
                this.bl = bl;
                GridDataLine.Visibility = Visibility.Hidden;
                FinishAddLine.Visibility = Visibility.Hidden;
                GetStations = ConvertList(bl.GetAllStations());
                allStations = ConvertList(bl.GetAllStations());
                updataStationLine.Visibility = Visibility.Visible;
                add = 0;
                orderGrid();
                RefreshStation();
            }
            catch (BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        #endregion



        #region UpdateStationLine
        /// <summary>
        /// Arranges the two listView according to the stations that already exist in the above line
        /// </summary>
        private void orderGrid()
        {
            //select the Station that That I have in line as line stations////
            IEnumerable<Station> stations = from item in allStations
                                            from linestation in line.StationsOfBus
                                            where linestation.StationCode == item.Code
                                            orderby linestation.LineStationIndex
                                            select item;
            /////over the list of station- to add Station to the LineStation ListView and remove from the Station ListView////
            foreach (BO.Station stat in stations)
            {
                foreach (BO.LineStation lins in line.StationsOfBus)
                {
                    if (lins.StationCode == stat.Code)
                    {
                        convertStation = lins;
                        add++;
                        addToListLineStation(stat);
                    }


                }
            }
            keepLineID = line.StationsOfBus.ElementAt(0).LineId;
        }


        private void addToListLineStation(BO.Station ToAdd)
        {
            convertStation.LineStationExist = true;
            GetLineStations.Add(convertStation);
            Station check = GetStations.ToList().Find(b => b.StationExist && b.Code == ToAdd.Code);
            GetStations.Remove(check);
            RefreshLineStation();
            RefreshStation();
        }
        #endregion
        #region refresh
        /// <summary>
        /// refresh to the data in the listView of station 
        /// </summary>
        private void RefreshStation()
        {

            Station.ItemsSource = GetStations;
            Station.Items.Refresh();
            if (LineNumber.Text != "" && AreaComboBox.SelectedIndex != -1 && GetLineStations.Count() >= 2)
            {
                FinishAddLine.IsEnabled = true;
            }
            else FinishAddLine.IsEnabled = false;

        }
        /// <summary>
        ///  refresh to the data in the listView of station of the line
        /// </summary>
        private void RefreshLineStation()
        {
            StationOfTheLine.ItemsSource = GetLineStations;
            StationOfTheLine.Items.Refresh();

        }
        #endregion

        #region Button Click

        /// <summary>
        ///  Delete a station from the line
        /// </summary>
        private void CancelStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var fxElt = sender as FrameworkElement; //get the Station from the listView 
                BO.LineStation toRemove = fxElt.DataContext as BO.LineStation;
                int index = toRemove.LineStationIndex;
                //////Arrange the list of line stations according to the indexes, station before and after instead of the deleted station//////////////////////////////////////////////////
                if (index < GetLineStations.Count())
                {
                    for (int i = GetLineStations.Count() - 1; i > index - 1; i--)
                    {
                        GetLineStations[i].LineStationIndex--;
                    }
                }
                if (toRemove.NextStation != 0 && toRemove.PrevStation != 0)
                {
                    BO.LineStation station1 = GetLineStations.ToList().Find(b => b.StationCode == toRemove.NextStation);
                    BO.LineStation station2 = GetLineStations.ToList().Find(b => b.StationCode == toRemove.PrevStation);
                    station2.NextStation = station1.StationCode;
                    station1.PrevStation = station2.StationCode;
                }
                if (toRemove.NextStation != 0 && toRemove.PrevStation == 0)
                {
                    BO.LineStation station1 = GetLineStations.ToList().Find(b => b.StationCode == toRemove.NextStation);
                    station1.PrevStation = 0;
                }
                if (toRemove.NextStation == 0 && toRemove.PrevStation != 0)
                {
                    BO.LineStation station1 = GetLineStations.ToList().Find(b => b.StationCode == toRemove.PrevStation);
                    station1.NextStation = 0;
                }

                ///// Add the station back to the full list of stations///////////////////////
                BO.Station station = allStations.ToList().Find(b => b.Code == toRemove.StationCode);
                GetStations.Add(station);
                GetLineStations.Remove(toRemove);
                add--;
                RefreshLineStation();
                RefreshStation();


            }
            catch (BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }






        }



        /// <summary>
        /// Add Station to the line
        /// </summary>
        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                convertStation = new BO.LineStation();
                add++;
                var fxElt = sender as FrameworkElement; //get the Station from the listView
                BO.Station ToAdd = fxElt.DataContext as BO.Station;
                convertStation.StationCode = ToAdd.Code;
                convertStation.LineStationIndex = add;

                ///convert from Station to LineStation///////////

                if (add > 1)
                {
                    convertStation.PrevStation = GetLineStations[add - 2].StationCode;
                    convertStation.NextStation = 0;
                    GetLineStations[add - 2].NextStation = convertStation.StationCode;
                }
                else
                {
                    convertStation.PrevStation = 0;
                    convertStation.NextStation = 0;
                }


                addToListLineStation(ToAdd);



            }
            catch (BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                add--;
            }

        }

        /// <summary>
        /// add the line to the System
        /// </summary>
        private void FinishAddLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MessageBoxResult result = MessageBox.Show("אתה בטוח שברצונך לשמור קו זה", "add messege", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            try
                            {
                                newLine = GridDataLine.DataContext as BO.Line;

                                newLine.StationsOfBus = GetLineStations;

                                newLine.FirstStationCode = GetLineStations.ToList().ElementAt(0).StationCode;
                                newLine.LastStationCode = GetLineStations.ToList().ElementAt(GetLineStations.Count() - 1).StationCode;
                                newLine.LineExist = true;


                                int idnumber = bl.AddLine(newLine);
                                idLineFromDS = idnumber;
                                newLine.IdNumber = idnumber;



                                MessageBox.Show("הקו נשמר בהצלחה למערכת", "Success Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                this.DialogResult = true;
                                this.Close();

                            }
                            catch (BO.BadIdException a)
                            {
                                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                            }


                            return;
                        }
                    case MessageBoxResult.No:
                        {
                            break;
                        }


                }

            }
            catch (BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// update Line detiales
        /// </summary>
        private void updataStationLine_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                MessageBoxResult result = MessageBox.Show("אתה בטוח שברצונך לעדכן את פרטי התחנה הבאה?", "update Message", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            try
                            {
                                line.StationsOfBus = GetLineStations;
                                foreach (var item in line.StationsOfBus)
                                {
                                    item.LineId = line.IdNumber;
                                }

                                bool a = bl.UpdateLineStation(line);

                                this.DialogResult = true;
                                this.Close();
                            }
                            catch (BO.BadIdException a)
                            {
                                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            return;
                        }
                    case MessageBoxResult.No:
                        {
                            break;
                        }
                    case MessageBoxResult.Cancel:
                        {
                            this.Close();
                            break;
                        }
                }

            }

            catch (BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// To add a station the not exsit
        /// </summary>
        private void AddNewStation_Click(object sender, RoutedEventArgs e)
        {
            AddStation addStation = new AddStation(bl);

            bool? result = addStation.ShowDialog();

            if (result != null)
            {
                try
                {
                    GetStations = ConvertList(bl.GetAllStations());
                    RefreshStation();
                }
                catch (BO.BadIdException a)
                {
                    MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

        }
        #endregion

        #region More Func
        private void LineNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x));
        }
        public ObservableCollection<T> ConvertList<T>(IEnumerable<T> listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }
        #endregion
    }
}

