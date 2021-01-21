using BlAPI;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace PLGui
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        #region reset
        IBL bl = factoryBL.GetBl();
        IEnumerable<BO.User> allUser;
        string name;
        string pas;
        bool admin;
        #endregion

        #region constructors
        /// <summary>
        /// For new  Regular user
        /// </summary>
        public AddUser()
        {
            InitializeComponent();
            admin = false;
            refresh();

        }

        /// <summary>
        /// For new Admin User
        /// </summary>
        /// <param name="bll"></param>
        public AddUser(IBL bll)
        {
            InitializeComponent();
            this.bl = bll;
            admin = true;
            refresh();
        }

        #endregion constructors

        #region Refresh
        public void refresh()
        {
            try
            {
                allUser = bl.GetAllUsers();
            }
            catch (BO.BadNameExeption a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion


        #region Buttons Click
        /// <summary>
        /// to Add the new User
        /// </summary>
        private void okey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                name = userNameTextBox.Text;
                pas = passwordTextBox.Password;
                string mail = Mail.Text;
                string pas2 = passwordBox2.Password;
                if (pas != pas2)
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
                                this.Close();
                                return;
                            }

                    }
                }
                if (pas.Count() >= 8)//min Lengthto password
                {
                    if (check())
                    {
                        var v = from item in allUser
                                where item.MailAddress == mail
                                select item;
                        if (v.Count() != 0)
                        {
                            MessageBox.Show(" כתובת  מייל כבר קייימת במערכת, בבקשה הכנס כתובת חדשה ", "שם משתמש", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show(".הכנס כתובת אימייל תקינה ונסה שוב ", "New User", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                        switch (result)
                        {
                            case MessageBoxResult.OK:
                                {
                                    return;
                                }
                            case MessageBoxResult.Cancel:
                                {
                                    this.Close();
                                    return;
                                }

                        }
                    }

                    bl.AddUser(name, pas, admin, mail);

                    MessageBox.Show(" החשבון נוצר בהצלחה ", "שם משתמש", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("  על הסיסמא להכיל לפחות 8 תווים", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (BO.BadNameExeption)
            {
                MessageBox.Show(" שם משתמש קיים במערכת, בבקשה אכנס שם אחר", "שם משתמש", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            refresh();

        }
        #endregion

        #region moreFunc
        public bool check()
        //הפונקציה בודקת את נכונות הפרטים שהמשתמש הכניס למערכת
        {//בדיקה למייל
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(Mail.Text);
        }
        #endregion
    }

}
