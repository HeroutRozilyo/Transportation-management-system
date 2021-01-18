using System;




namespace dotNet5781_02_4789_9647.Properties
{
    public class BusStation : Station
    {
        private static Random r = new Random();

        //------------------
        //constructor

        public BusStation()         //deafult constructor
        {

            Distance = r.NextDouble() * (3000 - 100) + 100;         //3000m

            TravelTime = TimeSpan.FromMinutes(Distance / 6000);     //standart average speed is 60 km/minutes
        }

        public BusStation(Station a)        //constructor
        {
            this.Distance = r.NextDouble() * (3000 - 100) + 100;
            this.TravelTime = TimeSpan.FromMinutes(Distance / 6000);
            this.Latitude = a.Latitude;
            this.Longitude = a.Longitude;
            this.BusStationKey = a.BusStationKey;
            this.Address = a.Address;
        }

        //public BusStation(ref BusStation busStation)        
        //{
        //    Distance = busStation.Distance;
        //    TravelTime = busStation.TravelTime;
        //    BusStationKey = busStation.BusStationKey;
        //    Address = busStation.Address;
        //    Latitude = busStation.Latitude;
        //    Longitude = busStation.Longitude;
        //}

        public BusStation(int id, string name) : base(id, name)      //constructor that accept values
        {
            Distance = r.NextDouble() * (3000 - 100) + 100;
            TravelTime = TimeSpan.FromMinutes(Distance / 6000);
        }




        public double Distance        /// distance from previous BusStation
        {
            get; set;
        }


        public TimeSpan TravelTime          /// Travel time from previous BusStation
        {
            get;
            set;
        }


        //public override string ToString()
        //{
        //    string result = "";

        //    result = "For station number "+ BusStationKey+ " The travel time is: " + TravelTime + " and the distance is between the two stations is: " + Distance;

        //    return result;
        //}
    }
}

