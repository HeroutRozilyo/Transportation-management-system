using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DALAPI
{
    public interface IDAL
    {
        #region Bus
        Bus GetBus(int licence); //return the bus exsis according to the licence
        IEnumerable<DO.Bus> GetAllBuses(); //return all the buses that we have
        IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> buscondition);
        IEnumerable<DO.Bus> GetAllBusesStusus(DO.STUTUS stusus);
        int AddBus(DO.Bus bus);
        bool DeleteBus(int licence);
        bool UpdateBus(DO.Bus buses);

        #endregion Bus

        #region Line
        DO.Line GetLine(int idline);
        int AddLine(DO.Line line);
        IEnumerable<DO.Line> GetAllLineBy(Predicate<DO.Line> linecondition);//return according to condition and just the working line
        IEnumerable<DO.Line> GetAllLine();//return all the lines in the list
        IEnumerable<object> GetLineFields(Func<int, bool, object> generate);//return all the lines on the country that exsis and withe the sme lineNumber
        IEnumerable<DO.Line> GetAllLinesArea(DO.AREA area);
        void UpdateLine(DO.Line line);
        void DeleteLine(int idnumber);
        #endregion Line

        #region Stations
         DO.Stations GetStations(int code); //check if the Stations exsis according to the code
        IEnumerable<DO.Stations> GetAllStations(); //return all the stations that we have
        IEnumerable<DO.Stations> GetAllStationsBy(Predicate<DO.Stations> Stationscondition);
        void AddStations(DO.Stations station);
        void DeleteStations(int code);
        void UpdateStations(DO.Stations stations);
        #endregion Stations

        #region LineTrip
        DO.LineTrip GetLineTrip(TimeSpan start, int idline);//return specific linetrip according to
        IEnumerable<DO.LineTrip> GetAllTripline(int idline); //return all the lineTrip of specific line
        IEnumerable<DO.LineTrip> GetAllLineTripsBy(Predicate<DO.LineTrip> StationsLinecondition);
        void AddLineTrip(DO.LineTrip lineTrip);
        void UpdatelineTrip(DO.LineTrip lineTrip);
        #endregion LineTrip

        #region LineStation
        LineStation GetLineStation(int Scode, int idline); //return specific stations according to code of the station and line that Passing through it

        IEnumerable<DO.LineStation> GetAllStationsLine(int idline); //return all the stations that we have with the same line

        IEnumerable<DO.LineStation> GetAllStationsCode(int code); //return all the stations that we have with the same code

        IEnumerable<DO.LineStation> GetAllLineStationsBy(Predicate<DO.LineStation> StationsLinecondition);

        IEnumerable<DO.LineStation> GetAllLineAt2Stations(int code1, int cod2); //get 2 stations and return all the lines this 2 stations is adjacted at them

        void AddLineStations(DO.LineStation station);

        void DeleteStationsFromLine(int Scode, int idline);

        void DeleteStationsFromLine(int Scode); //we use here at foreach because it more effective.

        void UpdateStations(DO.LineStation linestations);
        #endregion LineStation

        #region AdjeacentStations
        DO.AdjacentStations GetAdjacentStations(int Scode1, int Scode2); //return specific AdjacentStations

        IEnumerable<DO.AdjacentStations> GetAllAdjacentStations(int stationCode);//return all the AdjacentStations that we have for this station code
        IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsTo(int stationCode);//return all the AdjacentStations that we have for this station code (from end) 

        IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> StationsLinecondition);

        void AddLineStations(DO.AdjacentStations adjacentStations);

        void DeleteAdjacentStationse(int Scode1, int Scode2);

        void DeleteAdjacentStationseBStation(int Scode1);

        void UpdateAdjacentStations(DO.AdjacentStations adjacentStations);

        #endregion AdjeacentStations

        #region User
        DO.User GetUser(string name); //check if the user exsis according to the name

        IEnumerable<DO.User> GetAlluser();//return all the user that we have

        IEnumerable<DO.User> GetAlluserAdmin(); //return all the user Admin we have;

        IEnumerable<DO.User> GetAlluserNAdmin(); //return all the user not Admin we have

        IEnumerable<DO.User> GetAlluserBy(Predicate<DO.User> userConditions);

        void AddUser(DO.User user);

        void DeleteUser(string name);

        void UpdateUser(DO.User user);
        #endregion User

        #region Trip
        DO.Trip GetTrip(int id); //check if the Trip exsis according to the id

        IEnumerable<DO.Trip> GetAllTrip(); //return all the stations that we have

        IEnumerable<DO.Trip> GetAllTripLine(int line); //return all the trip that we have in thid line

        IEnumerable<DO.Trip> GetAllTripsBy(Predicate<DO.Trip> Tripcondition);

        void AddTrip(DO.Trip trip);

        void DeleteTrip(int id);

        void UpdateStations(DO.Trip trip);

        #endregion Trip
    }
}
