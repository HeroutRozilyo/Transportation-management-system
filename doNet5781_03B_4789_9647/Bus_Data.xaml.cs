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
    /// Interaction logic for Bus_Data.xaml
    /// </summary>
    public partial class Bus_Data : Window
    {
        public Bus temp = new Bus();
     
    
        public Bus_Data(Bus v) //constructor that get bus to show
        {
            InitializeComponent();
            temp = v;
            this.DataContext = temp;
        }

        public Bus_Data(object item) //constructor that get object to show
        {
            InitializeComponent();

            this.DataContext = item;
      
        }


        private void treat_Click(object sender, RoutedEventArgs e) //send the bus to treatment
        {         
            (this.DataContext as Bus).treatment();
            this.Close();
        }

        private void fuel_Click(object sender, RoutedEventArgs e) //send the bus to refulling
        {
            (this.DataContext as Bus).Refuelling();
            this.Close();


        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {




        }
    }
}
