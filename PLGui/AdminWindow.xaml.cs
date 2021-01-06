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

        public AdminWindow()
        {
            InitializeComponent();

            
        }

        public AdminWindow(IBL _bl)
        {
            InitializeComponent();
            this.bl = _bl;
        }

        private void AdminWindow_Closing(object sender, CancelEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            
        }

        

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
            frame.Content = new StationWindowP(bl);
        }

        private void AddManeger_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
