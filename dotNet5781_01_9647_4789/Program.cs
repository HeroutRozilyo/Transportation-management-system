using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_9647_4789
{
   class Program
    {
        static void Main(string[] args)
        {
            List<Bus> buses = new List<Bus>();
            ACTION action;
            bool success;
            SartEgged(buses, out action, out success);
        }

        private static void SartEgged(List<Bus> buses, out ACTION action, out bool success)
        {
            do
            {
                do
                {
                    Console.WriteLine("choose an action");
                    Console.WriteLine("ADD_BUS, PICK_BUS, MAINTENANCE, REFUELLING, EXIT = -1");
                    success = Enum.TryParse(Console.ReadLine(), out action);

                } while (success == false);
                switch (action)
                {
                    case ACTION.ADD_BUS:
                        try
                        {
                            buses.Add(new Bus());
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                        //print all buses
                        foreach (Bus bus in buses)
                        {
                            Console.WriteLine(bus);
                        }
                        break;
                    case ACTION.PICK_BUS:
                        printall(buses);
                        string registration = Console.ReadLine();
                        Bus bus = findBuses(buses, registration);
                        if (bus != null)
                        {
                            Console.WriteLine("the bus is {0} ", bus);
                        }
                        else
                        {
                            Console.WriteLine("ein kaze!!!");
                        }
                        break;
                    case ACTION.MAINTENANCE:
                        break;
                    case ACTION.REFUELLING:
                        break;
                    case ACTION.EXIT:
                        break;
                    default:
                        break;
                }
            } while (action != ACTION.EXIT);
        }
    }
}

  private static void printall(List<Bus> buses)
  {
    foreach (Bus bus in buses)
    {
         Console.WriteLine(bus);
    }
  }

  private static Bus findBuses(List<Bus> buses, string registration)
  { 
      registration = registration.Replace("-", string.Empty);

       Bus bus = null;
       foreach (Bus item in buses)
       {
          if (item.Registration == registration)
          {
                 bus = item;
          }
       }
      return bus;
  }