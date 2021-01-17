using BlAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private IBL bl;
        private BO.User userNow=new BO.User();
        
      
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
            if(frame.CanGoBack)
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
