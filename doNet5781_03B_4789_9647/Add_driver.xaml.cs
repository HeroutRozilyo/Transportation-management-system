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

namespace doNet5781_03B_4789_9647
{
    /// <summary>
    /// Interaction logic for Add_driver.xaml
    /// </summary>
    public partial class Add_driver : Window
        
    {
        public Drivers driver;
        public Add_driver()
        {

            InitializeComponent();
        driver = new Drivers();
        this.DataContext = NEwDriver;
    }
        public Drivers NEwDriver
        {

            get
            {
                return driver;
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Drivers driver = new Drivers(int.Parse(this.Idnum.Text), this.NameDriverTe.Text);
                this.DialogResult = true;
                this.Close();

            }
            catch (Exception)/////
            {

                MessageBox.Show("The new licence is not valid,\n please enter again number licence with 8 digite", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
