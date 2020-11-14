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
        private static Random r = new Random();


        static void restertStationn(ref List<BusStation> bStation, ref BusCompany company)
        {
            for(int i=0;i<40;i++)
            {
                string name = string.Format("station " +(i+1)); 
                bStation.Add(new BusStation((i + 1), name));
            }

            for(int i=0;i<10;i++)
            {
                company.ComanyBus.Add( new LineBus(bStation[i])); //now we restart 10 buses
            }
            for(int k=1;k<=3;k++)
            {
                int j = 10 ;
                for (int i = 0; i < 10; i++)
                {
                    j = 10*k;
                    company.ComanyBus[i].addAtLineBus(bStation[j]);
                    j++;
                }
            }
            for(int j=5;j<15;j++) // now will pass at 10 stations more than 1 line bus.
            { 
                company.ComanyBus[j-5].addAtLineBus(bStation[j]);
            }
        }

        enum options
        {
            ADD_NEW_LINE=1,ADD_STATION,DELETE_LINE,DELETE_STATION,SEARCH_LINE_AT_STATION,SEARCH_PATH,PRINT_LINES,PRINT_STATIONS,EXIST=-1
        }

       static void print_lines(BusCompany egged)
        {
            Console.WriteLine("The lines in our company is: ");
            for(int i=0;i<egged.ComanyBus.Count();i++)
            {
                Console.WriteLine(egged.ComanyBus[i].NumberID);
            }
        }


        static void print_stations_At_Linee(BusCompany egged,int index)
        {
           
            for(int i=0;i<egged[index].BusStations.Count;i++)
            {
                Console.WriteLine(egged[index].BusStations[i]);
            }

        }



        static int search(int line,BusCompany egged)
        {
            for(int i=0;i<egged.ComanyBus.Count;i++)
            {
                if (egged[i].NumberID == line)
                    return i;
            }

            throw new ArgumentException(string.Format("{0} NumberLine not exist already", line));

        }
    

        static void Main(string[] args)
        {

            BusCompany egged = new BusCompany();
            List<BusStation> stations = new List<BusStation>();
            restertStationn(ref stations,ref egged);

            Console.WriteLine("Please enter your choice: " +
                "1. New line bus\n 2.Add new stop station to the line\n 3.Delete line bus\n 4.Delete station from the line\n 5.Find lines that path at this station\n 6.Print options to travel\n 7.Print all the buses\n 8.Print all the station and the lines that path there");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case (int)options.ADD_NEW_LINE:
                    {
                        Console.WriteLine("Please enter number of the new bus, the code to the first station and the name's station:  ");
                        int number = int.Parse(Console.ReadLine());
                        int code = int.Parse(Console.ReadLine());
                        string name = Console.ReadLine();
                        BusStation temp=new BusStation(code, name);
                        LineBus line = new LineBus(temp);
                        line.NumberID = number;
                        egged.addAtBusConpany(line);

                    }break;
                case (int)options.ADD_STATION:
                    {
                        Console.WriteLine("Please enter number of the bus");
                        print_lines(egged);
                        int number = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter code station and the name of the new station ");
                        int code = int.Parse(Console.ReadLine());
                        string name = Console.ReadLine();

                        if (egged.findLine_BusConpany(number))
                        {
                            BusStation temp = new BusStation(code, name);
                            int index = search(number, egged);
                            bool flag = egged[index].findStion(code);
                            if (!flag)
                            {
                                egged[index].addAtLineBus(temp);
                            }
                            else
                                throw new ArgumentException(string.Format("The station is allready exsis  {0}  in this line", temp));


                        }
                        else
                            throw new ArgumentException(string.Format("ERROR! The line {0} is not exsis", number));
                    }
                    break;
                case (int)options.DELETE_LINE:
                    {
                        Console.WriteLine("Please enter number of the bus");
                        print_lines(egged);
                        int number = int.Parse(Console.ReadLine());
                        egged.deleteAtBusConpany(number);
                    }
                    break;
                case (int)options.DELETE_STATION:
                    {
                        Console.WriteLine("Please enter number of the bus");
                        print_lines(egged);
                        int number = int.Parse(Console.ReadLine());
                        int index = search(number, egged);
                        Console.WriteLine("Please enter code station and the name of the new station");
                        print_stations_At_Linee(egged, index);
                        int code = int.Parse(Console.ReadLine());
                        string name = Console.ReadLine();
                        BusStation temp = new BusStation(code, name);
                        egged[index].DelAtLineBus(temp);
                    }
                    break;








                default:
                    break;
            }







            //try
            //{
            //    LineBus lineBus = egged[1123];

            //}
            //catch (ArgumentNullException ex)
            //{
            //    Console.WriteLine("your {0} is  {1}",ex.ParamName,ex.Message);
            //}
        }
    }
}
