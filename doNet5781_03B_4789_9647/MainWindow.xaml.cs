
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace doNet5781_03B_4789_9647
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Bus> egged = new List<Bus>();  //A list that will contain all the buses
        static Random r = new Random(DateTime.Now.Millisecond);
       
      

        public MainWindow()
        {
            initBuses(egged);
            InitializeComponent();
          






        }

        private void initBuses(List<Bus> egged)
        {
            int license1;
            DateTime date1;


            for (int i = 0; i < 10; i++) ///restart 10 buses 
            {
                date1 = new DateTime(r.Next(1990, DateTime.Today.Year), r.Next(1, DateTime.Today.Month), r.Next(1, DateTime.Today.Day));
                do
                {
                    if ((date1.Year < 2018))
                    {
                        license1 = r.Next(1000000, 10000000); //to random number with 7 digite
                    }
                    else
                    {
                        license1 = r.Next(10000000, 100000000); //to random number with 8 digite
                    }

                } while (findBuse(egged, license1)); //check if the license exsis

                Bus temp = new Bus(license1, date1);
                egged.Add(temp);


            }


            egged[2].lastTreat = (egged[2].lastTreat.AddYears(-1));
            egged[9].lastTreat = DateTime.Now.AddDays(-1);
            egged[8].newKm_from_LastTreatment = 19900;
            egged[4].Fuel = 50;




        }




        private static bool findBuse(List<Bus> buses, int num)  //function that check if the require bus exist
        {
            int temp1;
           

            foreach (Bus item in buses) //move on the list buses
            {
                string temp = item.License;

                temp = temp.Replace("-", string.Empty); //To remove the hyphens from our license number
                int.TryParse(temp, out temp1);

                if (temp1 == num) //check if the licenes equal
                {
                    return true;
                }
             
            }
            return false;
            
        }




    }
}
