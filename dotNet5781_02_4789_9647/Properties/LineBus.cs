using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4789_9647.Properties
{
    public class LineBus : BusStation, IComparable<LineBus>
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

       public int NumberID { get; set; }         //the number line
       public BusStation FirstStation { get; private set; }         //firststation
       public BusStation LastStation { get; private set; }       //last station
       public Zone Zone { get; set; }        //area at the country

        //------------------------------
        //constructors 

        public LineBus(BusStation station1, BusStation station2)           //constructor that get 2 stations to restart the new bus
        {
            busstations = new List<BusStation>();
            NumberID = r.Next(1, 999);

            //to insert the 2 stations we have:
            busstations.Insert(0, station1);
            busstations.Insert(1, station2);

            FirstStation = station1;
            LastStation = station2;

            Zone = (Zone)r.Next(0, 3);

            busstations[0].Distance = 0;
            busstations[0].TravelTime = TimeSpan.Zero;

        }

       
        

        public LineBus() :base()        //deafult constructor
        {
            busstations = new List<BusStation>();
            NumberID = 0;
            FirstStation = null;
            LastStation = null;
        }

        //------------------------------
        //the func add a new station to the bus. the func call to another function to add bus incordding to the place to add the station.

        public void addAtLineBus(BusStation busStation)
        {
            //Console.WriteLine("In this line there are " + busstations.Count + " stations" + ", where you want to add this station? [1-" + (busstations.Count + 1) + "]\n");
            // int index = (int.Parse(Console.ReadLine()) - 1);

            int index = r.Next(1, 2) - 1;      
            if (index == 0)     //if we need add the station at the begin
            {

                addFirstAtLineBus(busStation);

            }
            else
            {


                if (index > (busstations.Count))        //if the new place is not at the range
                {

                    throw new ArgumentOutOfRangeException("index", "index should be less than or equal to" + busstations.Count);
                }
                if (index == (busstations.Count))       //if we need to add at the last of the line path
                {
                    addLastAtLineBus(busStation);
                }
                else        //if we need to add the station between the first station to last station
                {
                    busstations.Insert(index, busStation);

                    // to random newest Distance and travelTime to the next station after the insert
                    BusStation a = new BusStation();
                    busstations[index + 1].Distance = a.Distance;
                    busstations[index + 1].TravelTime = a.TravelTime;
                }
            }
        }


        public void addLastAtLineBus(BusStation busStation) //add bus to the last place at the list
        { 
            busstations.Add(busStation);
            LastStation = busstations[busstations.Count - 1];
        }

       

        public void addFirstAtLineBus(BusStation bus) //add bus to the first place at the list
        {
            //its the first station so the distance and the travel time from the prestation is zero
            bus.TravelTime = TimeSpan.Zero;
            bus.Distance = 0;   
            
            busstations.Insert(0, bus);
            FirstStation = busstations[0];
            busstations[1].TravelTime = TimeSpan.FromMinutes(Distance / 6000);
            busstations[1].Distance = r.NextDouble() * (3000 - 100) + 100;

        }

        //------------------------------
        //the func delete a  station from the bus. the func call to another function to delete the station.

        public void DelAtLineBus(BusStation bStation)
        {
            int index = find(bStation);     //to find the index of the station

            if (index == 0)     //if we need to delete the first station
            {
                DelFirstAtLineBus(bStation);
            }
            else
            {
                if (index == -1)        //if the staion to delete not exsis
                {
                    throw new ArgumentOutOfRangeException("index", "index should be less than or equal to" + busstations.Count);
                }

                if (index == busstations.Count)     //if we need delete the last station
                {
                    DelLastAtLineBus(bStation);
                }
                else        // to delete station at the middle of the list
                {
                    busstations.RemoveAt(index);

                    // to random newest Distance and travelTime to the next station after the delete
                    BusStation a = new BusStation();
                    busstations[index].Distance = a.Distance;
                    busstations[index].TravelTime = a.TravelTime;
                }


            }

        }

        public void DelLastAtLineBus(BusStation bStation)       //delete last station
        {
            busstations.Remove(bStation);
            LastStation = busstations[busstations.Count - 1];


        }

        public void DelFirstAtLineBus(BusStation busStation)        //delete first station
        {
            busstations.RemoveAt(0);
            FirstStation = busstations[0];
            busstations[0].TravelTime = TimeSpan.Zero;
            busstations[0].Distance = 0;//there are not have station befor
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
            for (; i < busstations.Count ; i++)
            {
                if(busstations[i]==bStation)
                {
                    return i;
                }

            }
            return -1;
        }

        public bool findStion(int id) //return true if the station exsis at the current line
        {

            int i = 0;
            for (; i < busstations.Count; i++)
            {
                if (busstations[i].BusStationKey == id)
                {
                    return true;
                }

            }
            return false;
        }

        public BusStation find_Station(int id) //return the busstion  of the station
        {

            int i = 0;
            for (; i < busstations.Count; i++)
            {
                if (busstations[i].BusStationKey == id)
                {
                    return busstations[i];
                }

            }
            return null;
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

            temp.NumberID = this.NumberID;
            return temp;
        }

    
        public TimeSpan TravelLine(LineBus BLine1)
        {
           TimeSpan timeSpan = stationTravelTime(BLine1.busstations[0], BLine1.busstations[BLine1.busstations.Count()-1]);
            return timeSpan;
        }

        public LineBus Compare(LineBus obj)
        {
            int t = this.CompareTo(obj);
            if (t < 0) return obj;
            else  return this;
            
        }

        public int CompareTo(LineBus obj)
        {
            LineBus otherLine = (LineBus)obj;

            //calculate the total time of the busesLine:
            TimeSpan t1 = this.TravelLine(this);
            TimeSpan t2 = otherLine.TravelLine(otherLine);

            return t1.CompareTo(t2);
        }


        //public int CompareTo(object obj)
        //{
        //    TimeSpan a = TravelLine((LineBus)obj);
        //    return a.CompareTo(TravelLine(this));

        //}


    }

}


