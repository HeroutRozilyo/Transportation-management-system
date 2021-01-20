using BlAPI;
using System.Windows;

namespace PLGui
{
    /// <summary>
    /// Main screen - Login / new user
    /// </summary>
    public partial class MainWindow : Window
    {
        #region varieble
        IBL bl = factoryBL.GetBl();
        LoginWindow wnd;
        AddUser addUser;
        #endregion

        #region connstructor
        public MainWindow()
        {
            IBL bl = factoryBL.GetBl();

            InitializeComponent();
        }
        #endregion

        #region button
        /// <summary>
        /// to login to the program
        /// </summary>
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            wnd = new LoginWindow(bl);
            wnd.Show();
            this.Close();

        }

        /// <summary>
        /// to add new user
        /// </summary>
        private void NewUser_Click(object sender, RoutedEventArgs e)
        {          
            addUser = new AddUser();

            bool? result = addUser.ShowDialog();
            if (result == true)
            {

                wnd = new LoginWindow(bl);
                wnd.Show();
                this.Close();
            }
        }
        #endregion
    }
}
