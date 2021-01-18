using System.Collections.Generic;
using System.Device.Location;

namespace BO
{
    public class Station
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public GeoCoordinate Coordinate { get; set; }
        public bool StationExist { get; set; }

        public IEnumerable<LineStation> LineAtStation { get; set; }
        public IEnumerable<AdjacentStations> StationAdjacent { get; set; }


    }
}
