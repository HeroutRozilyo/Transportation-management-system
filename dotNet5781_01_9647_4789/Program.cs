using dotNet5781_01_9647_4789;using System;using System.Collections.Generic;using System.IO;
using System.Linq;using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;using System.Text;using System.Threading.Tasks;namespace dotNet5781_01_9647_4789{    class Program       {        private const int FULLTANK = 1200;        static void Main(string[] args)        {            List<Bus> buses = new List<Bus>();  //A list that will contain all the buses

            ACTION action;            bool success;            SartEgged(buses, out action, out success);        }       static Random r = new Random(DateTime.Now.Millisecond);  //Set a random variable to calculate the time later

        //--------------
        //Adding buses to the list and updating the fuel, etc.
        //--------------
        private static void SartEgged(List<Bus> buses, out ACTION action, out bool success)        {            do            {                do                {
                    //Action selection
                    
                    Console.WriteLine("choose an action");                    Console.WriteLine("ADD_BUS, PICK_BUS, MAINTENANCE, REFUELLING, EXIT = -1");                    success = Enum.TryParse(Console.ReadLine(), out action);                } while (success == false);                switch (action)                {                    case ACTION.ADD_BUS: //add a new bus to our list                        {

                            try
                            {
                                Bus temp = new Bus();
                                if (findBuses(buses, temp.License) == null) //in order to check if we have a bus like the bus that we need to had
                                {   //if we dont have bus, so add him to the list
                                    buses.Add(temp);
                                }
                                else //if we have yet this bus on the list
                                    Console.WriteLine("This license number already exist in tha list ");
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
                        }                    case ACTION.PICK_BUS: //in orser to choose bus                        {                            printall(buses);    //print all the bus that we have                            Console.WriteLine("please enter license");                            string registration = Console.ReadLine();   //cin the choice                            int number = r.Next(1,20000);   //to choose a random number for the require drive                            Bus bus = findBuses(buses, registration); //find the bus that the customer choose to use                            if (bus != null ) //we have the bus                            {                                if (number + bus.Km > 0 && number + bus.Km < 20000) //if the bus can take this travel                                {                                    Console.WriteLine("the bus  {0} take the drive", bus);                                    bus.Km += number;                                }                                else Console.WriteLine("ERROR-the bus don't have inafe KM to take the drive");                            }                            else                            {                                                                Console.WriteLine("ERROR- the bus are not exit ");                            }                        }                        break;                    case ACTION.MAINTENANCE: //in order to treat the bus                        {
                          //-----                          //get the licens of the bus and check if he exist                            Console.WriteLine("please enter license");                            string registration = Console.ReadLine();
                            Bus bus = findBuses(buses, registration);                            //-----                            //choose the action that you want to do on the bus                            Console.WriteLine("for car treatment please enter 1 ");                            Console.WriteLine("for Refueling the vehicle please enter 2 ");                            string a = Console.ReadLine();                            SomeTreatment(bus, a); //go to a function to do the require treatment                                                    }                        break;                    case ACTION.REFUELLING: //in order to print data on the buses from the ladt treatment                        {
                            foreach (Bus bus in buses)
                            {
                                Console.WriteLine("Vehicle number {0} the km from the last treatment is {1} ", bus.License, bus.Km-bus.NEWKm);
                               
                            }
                        }                        break;                    case ACTION.EXIT:                        break;                    default:                        break;                }            } while (action != ACTION.EXIT);        }        private static void printall(List<Bus> buses)   //function that move on all the list buses and print the data on them        {            foreach (Bus bus in buses)            {                Console.WriteLine(bus);            }        }        private static Bus findBuses(List<Bus> buses, string registration)  //function that check if the require bus exist        {                        Bus bus = null;            foreach (Bus item in buses) //move on the list buses            {                string temp = item.License;                temp = temp.Replace("-", string.Empty); //To remove the hyphens from our license number
                registration = registration.Replace("-", string.Empty); //To remove the hyphens from our license number
                if (temp == registration) //check if the licenes equal                {                    bus = item;                }            }            return bus;        }        private static void SomeTreatment(Bus bus,string a) // function that treat at the bus
        {
            if(bus==null)
            {
                Console.WriteLine("ERROR- the bus are not exists");
            }
             if(a=="2") //so we need to fuel
            {
                bus.Fuel = FULLTANK;
            }
            if (a == "1") //we need treat the bus
            {
                DateTime currentDate = DateTime.Now;
               bus.Checkup = currentDate;
                bus.NEWKm = bus.Km;
            }
        }

    }}



/*license is: 123-45-678, starting date: 20/12/2020 00:00:00
license is: 12-345-67 , starting date: 12/12/2003 00:00:00
please enter license
12345678
ERROR-the bus don't have inafe KM to take the drive
choose an action
ADD_BUS, PICK_BUS, MAINTENANCE, REFUELLING, EXIT = -1
2
license is: 123-45-678, starting date: 20/12/2020 00:00:00
license is: 12-345-67 , starting date: 12/12/2003 00:00:00
please enter license
12345678
ERROR-the bus don't have inafe KM to take the drive
choose an action
ADD_BUS, PICK_BUS, MAINTENANCE, REFUELLING, EXIT = -1
2
license is: 123-45-678, starting date: 20/12/2020 00:00:00
license is: 12-345-67 , starting date: 12/12/2003 00:00:00
please enter license
12345678
the bus  license is: 123-45-678, starting date: 20/12/2020 00:00:00 take the drive
choose an action
ADD_BUS, PICK_BUS, MAINTENANCE, REFUELLING, EXIT = -1
4
Vehicle number 123-45-678 the km from the last treatment is 2325
Vehicle number 12-345-67 the km from the last treatment is 0
choose an action
ADD_BUS, PICK_BUS, MAINTENANCE, REFUELLING, EXIT = -1
-1
Press any key to continue . . .*/