using BlAPI;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;

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
                if (txtPassword.Password.Length != 0)
                {
                    BO.User users = new BO.User();
                    users.UserName = txtUserName.Text;
                    users.Password = txtPassword.Password;
                   BO.User ex = bl.findUser(users);
                    if (ex.Admin)
                    {
                        AdminWindow wnd = new AdminWindow(bl, ex);
                        wnd.Show();

                    }
                    else
                    {
                        UserWindow wnd = new UserWindow(bl, ex);
                        wnd.Show();
                    }

                    this.Close();
                }
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
            if (check())
            {
                try
                {

                    BO.User user = new BO.User();
                    user = bl.getUserByEmail(emailTextBOx.Text);
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress("projectdh209@gmail.com");
                        mail.To.Add(user.MailAddress);
                        mail.Subject = "שחזור סיסמא";
                        mail.Body = string.Format("Hello {0} ,your Password to GMG system is- {1} .It is recommended that you change your password the next time you log in. Good Day!", user.UserName,user.Password);
                        mail.IsBodyHtml = true;



                        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtp.Credentials = new NetworkCredential("projectdh209@gmail.com", "h0558828934");
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
