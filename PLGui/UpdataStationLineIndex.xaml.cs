using BlAPI;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UpdataStationLineIndex.xaml
    /// </summary>
    public partial class UpdataStationLineIndex : Window
    {
        private IBL bl;
        private BO.Line line;
        private object temp;

        public UpdataStationLineIndex()
        {
            InitializeComponent();
        }

        PL.LineStationUI UIstation;
        public UpdataStationLineIndex(BO.Line line, object temp)
        {
            InitializeComponent();
           
            this.line = line;
            this.temp = temp;
            UIstation = (PL.LineStationUI)temp;
             IndexCOmboBox.ItemsSource = Convert<BO.LineStation>(line.StationsOfBus);
            IndexCOmboBox.SelectedIndex = UIstation.LineStationIndex;




        }
        public ObservableCollection<T> Convert<T>(IEnumerable<T> listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }

        private void IndexCOmboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            
        }
    }
}
