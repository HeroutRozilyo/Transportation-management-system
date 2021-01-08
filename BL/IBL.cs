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
        BO.Line GetLineByLine(int lineid);//return line
        IEnumerable<BO.LineStation> AddLine(BO.Line line);

        //  IEnumerable<BO.LineStation> AddStationLine(BO.LineStation station); //we add station to the bus travel

        void DeleteLine(int idLine);
        BO.LineStation DeleteStation(int idline, int code); //delete station from the line travel
        bool UpdateLine(BO.Line line);
        void AddOneTripLine(LineTrip line); //func that get new lineTrip and update the list at DS
        bool CreatAdjStations(int station1, int station2);
        double CalucateTravel(int lineId); //return the sum of time travel
        IEnumerable<object> DetailsOfStation(IEnumerable<LineStation> lineStations); //creat a new object in order to return all data on station
        IEnumerable<BO.LineStation> UpdateLineStationForIndexChange(BO.Line line);
        // bool UpdateLineStation(BO.Line line);
        IEnumerable<BO.LineStation> UpdateLineStation(BO.Line line);

        bool UpdateLineTrip(int oldTripLineIndex, BO.LineTrip newLineTrip);
        void DeleteLineTrip(BO.LineTrip toDel);
        IEnumerable<BO.Line> GetAllLineIndStation(int StationCode);

        #endregion

        #region Station
        IEnumerable<BO.Station> GetAllStations();
        void AddStation(BO.Station station);
        void DeleteStation(int code);
        void UpdateStation(BO.Station station);
        BO.Station GetStationByCode(int Code);
        #endregion
    }
}
