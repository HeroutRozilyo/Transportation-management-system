using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class LineStationUI
    {

        public int StationCode { get; set; }
        public int LineStationIndex { get; set; }
        public bool LineStationExist { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        //public GeoCoordinate Coordinate { get; set; }

    }
}
