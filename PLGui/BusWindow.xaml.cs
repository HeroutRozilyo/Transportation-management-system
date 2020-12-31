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
        private BO.Bus bus,newbus;
        private ObservableCollection<BO.Bus> egged = new ObservableCollection<BO.Bus>();
        private List<BO.Bus> temp = new List<BO.Bus>();
        private bool add = false;
        public BusWindow()
        {
            InitializeComponent();
        }
        
        public BusWindow(IBL _bl)
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
        }
        
        private void buses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (add)
            {
                egged.RemoveAt(egged.Count - 1);
                add = false;
            }
            bus = (buses.SelectedItem as BO.Bus);
           busesData.DataContext = bus;


        }

        private void RefreshDataBus()
        {
            temp = bl.GetAllBus().ToList();
            egged = Convert<BO.Bus>(temp);//to make ObservableCollection
           
        }
      

        private void RefulingClick(object sender, RoutedEventArgs e)
        {
            bus = (buses.SelectedItem as BO.Bus);
            if(bus!=null)
            {
                try
                {
                    bus = bl.Refuelling(bus);
                    FuelTextBox.Text = bus.FuellAmount.ToString();
                }
                catch(BO.BadBusLicenceException a)
                {
                    MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
          

        }

        private void Treatment_Click(object sender, RoutedEventArgs e)
        {
            bus = (buses.SelectedItem as BO.Bus);
            if (bus != null)
            {
                try
                {
                    bus = bl.treatment(bus);
                    FuelTextBox.Text = bus.FuellAmount.ToString();
                    lastTreatmentTextBox.SelectedDate = bus.LastTreatment;
                    NewKmTextboBox.Text = bus.KilometrFromLastTreat.ToString();


                }
                catch (BO.BadBusLicenceException a)
                {
                    buses.SelectedIndex = 0;
                    MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddBus_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (!add)
                {
                    BO.Bus busadd = new BO.Bus();
                    busadd.StartingDate = DateTime.Today;
                    busadd.LastTreatment = DateTime.Today;
                  
                    egged.Add(busadd);
                    buses.SelectedIndex = egged.Count() - 1;
                    add = true;


                }
                else if (LincestextBox.Text != "")
                {
                    newbus = new BO.Bus();
                    add = false;
                    HelpAddBus();
                    egged.RemoveAt(egged.Count - 1);
                    bl.AddBus(newbus);
                    

                }
                else
                    buses.SelectedIndex = egged.Count() - 1;
                buses.IsReadOnly = false;
              
            }
            catch(BO.BadBusLicenceException a)
            {
                buses.SelectedIndex = 0;
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }






        }

        private void NewKmTextboBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return && add && LincestextBox.Text != "" && StartingDate != null)  //if enter       
                {
                    newbus = new BO.Bus();
                    add = false;
                    HelpAddBus();
                    egged.RemoveAt(egged.Count - 1);
                    bl.AddBus(newbus);
                    RefreshDataBus();

                }
                if (e.Key == Key.Return && add && (LincestextBox == null || StartingDate.Text == null))
                    MessageBox.Show("In order to create a bus, you need to fill in the license number   and Starting Date field", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
            catch(BO.BadBusLicenceException a)
            {
                buses.SelectedIndex = 0;
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void  HelpAddBus()
        {
            newbus = new BO.Bus();
            newbus.Kilometrz = double.Parse(KmtextBox.Text);
            newbus.LastTreatment = DateTime.Parse(lastTreatmentTextBox.Text);
            newbus.Licence = LincestextBox.Text;
            newbus.StartingDate = DateTime.Parse(StartingDate.Text);
            newbus.StatusBus = (BO.STUTUS)StatusComboBox.SelectedIndex;
            newbus.KilometrFromLastTreat = double.Parse(NewKmTextboBox.Text);
        }
    }
}
