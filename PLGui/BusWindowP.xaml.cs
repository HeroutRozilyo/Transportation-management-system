using BlAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PLGui
{
    /// <summary>
    /// Interaction logic for busTry.xaml
    /// </summary>
    public partial class BusWindowP : Page
    {
        #region reset
        private IBL bl;
        private BO.Bus bus, newbus;
        private ObservableCollection<BO.Bus> egged = new ObservableCollection<BO.Bus>();
        private List<BO.Bus> temp = new List<BO.Bus>();
        public bool add = false;
        int index;
        #endregion

        #region constructors
        [Obsolete("not using",true)]
        public BusWindowP()
        {
            InitializeComponent();
        }

        public BusWindowP(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            LincestextBox.IsReadOnly = true;
            RefreshDataBus();
            StatusComboBox.ItemsSource = Enum.GetValues(typeof(BO.STUTUS));
           
            this.DataContext = newbus = new BO.Bus();
           
        buses.ItemsSource = egged;
            buses.SelectedIndex = 0;
            StartingDate.DisplayDateEnd = DateTime.Today;
            lastTreatmentTextBox.DisplayDateStart = DateTime.Today.AddYears(-3);
            lastTreatmentTextBox.DisplayDateEnd = DateTime.Today;

        }

        #endregion

        #region refresh and ComboBox
        private void buses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            bus = (buses.SelectedItem as BO.Bus);
            busesData.DataContext = bus;
            add = false;
            Update2.IsEnabled = true;
            send.IsEnabled = true;
            fuel.IsEnabled = true;
            DeleteBus1.IsEnabled = true;


            ////
        }

        private void RefreshDataBus()
        {


            egged = Convert<BO.Bus>(bl.GetAllBus());//to make ObservableCollection
            buses.ItemsSource = egged;

        }
        #endregion

        #region Button Click

        /// <summary>
        /// to send Bus to refuling
        /// </summary>
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

        /// <summary>
        /// To send Bus to treatment
        /// </summary>
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

        /// <summary>
        /// Add Bus to the system
        /// </summary>
        private void AddBus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!add)//if it's the first time that the user click the button
                {
                    RefreshDataBus();
                    LabelStatus.Visibility = Visibility.Hidden;
                    StatusComboBox.Visibility = Visibility.Hidden;
                    add = true;
                    Update2.IsEnabled = false;
                    send.IsEnabled = false;
                    fuel.IsEnabled = false;
                    DeleteBus1.IsEnabled = false;
                    LincestextBox.IsReadOnly = false;
                    sendTrip.IsEnabled = false;


                }
                else//to finally add the bus
                if (LincestextBox.Text != "" && StartingDate.Text != "")
                {
                    add = false;
                    HelpAddBus();
                    bl.AddBus(newbus);
                    RefreshDataBus();
                    buses.SelectedIndex = egged.Count() - 1;
                    LabelStatus.Visibility = Visibility.Visible;
                    StatusComboBox.Visibility = Visibility.Visible;
                    LincestextBox.IsReadOnly = true;
                    sendTrip.IsEnabled = true;
                    MessageBox.Show("האוטובוס נוסף בהצלחה למערכת", "Success Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("כדי להוסיף אוטובוס, בבקשה אכנס מספר רישוי ותאריך התחלה. כדי לבטל לחץ cancle", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
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
                add = true;
            }

        }



        /// <summary>
        /// for add bus by Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {



                if (e.Key == Key.Return && add && LincestextBox.Text != "" && StartingDate.Text != "" )  //if enter            
                {
                    Update2.IsEnabled = true;
                    send.IsEnabled = true;
                    fuel.IsEnabled = true;
                    DeleteBus1.IsEnabled = true;
                    AddBus.IsEnabled = true;
                    add = false;
                    HelpAddBus();
                    bl.AddBus(newbus);
                    RefreshDataBus();
                    buses.SelectedIndex = egged.Count() - 1;
                    LabelStatus.Visibility = Visibility.Visible;
                    StatusComboBox.Visibility = Visibility.Visible;
                    LincestextBox.IsReadOnly = false;
                    sendTrip.IsEnabled = false;
                    MessageBox.Show("האוטובוס נוסף בהצלחה למירכת", "Success Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                if (e.Key == Key.Return && add && (LincestextBox == null || StartingDate.Text == ""))
                {
                    MessageBoxResult result = MessageBox.Show("כדי להוסיף אוטובוס, נא אכנס מספר רישוי ותאריך תחילת פעילות . לביטול לחץ בטל", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
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

        /// <summary>
        /// to update the detiales of the bus in the system
        /// </summary>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bus != null&&LincestextBox.Text != "" && StartingDate.Text != "")
                {
                    HelpAddBus();
                    bus.BusExsis = true;
                    bl.UpdateBus(newbus);

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

       /// <summary>
       /// to delete bus from the station
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
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

        /// <summary>
        /// send Bus to trip
        /// </summary>
        private void sendTrip_Click(object sender, RoutedEventArgs e)
        {
            index = buses.SelectedIndex;
            SendToTrip wnd = new SendToTrip(bl, bus);
            bool? result = wnd.ShowDialog();
            if (result == true)
            {
                RefreshDataBus();
                buses.SelectedIndex = index;
                double km = wnd.Km;
              

            }
        }
        #endregion

        #region More func
        public ObservableCollection<T> Convert<T>(IEnumerable<T> listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }
        private void LincestextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void KmtextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var ch in e.Text)
            {

                if (!((Char.IsDigit(ch) || ch.Equals('.'))))
                {
                    e.Handled = true;

                    break;
                }
            }
        }

      

        /// <summary>
        /// to take out the detials from the TextBoxs
        /// </summary>
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
        #endregion
    }
}




