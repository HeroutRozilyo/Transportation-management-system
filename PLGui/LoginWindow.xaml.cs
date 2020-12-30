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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
//            InitializeComponent();
            BO.User user = new BO.User();
            
        }

        private void Click_Submit(object sender, RoutedEventArgs e)
        {
            BO.User user = new BO.User();
            user =(this.DataContext as BO.User);
            
            this.Close();
        }
    }

    /* public partial class Bus_Data : Window
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
*/
}
