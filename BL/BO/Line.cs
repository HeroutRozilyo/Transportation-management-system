using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Line
    {
        public int IdNumber { get; set; }
        public int NumberLine { get; set; }
        public int FirstStationCode { get; set; }
        public int LastStationCode { get; set; }
        public AREA Area { get; set; }
        public bool LineExsis { get; set; }
        public IEnumerable<LineStation> StationsOfBus { get; set; }
        public IEnumerable<LineTrip> TimeLineTrip { get; set; }

    }
}
