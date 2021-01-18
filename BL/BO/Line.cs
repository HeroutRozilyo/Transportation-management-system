using System.Collections.Generic;

namespace BO
{
    public class Line
    {
        public int IdNumber { get; set; }
        public int NumberLine { get; set; }
        public int FirstStationCode { get; set; }
        public int LastStationCode { get; set; }
        public AREA Area { get; set; }
        public bool LineExist { get; set; }
        public IEnumerable<LineStation> StationsOfBus { get; set; }
        public IEnumerable<LineTrip> TimeLineTrip { get; set; }
        public double TimeTravel { get; set; }

    }
}
