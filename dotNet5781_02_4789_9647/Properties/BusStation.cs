using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4789_9647.Properties
{
    public class BusStation : Station
    {

        /// <summary>
        /// distance from previous BusStation
        /// </summary>
        public double Distance { get; set; }
        /// <summary>
        /// Travel time from previous BusStation
        /// </summary>
        public TimeSpan TravelTime { get; set; }


        public override string ToString()
        {

            //TODO
            return base.ToString();
        }
    }
}
