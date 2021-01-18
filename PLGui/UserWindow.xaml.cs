using BlAPI;
using System.Windows;

namespace PLGui
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private IBL bl;
        private BO.User userNow = new BO.User();


        public UserWindow()
        {
            InitializeComponent();
        }

        public UserWindow(IBL bl, BO.User users)
        {
            this.DataContext = this;
            InitializeComponent();
            this.bl = bl;
            userNow = users;
            NameTextBlock.Text = users.UserName;

        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        private void Disengagement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void forward_Click(object sender, RoutedEventArgs e)
        {
            if (frame.CanGoForward)
            {
                frame.GoForward();
            }
        }

        private void accountDatiels_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new AccountDetails(bl, userNow));
        }

        private void contantUs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void line_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new LineUser(bl));
        }

        private void station_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new User());
        }

    }
}
