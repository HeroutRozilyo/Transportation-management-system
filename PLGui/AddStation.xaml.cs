using BlAPI;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Device.Location;
using System.Collections.Generic;
using BO;

namespace PLGui
{
    /// <summary>
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Window
    {
        private IBL bl;
        public BO.Station toAdd = new BO.Station();
        private IEnumerable<LineStation> stationsOfBus;

        public BO.Station NewStation
        {

            get
            {
                return toAdd;
            }

        }

        public AddStation()
        {
            InitializeComponent();
        }

        public AddStation(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
           
        }

        public AddStation(IEnumerable<LineStation> stationsOfBus, IBL bl)
        {
            this.stationsOfBus = stationsOfBus;
            this.bl = bl;
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource stationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationViewSource")));
        //    Load data by setting the CollectionViewSource.Source property:
        //     stationViewSource.Source = [generic data source]
        //}

        private void stationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
      
{
            try
            {
                if (e.Key == Key.Return  && addressTextBox.Text != "" && codeTextBox.Text != "" && nameTextBox.Text!="" && longitudeTextBox.Text != "" && latitudeTextBox.Text != "")  //if enter            
                {
                    MessageBoxResult resultMessege = MessageBox.Show("You sure you want to add that line?", "Delete Line Message", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                    switch (resultMessege)
                    {
                        case MessageBoxResult.Yes:
                            {
                                helpData();
                                bl.AddStation(toAdd);
                                MessageBox.Show("The Bus Was Successfully added to the System", "Success Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                this.DialogResult = true;
                                this.Close();
                                break;
                            }
                        case MessageBoxResult.No:
                        {
                                break;
                            
                        }
                        case MessageBoxResult.Cancel:
                            {
                            this.DialogResult= false;
                            this.Close();
                            break;
                            }

                    }
                   
                }
                if (e.Key == Key.Return  && (addressTextBox.Text == "" || codeTextBox.Text == "" || nameTextBox.Text == ""|| longitudeTextBox.Text==""|| latitudeTextBox.Text==""))
                {
                    MessageBoxResult result = MessageBox.Show("In order to create a Station, you have to fill all the field", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    switch (result)
                    {
                        case MessageBoxResult.OK: break;
                        case MessageBoxResult.Cancel:
                            {
                                this.DialogResult = false;
                                this.Close();
                                break;
                            }
                          
                    }
                }
            }
            catch (BO.BadCoordinateException a)
            {
               
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void helpData()
        {
            toAdd.Address = addressTextBox.Text;
            toAdd.Code = Convert.ToInt32(codeTextBox.Text);
            toAdd.Coordinate = new GeoCoordinate(double.Parse((latitudeTextBox.Text)), double.Parse(longitudeTextBox.Text));
            toAdd.Name = nameTextBox.Text;
            toAdd.StationExist = (bool)stationExistCheckBox.IsChecked;
            
        }
    }
}
