using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Line
    {
        public int Id { get; set; }
        public int CodeLine { get; set; }
        public int FirstStationCode { get; set; }
        public int LastStationCode { get; set; }
        public AREA Area { get; set; }
    }
}
