namespace BO
{
    /// <summary>
    ///   Entity following stations on the line
    /// </summary>
    public class AdjacentStations
    {
        /// <summary>
        /// the first station. the order is important
        /// </summary>
        public int Station1 { get; set; }

        /// <summary>
        /// the second station
        /// </summary>
        public int Station2 { get; set; }

        /// <summary>
        /// the distance between the station us calucate by the distance cetween the coordinate station
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// the time travel between the station. calucate by the distance * random number between 1-1.5 mult the average fast
        /// </summary>
        public double TimeAverage { get; set; }
    }
}
