using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{/// <summary>
/// Entity following stations on the line
/// </summary>
    public class AdjacentStations 
    {
        /// <summary>
        /// The first of the following stations-Unique ID Number Part 1
        /// </summary>
        public int Station1 { get; set; }
        /// <summary>
        /// The second of the following stations-Unique ID Number Part 2
        /// </summary>
        public int Station2 { get; set; }
        /// <summary>
        /// Distance in meters between stations
        /// </summary>
        public double Distance { get; set; }
        /// <summary>
        /// Average travel time between stations
        /// </summary>
        public double TimeAverage { get; set; }
        /// <summary>
        /// Bool variable if the tracking station does exist in the system
        /// </summary>
        public bool AdjacExsis { get; set; }
    }
}
