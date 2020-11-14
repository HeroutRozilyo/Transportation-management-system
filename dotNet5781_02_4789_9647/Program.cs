using dotNet5781_02_4789_9647.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4789_9647
{
    class Program
    {
        public double distanceBetweenStations(BusStation bStation1, BusStation bStation2)
        static void Main(string[] args)
        {

            BusCompany egged = new BusCompany();

            
            
            
            //egged.addAtBusConpany(new LineBus { NumberID = 1123, Zone = Zone.JERUSALEM });
            

            try
            {
                LineBus lineBus = egged[1123];

            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("your {0} is  {1}",ex.ParamName,ex.Message);
            }
        }
    }
}
