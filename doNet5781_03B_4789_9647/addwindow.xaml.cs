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
using System.Resources;


namespace doNet5781_03B_4789_9647
{
    /// <summary>
    /// Interaction logic for addWindow.xaml
    /// </summary>
    
    public partial class addWindow : Window
    {
       
     
   
       private Bus myBus = new Bus();
        public addWindow()
        {
            InitializeComponent();

            this.DataContext = myBus;




        }

        public Bus NewBus
        {
            get
            {
                return myBus;
            }
        }

        private void okey_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;
            this.Close();


        }

        private void licenseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}



