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
        public static List<Bus> ListBus;
        public static List<Stations> ListStations;
        public static List<Line> ListLine;
        public static List<LineStation> ListLineStations;
        public static List<User> ListUsers;
        public static List<LineTrip> ListLineTrip;
        public static List<BusOnTrip> ListBusOnTrip;
        public static List<Trip> ListTrip;
        public static List<AdjacentStations> ListAdjacentStations;

        static DataSource()
        {
            InitAllLists();
        }

        static void InitAllLists()
        {

        }
    }
}
