using dotNet5781_01_9647_4789;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_9647_4789
{
    class Program
    {
        private const int FULLTANK = 1200;
        static void Main(string[] args)
        {
            List<Bus> buses = new List<Bus>();
            ACTION action;
            bool success;
            SartEgged(buses, out action, out success);
        }

       static Random r = new Random(DateTime.Now.Millisecond);
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
                        {
                            try
                            {
                                Bus temp = new Bus();
                                if (findBuses(buses, temp.License) == null)
                                {
                                    buses.Add(temp);
                                }
                               else
                                    Console.WriteLine("ERROR");
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
                        }
               
                        break;
                    case ACTION.PICK_BUS:
                        {
                            printall(buses);
                            Console.WriteLine("please enter a bus license");
                            string registration = Console.ReadLine();
                            int number = r.Next(1, 20000);

                            Bus bus = findBuses(buses, registration);
                            if (bus != null)
                            {
                                if (number + bus.Km > 0 && number + bus.Km < 20000)
                                {
                                    Console.WriteLine("the bus  {0} and take this drive", bus);
                                    bus.Km += number;

                                }
                                else
                                {
                                    Console.WriteLine("the bus cant take this drive");
                                }
                            }
                            else
                            {
                                Console.WriteLine("the bus not exit");
                            }
                        }
                        break;
                    case ACTION.MAINTENANCE:
                        {
                            Console.WriteLine("please enter a bus license");
                            string registration = Console.ReadLine();
                            Bus bus = findBuses(buses, registration);
                            Console.WriteLine("for car treatment enter 1, for refueling the car please enter 2");
                            string a = Console.ReadLine();

                            someTreatment(bus,a);
                        }
                        break;
                    case ACTION.REFUELLING:
                        {
                            foreach(Bus bus in buses)
                            {
                                int temp = (bus.Km - bus.NewKm);
                                Console.WriteLine(" {0} the number of km from the last treatment  {1}", bus,temp);
                            }
                        }
                        break;
                    case ACTION.EXIT:
                        break;
                    default:
                        break;
                }
            } while (action != ACTION.EXIT);
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
            Bus bus = null;
            foreach (Bus item in buses)
            {
                string temp = item.License;
                temp = temp.Replace("-", string.Empty); 
                registration = registration.Replace("-", string.Empty);
                if (temp == registration)
                {
                    bus = item;
                }
            }
            return bus;
        }

        private static void someTreatment(Bus bus,string a)
        {
            if(a=="2")
            {
                bus.Fuel = FULLTANK;
            }
            if(a=="1")
            {
                DateTime currentDate = DateTime.Now;
                bus.Checkup=currentDate;
                bus.NewKm = bus.Km;
            }
        }

    }
}


