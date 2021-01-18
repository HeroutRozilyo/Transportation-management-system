namespace DO
{
    /// <summary>
    /// An entity presenting a single station
    /// </summary>
    public class Stations
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
        /// Station longitude and latitude (exact location)
        /// </summary>
        public double Latitude { get; set; }
        public double Longtitude { get; set; }

        /// <summary>
        /// Station address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// If the station still exists
        /// </summary>
        public bool StationExist { get; set; }
    }
}
