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
        public IEnumerable<DO.Bus> GetAllBusesStusus(DO.STUTUS stusus) //return all the buses that we have
        {
            return from bus in DataSource.ListBus
                   where(bus.StatusBus==stusus)
                   select bus.Clone();
        }

        public IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> buscondition) //איך כותבים??
        {
            var list = from bus in DataSource.ListBus
                       where (bus.BusExsis && buscondition(bus))
                       select bus.Clone();
            return list;
        }

        public int AddBus(DO.Bus bus)
        {
            if (DataSource.ListBus.FirstOrDefault(b => b.Licence == bus.Licence) != null) //if != null its means that this licence is allready exsis
                throw new DO.WrongLicenceException(bus.Licence, "This licence already exsis");
            DataSource.ListBus.Add(bus.Clone());
            return 1;
        }

        ///===================================================================
        ///לבדוק איך מעדכנים נתון בתוך הרשימה שלי
        public bool DeleteBus(int licence)
        {
            DO.Bus bus = DataSource.ListBus.Find(b => b.Licence == licence);
            if (bus != null && bus.BusExsis)
            {
                // bus.BusExsis = false;
                DataSource.ListBus.Find(b => b.Licence == licence).BusExsis = false;
                return true;
            }
            else
                throw new DO.WrongLicenceException(licence, "Licence not exsis");
        }
        public bool UpdateBus(DO.Bus buses)
        {
            DO.Bus bus = DataSource.ListBus.Find(b => b.Licence == buses.Licence);
            if (bus != null && bus.BusExsis)
            {
                DataSource.ListBus.Remove(bus);
                DataSource.ListBus.Add(buses.Clone());
                return true;
            }
            else
                throw new DO.WrongLicenceException(buses.Licence, "Licence not exsis");
        }




        #endregion Bus

        #region Line
        public DO.Line GetLine(int idline)
        {
            DO.Line line = DataSource.ListLine.Find(l => l.IdNumber == idline);
            if (line != null && line.LineExsis)
                return line.Clone();
            else
                throw new DO.WrongIDExeption(idline, "The line not exsis ");

        }

        public void AddLine(DO.Line line)
        {
            //eliezer told us that we can do here checking because check according to idnumber is meaningless
            line.IdNumber = ++DS.Config.idLineCounter;
          
            DataSource.ListLine.Add(line.Clone());

        }
        
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


        public void UpdateLine(DO.Line line)
        {
            DO.Line tempLine = DataSource.ListLine.Find(b => b.IdNumber == line.IdNumber && b.LineExsis == true);
            if (tempLine != null)
            {
                DataSource.ListLine.Remove(tempLine);
                DataSource.ListLine.Add(line.Clone());
            }
            else
                throw new DO.WrongIDExeption(line.IdNumber, "Licence not exsis");
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
                throw new DO.WrongIDExeption(code, $"Licence not valid:{code}");

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
                throw new DO.WrongIDExeption(station.Code, "This station not exsis");/////////////////////////////////////////////////////////////////
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
                throw new DO.WrongIDExeption(code, "Code station not exsis");//////////////////////////////////////////////////////
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
                throw new DO.WrongIDExeption(stations.Code, "Code station not exsis");///////////////////////////////////////////////////////
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
                throw new DO.WrongIDExeption(Scode, $"Station code not exsis:{Scode}");/////////////////////////////////////////////////////////

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
                throw new DO.WrongIDExeption(station.StationCode, "This station already exsis");/////////////////////////////////////////////////////////////////
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
                throw new DO.WrongIDExeption(Scode, "Code not exsis");//////////////////////////////////////////////////////
        }
        public void DeleteStationsFromLine(int Scode) //we use here at foreach because it more effective.
        {
            foreach (DO.LineStation item in DataSource.ListLineStations)
            {
                if (item.StationCode == Scode)
                    item.LineStationExsis = false;
            }

        }

        public void UpdateStations(DO.LineStation linestations)
        {
            DO.LineStation station = DataSource.ListLineStations.Find(b => b.StationCode == linestations.StationCode && b.LineId == linestations.LineId && b.LineStationExsis);
            if (station != null)
            {
                DataSource.ListLineStations.Remove(station);
                DataSource.ListLineStations.Add(linestations.Clone());
            }
            else
                throw new DO.WrongIDExeption(linestations.StationCode, "Code not exsis");///////////////////////////////////////////////////////
        }

        #endregion LineStation

        #region LineTrip

        public DO.LineTrip GetLineTrip(TimeSpan start, int idline) //return specific linetrip according to start time oand id line 
        {
            DO.LineTrip linetrip = DataSource.ListLineTrip.Find(b => b.KeyId == idline && b.StartAt == start);
            if (linetrip != null)
            {
                return linetrip.Clone();
            }
            else
                throw new DO.WrongIDExeption(idline, $"Wrong ID:{start}");/////////////////////////////////////////////////////////

        }

        public IEnumerable<DO.LineTrip> GetAllTripline(int idline) //return all the lineTrip of specific line
        {
            return from linetrip in DataSource.ListLineTrip
                   where (linetrip.KeyId == idline)
                   select linetrip.Clone();
        }


        public IEnumerable<DO.LineTrip> GetAllLineTripsBy(Predicate<DO.LineTrip> StationsLinecondition)
        {
            var list = from linetrip in DataSource.ListLineTrip
                       where (StationsLinecondition(linetrip))
                       select linetrip.Clone();
            return list;
        }

        public void AddLineTrip(DO.LineTrip lineTrip)
        {
            DataSource.ListLineTrip.Add(lineTrip.Clone());
        }



        public void UpdatelineTrip(DO.LineTrip lineTrip)
        {
            DO.LineTrip station = DataSource.ListLineTrip.Find(b => b.KeyId == lineTrip.KeyId && lineTrip.StartAt == b.StartAt);
            if (station != null)
            {
                DataSource.ListLineTrip.Remove(station);
                DataSource.ListLineTrip.Add(lineTrip.Clone());
            }
            else
                throw new DO.WrongIDExeption(lineTrip.KeyId, "LineTrip not exsis");///////////////////////////////////////////////////////
        }


        #endregion LineTrip

        #region AdjacentStations

        public DO.AdjacentStations GetAdjacentStations(int Scode1, int Scode2) //return specific AdjacentStations
        {
            DO.AdjacentStations linestations = DataSource.ListAdjacentStations.Find(b => b.Station1 == Scode1 && b.Station2 == Scode2 || b.Station1 == Scode2 && b.Station2 == Scode1);
            if (linestations != null)
            {
                return linestations.Clone();
            }
            else
                throw new DO.WrongIDExeption(Scode1, $"Code not valid:{Scode1}");/////////////////////////////////////////////////////////

        }

        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations(int stationCode) //return all the AdjacentStations that we have for this station code
        {
            return from station in DataSource.ListAdjacentStations
                   where (stationCode == station.Station1)
                   select station.Clone();
        }
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsTo(int stationCode) //return all the AdjacentStations that we have for this station code (from end)
        {
            return from station in DataSource.ListAdjacentStations
                   where (stationCode == station.Station2)
                   select station.Clone();
        }


        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> StationsLinecondition)
        {
            var list = from stations in DataSource.ListAdjacentStations
                       where (StationsLinecondition(stations))
                       select stations.Clone();
            return list;
        }

        public void AddLineStations(DO.AdjacentStations adjacentStations)
        {
            DO.AdjacentStations temp = DataSource.ListAdjacentStations.Find(b => b.Station1 == adjacentStations.Station1 && b.Station2 == adjacentStations.Station2);
            if (temp != null)
                throw new DO.WrongIDExeption(adjacentStations.Station1, "This adjacent station already exsis ");/////////////////////////////////////////////////////////////////
            DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
        }

        public void DeleteAdjacentStationse(int Scode1, int Scode2)
        {
            DO.AdjacentStations stations = DataSource.ListAdjacentStations.Find(b => b.Station1 == Scode1 && b.Station2 == Scode2);
            if (stations != null)
            {
                DataSource.ListAdjacentStations.Remove(stations);
            }
            else
                throw new DO.WrongIDExeption(Scode1, "Code not exsis");//////////////////////////////////////////////////////
        }
        public void DeleteAdjacentStationseBStation(int Scode1)
        {
            foreach (DO.AdjacentStations item in DataSource.ListAdjacentStations)
            {
                if (item.Station1 == Scode1 || item.Station2 == Scode1)
                    DataSource.ListAdjacentStations.Remove(item);
            }

        }

        public void UpdateAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            DO.AdjacentStations station = DataSource.ListAdjacentStations.Find(b => b.Station1 == adjacentStations.Station1 && b.Station2 == adjacentStations.Station2);
            if (station != null)
            {
                DataSource.ListAdjacentStations.Remove(station);
                DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
            }
            else
                throw new DO.WrongIDExeption(adjacentStations.Station1, "Code not exsis");///////////////////////////////////////////////////////
        }
        #endregion AdjacentStations

        #region User
     
        public DO.User GetUser(string name) //check if the user exsis according to the name
        {
            DO.User user = DataSource.ListUsers.Find(b => b.UserName == name && b.UserExsis);
            if (user != null)
            {
                return user.Clone();
            }
            else
                throw new DO.WrongNameExeption(name, $"Name not valid:{1}");/////////////////////////////////////////////////////////

        }
        public IEnumerable<DO.User> GetAlluser() //return all the user that we have
        {
            return from user in DataSource.ListUsers
                   where (user.UserExsis)
                   select user.Clone();
        }
        public IEnumerable<DO.User> GetAlluserAdmin() //return all the user Admin we have
        {
            return from user in DataSource.ListUsers
                   where (user.UserExsis&&user.Admin)
                   select user.Clone();
        }
        public IEnumerable<DO.User> GetAlluserNAdmin() //return all the user not Admin we have
        {
            return from user in DataSource.ListUsers
                   where (user.UserExsis && !user.Admin)
                   select user.Clone();
        }

        public IEnumerable<DO.User> GetAlluserBy(Predicate<DO.User> userConditions) //איך כותבים??
        {
            var users = from u in DataSource.ListUsers
                       where (u.UserExsis && userConditions(u))
                       select u.Clone();
            return users;
        }

        public void AddUser(DO.User user)
        {
            if (DataSource.ListUsers.FirstOrDefault(b => b.UserName == user.UserName) != null) //if != null its means that this name is allready exsis
                throw new DO.WrongNameExeption(user.UserName, "This name already exsis");/////////////////////////////////////////////////////////////////
            DataSource.ListUsers.Add(user.Clone());
        }


        public void DeleteUser(string name)
        {
            DO.User userDelete = DataSource.ListUsers.Find(b => b.UserName == name && b.UserExsis);
            if (userDelete != null)
            {
                userDelete.UserExsis = false;

            }
            else
                throw new DO.WrongNameExeption(name, "Name not exsis");//////////////////////////////////////////////////////
        }

        public void UpdateUser(DO.User user)
        {
            DO.User u = DataSource.ListUsers.Find(b => b.UserName == user.UserName && b.UserExsis);
            if (u != null)
            {
                DataSource.ListUsers.Remove(u);
                DataSource.ListUsers.Add(user.Clone());
            }
            else
                throw new DO.WrongNameExeption(user.UserName, "Name not exsis");///////////////////////////////////////////////////////
        }

        #endregion User

        #region Trip

 

        public DO.Trip GetTrip(int id) //check if the Trip exsis according to the id
        {
            DO.Trip trip = DataSource.ListTrip.Find(b => b.Id == id&&b.TripExsis);
            if (trip != null)
            {
                return trip.Clone();
            }
            else
                throw new DO.WrongIDExeption(id, $"ID not valid:{id}");/////////////////////////////////////////////////////////

        }
        public IEnumerable<DO.Trip> GetAllTrip() //return all the stations that we have
        {
            return from trip in DataSource.ListTrip
                   where (trip.TripExsis)
                   select trip.Clone();
        }
        public IEnumerable<DO.Trip> GetAllTripLine(int line) //return all the trip that we have in thid line
        {
            return from trip in DataSource.ListTrip
                   where (trip.TripExsis&&trip.LineId==line)
                   select trip.Clone();
        }

        public IEnumerable<DO.Trip> GetAllTripsBy(Predicate<DO.Trip> Tripcondition) //איך כותבים??
        {
            var list = from trip in DataSource.ListTrip
                       where (trip.TripExsis && Tripcondition(trip))
                       select trip.Clone();
            return list;
        }

        public void AddTrip(DO.Trip trip)
        {
            if (DataSource.ListTrip.FirstOrDefault(b => b.UserName == trip.UserName) != null) //if != null its means that this licence is allready exsis
                throw new DO.WrongNameExeption(trip.UserName, "This User already in trip");
            trip.Id = ++DS.Config.tripUser;

            DataSource.ListTrip.Add(trip.Clone());
        }


        public void DeleteTrip(int id)
        {
            DO.Trip stations = DataSource.ListTrip.Find(b => b.Id == id && b.TripExsis);
            if (stations != null)
            {
                stations.TripExsis = false;

            }
            else
                throw new DO.WrongIDExeption(id, "ID not exsis");
        }

        public void UpdateStations(DO.Trip trip)
        {
            DO.Trip t = DataSource.ListTrip.Find(b => b.Id== trip.Id && b.TripExsis);
            if (t != null)
            {
                DataSource.ListTrip.Remove(t);
                DataSource.ListTrip.Add(trip.Clone());
            }
            else
                throw new DO.WrongIDExeption(trip.Id, "Id not exsis");
        }
        #endregion Trip
    }
}
