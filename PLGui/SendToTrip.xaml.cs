using BlAPI;
using BO;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for SendToTrip.xaml
    /// </summary>
    public partial class SendToTrip : Window
    {
        double km;
        private IBL bl;
        private BO.Bus bus;

        public Double Km
        {
            get { return km; }
            set { km = value; }
        }
        public SendToTrip()
        {
            InitializeComponent();
        }

        public SendToTrip(IBL bl, BO.Bus bus)
        {
            this.bl = bl;
            this.bus = bus;
            InitializeComponent();
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return)  //if enter       
            {
                km = double.Parse(numOfKm.Text);
                if (km != 0)
                {
                    try
                    {
                        bool okey = bl.sendTotrip(bus, km);
                        if (okey)
                        {
                            this.DialogResult = true;

                        }
                        else this.DialogResult = false;
                    }
                    catch(BO.BadBusLKMException )
                    {
                        MessageBox.Show( "האוטובוס לא יכול לבצע את הנסיעה המבוקשת","",MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else MessageBox.Show("בבקשה הכנס מספר קילומטרים", "ERROR KM", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                this.Close();



            }
        }

        private void numOfKm_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x) || '.'.Equals(x));
        }
    }
}
