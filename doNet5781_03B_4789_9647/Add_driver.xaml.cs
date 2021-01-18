using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace doNet5781_03B_4789_9647
{//
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
            this.DataContext = driver;
        }

        public Drivers NEwDriver
        {
            get
            {
                return driver;
            }

        }

        private void AddDriver_click(object sender, RoutedEventArgs e) //to add driver after rhe customer insert data
        {
            try
            {
                driver = new Drivers(int.Parse(this.Idnum.Text), (this.NameDriverTe.Text)); //creat driver with the nea data. if ID not valid or allresy exsis so throw.
                this.DialogResult = true;
                this.Close();

            }
            catch (ArgumentException a)
            {

                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void button_Click(object sender, RoutedEventArgs e) //to add driver when the customer do enter and not click on the button to add.
        {
            try
            {
                driver = new Drivers(int.Parse(this.Idnum.Text), this.NameDriverTe.Text);

                this.DialogResult = true;
                this.Close();

            }
            catch (ArgumentException messege)
            {

                MessageBox.Show(messege.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }


        // func that enable nlly to insert numbers to ID text lable.
        private void Idnum_PreviewKeyDown(object sender, KeyEventArgs e)
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

        //func that enable onlly to insert letters to Name text lable.
        private void NameDriverTe_PreviewKeyDown(object sender, KeyEventArgs e)
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


            if (Char.IsLetter(c))
                return;

            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
            return;
        }


    }
}
