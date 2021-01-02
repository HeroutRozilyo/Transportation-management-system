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
        /*private IBL bl;
       
        {
            InitializeComponent();
            bl = _bl;
            
            RefreshDataBus();
            StatusComboBox.ItemsSource = Enum.GetValues(typeof(BO.STUTUS));
            buses.IsReadOnly = true;
            buses.ItemsSource = egged;
            buses.SelectedIndex = 0;
           



        }

        public ObservableCollection<T> Convert<T>(IEnumerable<T>listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }*/
        private ObservableCollection<BO.Line> egged = new ObservableCollection<BO.Line>();
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

        }
        public ObservableCollection<T> Convert<T>(IEnumerable<T> listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }
        private void RefreshLine()
        {
            egged = Convert<BO.Line>(bl.GetAllLine());//to make ObservableCollection
           // LineComboBOx.ItemsSource = egged;
        }
       

        private void comboBoxArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            area = (BO.AREA)(comboBoxArea.SelectedItem);
            egged = Convert(bl.GetLineByArea(area));
            ListOfLine.ItemsSource = egged;
            GridDataLine.DataContext = null;
          //  busesData.DataContext = bus;
        }

        private void ListOfLine_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = (ListView)sender; //to get the line
			object item = list.SelectedItem;
            GridDataLine.DataContext = item;






        }

        private void DeleteLine_Click(object sender, RoutedEventArgs e)
        {

        }

        

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateLine_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
