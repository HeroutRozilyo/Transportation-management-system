using BlAPI;
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
    /// Interaction logic for LineWindowP.xaml
    /// </summary>
    public partial class LineWindowP : Page
    {
        #region reset
        private IBL bl;
        private BO.Line line;
        private ObservableCollection<BO.Line> egged = new ObservableCollection<BO.Line>();
        private ObservableCollection<object> lineStationOfLine = new ObservableCollection<object>();
        private ObservableCollection<BO.Line> GENERAL = new ObservableCollection<BO.Line>();
        private ObservableCollection<BO.Line> SOUTH = new ObservableCollection<BO.Line>();
        private ObservableCollection<BO.Line> CENTER = new ObservableCollection<BO.Line>();
        private ObservableCollection<BO.Line> JERUSALEM = new ObservableCollection<BO.Line>();
        private ObservableCollection<BO.Line> NORTH = new ObservableCollection<BO.Line>();
        private ObservableCollection<BO.Line> YOSH = new ObservableCollection<BO.Line>();
        private BO.AREA area;
        BO.LineStation TempLineStation = new BO.LineStation();
        BO.LineTrip thisLooz = new BO.LineTrip();
        BO.LineTrip thisLooz1 = new BO.LineTrip();
        int indexLineTripOld;
        private bool isUpdateLooz = false;
        #endregion
        #region constructors
        [Obsolete("not using", true)]
        public LineWindowP()
        {
            InitializeComponent();
        }

        public LineWindowP(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            comboBoxArea.ItemsSource = Enum.GetValues(typeof(BO.AREA));
            AreaUpdateLineTextBox.ItemsSource = Enum.GetValues(typeof(BO.AREA));
            StationLineList.ItemsSource = lineStationOfLine;
            NewLooz.Visibility = Visibility.Hidden;
            NewLooz.DataContext = new BO.LineTrip();
            comboBoxArea.SelectedIndex = 0;





        }
        #endregion

        
        #region refresh
        /// <summary>
        /// to set new Area Line in the ListView Line/>
        /// </summary>
        private void RefreshLine()
        {

            switch (area)
            {
                case BO.AREA.CENTER:
                    {
                        egged = CENTER;
                        break;
                    }
                case BO.AREA.GENERAL:
                    {
                        egged = GENERAL;
                        break;
                    }
                case BO.AREA.JERUSALEM:
                    {
                        egged = JERUSALEM;
                        break;
                    }
                case BO.AREA.NORTH:
                    {
                        egged = NORTH;
                        break;
                    }
                case BO.AREA.SOUTH:
                    {
                        egged = SOUTH;
                        break;
                    }
                case BO.AREA.YOSH:
                    {
                        egged = YOSH;
                        break;
                    }
            }
            ListOfLine.ItemsSource = egged;
            ListOfLine.Items.Refresh();


        }

        /// <summary>
        /// to refresh the Line detials 
        /// </summary>
        private void RefreshStationListView()
        {
            if (line != null)
            {
                line = bl.GetLineByLine(line.IdNumber);
                lineStationOfLine = Convert(bl.DetailsOfStation(line.StationsOfBus));
                AreaUpdateLineTextBox.SelectedItem = line.Area;
                Looz.ItemsSource = line.TimeLineTrip;
                ListOfLine.SelectedIndex = egged.ToList().FindIndex(b => b.IdNumber == line.IdNumber);
                Looz.Items.Refresh();
                NewLooz.DataContext = new BO.LineTrip();
            }
            else
            {
                lineStationOfLine = null;
                Looz.ItemsSource = null;
            }
            StationLineList.ItemsSource = lineStationOfLine;
            GridDataLine.DataContext = line;

            Looz.Visibility = Visibility.Visible;

            NewLooz.Visibility = Visibility.Hidden;



            //
        }
        #endregion

        /// <summary>
        /// To get Line by grouping
        /// </summary>
      

        #region Data selection
        private void comboBoxArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listArea();
            area = (BO.AREA)(comboBoxArea.SelectedItem);
           
            RefreshStationListView();

        }

        private void ListOfLine_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = (ListView)sender; //to get the line
            line = list.SelectedItem as BO.Line;
            RefreshStationListView();

        }

        /// <summary>
        /// add finally tripLine to the line
        /// </summary>
        private void tripLineExistCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {

                if (finishAtTextBox.Text != "" && frequencyTextBox.Text != "" && frequencyTextBox.Text != "" && startAtTextBox.Text != "")
                {

                    MessageBoxResult result = MessageBox.Show("?לשמור שינויים ", "הודעת מערכת", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            {

                                BO.LineTrip addLineTrip = new BO.LineTrip();
                                addLineTrip = NewLooz.DataContext as BO.LineTrip;
                                addLineTrip.KeyId = line.IdNumber;
                                if (addLineTrip.Frequency == 0) addLineTrip.FinishAt = addLineTrip.StartAt;

                                if (addLineTrip.FinishAt < addLineTrip.StartAt)//like line that start in 22:00 and over in 1:00:00
                                {
                                    int hour = 24 + addLineTrip.FinishAt.Hours;
                                    TimeSpan toChange = new TimeSpan(hour, 0, 0);
                                    addLineTrip.FinishAt = toChange;
                                }
                                if (!isUpdateLooz)
                                {

                                    bl.AddOneTripLine(addLineTrip);

                                }
                                else
                                {
                                    isUpdateLooz = false;
                                    bl.UpdateLineTrip(indexLineTripOld, addLineTrip);

                                }

                                MessageBox.Show("השינויים נשמרו בהצלחה", "Success Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                Looz.Visibility = Visibility.Visible;
                                NewLooz.Visibility = Visibility.Hidden;
                                NewLooz.DataContext = new BO.LineTrip();
                                RefreshStationListView();
                                break;


                            }
                        case MessageBoxResult.No:
                            {
                                break;
                            }
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("אנא מלא את כל השדות", "הודעת מערכת", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    switch (result)
                    {
                        case MessageBoxResult.OK:
                            {
                                break;
                            }
                        case MessageBoxResult.Cancel:
                            {

                                NewLooz.DataContext = null;
                                NewLooz.Visibility = Visibility.Hidden;
                                RefreshStationListView();
                                break;
                            }
                    }

                }
            }
            catch (BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                NewLooz.DataContext = null;
                NewLooz.Visibility = Visibility.Hidden;
                Looz.Visibility = Visibility.Visible;

                RefreshStationListView();


            }
        }

        /// <summary>
        /// Cancel adding the schedule
        /// </summary>
        private void cancleTripLineAdd_Checked(object sender, RoutedEventArgs e)
        {
            Looz.Visibility = Visibility.Visible;
            AddSchedules.Visibility = Visibility.Visible;
            NewLooz.Visibility = Visibility.Hidden;
            cancleTripLineAdd.IsChecked = false;
            RefreshStationListView();
        }

        #endregion

        #region Button Click
        /// <summary>
        /// To delete Line drom the system
        /// </summary>
        private void DeleteLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MessageBoxResult result = MessageBox.Show("אתה בטוח שברצונך למחוק קו זה?", "Delete Line Message", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            var fxElt = sender as FrameworkElement;
                            BO.Line lineToDelete = fxElt.DataContext as BO.Line;
                            bl.DeleteLine(lineToDelete.IdNumber);
                            line = null;
                            listArea();
                         
                            RefreshStationListView();
                            MessageBox.Show("הקו נמחק בהצלחה", "Success Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            break;
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
        /// Add Line to the System
        /// </summary>
        private void AddLine_Click(object sender, RoutedEventArgs e)
        {

            AddLine addline = new AddLine(bl);
            bool? result = addline.ShowDialog();
            if (result == true)
            {
                BO.Line newline = addline.NewLine;
                line = newline;
                listArea();

                RefreshStationListView();
                comboBoxArea.SelectedItem = newline.Area;
                



            }
        }
        /// <summary>
        /// update Line detials
        /// </summary>
        private void UpdateLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (line != null)
                    bl.UpdateLine(line);
                comboBoxArea.SelectedItem = line.Area;
                ListOfLine.SelectedItem = line;
                listArea();
                RefreshStationListView();

                MessageBox.Show("פרטי הקו נשמרו בהצלחה", "Success Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// delete StationLine from the Line
        /// </summary>
        private void DeleteStationLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fxElt = sender as FrameworkElement; //get the licence of the bus to refulling. 
                String ToDel = fxElt.DataContext.ToString();
                convertFromObbject(ToDel);
                bl.DeleteStation(line.IdNumber, TempLineStation.StationCode);
                RefreshStationListView();
            }
            catch (BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Update Index of StationLine
        /// </summary>
        private void UpdataLineStation_Click(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement; 
            string StationLineData = fxElt.DataContext.ToString();
            convertFromObbject(StationLineData);//to TempLineStation
            UpdataStationLineIndex updataStationLineIndex = new UpdataStationLineIndex(line, TempLineStation, bl);
            bool? result = updataStationLineIndex.ShowDialog();
            if (result == true)
            {
                BO.Line newline = updataStationLineIndex.NewLine;
                line = bl.GetLineByLine(newline.IdNumber);
                RefreshStationListView();
                comboBoxArea.SelectedItem = newline.Area;

            }


        }

        /// <summary>
        /// Add LineStation to the line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            AddLine addStationTotheLine = new AddLine(line, bl);
            bool? result = addStationTotheLine.ShowDialog();
            if (result == true)
            {
                RefreshStationListView();
            }



        }

        /// <summary>
        /// Add schedual to the line
        /// </summary>
        private void AddSchedules_Click(object sender, RoutedEventArgs e)
        {
            NewLooz.Visibility = Visibility.Visible;
            Looz.Visibility = Visibility.Hidden;

        }
        /// <summary>
        /// update Sceduals
        /// </summary>
        private void UpdataLineTrip_Click(object sender, RoutedEventArgs e)
        {
            Button toconvert = sender as Button;
            thisLooz = toconvert.DataContext as BO.LineTrip;
            indexLineTripOld = line.TimeLineTrip.ToList().FindIndex(b => thisLooz.StartAt == b.StartAt);
            thisLooz.TripLineExist = false;
            isUpdateLooz = true;
            Looz.Visibility = Visibility.Hidden;
            NewLooz.DataContext = thisLooz;

            NewLooz.Visibility = Visibility.Visible;


        }

        /// <summary>
        /// delete Sceduals of the line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteTripLine_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("האם למחוק לוח זמנים? פעולה זו בלתי הפיכה", "Add Line trip Message", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        Button toconvert = sender as Button;
                        thisLooz = toconvert.DataContext as BO.LineTrip;
                        bl.DeleteLineTrip(thisLooz);
                        RefreshLine();
                        RefreshStationListView();
                        MessageBox.Show("לוח הזמנים נמחק בהצלחה", "הודעת מערכת", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                case MessageBoxResult.No:
                    {
                        break;
                    }
            }
        }
        #endregion
        #region More func
        private void listArea()
        {
            CENTER.Clear(); GENERAL.Clear(); NORTH.Clear(); SOUTH.Clear(); YOSH.Clear(); JERUSALEM.Clear();
            foreach (var group in bl.GetLinesByAreaG())
            {
                if (group.Key == BO.AREA.CENTER)
                    foreach (var line in group)
                        CENTER.Add(line);
                if (group.Key == BO.AREA.GENERAL)
                    foreach (var line in group)
                        GENERAL.Add(line);
                if (group.Key == BO.AREA.JERUSALEM)
                    foreach (var line in group)
                        JERUSALEM.Add(line);
                if (group.Key == BO.AREA.NORTH)
                    foreach (var line in group)
                        NORTH.Add(line);
                if (group.Key == BO.AREA.SOUTH)
                    foreach (var line in group)
                        SOUTH.Add(line);
                if (group.Key == BO.AREA.YOSH)
                    foreach (var line in group)
                        YOSH.Add(line);

                RefreshLine();
            }
        }

        /// <summary>
        /// Except from Object the details related to us to lineStation
        /// </summary>
        /// <param name="StationLineData"></param>
        private void convertFromObbject(string StationLineData)
        {
            StationLineData = StationLineData.Remove(0, 16);
            int index = StationLineData.IndexOf(",");
            TempLineStation.StationCode = int.Parse(StationLineData.Substring(0, index));
            index = StationLineData.IndexOf("= ");
            StationLineData = StationLineData.Remove(0, index + 2);
            TempLineStation.LineStationIndex = int.Parse(StationLineData.Substring(0, StationLineData.IndexOf(",")));


        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource lineTripViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("lineTripViewSource")));

        }

        private void EndTimeLooz_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x) || ':'.Equals(x));
        }

        private void frequencyTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x) || '.'.Equals(x));
        }

        private void startAtTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x) || ':'.Equals(x));
        }

        public ObservableCollection<T> Convert<T>(IEnumerable<T> listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }
        #endregion
    }
}

