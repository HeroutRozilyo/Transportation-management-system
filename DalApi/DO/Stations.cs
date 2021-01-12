using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;


namespace DO
{
    public class Stations
    {
        public int Code { get; set; }
        public string Name { get; set; }
     //   public  GeoCoordinate Coordinate { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }

        public string Address { get; set; }
        public bool StationExist { get; set; }
    }
}
