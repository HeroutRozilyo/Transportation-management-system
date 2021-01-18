using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


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
            myBus = new Bus();
            this.DataContext = myBus;
        }




        public Bus NewBus
        {

            get
            {
                return myBus;
            }

        }


        private void okey_Click(object sender, RoutedEventArgs e) //to add the data of bus that the customer insert
        {
            try
            {
                myBus = new Bus(int.Parse(this.licenseTextBox.Text), DateTime.Parse(this.lastTreatDatePicker.Text)); //build bus with this data. if the licence is allready exsis or not valid we have exeption.
                this.DialogResult = true;
                this.Close();

            }
            catch (ArgumentException a)
            {

                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }



        }


        private void cancle_Click(object sender, RoutedEventArgs e) //close the window
        {
            this.Close();
        }



        //func that enable insert onlly number to the text lable.
        private void licenseTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;

            //allow list of system keys 
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
             || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right || e.Key == Key.Decimal)
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox

            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
            return;
        }

        private void kmTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}



