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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        IBL bl = factoryBL.GetBl();
        string name;
        string pas;
        bool admin=false;

        public AddUser()
        {
            InitializeComponent();
        }

        public AddUser(IBL bll)
        {
            InitializeComponent();
            this.bl = bll;
            admin = true;
        }



        private void okey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                name = userNameTextBox.Text;
                pas = passwordTextBox.Text;
                if(admin)
                    bl.AddUser(name, pas, true);
                else
                    bl.AddUser(name, pas, false);
               
                this.DialogResult = true;
                this.Close();
            }
            catch(BO.BadNameExeption ex)
            {
                MessageBox.Show(" שם משתמש קיים במערכת, בבקשה אכנס שם אחר", "שם משתמש", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void userNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsLetter(x));
        }

        private void passwordTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x));
        }
    }
}
