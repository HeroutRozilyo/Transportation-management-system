using System;

namespace BO
{
    /// <summary>
    /// An entity that displays a schedule of a particular line
    /// </summary>
    public class LineTrip
    {
        /// <summary>
        ///The ID number of the line to which the schedule is associated,Unique ID Part 1
        /// </summary>
        public int KeyId { get; set; }

        /// <summary>
        /// Start time of the schedule - Unique ID Part 2
        /// </summary>
        public TimeSpan StartAt { get; set; }


        /// <summary>
        /// Frequency of the line - how long does the line leave the first station? (Frequency 0 - one-time departure)
        /// </summary>
        public double Frequency { get; set; } //if 0 so its mean single exit 

        /// <summary>
        /// End time of the line
        /// </summary>
        public TimeSpan FinishAt { get; set; } //It is possible to have several end times per hour

        /// <summary>
        /// If the above schedule still exists and has not been deleted
        /// </summary>
        public bool TripLineExist { get; set; }

    }
}
