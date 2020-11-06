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

        /// <summary>
        /// distance from previous BusStation
        /// </summary>
        public double Distance
        {
            get { return Distance; }


            set
            {
               
               Random r=new Random();
               double a = r.Next(0, 3000); //3000m
               Distance = a;
            }
        }

      

        /// <summary>
        /// Travel time from previous BusStation
        /// </summary>
        public TimeSpan TravelTime
        {
            get { return TravelTime; }
            set
            {
                //x=x0+vt.. we decided that the average speed is 60kmsh=6000m
                
                TravelTime = TimeSpan.FromMinutes(Distance / 6000);
            }
        }


        public override string ToString()
        {
            string result="";

            result ="Travle time is: "+ TravelTime + ",Distance between the two stations is: " + Distance;

            return result;
            
           // return base.ToString();
        }
    }
}
