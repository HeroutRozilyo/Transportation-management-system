using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace DS
{
    public static class DataSource
    {
        public static List<Bus> ListBus=new List<Bus>();
        public static List<Stations> ListStations=new List<Stations>();
        public static List<Line> ListLine=new List<Line>();
        public static List<LineStation> ListLineStations=new List<LineStation>();
        public static List<User> ListUsers=new List<User>();
        public static List<LineTrip> ListLineTrip=new List<LineTrip>();
        public static List<BusOnTrip> ListBusOnTrip=new List<BusOnTrip>();
        public static List<Trip> ListTrip=new List<Trip>();

    }
}
