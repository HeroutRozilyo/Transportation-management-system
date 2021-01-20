using BlAPI;
using System.Windows;
using System;
namespace PLGui
{
    /// <summary>
    /// the main window of the user
    /// </summary>
    public partial class UserWindow : Window
    {
        #region varieble
        private IBL bl;
        private BO.User userNow = new BO.User();
        #endregion

        #region constructor
        [Obsolete("not use",true)]
        public UserWindow()
        {
            InitializeComponent();
        }

        public UserWindow(IBL bl, BO.User users)
        {
            InitializeComponent();
            this.DataContext = this;        
            this.bl = bl;
            userNow = users;
            NameTextBlock.Text = users.UserName;

        }
        #endregion

        #region button
        /// <summary>
        /// go back to the preview page
        /// </summary>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        /// <summary>
        /// to go out from user accont to the main window
        /// </summary>
        private void Disengagement_Click(object sender, RoutedEventArgs e)
        {
            MainWindow a = new MainWindow();
            a.Show();
            this.Close();
          
        }

        /// <summary>
        /// to go forward
        /// </summary>
        private void forward_Click(object sender, RoutedEventArgs e)
        {
            if (frame.CanGoForward)
            {
                frame.GoForward();
            }
        }

        /// <summary>
        /// details of the user
        /// </summary>
        private void accountDatiels_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new AccountDetails(bl, userNow));
        }

        private void contantUs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("מצטערים, יישום זה עדיין לא זמין למשתמש", "בבניה", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void line_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new LineUser(bl));
        }

        private void station_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new User());
        }
        #endregion
    }
}
