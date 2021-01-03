using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station
    {
        public int Code { get; set; }
        public string Name { get; set; }
       // public string Address { get; set; }
        public GeoCoordinate Coordinate { get; set; }
        public bool StationExsis { get; set; }
        public IEnumerable<LineStation> LineAtStation { get; set; }

    }
}
