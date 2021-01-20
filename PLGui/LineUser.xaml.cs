using BlAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PLGui
{
    /// <summary>
    /// Interaction logic for LineUser.xaml
    /// </summary>
    public partial class LineUser : Page
    {
        #region reset
        string numberText;
        private IBL bl;
        IEnumerable<BO.Line> searchResult;
        private ObservableCollection<object> lineStationOfLine = new ObservableCollection<object>();
        BO.Line line = new BO.Line();
        #endregion
        #region constructor
        [Obsolete("not using",true)]
        public LineUser()
        {
            InitializeComponent();
        }

        public LineUser(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            NotExist.Visibility = Visibility.Hidden;
        }
        #endregion

        #region refresh
        public void refreshLineData()
        {
            if (line != null)
            {

                Looz.ItemsSource = line.TimeLineTrip;
                lineStationOfLine = ConvertList(bl.DetailsOfStation(line.StationsOfBus));

            }
            else
            {
                Looz.ItemsSource = null;
                lineStationOfLine = null;
            }
            GridDataLine.DataContext = line;
            StationLineList.ItemsSource = lineStationOfLine;

        }
        #endregion
        #region Button Click and DoubleClick
        /// <summary>
        ///  Search for a specific line
        /// </summary>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (numberText != null)
            {
                int numberLine = Convert.ToInt32(numberText);
                SearchHelp(numberLine);
            }

        }
        /// <summary>
        /// to search by Enter
        /// </summary>
        private void textBoxSeaarchLine_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {

                TextBox text = sender as TextBox;
                int lineNum = Convert.ToInt32(text.Text);
                SearchHelp(lineNum);

            }
            if (e.Key == Key.Back)
            {
                numberLineSearchRes.ItemsSource = searchResult;


            }

        }
        /// <summary>
        /// to Choose a specific line </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numberLineSearchRes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = (ListView)sender; //to get the line
            line = list.SelectedItem as BO.Line;
            refreshLineData();
        }
        #endregion


        #region focus on search TextBox
        private void textBoxSeaarchLine_GotFocus(object sender, RoutedEventArgs e)
        {
            line = null;
            numberLineSearchRes.SelectedIndex = -1;
            refreshLineData();
            textBoxSeaarchLine.Text = null;


        }

        private void textBoxSeaarchLine_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxSeaarchLine.Text != "...חפש מספר קו כאן" && textBoxSeaarchLine.Text != "")
                numberText = textBoxSeaarchLine.Text;
            textBoxSeaarchLine.Text = "...חפש מספר קו כאן";
        }

        #endregion

        #region More func
        private void textBoxSeaarchLine_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x));
        }

        public ObservableCollection<T> ConvertList<T>(IEnumerable<T> listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }
       

        /// <summary>
        /// do the search line and connected to the listView
        /// </summary>
        /// <param name="numberLine"></param>
        public void SearchHelp(int numberLine)
        {


            searchResult = bl.GetLineByLineCode(numberLine);
            if (searchResult.Count() > 0)
            {
                NotExist.Visibility = Visibility.Hidden;
                numberLineSearchRes.ItemsSource = searchResult;

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(numberLineSearchRes.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Area");
                view.GroupDescriptions.Add(groupDescription);


            }
            else
            {

                numberLineSearchRes.ItemsSource = searchResult;
                NotExist.Visibility = Visibility.Visible;
            }
        }
        #endregion
    }

}


