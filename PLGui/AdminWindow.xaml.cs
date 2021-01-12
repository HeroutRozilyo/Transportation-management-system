using BlAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PLGui
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private IBL bl;
        private AddUser addUser;
        private BO.User userNow = new BO.User();
        public AdminWindow()
        {
            InitializeComponent();

            
        }

        public AdminWindow(IBL _bl, BO.User users)
        {
            InitializeComponent();
            userNow = users;
            this.bl = _bl;
            NameTextBlock.Text = userNow.UserName;
        }

        private void AdminWindow_Closing(object sender, CancelEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            
        }
        //
        

        private void buses_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = (new BusWindowP(bl));
        }

        private void line_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = (new LineWindowP(bl));
        }

        private void station_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = (new  StationWindowP(bl));
        }

     

        private void user_Click_1(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow = new UserWindow(bl,userNow);
            userWindow.Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if(frame.CanGoBack)
            frame.GoBack();
        }

        private void forward_Click(object sender, RoutedEventArgs e)
        {
            if(frame.CanGoForward)
            frame.GoForward();
        }

        private void Disengagement_Click(object sender, RoutedEventArgs e)
        {
           
            LoginWindow wnd = new LoginWindow();
            this.Close();
            wnd.Show();
        }

        private void contantUs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void accountDatiels_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddManeger_Click(object sender, RoutedEventArgs e)
        {
            addUser = new AddUser(bl);

            bool? result = addUser.ShowDialog();
            
        }
    }
}
