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
    /// Interaction logic for BusData.xaml
    /// </summary>
    public partial class BusData : Window
    {
        private BO.Bus item;
        public BO.Bus temp = new BO.Bus();
        public BusData()
        {
            InitializeComponent();
        }

        public BusData(BO.Bus item)
        {
            InitializeComponent();
            this.item = item;
            temp = item;
            this.DataContext = item;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
