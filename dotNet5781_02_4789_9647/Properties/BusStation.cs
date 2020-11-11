using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;




namespace dotNet5781_02_4789_9647.Properties
{
    public class BusStation : Station
    {
        private static Random r = new Random();

        /// <summary>
        /// distance from previous BusStation
        /// </summary>
        /// 
        public double Distance
        {
            get; set;
        }


        public BusStation()
        {
           //3000m
            Distance = r.NextDouble() * (3000 - 100) + 100; 
            //standart average speed is 60 km/minutes
            TravelTime = TimeSpan.FromMinutes(Distance / 6000);
        }


        /// <summary>
        /// Travel time from previous BusStation
        /// </summary>
        public TimeSpan TravelTime
        {
            get;
            set;
        }


        public override string ToString()
        {
            string result = "";

            result = "Travel time is: " + TravelTime + ",Distance between the two stations is: " + Distance;

            return result;
        }
    }
}

