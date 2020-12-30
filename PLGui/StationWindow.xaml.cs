using BlAPI;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        private IBL bl;

        public StationWindow()
        {
            InitializeComponent();
        }

        public StationWindow(IBL bl)
        {
            this.bl = bl;
        }
    }
}
