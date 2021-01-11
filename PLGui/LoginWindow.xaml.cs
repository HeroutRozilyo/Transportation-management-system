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
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace PLGui
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IBL bl;

        public LoginWindow()
        {
            InitializeComponent();
            BO.User user = new BO.User();
            passEmail.Visibility = Visibility.Hidden;
        }

        public LoginWindow(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            BO.User user = new BO.User();
            passEmail.Visibility = Visibility.Hidden;
        }

        private void Click_Submit(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.User users = new BO.User();
                users.UserName = txtUserName.Text;
                users.Password = txtPassword.Password;
                bool ex = bl.findUser(users);
                if (ex)
                {
                    AdminWindow wnd = new AdminWindow(bl);
                    wnd.Show();

                }
                else
                {
                    UserWindow wnd = new UserWindow(bl);
                    wnd.Show();
                }

                this.Close();
            }
            catch (BO.BadNameExeption a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void forgetPassword_Click(object sender, RoutedEventArgs e)
        {
            passEmail.Visibility = Visibility.Visible;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(check())
            {
                try
                {
                   
                    BO.User user = new BO.User();
                    user = bl.getUserByEmail(emailTextBOx.Text);
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress("rozilyo@g.jct.ac.il.com");
                        mail.To.Add(user.MailAddress);
                        mail.Subject = "שחזור סיסמא";
                        mail.Body =string.Format("Your Password is-{0}",user.Password);
                        mail.IsBodyHtml = true;
                       
                       

                        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtp.Credentials = new NetworkCredential("rozilyo@g.jct.ac.il", "h209179647");
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }
                        MessageBox.Show("סיסמתך נשלחה לכתובת האימייל בהצלחה", "", MessageBoxButton.OK);
                    }
                }
                catch (SmtpFailedRecipientException ex)
                {
                    MessageBox.Show(ex.Message, "שגיאת מייל", MessageBoxButton.OK);
                   
                }
                catch (SmtpException ex)
                {
                    MessageBox.Show(ex.Message, "שגיאת מייל", MessageBoxButton.OK);
                }
                passEmail.Visibility = Visibility.Hidden;

               
            }
            else MessageBox.Show("הכנס כתובת אימייל תיקנית", "שגיאת מייל", MessageBoxButton.OK);

        }
        public bool check()
        //הפונקציה בודקת את נכונות הפרטים שהמשתמש הכניס למערכת
        {//בדיקה למייל
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(emailTextBOx.Text);
        }
    }
}
