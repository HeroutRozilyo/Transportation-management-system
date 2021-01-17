using BlAPI;
using BO;
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
    /// Interaction logic for RealTimeStation.xaml
    /// </summary>
    public partial class RealTimeStation : Window, INotifyPropertyChanged
    {
        private IBL bl;
        private Station stationData1;

        public RealTimeStation()
        {
          InitializeComponent();
          TimeSpan n= TimeSpan.Parse(BO.ClockS.Instance.ToString());


        }

        public RealTimeStation(IBL bl, Station stationData1)
        {
            InitializeComponent();
            this.bl = bl;
            this.stationData1 = stationData1;
            TimeSpan n = TimeSpan.Parse(BO.ClockS.Instance.ToString());
            kuku.Text = String.Format("{0:D2}:{1:D2}:{2:D2}", n.Hours, n.Minutes, n.Seconds);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
