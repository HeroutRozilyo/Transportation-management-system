﻿using BlAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


//...

namespace PLGui
{
    /// <summary>
    /// Interaction logic for User.xaml. when we search on path between 2 stations
    /// </summary>
    public partial class User : Page
    {
        IBL bl = factoryBL.GetBl();
        private ObservableCollection<Object> temp = new ObservableCollection<Object>();
        private ObservableCollection<BO.Line> line1 = new ObservableCollection<BO.Line>();
        private ObservableCollection<BO.Line> line2 = new ObservableCollection<BO.Line>();
        private ObservableCollection<BO.Line> empty = new ObservableCollection<BO.Line>();

        BO.Station stationData1 = new BO.Station();
        private IEnumerable<BO.Line> temp1;
        BO.Station stationData2 = new BO.Station();
        private IEnumerable<BO.Line> temp2;

        private ObservableCollection<BO.Station> stations1 = new ObservableCollection<BO.Station>();
        private ObservableCollection<BO.Station> stations2 = new ObservableCollection<BO.Station>();



        /// <summary>
        /// defult constructor
        /// </summary>
        public User()
        {
            InitializeComponent();

            //to unsert data to the window
            RefreshStation(); 
            RefreshStationall();
            realTime.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// convert the ienumerable from BO to be a collection observer
        /// </summary>
        public ObservableCollection<T> ConvertList<T>(IEnumerable<T> listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }

        /// <summary>
        /// refresh the window of station
        /// </summary>
        private void RefreshStation()
        {
            //we need 2 list in order to do the 2 combobox not to depent
            stations1 = ConvertList(bl.GetAllStations());//to make ObservableCollection
            stations2 = ConvertList(bl.GetAllStations());
            foreach (var item in stations1)//creat the first combobox
            {
                ComboBoxItem newItem1 = new ComboBoxItem();
                newItem1.Content = item.Code + "   " + item.Name;
                station1.Items.Add(newItem1);

            }
            foreach (var item in stations2)//creat the secont combobox
            {
                ComboBoxItem newItem2 = new ComboBoxItem();
                newItem2.Content = item.Code + "   " + item.Name;
                station2.Items.Add(newItem2);
            }

        }

        private void station1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string a = station1.SelectedItem.ToString();

            int codStation = getNum1(a);

            temp1 = bl.GetAllLineIndStation(codStation);
            // LineInStation1.ItemsSource = temp1;

        }
        private int getNum1(string a)
        {

            int b = a.Length;
            string c = "";

            for (int i = 0; i < b; i++)
            {
                if (a.ElementAt(i) <= '9' && a.ElementAt(i) >= '0')
                    c += a.ElementAt(i);
            }
            int codStation = Convert.ToInt32(c);
            return codStation;
        }

        private void station2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string a = station2.SelectedItem.ToString();

            int codStation = getNum1(a);

            temp2 = bl.GetAllLineIndStation(codStation);

            //  LineInStation2.ItemsSource = temp2;

        }




        private void checkOkey_Click(object sender, RoutedEventArgs e)
        {
            temp = null;
            int cod1 = getNum1(station1.SelectedItem.ToString());
            int cod2 = getNum1(station2.SelectedItem.ToString());
            if (cod1 == cod2)
            {
                MessageBox.Show("אופס...תחנות המוצא והיעד קרובות מידי", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else temp = ConvertList(bl.TravelPath(cod1, cod2));

            OpsiaLine.ItemsSource = temp;

        }










        private ObservableCollection<BO.Station> stations = new ObservableCollection<BO.Station>();
        private IEnumerable<BO.Line> lineStation;
        int oldCode;
        bool add = false;
        private void RefreshStationall()
        {
            stations = ConvertList(bl.GetAllStations());//to make ObservableCollection
            ListOfStations.ItemsSource = stations;
        }

        public void RefreshLineInStation()
        {
            // beforAdj.Clear();
            // afterAdj.Clear();

            StationDataGrid.DataContext = stationData1;

            lineStation = null;
            if (stationData1 != null)
            {
                try
                {


                    oldCode = stationData1.Code;
                    lineStation = bl.GetAllLineIndStation(stationData1.Code);
                }
                catch (BO.BadIdException ex)
                {

                }


            }
            LineInStation.ItemsSource = lineStation;

        }

        private void ListOfStations_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            add = false;
            var list = (ListView)sender; //to get the line
            stationData1 = list.SelectedItem as BO.Station;
           
            RefreshLineInStation();
            if(codeTextBox.Text!="")
            {
                realTime.Visibility = Visibility.Visible;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (numberText != null)
            {
                int Sera = Convert.ToInt32(numberText);
                BO.Station SearchResult = bl.GetStationByCode(Sera);
                if (SearchResult != null)
                {
                    ObservableCollection<BO.Station> a = new ObservableCollection<BO.Station>();
                    a.Add(SearchResult);
                    ListOfStations.ItemsSource = a;

                }
                else
                {
                    ListOfStations.ItemsSource = stations;
                    //NotExist.Visibility = Visibility.Visible;
                }
            }
        }

        private void textBoxTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {

                TextBox text = sender as TextBox;
                int lineSteation = int.Parse(text.Text);
                BO.Station SearchResult = bl.GetStationByCode(lineSteation);
                if (SearchResult != null)
                {
                    ObservableCollection<BO.Station> a = new ObservableCollection<BO.Station>();
                    a.Add(SearchResult);
                    ListOfStations.ItemsSource = a;

                }
                else
                {
                    ListOfStations.ItemsSource = stations;
                    //NotExist.Visibility = Visibility.Visible;
                }


            }
            if (e.Key == Key.Back)
            {
                // NotExist.Visibility = Visibility.Hidden;
                ListOfStations.ItemsSource = stations;
            }
        }

        private void textBoxTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x));
        }




        private void textBoxTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ListOfStations.ItemsSource = stations;
            textBoxTextBox.Text = null;
            ListOfStations.SelectedIndex = -1;
            stationData1 = null;
            realTime.Visibility = Visibility.Hidden;
            RefreshLineInStation();

        }
        string numberText;

        private void textBoxTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxTextBox.Text != "Search Station here...." && textBoxTextBox.Text != "")
                numberText = textBoxTextBox.Text;
            textBoxTextBox.Text = "Search Station here....";
        }




        private void ListOfStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        private void button_Click(object sender, RoutedEventArgs e)
        {

            RealTimeStation r = new RealTimeStation(bl, stationData1);
            r.ShowDialog();


        }
    }
}
