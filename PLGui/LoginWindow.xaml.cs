using BlAPI;
using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;

namespace PLGui
{
    /// <summary>
    /// Login to the System (for Admin and User)
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IBL bl;
        #region constructors
        [Obsolete("not in use",true)]
        public LoginWindow()
        {
            InitializeComponent();
        }

        public LoginWindow(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            BO.User user = new BO.User();
            passEmail.Visibility = Visibility.Hidden;

        }

        #endregion constructors

        #region Button Click
        /// <summary>
        /// To login
        /// </summary>
        private void Click_Submit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPassword.Password.Length != 0)//if the user entering a password.
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
            catch (BO.BadNameExeption a)//The data was incorrect, the user does not exist in the system
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// Password recovery
        /// </summary>
        private void forgetPassword_Click(object sender, RoutedEventArgs e)
        {
            passEmail.Visibility = Visibility.Visible;
        }

        private void SendMail_Click(object sender, RoutedEventArgs e)
        {
            if (check())//checking is the Email proper
            {
                try
                {

                    BO.User user = new BO.User();
                    user = bl.getUserByEmail(emailTextBOx.Text);//to get all the user
                    using (MailMessage mail = new MailMessage())//new Mail
                    {
                        mail.From = new MailAddress("projectdh209@gmail.com");//from the Host
                        mail.To.Add(user.MailAddress);//the User
                        mail.Subject = "שחזור סיסמא";
                        mail.Body = string.Format("Hello {0} ,your Password to GMG system is- {1} .We recommend changing your password the next time you log in. Good Day!", user.UserName,user.Password);
                        mail.IsBodyHtml = true;



                        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))//to connect Gmail
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
        #endregion

        #region moreFunc
        public bool check()//checking if the Mail is proper


        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");//MailFormat
            return regex.IsMatch(emailTextBOx.Text);
        }
        #endregion moreFunc
    }
}
