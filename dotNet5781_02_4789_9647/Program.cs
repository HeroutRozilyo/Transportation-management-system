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

        //----------------------------------------
        //restart the data of the stations and buses

        static void restertStationn(ref List<BusStation> bStation, ref BusCompany company)
        {
            for(int i=0;i<40;i++)       //restart stations
            {
                string name = string.Format("station " +(i+1)); 
                bStation.Add(new BusStation((i + 1), name));
            }

            for(int i=0;i<10;i++)       //restart 10 lines with first station and last station
            {
                company.ComanyBus.Add( new LineBus(bStation[i],bStation[i+1])); //now we restart 10 buses
            }
            for(int k=1;k<=3;k++)       //add stations to the lines
            {
                
               int j = 10 * k;
                for (int i = 0; i < 10; i++)
                {
                
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

       static void print_lines(BusCompany egged)        //func that print all the lines that we have
        {
            Console.WriteLine("The lines in our company is: ");
            for(int i=0;i<egged.ComanyBus.Count();i++)
            {
                Console.WriteLine(egged.ComanyBus[i].NumberID);
            }
        }


        static void print_stations_At_Linee(BusCompany egged,int index)     //print all the station at specific line
        {
           
            for(int i=0;i<egged[index].BusStations.Count;i++)
            {
                Console.WriteLine(egged[index].BusStations[i]);
                
            }

        }

        static void print_stations(List<BusStation> stations)       ///print all the stations
        {
            Console.WriteLine("The stations in our company: ");
            foreach (BusStation item in stations)
            {
                Console.WriteLine(item);
            }

        }



        static int search(int line, BusCompany egged)       //return the index of the line at the company
        {
            for(int i=0;i<egged.ComanyBus.Count;i++)
            {
                if (egged[i].NumberID == line)
                    return i;
            }

            throw new ArgumentException(string.Format("{0} NumberLine not exist already", line));

        }

        //static void lines_at_station(int code,BusCompany egged)
        //{
        //    Console.WriteLine("The line/lines that path at this station: ");
        //    for(int i=0;i<egged.ComanyBus.Count;i++)
        //    {
        //        for(int j=0;j<egged[i].BusStations.Count;j++)
        //        {
        //            if(egged[i].BusStations[j].BusStationKey==code)
        //            {
        //                Console.WriteLine(egged[i].NumberID);
        //                break;
        //            }
                    
        //        }
        //    }
        //}

        static void time_path(BusCompany egged,int code1,int code2)     //check the travel time between 2 stations. check all the path at the lines that move at those stations
        {
            //help variable

            BusCompany temp = new BusCompany();
            TimeSpan timeSpan = TimeSpan.Zero;
            LineBus b = new LineBus();

            foreach (LineBus item in egged)     //move at all the lines
            {
                BusStation begin;
                BusStation end;

                //check if the stations exsis at this bus

                begin = item.find_Station(code1);
                end = item.find_Station(code2);
                if (begin!=null&&end!=null)     //if the bus path at those stations
                {
                  if(item.find(begin)<item.find(end))       //check if the staion at the Proper order
                    {
                        b = item.pathbetweenStation(begin, end);        //calucate the time trval between the stations
                        temp.addAtBusConpany(b);        //add this bus to the list that keep the data
                    }
                  
                }

            }
             temp.sortBus();        //sort the lines that move between these to stations. and sort them according to the travel time between the stations.
            
            foreach(LineBus item in temp)
            {
                Console.WriteLine("line number: {0}, travel time between the two stations is: {1} ", item.NumberID , item.TravelLine(item));
            }


        }
        static BusStation find_station(List<BusStation> stations, int id)       //find if the station exsis according to code station
        {
            for (int i = 0; i < stations.Count; i++)
            {
                if (stations[i].BusStationKey == id)
                    return stations[i];
            }
            return null;

        }

        //------------------------------------
        //functions of the main

      static  void func_addline(ref BusCompany egged, ref List<BusStation> stations)        //to add line to the companey
        {
            Console.WriteLine("Please enter number of the new bus");
            int number = int.Parse(Console.ReadLine());
            if (egged.findLine_BusConpany(number))      //check if the line allready exsus
            {
                Console.WriteLine("The line exsis");
            }
            else
            {

                print_stations(stations);
                Console.WriteLine("pleas choose first station to the new bus, enter the code to the first station  ");
                int code1 = int.Parse(Console.ReadLine());
                Console.WriteLine("pleas choose last station to the new bus, enter the code to the last station  ");
                int code2 = int.Parse(Console.ReadLine());

                //-----------------------
                //in order to send 2 Busstation variable
                
                BusStation temp1 = new BusStation(find_station(stations, code1));
                temp1.BusStationKey = code1;
                BusStation temp2 = new BusStation(find_station(stations, code2));
                temp2.BusStationKey = code2;

                LineBus line = new LineBus(temp1, temp2);       //build the line
                line.NumberID = number;
                egged.addAtBusConpany(line);        //add the line
            }
        }
        static void func_addstation(ref BusCompany egged, ref List<BusStation> stations)        //func to add station to line
        {
           


                Console.WriteLine("Please enter number of the bus");
                print_lines(egged);
                int number = int.Parse(Console.ReadLine());
                Console.WriteLine("Please enter code station");
                int code = int.Parse(Console.ReadLine());
                string name = "station" + code;

                if (egged.findLine_BusConpany(number))     //if the line exsis
                {

                    BusStation temp = new BusStation(code, name);
                    int index = search(number, egged);       //find the index of the line
                    bool flag = egged[index].findStion(code);       //find if the line have this atation
                    if (!flag)      //if not have this station
                    {
                        egged[index].addAtLineBus(temp);        //add
                    }
                    else
                    { throw new ArgumentException(string.Format("The station is allready exsis  {0}  in this line", temp)); }

                }

                else
                { throw new ArgumentException(string.Format("ERROR! The line {0} is not exsis", number)); }
            
         //



        }



        static void Main(string[] args)
        {
            //------------------------
            //our variable

            BusCompany egged = new BusCompany();
            List<BusStation> stations = new List<BusStation>();
            restertStationn(ref stations,ref egged);

       
            int choice;
            do
            {
                Console.WriteLine("Please enter your choice:\n " +
               "1. New line bus\n 2.Add new stop station to the line\n 3.Delete line bus\n 4.Delete station from the line\n 5.Find lines that path at this station\n 6.Print options to travel\n 7.Print all the buses\n 8.Print all the station and the lines that path there\n -1 to exsis");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case (int)options.ADD_NEW_LINE:
                        {
                            try
                            {
                                func_addline(ref egged,ref stations);
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        break;
                    case (int)options.ADD_STATION:
                        {
                            try
                            {
                                func_addstation(ref egged, ref stations);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        break;
                    case (int)options.DELETE_LINE:
                        {
                            try
                            {
                                Console.WriteLine("Please enter number of the bus");
                                print_lines(egged);
                                int number = int.Parse(Console.ReadLine());

                                egged.deleteAtBusConpany(number);
                                print_lines(egged);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                        }
                        break;
                    case (int)options.DELETE_STATION:
                        {
                            try
                            {
                                print_lines(egged);

                                //----------------------------
                                //print messege and get data

                                Console.WriteLine("Please enter number of the bus");
                                int number = int.Parse(Console.ReadLine());
                                int index = search(number, egged);
                                Console.WriteLine("Please enter code station ");
                                print_stations_At_Linee(egged, index);
                                int code = int.Parse(Console.ReadLine());
                                BusStation temp = find_station(stations, code);

                                egged[index].DelAtLineBus(temp);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        break;
                    case (int)options.SEARCH_LINE_AT_STATION:
                        {
                            try
                            {
                                print_stations(stations);
                                Console.WriteLine("Please enter code of the station");
                                int code = int.Parse(Console.ReadLine());

                                List<int> a = egged.WhichBusAtTheSTation(code);
                                Console.WriteLine("In station number {0} the lines at this station ", code);
                                for (int i = 0; i < a.Count; i++)
                                {
                                    Console.WriteLine(a[i]);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }


                        }
                        break;
                    case (int)options.SEARCH_PATH:
                        {
                            try
                            {
                                print_stations(stations);
                                Console.WriteLine("Please enter code of the station 1");
                                int code1 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter code of the station 2");
                                int code2 = int.Parse(Console.ReadLine());

                                time_path(egged, code1, code2);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                        }
                        break;
                    case (int)options.PRINT_LINES:
                        {
                            try
                            {
                                foreach (LineBus item in egged)
                                {
                                    Console.WriteLine(item);
                                }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }


                        }
                        break;
                    case (int)options.PRINT_STATIONS:
                        {
                            try
                            {
                                int code;
                                for (int i = 0; i < stations.Count; i++)
                                {
                                    code = stations[i].BusStationKey;
                                    Console.WriteLine("The code of the stations is: {0}", code);
                                    List<int> a = egged.WhichBusAtTheSTation(code);

                                    for (int j = 0; j < a.Count; j++)
                                    {
                                        Console.WriteLine(a[j]);
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }


                        }
                        break;
                    default:
                        Console.WriteLine("ERROE!");
                        break;
                }



            } while (choice != (int)options.EXIST) ; 


        }
    }
}

//example:
//Please enter your choice:
// 1.New line bus
// 2.Add new stop station to the line
// 3.Delete line bus
// 4.Delete station from the line
// 5.Find lines that path at this station
// 6.Print options to travel
// 7.Print all the buses
// 8.Print all the station and the lines that path there
//1
//Please enter number of the new bus
//10
//The stations in our company:
//Bus Station Code: 1, 31.229218686246°N 34.6230897070482°E
//Bus Station Code: 2, 31.1438491209149°N 35.4310152476332°E
//Bus Station Code: 3, 32.0680239822567°N 34.7822339712094°E
//Bus Station Code: 4, 31.4000387314707°N 34.9907506782053°E
//Bus Station Code: 5, 32.0440259422381°N 35.2858825578289°E
//Bus Station Code: 6, 32.4315015946196°N 34.6372275698638°E
//Bus Station Code: 7, 32.2537940245838°N 35.3361836725083°E
//Bus Station Code: 8, 31.3531613820946°N 35.4502002583585°E
//Bus Station Code: 9, 31.4841680684519°N 35.1580380317094°E
//Bus Station Code: 10, 32.484058590645°N 34.7432256298341°E
//Bus Station Code: 11, 33.2598370395414°N 34.5726032254624°E
//Bus Station Code: 12, 31.7787776663801°N 35.493284004551°E
//Bus Station Code: 13, 32.6224588442233°N 34.4355625311544°E
//Bus Station Code: 14, 32.5496555207063°N 34.9771023447984°E
//Bus Station Code: 15, 32.1263925672166°N 34.7634531677065°E
//Bus Station Code: 16, 32.1561655501165°N 34.8470880831345°E
//Bus Station Code: 17, 32.3653418494693°N 35.2172823207999°E
//Bus Station Code: 18, 32.9287747323181°N 34.507518345773°E
//Bus Station Code: 19, 31.0528095631175°N 35.1776271272812°E
//Bus Station Code: 20, 31.4505610040159°N 34.3989807060449°E
//Bus Station Code: 21, 31.399531419901°N 35.0464197452862°E
//Bus Station Code: 22, 32.1411171197617°N 35.3968488506492°E
//Bus Station Code: 23, 31.0609587002829°N 35.10284239296°E
//Bus Station Code: 24, 32.6923769989947°N 34.3378962215213°E
//Bus Station Code: 25, 31.0588824608172°N 34.4448455939744°E
//Bus Station Code: 26, 32.2027820801841°N 34.4722435862628°E
//Bus Station Code: 27, 31.1403716465181°N 35.4931855892731°E
//Bus Station Code: 28, 32.2761517231707°N 35.3469891325789°E
//Bus Station Code: 29, 33.1404776054623°N 34.3817677107089°E
//Bus Station Code: 30, 31.5453203804071°N 34.7216673726317°E
//Bus Station Code: 31, 32.674626257445°N 35.0316135150993°E
//Bus Station Code: 32, 31.1975462326769°N 34.3812560195482°E
//Bus Station Code: 33, 31.7334426857221°N 34.4997823140583°E
//Bus Station Code: 34, 32.5810109927696°N 35.2368710833308°E
//Bus Station Code: 35, 31.0572439733228°N 35.4767397666242°E
//Bus Station Code: 36, 33.1517409320696°N 34.8749822997372°E
//Bus Station Code: 37, 32.1940118900938°N 34.9753107325525°E
//Bus Station Code: 38, 31.4499843706144°N 34.7326256666484°E
//Bus Station Code: 39, 32.6813723957079°N 34.8094699318099°E
//Bus Station Code: 40, 33.2261689751065°N 34.3436578735912°E
//pleas choose first station to the new bus, enter the code to the first station
//19
//pleas choose last station to the new bus, enter the code to the last station
//25
//Please enter your choice:
// 1.New line bus
// 2.Add new stop station to the line
// 3.Delete line bus
// 4.Delete station from the line
// 5.Find lines that path at this station
// 6.Print options to travel
// 7.Print all the buses
// 8.Print all the station and the lines that path there
//1
//Please enter number of the new bus
//13
//The stations in our company:
//Bus Station Code: 1, 31.229218686246°N 34.6230897070482°E
//Bus Station Code: 2, 31.1438491209149°N 35.4310152476332°E
//Bus Station Code: 3, 32.0680239822567°N 34.7822339712094°E
//Bus Station Code: 4, 31.4000387314707°N 34.9907506782053°E
//Bus Station Code: 5, 32.0440259422381°N 35.2858825578289°E
//Bus Station Code: 6, 32.4315015946196°N 34.6372275698638°E
//Bus Station Code: 7, 32.2537940245838°N 35.3361836725083°E
//Bus Station Code: 8, 31.3531613820946°N 35.4502002583585°E
//Bus Station Code: 9, 31.4841680684519°N 35.1580380317094°E
//Bus Station Code: 10, 32.484058590645°N 34.7432256298341°E
//Bus Station Code: 11, 33.2598370395414°N 34.5726032254624°E
//Bus Station Code: 12, 31.7787776663801°N 35.493284004551°E
//Bus Station Code: 13, 32.6224588442233°N 34.4355625311544°E
//Bus Station Code: 14, 32.5496555207063°N 34.9771023447984°E
//Bus Station Code: 15, 32.1263925672166°N 34.7634531677065°E
//Bus Station Code: 16, 32.1561655501165°N 34.8470880831345°E
//Bus Station Code: 17, 32.3653418494693°N 35.2172823207999°E
//Bus Station Code: 18, 32.9287747323181°N 34.507518345773°E
//Bus Station Code: 19, 31.0528095631175°N 35.1776271272812°E
//Bus Station Code: 20, 31.4505610040159°N 34.3989807060449°E
//Bus Station Code: 21, 31.399531419901°N 35.0464197452862°E
//Bus Station Code: 22, 32.1411171197617°N 35.3968488506492°E
//Bus Station Code: 23, 31.0609587002829°N 35.10284239296°E
//Bus Station Code: 24, 32.6923769989947°N 34.3378962215213°E
//Bus Station Code: 25, 31.0588824608172°N 34.4448455939744°E
//Bus Station Code: 26, 32.2027820801841°N 34.4722435862628°E
//Bus Station Code: 27, 31.1403716465181°N 35.4931855892731°E
//Bus Station Code: 28, 32.2761517231707°N 35.3469891325789°E
//Bus Station Code: 29, 33.1404776054623°N 34.3817677107089°E
//Bus Station Code: 30, 31.5453203804071°N 34.7216673726317°E
//Bus Station Code: 31, 32.674626257445°N 35.0316135150993°E
//Bus Station Code: 32, 31.1975462326769°N 34.3812560195482°E
//Bus Station Code: 33, 31.7334426857221°N 34.4997823140583°E
//Bus Station Code: 34, 32.5810109927696°N 35.2368710833308°E
//Bus Station Code: 35, 31.0572439733228°N 35.4767397666242°E
//Bus Station Code: 36, 33.1517409320696°N 34.8749822997372°E
//Bus Station Code: 37, 32.1940118900938°N 34.9753107325525°E
//Bus Station Code: 38, 31.4499843706144°N 34.7326256666484°E
//Bus Station Code: 39, 32.6813723957079°N 34.8094699318099°E
//Bus Station Code: 40, 33.2261689751065°N 34.3436578735912°E
//pleas choose first station to the new bus, enter the code to the first station
//19
//pleas choose last station to the new bus, enter the code to the last station
//25
//Please enter your choice:
// 1. New line bus
// 2.Add new stop station to the line
// 3.Delete line bus
// 4.Delete station from the line
// 5.Find lines that path at this station
// 6.Print options to travel
// 7.Print all the buses
// 8.Print all the station and the lines that path there
//2
//Please enter number of the bus
//The lines in our company is:
//100
//63
//464
//174
//454
//622
//545
//154
//211
//644
//10
//13
//13
//Please enter code station
//6
//Please enter name of the station
//t
//Please enter your choice:
// 1. New line bus
// 2.Add new stop station to the line
// 3.Delete line bus
// 4.Delete station from the line
// 5.Find lines that path at this station
// 6.Print options to travel
// 7.Print all the buses
// 8.Print all the station and the lines that path there
//7
//Bus Line: 100 ,Area: GENERAL ,Statins of the bus:  6  31  21  11  1  2
//Bus Line: 63 ,Area: CENTER ,Statins of the bus:  7  32  22  12  2  3
//Bus Line: 464 ,Area: SOUTH ,Statins of the bus:  8  33  23  13  3  4
//Bus Line: 174 ,Area: SOUTH ,Statins of the bus:  9  34  24  14  4  5
//Bus Line: 454 ,Area: CENTER ,Statins of the bus:  10  35  25  15  5  6
//Bus Line: 622 ,Area: GENERAL ,Statins of the bus:  11  36  26  16  6  7
//Bus Line: 545 ,Area: CENTER ,Statins of the bus:  12  37  27  17  7  8
//Bus Line: 154 ,Area: CENTER ,Statins of the bus:  13  38  28  18  8  9
//Bus Line: 211 ,Area: CENTER ,Statins of the bus:  14  39  29  19  9  10
//Bus Line: 644 ,Area: SOUTH ,Statins of the bus:  15  40  30  20  10  11
//Bus Line: 10 ,Area: CENTER ,Statins of the bus:  19  25
//Bus Line: 13 ,Area: GENERAL ,Statins of the bus:  6  19  25
//Please enter your choice:
// 1. New line bus
// 2.Add new stop station to the line
// 3.Delete line bus
// 4.Delete station from the line
// 5.Find lines that path at this station
// 6.Print options to travel
// 7.Print all the buses
// 8.Print all the station and the lines that path there

