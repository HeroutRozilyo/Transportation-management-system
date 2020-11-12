using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4789_9647.Properties
{
   
    /// Staion for bus
    public class Station
    {
        private static Random r = new Random();

        private const int MAXDIGITS = 1000000;
        private const int MIN_LAT = -90;
        private const int MAX_LAT = 90;
        private const int MIN_LON = -180;
        private const int MAX_LON = 180;

        private static List<int> numberofstation = new List<int>(); //a list to keep all the stations that we have
        private int busStationKey; //code to the station

        ///  place to the station
        private double latitude; 
        private double longitude;

        public String Address { get; set; } //name station

        public static List<int> Numberofstation()
        {
            get{

            }
        }



        /// constructors
        public Station()
        {
            latitude = 0;
            longitude = 0;
            Address = null;
            busStationKey = 0;
        }

        public Station(int lat,int lon,int id,string name)
        {
            Latitude = r.NextDouble() * (33.3 - 31) + 31; // in israel territory
            Longitude = r.NextDouble() * (35.5 - 34.3) + 34.3; // in israel territory
            Address = name;
            busStationKey = id;

            numberofstation.Add(busStationKey);
        }

    
        /// key value should  be unique and max 6 digits
        public int BusStationKey
        {
            get { return busStationKey; }

            set
            {
                if (numberofstation.Contains(value))
                {
                    throw new ArgumentException(
                        String.Format("{0} key number exists allready", value));
                }
                if (value <= 0 && value >= MAXDIGITS)
                {
                    throw new ArgumentException(
                       String.Format("{0} is not a valid key number", value));
                }
                busStationKey = value;
                numberofstation.Add(BusStationKey);
            }
        }

        public double Latitude
        {
            get { return latitude; }
            set
            {
                if (value >= MIN_LAT && value <= MAX_LAT)
                {
                    latitude = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Latitude",
                        String.Format("{0} should be between {1} and {2}", value, MIN_LAT, MAX_LAT));
                }
            }
        }

        public double Longitude
        {
            get { return longitude; }
            set
            {
                if (value >= MIN_LON && value <= MAX_LON)
                {
                    longitude = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Longitude",
                        String.Format("{0} should be between {1} and {2}", value, MIN_LON, MAX_LON));
                }
            }

        }

        public override string ToString()
        {
            String result = "Bus Station Code: " + busStationKey;
            result += String.Format(", {0}°{1} {2}°{3}",
                Math.Abs(Latitude), (Latitude > 0) ? "N" : "S",
                Math.Abs(Longitude), (Longitude > 0) ? "E" : "W");
            return result;
        }
    }
}
