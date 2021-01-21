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
        #region reset
        private IBL bl;
        private BO.User userNow;
        IEnumerable<BO.User> allUser;
        #endregion
        #region constructors
        [Obsolete("not use", true)]
        public AccountDetails()
        {
            InitializeComponent();
        }


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
        #endregion

        #region ButtonClick
        /// <summary>
        /// To Change Password
        /// </summary>
        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordGrid.Visibility = Visibility.Visible;
            ChangeMail.IsEnabled = false;
        }

        /// <summary>
        /// To change Mail Address
        /// </summary>
        private void ChangeMail_Click(object sender, RoutedEventArgs e)
        {
            ChangeMailGrid.Visibility = Visibility.Visible;
            ChangePass.IsEnabled = false;
        }
        /// <summary>
        /// To Save the New Password
        /// </summary>
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
                if (pas.Count() >= 8)//Min lentgh for Password
                {
                    if (pas != userNow.Password)
                    {
                        try
                        {
                            userNow.Password = pas;
                            bl.UpdateUser(userNow);
                            MessageBox.Show("!הסיסמה עודכנה בהצלחה", "סיסמה", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (BO.BadNameExeption a)
                        {
                            MessageBox.Show(a.Message, "סיסמה", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

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


        /// <summary>
        /// To Save the new Email Address
        /// </summary>
        private void ChangeMailButton_Click(object sender, RoutedEventArgs e)
        {
            string newMail = Mail.Text;
            if (newMail.Count() != 0)
            {
                if (check())//To check if the Mail Address proper
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


                            MessageBoxResult result = MessageBox.Show(" כתובת  מייל כבר קייימת במערכת, בבקשה הכנס כתובת חדשה", "", MessageBoxButton.OKCancel, MessageBoxImage.Error);
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

                            userNow.MailAddress = newMail;
                            try
                            {
                                bl.UpdateUser(userNow);
                                MessageBox.Show("!כתובת המייל עודכנה בהצלחה", "סיסמה", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            catch (BO.BadNameExeption a)
                            {
                                MessageBox.Show(a.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }

                    }
                }
                ChangeMailGrid.Visibility = Visibility.Hidden;
                mailAddressTextBlock.Text = userNow.MailAddress;
                ChangePass.IsEnabled = true;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show(" הכנס כתובת מייל חדשה", "", MessageBoxButton.OKCancel, MessageBoxImage.Error);
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


        }
        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("?האם אתה בטוח שברצונך למחוק את החשבון", "", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.OK:
                    {
                        try
                        {
                            bl.DeleteUser(userNow);
                            MessageBox.Show("!חשבונך נמחק בהצלחה, נשמח לראות אותך איתנו כאן שוב", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            if (userNow.Admin)
                            {
                                WindowCollection window = Application.Current.Windows;
                                int i = 0;
                                for (; i < window.Count; i++)
                                {
                                    if (window[i].Title == "AdminWindow")
                                        break;
                                }


                                MainWindow main = new MainWindow();
                                main.Show();
                                window[i].Close();


                            }
                            else
                            {
                                WindowCollection window = Application.Current.Windows;
                                int i = 0;
                                for (; i < window.Count; i++)
                                {
                                    if (window[i].Title == "UserWindow")
                                        break;
                                }
                                MainWindow main = new MainWindow();
                                main.Show();
                                window[i].Close();

                            }


                        }
                        catch (BO.BadNameExeption a)
                        {
                            MessageBox.Show(a.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        return;
                    }
                case MessageBoxResult.Cancel:
                    {

                        return;
                    }

            }
        }

        #endregion

        #region More Func
        public bool check()
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(Mail.Text);
        }
        #endregion


    }
}
