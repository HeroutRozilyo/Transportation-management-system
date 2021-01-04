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
    /// Interaction logic for LineWindow.xaml
    /// </summary>
    public partial class LineWindow : Window
    {
        private IBL bl;
        private BO.Line line;
       
        private ObservableCollection<BO.Line> egged = new ObservableCollection<BO.Line>();
        private ObservableCollection<object> lineStationOfLine = new ObservableCollection<object>();
        private List<PL.LineStationUI> lineStationOfLineUI = new List<PL.LineStationUI>();

        private BO.AREA area;
        public LineWindow()
        {
            InitializeComponent();
        }

        public LineWindow(IBL bl)
        {
            InitializeComponent();

            this.bl = bl;
         //   RefreshLine();
            comboBoxArea.ItemsSource = Enum.GetValues(typeof(BO.AREA));
            StationLineList.ItemsSource = lineStationOfLine;


        }
        public ObservableCollection<T> Convert<T>(IEnumerable<T> listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }
       
        private void RefreshLine()
        {
            egged = Convert(bl.GetLineByArea(area)) ;//to make ObservableCollection
            ListOfLine.ItemsSource = egged;
            

        }
        private void RefreshStationListView()
        {
            if (line != null)
            {
                lineStationOfLine = Convert(bl.DetailsOfStation(line.StationsOfBus));

                Looz.ItemsSource = line.TimeLineTrip;

            }
            else
            {
                lineStationOfLine = null;
                Looz.ItemsSource = null;
            }
            StationLineList.ItemsSource = lineStationOfLine;
            GridDataLine.DataContext = line;
          
        }

        private void comboBoxArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            area = (BO.AREA)(comboBoxArea.SelectedItem);
            RefreshLine();
            line = null;
            RefreshStationListView();
          //  busesData.DataContext = bus;
        }

        private void ListOfLine_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = (ListView)sender; //to get the line
            line = list.SelectedItem as BO.Line;
            RefreshStationListView();

        }

        private void DeleteLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                MessageBoxResult result= MessageBox.Show("You sure you want to delete that line?", "Delete Line Message", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch(result)
                {
                    case MessageBoxResult.Yes:
                        {
                            var fxElt = sender as FrameworkElement; //get the licence of the bus to refulling. 
                            BO.Line lineToDelete = fxElt.DataContext as BO.Line;
                            bl.DeleteLine(lineToDelete.IdNumber);
                            RefreshLine();
                            MessageBox.Show("The line was successfully deleted from the system", "Success Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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

        

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {

            AddLine addline = new AddLine(bl);
           bool?result= addline.ShowDialog();
            if(result!=null)
            {
                BO.Line newline = addline.NewLine;
                line = newline;
                RefreshStationListView();
                comboBoxArea.SelectedItem = newline.Area;
                RefreshLine();




            }
        }

        private void UpdateLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (line != null)
                    bl.UpdateLine(line);
                int index = comboBoxArea.SelectedIndex;
                RefreshLine();
                comboBoxArea.SelectedIndex = index;



                MessageBox.Show("Line details saved successfully", "Success Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteStationLine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdataLineStation_Click(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement; //get the licence of the bus to refulling. 
          object lineData = fxElt.DataContext as object;//to get the line

         
            UpdataStationLineIndex updataStationLineIndex = new UpdataStationLineIndex(line, lineData);

            updataStationLineIndex.ShowDialog();
           
            
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddSchedules_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
