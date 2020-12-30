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

        private void allbuses_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
