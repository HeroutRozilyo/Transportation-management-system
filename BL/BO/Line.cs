using System.Collections.Generic;

namespace BO
{
    /// <summary>
    /// An entity representing a bus line
    /// </summary>
    public class Line
    {
        /// <summary>
        /// A number runs to a line identifier
        /// </summary>
        public int IdNumber { get; set; }

        /// <summary>
        /// Official line number
        /// </summary>
        public int NumberLine { get; set; }

        /// <summary>
        /// Code of the first stop on the line
        /// </summary>
        public int FirstStationCode { get; set; }

        /// <summary>
        /// Code of the last stop on the line
        /// </summary>
        public int LastStationCode { get; set; }

        /// <summary>
        /// The area of ​​activity of the line
        /// </summary>
        public AREA Area { get; set; }

        /// <summary>
        /// Does the line still exist in the system?
        /// </summary>
        public bool LineExist { get; set; }

        /// <summary>
        /// station that this line travel at theme
        /// </summary>
        public IEnumerable<LineStation> StationsOfBus { get; set; }

        /// <summary>
        /// all the line trip of this line.. all the frequency
        /// </summary>
        public IEnumerable<LineTrip> TimeLineTrip { get; set; }

        /// <summary>
        /// keep tha time travel of this line
        /// </summary>
        public double TimeTravel { get; set; }

    }
}
