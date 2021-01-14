using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;



namespace BlAPI
{
    public interface IBL
    {
        #region Bus
        IEnumerable<BO.Bus> GetAllBus();
      //  IEnumerable<BO.Bus> GetBusByline(int line);
        IEnumerable<BO.Bus> GetBusByStatus(BO.STUTUS stutus);
        int AddBus(BO.Bus bus);
        bool DeleteBus(string licence);
        BO.Bus UpdateBus(BO.Bus bus);
        BO.Bus Refuelling(BO.Bus bus);
        BO.Bus treatment(BO.Bus bus);
        #endregion

        #region Line
        IEnumerable<BO.Line> GetAllLine(); //return all the lines that working 
        IEnumerable<BO.Line> GetLineBy(int stationCode); //return all the lines according to predicate
        IEnumerable<BO.Line> GetLineByArea(BO.AREA area); //return all the line according to their area
        IEnumerable<IGrouping<BO.AREA, BO.Line>> GetLinesByAreaG();
        BO.Line GetLineByLine(int lineid);//return line
        int AddLine(BO.Line line);
         IEnumerable<BO.Line> GetLineByLineCode(int LineCodeCode);
       
        //  IEnumerable<BO.LineStation> AddStationLine(BO.LineStation station); //we add station to the bus travel

        void DeleteLine(int idLine);
        void DeleteStation(int idline, int code); //delete station from the line travel
        bool UpdateLine(BO.Line line);
        void AddOneTripLine(LineTrip line); //func that get new lineTrip and update the list at DS
        bool CreatAdjStations(int station1, int station2);
        double CalucateTravel(int lineId); //return the sum of time travel
        IEnumerable<object> DetailsOfStation(IEnumerable<LineStation> lineStations); //creat a new object in order to return all data on station
        void UpdateLineStationForIndexChange(BO.Line line);
         bool UpdateLineStation(BO.Line line);
        //IEnumerable<BO.LineStation> UpdateLineStation(BO.Line line);

       bool UpdateLineTrip(int oldTripLineIndex, BO.LineTrip newLineTrip);

        void DeleteLineTrip(BO.LineTrip toDel);
        IEnumerable<BO.Line> GetAllLineIndStation(int StationCode);
        void AddAdjactStation(BO.LineStation line);
        #endregion

        #region Station
        IEnumerable<BO.Station> GetAllStations();
        void AddStation(BO.Station station);
        void DeleteStation(int code, IEnumerable<LineStation> a);
        void UpdateStation(BO.Station station, int oldCode);
        BO.Station GetStationByCode(int Code);
        void UpdateAdjac(BO.AdjacentStations adjacBO);
        #endregion

        #region User
        IEnumerable<object> TravelPath(int code1, int code2);
        bool findUser(BO.User a);
        void AddUser(string name, string pas, bool admin, string mail);
        double CalucateTime(BO.Line line, int cod1, int cod2);
        BO.User getUserByEmail(string email);
        IEnumerable<BO.User> GetAllUsers();
        bool EmailExsit(string mail);
        #endregion
    }
}
