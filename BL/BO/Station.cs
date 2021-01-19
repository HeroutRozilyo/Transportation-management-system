using System.Collections.Generic;
using System.Device.Location;

namespace BO
{
    /// <summary>
    /// An entity presenting a single station
    /// </summary>
    public class Station
    {
        /// <summary>
        /// Station Code (Unique ID)
        /// </summary>
        public int Code { get; set; }


        /// <summary>
        /// The official name of the station
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Station address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Station location- longtitude and latitude
        /// </summary>
        public GeoCoordinate Coordinate { get; set; }

        /// <summary>
        /// If the station still exists
        /// </summary>
        public bool StationExist { get; set; }

        /// <summary>
        /// list of all the line that move at this line station
        /// </summary>
        public IEnumerable<LineStation> LineAtStation { get; set; }

        /// <summary>
        /// list os all the adjacte statin with this station
        /// </summary>
        public IEnumerable<AdjacentStations> StationAdjacent { get; set; }


    }
}
