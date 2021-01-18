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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLGui
{
    /// <summary>
    /// Interaction logic for AccountDetails.xaml
    /// </summary>
    public partial class AccountDetails : Page
    {
        private IBL bl;
        private BO.User userNow;

        public AccountDetails()
        {
            InitializeComponent();
        }

        public AccountDetails(IBL bl, BO.User userNow)
        {
            this.bl = bl;
            this.userNow = userNow;
            ChangePasswordGrid.Visibility = Visibility.Hidden;
        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordGrid.Visibility = Visibility.Visible;
        }

        private void ChangeMail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SavaPass_Click(object sender, RoutedEventArgs e)
        {
            
            string pas = passwordTextBox.Password;
            string pas2 = passwordBox2.Password;
            if(pas!=pas2)
            {
                MessageBoxResult result = MessageBox.Show(".הכנס סיסמא זהה בשני השדות ונסה שוב", "New User", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        {
                            return;
                        }
                    case MessageBoxResult.Cancel:
                        {
                            ChangePasswordGrid.Visibility = Visibility.Hidden;
                            return;
                        }

                }

            }
            else
            {
                userNow.Password = pas;
                //bl.use
            }
            ChangePasswordGrid.Visibility = Visibility.Hidden;

        }
    }
}
