using BlAPI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System;

namespace PLGui
{
    /// <summary>
    /// to enable to the maneger choose the new index to the station
    /// </summary>
    public partial class UpdataStationLineIndex : Window
    {
        #region varieble
        private IBL bl;
        private BO.Line line;
        private BO.LineStation temp;

        public BO.Line NewLine
        {
            get { return line; }

        }
        IEnumerable<int> numberIndex;
        #endregion

        #region constructor
        [Obsolete("not use", true)]
        public UpdataStationLineIndex()
        {
            InitializeComponent();
        }



        /// <summary>
        /// get the line station to upadate at her the index
        /// </summary>
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
        #endregion

        #region moreFunc
        public ObservableCollection<T> Convert<T>(IEnumerable<T> listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }
        #endregion

        #region combobox
        /// <summary>
        /// to get the new index that the maneger choose and update the list line station
        /// </summary>
        private void IndexCOmboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("האם אתה בטוח שאתה רוצה לשנות את המיקום של התחנה? פעולה זו בלתי הפיכה", "Updata Index Station Message", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            BO.LineStation change = line.StationsOfBus.ToList().Find(b => b.LineStationIndex == temp.LineStationIndex && b.StationCode == b.StationCode);
                            List<BO.LineStation> help = line.StationsOfBus.ToList();

                            int index = help.FindIndex(b => b.LineStationIndex == temp.LineStationIndex && b.StationCode == b.StationCode);
                            help.RemoveAt(index);
                            
                            if (change.LineStationIndex < IndexCOmboBox.SelectedIndex + 1)
                            {
                                change.LineStationExist = false;
                            }
                            else
                            {
                                BO.LineStation change1= help.Find(b => b.LineStationIndex == IndexCOmboBox.SelectedIndex + 1 && b.StationCode != change.StationCode);
                                help.Remove(change1);
                                change1.LineStationIndex = change1.LineStationIndex+1;
                                help.Add(change1);

                            }
                            change.LineStationIndex = IndexCOmboBox.SelectedIndex + 1;
                          

                            help.Add(change);

                            line.StationsOfBus = from item in help
                                                 orderby item.LineStationIndex
                                           
                                                 select item;

                            bl.UpdateLineStationForIndexChange(line);




                            this.DialogResult = true;

                            this.Close();
                            break;
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
        #endregion

    }
}
