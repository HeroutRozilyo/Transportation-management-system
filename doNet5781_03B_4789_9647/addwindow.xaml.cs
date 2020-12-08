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
      
       public Bus myBus;
    

     
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

  


        public Bus NewBus
        {

            get
            {
                 return myBus;
            }

        }


        private void okey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myBus = new Bus
                {

                    License = this.licenseTextBox.Text,
                    StartingDate = DateTime.Parse(this.lastTreatDatePicker.Text)


                };

                this.DialogResult = true;
                this.Close();

            }
            catch(Exception)
            {
            
                MessageBox.Show("The new licence is not valid,\n please enter again number licence with 8 digite", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }



        }

       
        private void cancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void fuelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}

        private void kmTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //public static implicit operator addWindow(Bus_Data v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}



