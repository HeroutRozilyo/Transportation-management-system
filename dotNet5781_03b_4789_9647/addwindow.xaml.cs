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

namespace dotNet5781_03b_4789_9647
{
    /// <summary>
    /// Interaction logic for addwindow.xaml
    /// </summary>
    public partial class addWindow : Window
    {



        private Bus myBus;




        public addWindow()
        {
            InitializeComponent();
            try
            {
                myBus = new Bus();

                this.DataContext = myBus;
            }
            catch (Exception)
            {
                MessageBox.Show("The new licence is not valid,\n please enter again number licence with 8 digite", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }





        }

        private void InitializeComponent()
        {
            
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


        private void cancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
