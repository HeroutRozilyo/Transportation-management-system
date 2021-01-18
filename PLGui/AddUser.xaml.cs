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
        IBL bl = factoryBL.GetBl();
        IEnumerable<BO.User> allUser;
        string name;
        string pas;
        bool admin = false;

        public AddUser()
        {
            InitializeComponent();
            refresh();
        }

        public AddUser(IBL bll)
        {
            InitializeComponent();
            this.bl = bll;
            admin = true;
            refresh();
        }


        public void refresh()
        {
            allUser = bl.GetAllUsers();
        }
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
                                break;
                            }
                        case MessageBoxResult.Cancel:
                            {
                                this.Close();
                                return;
                            }

                    }
                }
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



                if (admin)
                    bl.AddUser(name, pas, true, mail);
                else
                    bl.AddUser(name, pas, false, mail);
                MessageBox.Show(" החשבון נוצר בהצלחה ", "שם משתמש", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();

            }
            catch (BO.BadNameExeption ex)
            {
                MessageBox.Show(" שם משתמש קיים במערכת, בבקשה אכנס שם אחר", "שם משתמש", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            refresh();

        }

        public bool check()
        //הפונקציה בודקת את נכונות הפרטים שהמשתמש הכניס למערכת
        {//בדיקה למייל
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(Mail.Text);
        }
    }

}
