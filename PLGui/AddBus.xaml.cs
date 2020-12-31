using BlAPI;
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
    /// Interaction logic for AddBus.xaml
    /// </summary>
    public partial class AddBus : Window
    {
        BO.Bus myBus;
        private IBL bl;

        public AddBus()
        {
            InitializeComponent();
            myBus = new BO.Bus();
            this.DataContext = myBus;
        }

        public AddBus(IBL bl)
        {
            this.bl = bl;
        }

        public BO.Bus NewBus
        {

            get
            {
                return myBus;
            }

        }
        private void licenseTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void KMTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void okey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myBus.Kilometrz = int.Parse(KMTextBox.Text);
                myBus.StartingDate = DateTime.Parse(lastTreatDatePicker.Text);
                myBus.Licence = licenseTextBox.Text;
                bl.AddBus(myBus);
                this.DialogResult = true;
                this.Close();

            }
            catch (BO.BadBusLicenceException a)
            {

                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void cancle_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

        }
    }
}
