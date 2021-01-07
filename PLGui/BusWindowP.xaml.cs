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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLGui
{
    /// <summary>
    /// Interaction logic for busTry.xaml
    /// </summary>
    public partial class BusWindowP : Page
    {
    
            private IBL bl;
            private BO.Bus bus, newbus;
            private ObservableCollection<BO.Bus> egged = new ObservableCollection<BO.Bus>();
            private List<BO.Bus> temp = new List<BO.Bus>();
            private bool add = false;


            public BusWindowP()
            {
                InitializeComponent();
            }

            public BusWindowP(IBL _bl)
            {
                InitializeComponent();
                bl = _bl;

                RefreshDataBus();
                StatusComboBox.ItemsSource = Enum.GetValues(typeof(BO.STUTUS));
                buses.IsReadOnly = true;
                buses.ItemsSource = egged;
                buses.SelectedIndex = 0;




            }

            public ObservableCollection<T> Convert<T>(IEnumerable<T> listFromBO)
            {
                return new ObservableCollection<T>(listFromBO);
            }

            private void buses_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {


               bus = (buses.SelectedItem as BO.Bus);
                busesData.DataContext = bus;
            add = false;

                ////
            }

            private void RefreshDataBus()
            {


                egged = Convert<BO.Bus>(bl.GetAllBus());//to make ObservableCollection
                buses.ItemsSource = egged;
          
          



            }


            private void RefulingClick(object sender, RoutedEventArgs e)
            {
                bus = (buses.SelectedItem as BO.Bus);
                if (bus != null)
                {
                    try
                    {
                        bus = bl.Refuelling(bus);
                        FuelTextBox.Text = bus.FuellAmount.ToString();

                    }
                    catch (BO.BadBusLicenceException a)
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
                    RefreshDataBus();
                   
                    add = true;


                }
                else if (LincestextBox.Text != "" && StartingDate.Text != "")
                {
                    add = false;
                   // HelpAddBus();
                     newbus = GridDataBus.DataContext as BO.Bus;
                    bl.AddBus(newbus);
                    RefreshDataBus();
                    buses.SelectedIndex = egged.Count() - 1;

                    MessageBox.Show("The Bus Was Successfully added to the System", "Success Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("In order to create a bus, you need to fill in the license number   and Starting Date field. To cancel the process click cancel", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    switch (result)
                    {
                        case MessageBoxResult.OK: break;
                        case MessageBoxResult.Cancel:
                            {
                                buses.SelectedIndex = 0;
                            }
                            break;
                    }
                }

                }
                catch (BO.BadBusLicenceException a)
                {
                    buses.SelectedIndex = 0;
                    MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                add=true;
                }

            }



            private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
            {
                try
                {
                    if (e.Key == Key.Return && add && LincestextBox.Text != "" && StartingDate.Text != "")  //if enter            
                    {

                        add = false;
                        HelpAddBus();
                        bl.AddBus(newbus);
                        RefreshDataBus();
                        buses.SelectedIndex = egged.Count() - 1;
                        MessageBox.Show("The Bus Was Successfully added to the System", "Success Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    if (e.Key == Key.Return && add && (LincestextBox == null || StartingDate.Text == ""))
                    {
                        MessageBoxResult result = MessageBox.Show("In order to create a bus, you need to fill in the license number   and Starting Date field. To cancel the process click cancel", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                        switch (result)
                        {
                            case MessageBoxResult.OK: break;
                            case MessageBoxResult.Cancel:
                                {
                                    buses.SelectedIndex = 0;
                                }
                                break;
                        }
                    }
                }
                catch (BO.BadBusLicenceException a)
                {
                    buses.SelectedIndex = 0;
                    MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void Update_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                if (bus != null)
                {
                    bus.BusExsis = true;
                    bl.UpdateBus(bus);
                    
                }
                    int index = buses.SelectedIndex;
                    RefreshDataBus();
                    buses.SelectedIndex = index;

                    MessageBox.Show("Bus details saved successfully", "Success Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                catch (BO.BadBusLicenceException a)
                {
                    MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }

            private void NewKmTextboBox_TextChanged(object sender, TextChangedEventArgs e)
            {

            }

            private void button_Click(object sender, RoutedEventArgs e)
            {


            }

            private void DeleteBus_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    if (bus != null)
                        bl.DeleteBus(bus.Licence);
                    RefreshDataBus();
                    buses.SelectedIndex = 0;
                }
                catch (BO.BadBusLicenceException a)
                {
                    MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

            public void HelpAddBus()
            {
                try
                {
                    newbus = new BO.Bus();
                    if (KmtextBox.Text == "") KmtextBox.Text = "0";
                    newbus.Kilometrz = double.Parse(KmtextBox.Text);
                    if (lastTreatmentTextBox.Text == "") lastTreatmentTextBox.Text = DateTime.Now.ToString();
                    newbus.LastTreatment = DateTime.Parse(lastTreatmentTextBox.Text);
                    newbus.Licence = LincestextBox.Text;
                    newbus.StartingDate = DateTime.Parse(StartingDate.Text);
                    newbus.StatusBus = (BO.STUTUS)StatusComboBox.SelectedIndex;
                    if (NewKmTextboBox.Text == "") NewKmTextboBox.Text = "0";
                    newbus.KilometrFromLastTreat = double.Parse(NewKmTextboBox.Text);
                    newbus.BusExsis = true;
                if (FuelTextBox.Text == "") FuelTextBox.Text = "1200";
                newbus.FuellAmount = int.Parse(FuelTextBox.Text);
                }
                catch (BO.BadBusLicenceException a)
                {
                    MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }
    }




