using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using DALAPI;
//using DO;
using DS;


namespace DL
{
    sealed class DalObject : IDAL
    {
        #region singelton
        static readonly DalObject instance = new DalObject();
        static DalObject() { }
        DalObject() { }
        public static DalObject Instance => instance;
        #endregion

        #region Bus
        public DO.Bus GetBus(int licence) //check if the bus exsis according to the licence
        {
            DO.Bus bus = DataSource.ListBus.Find(b => b.Licence == licence);
            if (bus != null && bus.BusExsis)
            {
                return bus.Clone();
            }
            else
                throw new DO.WrongLicenceException(licence, $"Licence not valid:{licence}");

        }
        public IEnumerable<DO.Bus> GetAllBuses() //return all the buses that we have
        {
            return from bus in DataSource.ListBus
                   select bus.Clone();
        }

        public IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> buscondition) //איך כותבים??
        {
            var list = from bus in DataSource.ListBus
                       where (bus.BusExsis && buscondition(bus))
                       select bus.Clone();
            return list;
        }

        public void AddBus(DO.Bus bus)
        {
            if (DataSource.ListBus.FirstOrDefault(b => b.Licence == bus.Licence) != null) //if != null its means that this licence is allready exsis
                throw new DO.WrongLicenceException(bus.Licence, "This licence already exsis");
            DataSource.ListBus.Add(bus.Clone());
        }

        ///===================================================================
        ///לבדוק איך מעדכנים נתון בתוך הרשימה שלי
        public void DeleteBus(int licence)
        {
            DO.Bus bus = DataSource.ListBus.Find(b => b.Licence == licence);
            if (bus != null && bus.BusExsis)
            {
                // bus.BusExsis = false;
                DataSource.ListBus.Find(b => b.Licence == licence).BusExsis = false;
            }
            else
                throw new DO.WrongLicenceException(licence, "Licence not exsis");
        }
        public void UpdateBus(DO.Bus buses)
        {
            DO.Bus bus = DataSource.ListBus.Find(b => b.Licence == buses.Licence);
            if (bus != null && bus.BusExsis)
            {
                DataSource.ListBus.Remove(bus);
                DataSource.ListBus.Add(buses.Clone());
            }
            else
                throw new DO.WrongLicenceException(buses.Licence, "Licence not exsis");
        }



        //public void UpdateBus(int licence, Action<DO.Bus> update)
        //{
        //    DO.Bus bus = DataSource.ListBus.Find(b => b.Licence == licence);
        //    if (bus != null && bus.BusExsis)//
        //    {


        //    }
        //    else
        //        throw new DO.WrongLicenceException(licence, "Licence not exsis");
        //}

        #endregion Bus

        #region Line

        public DO.Line GetLine(int idline)
        {
            DO.Line line = DataSource.ListLine.Find(l => l.IdNumber == idline);
            if (line != null && line.LineExsis)
                return line.Clone();
            else
                throw new DO.WrongLicenceException(idline, "The line not exsis ");

        }

        public void AddLine(DO.Line line)
        {
            if (DataSource.ListLine.FirstOrDefault(p => p.IdNumber == line.IdNumber) != null)
                throw new DO.WrongLicenceException(line.IdNumber, "these line exist");
            DataSource.ListLine.Add(line.Clone());

        }
        /// כדי לא ךשכוח. צריך לחשוב אם כל פעם שמוחקים אוטובוס אז צרך לעדכן גם ברשומה של הקו אם הוא לא קיים. אחרת צריך כל פעם לעשות בדיקות בקו אוטובוס נראלי. 
        /// 
        public IEnumerable<DO.Line> GetAllLineBy(Predicate<DO.Line> linecondition)//return according to condition and just the working line
        {
            var list = from line in DataSource.ListLine
                       where (line.LineExsis && linecondition(line))
                       select line.Clone();
            return list;
        }
        public IEnumerable<DO.Line> GetAllLine()//return all the lines in the list
        {
            return from line in DataSource.ListLine
                   select line.Clone();
        }

        public IEnumerable<object> GetLineFields(Func<int, bool, object> generate)//return all the lines on the country that exsis and withe the sme lineNumber
        {
            return from line in DataSource.ListLine
                   select generate(line.IdNumber, line.LineExsis);
        }

        //public IEnumerable<object> GetlinetListWithSelectedFields(Func<DO.Line, object> generate)
        //{
        //    return from student in DataSource.ListStudents
        //           select generate(student);
        //}
        public void UpdateLine(DO.Line line)
        {
            DO.Line tempLine = DataSource.ListLine.Find(b => b.IdNumber == line.IdNumber && b.LineExsis == true);
            if (tempLine != null)
            {
                DataSource.ListLine.Remove(tempLine);
                DataSource.ListLine.Add(line.Clone());
            }
            // else
            // throw new DO.WrongLicenceException(buses.Licence, "Licence not exsis");///////////////////////////////////////////////////
        }



        public void DeleteLine(int idnumber)
        {

            DO.Line LineToDelete = DataSource.ListLine.Find(b => b.IdNumber == idnumber);
            LineToDelete.LineExsis = false;

        }

        #endregion Line


        #region Stations
        public DO.Stations GetStations(int code) //check if the Stations exsis according to the code
        {
            DO.Stations stations = DataSource.ListStations.Find(b => b.Code == code && b.StationExsis);
            if (stations != null)
            {
                return stations.Clone();
            }
            else
                throw new DO.WrongLicenceException(code, $"Licence not valid:{code}");/////////////////////////////////////////////////////////

        }
        public IEnumerable<DO.Stations> GetAllStations() //return all the stations that we have
        {
            return from station in DataSource.ListStations
                   where (station.StationExsis)
                   select station.Clone();
        }

        public IEnumerable<DO.Stations> GetAllStationsBy(Predicate<DO.Stations> Stationscondition) //איך כותבים??
        {
            var list = from stations in DataSource.ListStations
                       where (stations.StationExsis && Stationscondition(stations))
                       select stations.Clone();
            return list;
        }

        public void AddStations(DO.Stations station)
        {
            if (DataSource.ListStations.FirstOrDefault(b => b.Code == station.Code) != null) //if != null its means that this licence is allready exsis
                throw new DO.WrongLicenceException(station.Code, "This licence already exsis");/////////////////////////////////////////////////////////////////
            DataSource.ListStations.Add(station.Clone());
        }


        public void DeleteStations(int code)
        {
            DO.Stations stations = DataSource.ListStations.Find(b => b.Code == code && b.StationExsis);
            if (stations != null)
            {
                stations.StationExsis = false;

            }
            else
                throw new DO.WrongLicenceException(code, "Licence not exsis");//////////////////////////////////////////////////////
        }

        public void UpdateStations(DO.Stations stations)
        {
            DO.Stations station = DataSource.ListStations.Find(b => b.Code == stations.Code && b.StationExsis);
            if (station != null)
            {
                DataSource.ListStations.Remove(station);
                DataSource.ListStations.Add(stations.Clone());
            }
            else
                throw new DO.WrongLicenceException(stations.Code, "Licence not exsis");///////////////////////////////////////////////////////
        }

        #endregion Stations

        #region LineStation


        public DO.LineStation GetLineStation(int Scode, int idline) //return specific stations according to code of the station and line that Passing through it
        {
            DO.LineStation linestations = DataSource.ListLineStations.Find(b => b.StationCode == Scode && b.LineId == idline && b.LineStationExsis == true);
            if (linestations != null)
            {
                return linestations.Clone();
            }
            else
                throw new DO.WrongLicenceException(Scode, $"Licence not valid:{Scode}");/////////////////////////////////////////////////////////

        }

        public IEnumerable<DO.LineStation> GetAllStationsLine(int idline) //return all the stations that we have with the same line
        {
            return from station in DataSource.ListLineStations
                   where (station.LineId == idline)
                   select station.Clone();
        }
        public IEnumerable<DO.LineStation> GetAllStationsCode(int code) //return all the stations that we have with the same code
        {
            return from station in DataSource.ListLineStations
                   where (station.StationCode == code)
                   select station.Clone();
        }

        public IEnumerable<DO.LineStation> GetAllLineStationsBy(Predicate<DO.LineStation> StationsLinecondition) 
        {
            var list = from stations in DataSource.ListLineStations
                       where (stations.LineStationExsis && StationsLinecondition(stations))
                       select stations.Clone();
            return list;
        }

        public void AddLineStations(DO.LineStation station)
        {
            DO.LineStation temp = DataSource.ListLineStations.Find(b => b.StationCode == station.StationCode && b.LineId == station.LineId && b.LineStationExsis);
            if (temp != null)
                throw new DO.WrongLicenceException(station.StationCode, "This licence already exsis");/////////////////////////////////////////////////////////////////
            DataSource.ListLineStations.Add(station.Clone());
        }

        public void DeleteStationsFromLine(int Scode, int idline)
        {
            DO.LineStation stations = DataSource.ListLineStations.Find(b => b.StationCode == Scode && b.LineId == idline && b.LineStationExsis);
            if (stations != null)
            {
                stations.LineStationExsis = false;
            }
            else
                throw new DO.WrongLicenceException(Scode, "Licence not exsis");//////////////////////////////////////////////////////
        }
        public void DeleteStationsFromLine(int Scode) //we use here at foreach because it more effective.
        {
            foreach(DO.LineStation item in DataSource.ListLineStations)
            {
                if (item.StationCode == Scode)
                    item.LineStationExsis = false;
            }

        }

        public void UpdateStations(DO.LineStation linestations)
        {
            DO.LineStation station = DataSource.ListLineStations.Find(b => b.StationCode == linestations.StationCode &&b.LineId== linestations.LineId && b.LineStationExsis);
            if (station != null)
            {
                DataSource.ListLineStations.Remove(station);
                DataSource.ListLineStations.Add(linestations.Clone());
            }
            else
                throw new DO.WrongLicenceException(linestations.StationCode, "Licence not exsis");///////////////////////////////////////////////////////
        }

        #endregion LineStation

    }
}
