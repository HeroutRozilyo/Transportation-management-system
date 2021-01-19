using BlAPI;
using System.Windows;

namespace PLGui
{
    /// <summary>
    /// Main screen - Login / new user
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = factoryBL.GetBl();
        LoginWindow wnd;
        AddUser addUser;

        public MainWindow()
        {
            IBL bl = factoryBL.GetBl();

            InitializeComponent();


        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            wnd = new LoginWindow(bl);
            wnd.Show();
            this.Close();

        }

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
    }
}
