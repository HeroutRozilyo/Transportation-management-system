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
        static void Main(string[] args)
        {

            BusCompany egged = new BusCompany();
            egged.add(new LineBus { NumberID = 1123, Zone = Zone.JERUSALEM });
            egged.add(new LineBus { NumberID = 11553, Zone = Zone.JERUSALEM });
            egged.add(new LineBus { NumberID = 118, Zone = Zone.JERUSALEM });
            egged.add(new LineBus { NumberID = 11993, Zone = Zone.JERUSALEM });

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
