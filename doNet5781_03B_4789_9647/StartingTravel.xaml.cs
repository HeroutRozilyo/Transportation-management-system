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
    /// Interaction logic for StartingTravel.xaml
    /// </summary>
    public partial class StartingTravel : Window
    {
        Bus temp = new Bus();
        Random r = new Random();
        public StartingTravel()
        {
            InitializeComponent();
        }


        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;

            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
             || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right||e.Key== Key.Decimal)
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

        public StartingTravel(Bus a)
        {
            InitializeComponent();
            temp = a;
            this.DataContext = temp;
        }


   

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
           
           if (e.Key == Key.Return)         
          {
                int a = int.Parse(numOfKm.Text);
                TimeSpan time = TimeSpan.Zero;
                if (temp.Take_travel(a))
                {
                    //x=x0+vt
                    double v = r.Next(20, 51); //velocitui of this bus
                    double t = a / v; //time of this travel.this time is in hour

                    time = TimeSpan.FromSeconds(6 * t); //time travel at our program

                   

                }
                else
                {
                    if (temp.Fuel - a <= 0)
                    {
                        MessageBox.Show("There is not enough fuel of the bus for this travel ", "ERROR FUEL", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    if (temp.newKm_from_LastTreatment + a > 20000)
                    {
                        MessageBox.Show("The bus cannot take the ride because it will go the number of miles allowed ", "ERROR KM", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    if (temp.lastTreat.AddYears(1) > DateTime.Today)
                    {
                        MessageBox.Show("It has been a year since the last treatment ", "ERROR TREATMENT", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
                this.Close();
            


           }

        }

      
    }
}
