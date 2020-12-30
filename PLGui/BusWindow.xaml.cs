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
    /// Interaction logic for BusWindow.xaml
    /// </summary>
    public partial class BusWindow : Window
    {
        private IBL bl;

        public BusWindow()
        {
            InitializeComponent();
        }
        private ObservableCollection<BO.Bus> egged = new ObservableCollection<BO.Bus>();
        private List<BO.Bus> temp = new List<BO.Bus>();
        public BusWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;

            temp = bl.GetAllBus().ToList();
            foreach (var item in temp)
             egged.Add(item);
            list_Bus_Data.ItemsSource = egged;
        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ///to build refuling func
        }

        private void list_Bus_Data_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = (ListView)sender; //to get the bus
            BO.Bus item = (BO.Bus)list.SelectedItem;

            BusData temp = new BusData(item); //open window to show bus data
            temp.ShowDialog();

            list_Bus_Data.Items.Refresh();
        }
    }
}
