using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4789_9647.Properties
{
    public class LineBus : BusStation,IComparable
    {
       private static Random r = new Random();

       private List<BusStation> busstations;  //list that keep all the station that the bus have
       public List<BusStation> BusStations
        {
            get
            {
                List<BusStation> temp = new List<BusStation>(busstations);
                return temp;
            }
        }

       public int NumberID { get; set; } //the number line
       public BusStation FirstStation { get; private set; } //firststation
       public BusStation LastStation { get; private set; } //last station
       public Zone Zone { get; set; } //area at the country

        public LineBus(BusStation bus)
        {
            busstations = new List<BusStation>();
            busstations.Add(bus);
            NumberID= r.Next(1, 999);
            FirstStation = bus;
            LastStation = bus;
            Distance = 0;
            Zone= (Zone)r.Next(0, 3);
        }

        public LineBus() :base()
        {
            busstations = new List<BusStation>();
            NumberID = 0;
            FirstStation = null;
            LastStation = null;
        }


        
        public void addLastAtLineBus(BusStation busStation) //add bus to the list
        {
            BusStation temp = new BusStation(busStation);

            busstations.Add(busStation);
            LastStation = busstations[busstations.Count - 1];

            
        }

        public void addFirstAtLineBus(BusStation bus) //a
        {
            busstations.Insert(0, bus);
            FirstStation = busstations[0];
            busstations[0].Distance = 0;

            bus.Distance = 0;

        }

        public void addAtLineBus( BusStation busStation)
        {
            //  StationLineBus add = new StationLineBus(station);
            //  Console.WriteLine("in this line there are " + listOfBus.Count + ", where you want to add this station? [1-" + listOfBus.Count + 1 + "]\n");
            int index = int.Parse(Console.ReadLine());

            if (index == 0)
            {
                addFirstAtLineBus(busStation);
            }
            else
            {
           

                if (index > busstations.Count)
                {
                    throw new ArgumentOutOfRangeException("index", "index should be less than or equal to" + busstations.Count);
                }
                if (index == busstations.Count)
                {
                    addLastAtLineBus(busStation);
                }
                else
                {
                    busstations.Insert(index, busStation);
                }
            }
        }


        public void DelLastAtLineBus(BusStation bStation)
        {
            busstations.Remove(bStation);
            LastStation = busstations[busstations.Count - 1];

        }

        public void DelFirstAtLineBus(BusStation busStation)
        {
            busstations.RemoveAt(0);
            FirstStation = busstations[0];
        }

        public void DelAtLineBus(BusStation bStation)
        {
            int index = find(bStation);

            if (index == 0)
            {
                DelFirstAtLineBus(bStation);
            }
            else
            {
                if (index == -1)
                {
                    throw new ArgumentOutOfRangeException("index", "index should be less than or equal to" + busstations.Count);
                }

                if (index == busstations.Count - 1)
                {
                    DelLastAtLineBus(bStation);
                }
                else
                {
                    busstations.RemoveAt(index);
                }


            }

        }


        public override string ToString()
        {

            string result = " ";
            int i = 0;
            for (; i < busstations.Count - 1; i++)
            {
                result += busstations[i].BusStationKey + "  ";

            }
            result += busstations[i].BusStationKey;
            return "Bus Line: " + NumberID+ " ,Area: " + Zone+ " ,Statins of the bus: " + result;

        }



        public int find(BusStation bStation) //return the number of the station
        {

            int i = 0;
            for (; i < busstations.Count - 1; i++)
            {
                if(busstations[i]==bStation)
                {
                    return i;
                }

            }
            return -1;
        }

        public bool findStion(int id) //return the busstion  of the station
        {

            int i = 0;
            for (; i < busstations.Count - 1; i++)
            {
                if (busstations[i].BusStationKey == id)
                {
                    return true;
                }

            }
            return false;
        }

        public bool pathstation(BusStation bStation) //return if the station exsis at the bus path.
        {
            int temp = find(bStation);
            if (temp == -1)
                return false;
            else
                return true;
        }

        public double distanceBetweenStations(BusStation bStation1, BusStation bStation2)
        {
            double sum=0;
            int begin = find(bStation1);
            int end= find(bStation2);
            for(int i=begin+1;i<=end;i++)
            {
                sum+=busstations[i].Distance;
            }
            return sum;
        }

        public TimeSpan stationTravelTime(BusStation bStation1, BusStation bStation2)
        {
            TimeSpan timeSpan = TimeSpan.Zero;
            int begin = find(bStation1);
            int end = find(bStation2);
            for (int i = begin + 1; i <= end; i++)
            {
                timeSpan += busstations[i].TravelTime;
            }

            return timeSpan;
        }

        public LineBus pathbetweenStation(BusStation bStation1, BusStation bStation2)
        {
            int begin = find(bStation1);
            int end = find(bStation2);
            LineBus temp=new LineBus();
            int j = 0;
            for(int i=begin;i<=end;i++)
            {
                temp.busstations.Insert(j, busstations[i]);
                 j++;
            }

            return temp;
        }

    
        public TimeSpan TravelLine(LineBus BLine1)
        {
           TimeSpan timeSpan = stationTravelTime(BLine1.busstations[0], BLine1.busstations[BLine1.busstations.Count() - 1]);
            return timeSpan;
        }

        public LineBus Compare(LineBus obj)
        {
            int t = this.CompareTo(obj);
            if (t < 0) return obj;
            else  return this;
            
        }
        public int CompareTo(object obj)
        {
            TimeSpan a = TravelLine((LineBus)obj);
            return a.CompareTo(/*((LineBus)obj).*/TravelLine(this));

        }
        

    }

}


