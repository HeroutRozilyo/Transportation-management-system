using BlAPI;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        private IBL bl;
        private ObservableCollection<BO.Station> GetStations = new ObservableCollection<BO.Station>();
        private ObservableCollection<BO.Station> allStations = new ObservableCollection<BO.Station>();
        private ObservableCollection<BO.LineStation> GetLineStations = new ObservableCollection<BO.LineStation>();
        BO.LineStation convertStation = new BO.LineStation();
        static int add;
        int idLineFromDS = 0;
        int keepLineID = 0;
        BO.Line newLine = new BO.Line();
        private BO.Line line;

        public BO.Line NewLine
        {

            get
            {
                return newLine;
            }

        }
        public List <BO.LineStation> StationList
        {

            get
            {
                return GetLineStations.ToList();
            }

        }

        public AddLine()
        {
            InitializeComponent();
        }
        public ObservableCollection<T> ConvertList<T>(IEnumerable<T> listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }

        public AddLine(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
          
            GetStations = ConvertList(bl.GetAllStations());
            allStations = ConvertList(bl.GetAllStations()); 
            RefreshStation();
            RefreshLineStation();
            add = 0;
            AreaComboBox.ItemsSource = Enum.GetValues(typeof(BO.AREA));
            this.DataContext = newLine;
            GridDataLine.Visibility = Visibility.Visible;
            FinishAddLine.Visibility = Visibility.Visible;
            updataStationLine.Visibility = Visibility.Hidden;



        }

        public AddLine(BO.Line line, IBL bl)
        {
            InitializeComponent();
            this.line = line;
            this.bl = bl;
            GridDataLine.Visibility = Visibility.Hidden;
            FinishAddLine.Visibility = Visibility.Hidden;
            GetStations = ConvertList(bl.GetAllStations());
            allStations = ConvertList(bl.GetAllStations());
            updataStationLine.Visibility = Visibility.Visible;
            add = 0;
            orderGrid();
            RefreshStation();

        }

        private void orderGrid()
        {
            IEnumerable<Station> stations = from item in allStations
                                            from linestation in line.StationsOfBus
                                            where linestation.StationCode == item.Code
                                            orderby linestation.LineStationIndex
                                            select item;
           foreach (BO.Station stat in stations)
            {
                foreach (BO.LineStation lins in line.StationsOfBus)
                    {  
                    if(lins.StationCode==stat.Code)
                    {
                        convertStation = lins;
                        add++;
                        addTOListLineStation(stat);
                    }

                
                }
            }
            keepLineID = line.StationsOfBus.ElementAt(0).LineId;
        }

        private void RefreshStation()
        {
            InitializeComponent();
            Station.ItemsSource = GetStations;
            Station.Items.Refresh();
            if(LineNumber.Text!=""&& AreaComboBox.SelectedIndex!=-1&&GetLineStations.Count()>=2)
            {
                FinishAddLine.IsEnabled = true;
            }
            else FinishAddLine.IsEnabled = false;

        }
        private void RefreshLineStation()
        {
            StationOfTheLine.ItemsSource = GetLineStations;
            StationOfTheLine.Items.Refresh();
           
        }
        private void CancelStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var fxElt = sender as FrameworkElement; //get the licence of the bus to refulling. 
                BO.LineStation ToAdd = fxElt.DataContext as BO.LineStation;
                int index = ToAdd.LineStationIndex;
                if (index < GetLineStations.Count())
                {
                    for (int i = GetLineStations.Count()-1; i > index-1; i--)
                    {
                        GetLineStations[i].LineStationIndex--;
                    }
                }
                if(ToAdd.NextStation!=0&&ToAdd.PrevStation!=0)
                {
                    BO.LineStation station1 = GetLineStations.ToList().Find(b => b.StationCode == ToAdd.NextStation);
                    BO.LineStation station2 = GetLineStations.ToList().Find(b => b.StationCode == ToAdd.PrevStation);
                    station2.NextStation = station1.StationCode;
                    station1.PrevStation = station2.StationCode;
                }
               if( ToAdd.NextStation != 0 && ToAdd.PrevStation == 0)
                {
                    BO.LineStation station1 = GetLineStations.ToList().Find(b => b.StationCode == ToAdd.NextStation);
                    station1.PrevStation = 0;
                }
                if (ToAdd.NextStation == 0 && ToAdd.PrevStation != 0)
                {
                    BO.LineStation station1 = GetLineStations.ToList().Find(b => b.StationCode == ToAdd.PrevStation);
                    station1.NextStation = 0;
                }


                BO.Station station = allStations.ToList().Find(b => b.Code == ToAdd.StationCode);
                GetStations.Add(station);
                GetLineStations.Remove(ToAdd);
                add--;
                RefreshLineStation();
                RefreshStation();
                
                
            }
            catch (BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
               
            }






        }




        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                convertStation = new BO.LineStation();
                add++;
                var fxElt = sender as FrameworkElement; //get the licence of the bus to refulling. 
                BO.Station ToAdd = fxElt.DataContext as BO.Station;
                convertStation.StationCode = ToAdd.Code;
                convertStation.LineStationIndex = add;
            


                if (add>1)
                {
                    convertStation.PrevStation = GetLineStations[add - 2].StationCode;
                    convertStation.NextStation = 0;
                    GetLineStations[add - 2].NextStation = convertStation.StationCode;
                }
                else
                {
                    convertStation.PrevStation = 0;
                    convertStation.NextStation = 0;
                }


                addTOListLineStation(ToAdd);
                


            }
            catch(BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                add--;
            }

        }

        private void addTOListLineStation(BO.Station ToAdd)
        {
            convertStation.LineStationExist = true;
            GetLineStations.Add(convertStation);
            Station check = GetStations.ToList().Find(b => b.StationExist && b.Code == ToAdd.Code);
            GetStations.Remove(check);
            RefreshLineStation();
            RefreshStation();
        }

        private void AreaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshStation();
        }

        

        private void FinishAddLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MessageBoxResult result = MessageBox.Show("אתה בטוח שברצונך להוסיף קו זה", "add messege", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            newLine=GridDataLine.DataContext as BO.Line;
                          
                          newLine.StationsOfBus = GetLineStations;
                            
                            newLine.FirstStationCode = GetLineStations.ToList().ElementAt(0).StationCode;
                            newLine.LastStationCode = GetLineStations.ToList().ElementAt(GetLineStations.Count() - 1).StationCode;
                            newLine.LineExist = true;


                            int idnumber =bl.AddLine(newLine);
                            idLineFromDS = idnumber;
                            newLine.IdNumber = idnumber;

                      

                            MessageBox.Show("הקו הוסף בהצלחה למערכת", "Success Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            this.DialogResult = true;
                            this.Close();
                            return;


                           
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

        private void LineNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshStation();
        }

        private void LineNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;

            //allow list of system keys 
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
             || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right )
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox

            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
            return;
        }
      
     

        private void AddNewStation_Click(object sender, RoutedEventArgs e)
        {


            
            AddStation  addStation = new AddStation(bl);
            
            bool? result = addStation.ShowDialog();

            if (result != null)
            {
                GetStations = ConvertList(bl.GetAllStations());
                RefreshStation();

            }

        }

        private void updataStationLine_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                MessageBoxResult result = MessageBox.Show("אתה בטוח שברצונך לעדכן את פרטי התחנה הבאה?", "update Message", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            line.StationsOfBus = GetLineStations;
                            foreach(var item in line.StationsOfBus)
                            {
                                item.LineId = line.IdNumber;
                            }

                            bool a = bl.UpdateLineStation(line);
                       
                            this.DialogResult = true;
                            this.Close();




               //             RefreshLineStation();
                            return;
                        }
                    case MessageBoxResult.No:
                        {
                            break;
                        }
                    case MessageBoxResult.Cancel:
                        {
                            this.Close();
                            break;
                        }
                }

                }
            
                catch (BO.BadIdException a)
            {
                MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LineNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x));
        }
    } 
    }

