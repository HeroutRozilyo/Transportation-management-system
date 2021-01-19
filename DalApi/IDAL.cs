using DO;
using System;
using System.Collections.Generic;

namespace DALAPI
{
    public interface IDAL
    {
        #region Bus
        Bus GetBus(string licence); //return the bus exsis according to the licence
        IEnumerable<DO.Bus> GetAllBuses(); //return all the buses that we have
        IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> buscondition);//return busus by condition

        //IEnumerable<DO.Bus> GetAllBusesStusus(DO.STUTUS stusus);//get all the buses by stutus

        int AddBus(DO.Bus bus);

        bool DeleteBus(string licence);

        bool UpdateBus(DO.Bus buses);

        #endregion Bus

        #region Line
        DO.Line GetLine(int idline);//get line by idNumber
        IEnumerable<DO.Line> GetAllLineBy(Predicate<DO.Line> linecondition);//return according to condition and just the working line
        IEnumerable<DO.Line> GetAllLine();//return all the lines in the list

        //IEnumerable<DO.Line> GetAllLinesArea(DO.AREA area);

        int AddLine(DO.Line line);

        void UpdateLine(DO.Line line);

        void DeleteLine(int idnumber);
        #endregion Line

        #region Stations
        DO.Stations GetStations(int code); //return  the Stations  according to the code
        IEnumerable<DO.Stations> GetAllStations(); //return all the stations that we have
        IEnumerable<DO.Stations> GetAllStationsBy(Predicate<DO.Stations> Stationscondition);

        void AddStations(DO.Stations station);

        void DeleteStations(int code);

        void UpdateStations(DO.Stations stations, int oldCode);
        #endregion Stations

        #region LineTrip
        DO.LineTrip GetLineTrip(TimeSpan start, int idline);//return specific linetrip according to
      //  IEnumerable<DO.LineTrip> GetAllTripline(int idline); //return all the lineTrip of specific line
        IEnumerable<DO.LineTrip> GetAllLineTripsBy(Predicate<DO.LineTrip> StationsLinecondition);

        void AddLineTrip(DO.LineTrip lineTrip);

        void UpdatelineTrip(DO.LineTrip lineTrip);

        void DeleteLineTrip(int idline); //when we delete line we need to delete his line trip
        void DeleteLineTrip1(DO.LineTrip lineTrip);//delete specificLineTrip
        #endregion LineTrip

        #region LineStation
        LineStation GetLineStation(int Scode, int idline); //return specific stations according to code of the station and line that Passing through it
        IEnumerable<DO.LineStation> GetAllLineStationsBy(Predicate<DO.LineStation> StationsLinecondition);//return all the stationLine that Meet the criteria

        //IEnumerable<DO.LineStation> GetAllStationsLine(int idline); //return all the stations that we have with the same line
        //     IEnumerable<DO.LineStation> GetAllStationsCodeline(int code); //return all the stations that we have with the same code



        void AddLineStations(DO.LineStation station);

        void UpdateLineStations(DO.LineStation linestations);//Update details
        void UpdateLineStationsCode(DO.LineStation linestations, int oldCode);//toUpdate also Code station if he change 


        int DeleteStationsFromLine(int Scode, int idline);//delete specific LineStation
        void DeleteStationsFromLine(int Scode); ///delete StationLine according lineId(when line delete)
        void DeleteStationsOfLine(int idline); // when we delete line we need delete all his stationLine

        #endregion LineStation

        #region AdjeacentStations

        DO.AdjacentStations GetAdjacentStations(int Scode1, int Scode2); //return specific AdjacentStations
        IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> StationsLinecondition);//return by condition

        //     IEnumerable<DO.AdjacentStations> GetAllAdjacentStations(int stationCode);//return all the AdjacentStations that we have for this station code
        //void DeleteAdjacentStationse(int Scode1, int Scode2);

        void AddLineStations(DO.AdjacentStations adjacentStations);

        void DeleteAdjacentStationseBStation(int Scode1);//if station delete

        void UpdateAdjacentStations(DO.AdjacentStations adjacentStations);
        void UpdateAdjacentStations(int code1, int code2, int codeChange, int oldCode); //when we change the number code of the station

        #endregion AdjeacentStations

        #region User
        DO.User GetUser(string name); //check if the user exsis according to the name
        IEnumerable<DO.User> GetAlluser();//return all the user that we have
        IEnumerable<DO.User> GetAlluserBy(Predicate<DO.User> userConditions);
        DO.User getUserBy(Predicate<DO.User> userConditions);

        //IEnumerable<DO.User> GetAlluserAdmin(); //return all the user Admin we have;
        //IEnumerable<DO.User> GetAlluserNAdmin(); //return all the user not Admin we have


        void AddUser(DO.User user);

        void DeleteUser(string name);

        void UpdateUser(DO.User user);
        #endregion User


    }
}
