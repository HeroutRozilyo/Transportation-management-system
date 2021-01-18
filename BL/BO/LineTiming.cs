using System;

namespace BO
{
    public class LineTiming
    {
        public TimeSpan tripStart { get; set; }
        public int LineId { get; set; }
        public int LineNumber { get; set; }
        public string LastStation { get; set; }
        public TimeSpan ExpectedTimeArrive { get; set; }

    }
}
