using BlAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        IEnumerable<BO.User> allUser;
        public AccountDetails(IBL bl, BO.User userNow)
        {
            InitializeComponent();
            this.bl = bl;
            this.userNow = userNow;
            ChangePasswordGrid.Visibility = Visibility.Hidden;
            ChangeMailGrid.Visibility = Visibility.Hidden;
            allUser = bl.GetAllUsers();
            UserDatGrid.DataContext = userNow;
        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordGrid.Visibility = Visibility.Visible;
            ChangeMail.IsEnabled =false;
        }

        private void ChangeMail_Click(object sender, RoutedEventArgs e)
        {
            ChangeMailGrid.Visibility = Visibility.Visible;
            ChangePass.IsEnabled = false;
        }

        private void SavaPass_Click(object sender, RoutedEventArgs e)
        {
            
            string pas = passwordTextBox.Password;
            string pas2 = passwordBox2.Password;

            if (pas != pas2)
            {
                MessageBoxResult result = MessageBox.Show(".הכנס סיסמא זהה בשני השדות ונסה שוב", "סיסמה", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        {
                            return;
                        }
                    case MessageBoxResult.Cancel:
                        {
                            ChangePasswordGrid.Visibility = Visibility.Hidden;
                            ChangeMail.IsEnabled = true;
                            return;
                        }

                }

            }
            else
            {
                if (pas.Count() >= 8)
                {
                    if (pas != userNow.Password)
                    {
                        userNow.Password = pas;
                        bl.UpdateUser(userNow);
                        MessageBox.Show("!הסיסמה עודכנה בהצלחה", "סיסמה", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("על הסיסמא החדשה להיות שונה מהסיסמא האחרונה", "סיסמה", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                        switch (result)
                        {
                            case MessageBoxResult.OK:
                                {
                                    return;
                                }
                            case MessageBoxResult.Cancel:
                                {
                                    ChangePasswordGrid.Visibility = Visibility.Hidden;
                                    ChangeMail.IsEnabled = true;

                                    return;
                                }

                        }
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("על הסיסמא החדשה להכיל לפחות 8 תווים", "סיסמה", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    switch (result)
                    {
                        case MessageBoxResult.OK:
                            {
                                return;
                            }
                        case MessageBoxResult.Cancel:
                            {
                                ChangePasswordGrid.Visibility = Visibility.Hidden;
                                ChangeMail.IsEnabled = true;

                                return;
                            }
                    }

                }
                ChangePasswordGrid.Visibility = Visibility.Hidden;
                ChangeMail.IsEnabled = true;
            }

        }

        private void ChangeMailButton_Click(object sender, RoutedEventArgs e)
        {
            string newMail = Mail.Text;
            if (check())
            {
                if (newMail == userNow.MailAddress)
                {

                    MessageBoxResult result = MessageBox.Show("על הכתובת אימייל האחדשה להיות שונה מהכתובת במערכת", "", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    switch (result)
                    {
                        case MessageBoxResult.OK:
                            {
                                return;
                            }
                        case MessageBoxResult.Cancel:
                            {
                                ChangePasswordGrid.Visibility = Visibility.Hidden;
                                ChangePass.IsEnabled = true;
                                return;
                            }

                    }

                }
                else
                {
                    var v = from item in allUser
                            where item.MailAddress == newMail
                            select item;
                    if (v.Count() != 0)
                    {
                        MessageBox.Show(" כתובת  מייל כבר קייימת במערכת, בבקשה הכנס כתובת חדשה ", "שם משתמש", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    userNow.MailAddress = newMail;
                    bl.UpdateUser(userNow);
                    MessageBox.Show("!כתובת המייל עודכנה בהצלחה", "סיסמה", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            ChangeMailGrid.Visibility = Visibility.Hidden;
            mailAddressTextBlock.Text = userNow.MailAddress;
            ChangePass.IsEnabled = true;


        }
        public bool check()
        //הפונקציה בודקת את נכונות הפרטים שהמשתמש הכניס למערכת
        {//בדיקה למייל
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(Mail.Text);
        }
    }
}
