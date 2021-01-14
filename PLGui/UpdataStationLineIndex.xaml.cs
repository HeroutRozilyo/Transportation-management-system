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
        private BO.LineStation temp;
       
        //
        public UpdataStationLineIndex()
        {
            InitializeComponent();
        }

       

        public BO.Line NewLine
        { get { return line; }
            
        }
        IEnumerable<int> numberIndex;
        public UpdataStationLineIndex(BO.Line line, BO.LineStation ToUpdate, IBL bl)
        {
            InitializeComponent();
           
            this.line = line;
            this.temp = ToUpdate;
            this.bl = bl;
            DataContext = line;
            numberIndex = from item in line.StationsOfBus
                          select item.LineStationIndex;
            IndexCOmboBox.ItemsSource = Convert<int>(numberIndex);

          








        }
        public ObservableCollection<T> Convert<T>(IEnumerable<T> listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }

        private void IndexCOmboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to save the changes?", "Updata Index Station Message", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            BO.LineStation change = line.StationsOfBus.ToList().Find(b => b.LineStationIndex == temp.LineStationIndex && b.StationCode == b.StationCode);
                            List<BO.LineStation> help = line.StationsOfBus.ToList();
                            help.RemoveAt(help.FindIndex(b => b.LineStationIndex == temp.LineStationIndex && b.StationCode == b.StationCode));
                            change.LineStationIndex = IndexCOmboBox.SelectedIndex+1;
                            help.Add(change);

                            line.StationsOfBus = from item in help
                                                 select item;

                            bl.UpdateLineStationForIndexChange(line);




                            this.DialogResult = true;

                            this.Close();
                            break;//
                        }
                    case MessageBoxResult.No:
                        {
                            break;
                        }
                }
            }
            catch (BO.BadIdException a)
            {

                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }



        
    }
}
