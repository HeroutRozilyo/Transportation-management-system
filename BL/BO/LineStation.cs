namespace BO
{
    /// <summary>
    /// An entity representing a particular station on the line
    /// </summary>
    public class LineStation
    {
        /// <summary>
        /// The identification number of the line belonging to the station line
        /// </summary>
        public int LineId { get; set; }

        /// <summary>
        /// The station code of the line station
        /// </summary>
        public int StationCode { get; set; }

        /// <summary>
        /// The station number on the line
        /// </summary>
        public int LineStationIndex { get; set; }

        /// <summary>
        /// If the station still exists on the line?
        /// </summary>
        public bool LineStationExist { get; set; }

        /// <summary>
        /// The station code in the line before the current station
        /// </summary>
        public int PrevStation { get; set; }

        /// <summary>
        /// The station code in the line after the current station
        /// </summary>
        public int NextStation { get; set; } 

        /// <summary>
        /// keep the distance at meter from the next station at the bus path
        /// </summary>
        public double DistanceFromNext { get; set; }

        /// <summary>
        /// keep the time travel to the next station
        /// </summary>
        public double TimeAverageFromNext { get; set; }



    }
}
