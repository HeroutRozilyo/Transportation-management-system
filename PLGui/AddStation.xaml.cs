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



        

        private void helpData()
        {
            toAdd.Address = addressTextBox.Text;
            toAdd.Code = Convert.ToInt32(codeTextBox.Text);
            toAdd.Coordinate = new GeoCoordinate(double.Parse((latitudeTextBox.Text)), double.Parse(longitudeTextBox.Text));
            toAdd.Name = nameTextBox.Text;
            toAdd.StationExist = true;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            helpData();
            bl.AddStation(toAdd);
            MessageBox.Show("האוטובוס נוסף בהצלחה למערכת", "Success Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            this.DialogResult = true;
            this.Close();
         
        }
    }
}
