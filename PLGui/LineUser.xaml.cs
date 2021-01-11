﻿using BlAPI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLGui
{
    /// <summary>
    /// Interaction logic for LineUser.xaml
    /// </summary>
    public partial class LineUser : Page
    {
        IEnumerable< BO.Line> searchResult;
        private ObservableCollection<object> lineStationOfLine = new ObservableCollection<object>();
        BO.Line line = new BO.Line();
        public LineUser()
        {
            InitializeComponent();
        }

        public LineUser(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            NotExist.Visibility = Visibility.Hidden;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {


            if (numberText != null)
            {
                int Sera = Convert.ToInt32(numberText);
                searchResult = bl.GetLineByLineCode(Sera);
                if (searchResult.Count()> 0)
                {
                    numberLineSearchRes.ItemsSource = searchResult;

                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(numberLineSearchRes.ItemsSource);
                    PropertyGroupDescription groupDescription = new PropertyGroupDescription("Area");
                    view.GroupDescriptions.Add(groupDescription);
                   

                }
                else
                {
                    
                    numberLineSearchRes.ItemsSource = searchResult;
                    NotExist.Visibility = Visibility.Visible;
                }
               
            }

        }
        string numberText;
        private IBL bl;

       
        private void textBoxSeaarchLine_GotFocus(object sender, RoutedEventArgs e)
        {
            line = null;
            numberLineSearchRes.SelectedIndex = -1;
            refreshLineData();
            textBoxSeaarchLine.Text = null;
            

        }

        private void textBoxSeaarchLine_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxSeaarchLine.Text != "...חפש מספר קו כאן"&& textBoxSeaarchLine.Text != "")
                numberText = textBoxSeaarchLine.Text;
            textBoxSeaarchLine.Text = "...חפש מספר קו כאן";
        }

        private void numberLine_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void textBoxSeaarchLine_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {

                TextBox text = sender as TextBox;
                int Sera = Convert.ToInt32(text.Text);
                searchResult = bl.GetLineByLineCode(Sera);
                if (searchResult.Count()> 0)
                {
                    numberLineSearchRes.ItemsSource = searchResult;

                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(numberLineSearchRes.ItemsSource);
                    PropertyGroupDescription groupDescription = new PropertyGroupDescription("Area");
                    view.GroupDescriptions.Add(groupDescription);

                }
                else
                {
                    
                    numberLineSearchRes.ItemsSource = searchResult;
                    NotExist.Visibility = Visibility.Visible;

                }

               
            }
            if (e.Key == Key.Back)
            {
                numberLineSearchRes.ItemsSource = searchResult;
                
               
            }
           

        }

        private void textBoxSeaarchLine_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x));
        }

        private void numberLineSearchRes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = (ListView)sender; //to get the line
            line = list.SelectedItem as BO.Line;
            refreshLineData();
        }

        public ObservableCollection<T> ConvertList<T>(IEnumerable<T> listFromBO)
        {
            return new ObservableCollection<T>(listFromBO);
        }
         public void refreshLineData()
        {
            if (line != null)
            {
               
                Looz.ItemsSource = line.TimeLineTrip;
                lineStationOfLine = ConvertList(bl.DetailsOfStation(line.StationsOfBus));
                
            }
            else
            {
                Looz.ItemsSource =null;
                lineStationOfLine = null;
            }
            GridDataLine.DataContext = line;
            StationLineList.ItemsSource = lineStationOfLine;

        }
    }

}
    
    