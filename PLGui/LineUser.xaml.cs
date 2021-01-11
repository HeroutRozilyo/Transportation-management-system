using BlAPI;
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
        IEnumerable<IGrouping<BO.AREA, BO.Line>> searchResult;
        public LineUser()
        {
            InitializeComponent();
        }

        public LineUser(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {


            if (numberText != null)
            {
                int Sera = Convert.ToInt32(numberText);
                searchResult = bl.GetLinesBylineCodeG(Sera);
                if (searchResult != null)
                {
                    treeViewKav.ItemsSource = searchResult;

                }
                else
                {

                    NotExist.Visibility = Visibility.Visible;
                }
            }

        }
        string numberText;
        private IBL bl;

       
        private void textBoxSeaarchLine_GotFocus(object sender, RoutedEventArgs e)
        {
            
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
    }
}
    
    
