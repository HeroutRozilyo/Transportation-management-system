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
        private BO.Bus bus;
        private ObservableCollection<BO.Bus> egged = new ObservableCollection<BO.Bus>();
        private List<BO.Bus> temp = new List<BO.Bus>();

        public BusWindow()
        {
            InitializeComponent();
        }
        
        public BusWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;

            temp = bl.GetAllBus().ToList();
            egged = Convert<BO.Bus>(temp);//to make ObservableCollection
            StatusComboBox.ItemsSource = Enum.GetValues(typeof(BO.STUTUS));
            //buses.DisplayMemberPath = "Licence";//show only specific Property of object
            //buses.SelectedValuePath = "Licence";//selection return only specific Property of object
            //buses.SelectedIndex = 0; //index of the object to be selected
            buses.ItemsSource = egged;
        

        }
        public ObservableCollection<T> Convert<T>(IEnumerable<T>listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }
        
        private void buses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bus = (buses.SelectedItem as BO.Bus);
            busesData.DataContext = bus;
            


        }

        private void RefreshDataBus()
        {
            throw new NotImplementedException();
        }
    }
}
