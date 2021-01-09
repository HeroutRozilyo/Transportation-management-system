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
    /// Interaction logic for DataAdjact.xaml
    /// </summary>
    public partial class DataAdjact : Window
    {

        IBL bl = factoryBL.GetBl();
        private BO.LineStation line;


        public DataAdjact()
        {
            InitializeComponent();
        }

        public DataAdjact(BO.LineStation station)
        {
            InitializeComponent();

            blickStation1.Text = station.StationCode.ToString();
            blockStation2.Text = station.NextStation.ToString();

        }
        public DataAdjact(BO.AdjacentStations station)
        {
            InitializeComponent();

            blickStation1.Text = station.Station1.ToString();
            blockStation2.Text = station.Station2.ToString();
        }



        public BO.LineStation NewLine
        {
            get
            {
                return line;
            }

        }

        private void textDistance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x) || '.'.Equals(x));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double a = Convert.ToDouble(textDistance.Text);
            double b = Convert.ToDouble(textTime.Text);
            line = new BO.LineStation()
            {
                DistanceFromNext = a,
                TimeAverageFromNext = b,
                LineStationExist = true,
                StationCode = Convert.ToInt32(blickStation1.Text),
                NextStation = Convert.ToInt32(blockStation2.Text),
            };


            bl.AddAdjactStation(line);
        
            this.Close();
        }
    }
}
