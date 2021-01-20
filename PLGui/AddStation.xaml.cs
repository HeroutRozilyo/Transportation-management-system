using BlAPI;
using BO;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Windows;

namespace PLGui
{
    /// <summary>
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Window
    {
        #region reset
        private IBL bl;
        public BO.Station toAdd = new BO.Station();
        private IEnumerable<LineStation> stationsOfBus;
        #endregion

        #region To return
        public BO.Station NewStation
        {

            get
            {
                return toAdd;
            }

        }
        #endregion

        #region constructors
        [Obsolete("not using",true)]
        public AddStation()
        {
            InitializeComponent();
        }

        public AddStation(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;

        }
        #endregion

        #region Button click
        /// <summary>
        /// to add the station to the system
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helpData();
                bl.AddStation(toAdd);
                MessageBox.Show("התחנה נוספה בהצלחה למערכת", "Success Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                this.DialogResult = true;
                this.Close();
            }
            catch (BO.BadCoordinateException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        #endregion

        #region More func
        /// <summary>
        /// to take of the data from the textBoxs
        /// </summary>
        private void helpData()
        {
            toAdd.Address = addressTextBox.Text;
            toAdd.Code = Convert.ToInt32(codeTextBox.Text);
            toAdd.Coordinate = new GeoCoordinate(double.Parse((latitudeTextBox.Text)), double.Parse(longitudeTextBox.Text));
            toAdd.Name = nameTextBox.Text;
            toAdd.StationExist = true;

        }
        #endregion
    }
}
