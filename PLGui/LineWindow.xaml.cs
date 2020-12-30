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
    /// Interaction logic for LineWindow.xaml
    /// </summary>
    public partial class LineWindow : Window
    {
        private IBL bl;

        public LineWindow()
        {
            InitializeComponent();
        }

        public LineWindow(IBL bl)
        {
            this.bl = bl;
        }
    }
}
