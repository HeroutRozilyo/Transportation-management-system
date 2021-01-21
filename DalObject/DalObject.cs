using DALAPI;
//using DO;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;


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

        #region get

        public DO.Bus GetBus(string licence)
        {
            int indexBus = DataSource.ListBus.FindIndex(b => b.Licence == licence);
            if (indexBus != -1 && DataSource.ListBus[indexBus].BusExist)
            {
                return DataSource.ListBus[indexBus].Clone();
            }
            else
                throw new DO.WrongLicenceException(int.Parse(licence), $"{licence}:מספר רישוי לא תקין");

        }
        public IEnumerable<DO.Bus> GetAllBuses()
        {
            return from bus in DataSource.ListBus
                   where (bus.BusExist == true)
                   select bus.Clone();
        }

        public IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> buscondition)
        {
            var list = from bus in DataSource.ListBus
                       where (bus.BusExist && buscondition(bus))
                       select bus.Clone();
            return list;
        }


        #endregion AllGetBus

        #region add
        public int AddBus(DO.Bus bus)
        {
            if (DataSource.ListBus.FirstOrDefault(b => b.Licence == bus.Licence) != null) //if != null its means that this licence is allready exsis
                throw new DO.WrongLicenceException(int.Parse(bus.Licence), "המספר רישוי כבר קיים במערכת");
            bus.BusExist = true;
            DataSource.ListBus.Add(bus.Clone());
            return 1;
        }

        #endregion

        #region delete
        public bool DeleteBus(string licence)
        {
            int indexOfBus = DataSource.ListBus.FindIndex(b => b.Licence == licence);
            if (indexOfBus != -1 && DataSource.ListBus[indexOfBus].BusExist)
            {
                // bus.BusExsis = false;
                DataSource.ListBus[indexOfBus].BusExist = false;

                return true;
            }
            else
                throw new DO.WrongLicenceException(int.Parse(licence), "מספר הרישוי לא קיים במערכת");
        }
        #endregion

        #region update
        public bool UpdateBus(DO.Bus bus)
        {
            int indexOftheBUs = DataSource.ListBus.FindIndex(b => b.Licence == bus.Licence);
            if (indexOftheBUs != -1 && DataSource.ListBus[indexOftheBUs].BusExist)
            {
                DataSource.ListBus[indexOftheBUs] = bus;

            
                return true;
            }
            else
                throw new DO.WrongLicenceException(int.Parse(bus.Licence), "מספר הרישוי לא קיים במערכת");
        }
        #endregion



        #endregion Bus

        #region Line

        #region get
        public DO.Line GetLine(int idline)
        {
            DO.Line line = DataSource.ListLine.Find(l => l.IdNumber == idline);
            if (line != null && line.LineExist)
                return line.Clone();
            else
                throw new DO.WrongIDExeption(idline, " הקו לא קיים במערכת ");

        }
        public IEnumerable<DO.Line> GetAllLineBy(Predicate<DO.Line> linecondition)//return according to condition and just the working line
        {
            var list = from line in DataSource.ListLine
                       where (line.LineExist && linecondition(line))
                       select line.Clone();
            return list;
        }
        public IEnumerable<DO.Line> GetAllLine()//return all the lines in the list
        {
            return from line in DataSource.ListLine
                   where line.LineExist
                   select line.Clone();
        }
        #endregion

        #region add
        public int AddLine(DO.Line line)
        {
            //eliezer told us that we can do here checking because check according to idnumber is meaningless
            line.IdNumber = ++DS.Config.idLineCounter;

            DataSource.ListLine.Add(line.Clone());
            return line.IdNumber;

        }
        #endregion



        #region update
        public void UpdateLine(DO.Line line)
        {
            DO.Line tempLine = DataSource.ListLine.Find(b => b.IdNumber == line.IdNumber && b.LineExist == true);
            if (tempLine != null)
            {
                DataSource.ListLine.Remove(tempLine);
                DataSource.ListLine.Add(line.Clone());
            }
            else
                throw new DO.WrongIDExeption(line.IdNumber, " הקו לא קיין במערכת ");
        }
        #endregion

        #region delete
        public void DeleteLine(int idnumber)
        {

            DO.Line LineToDelete = DataSource.ListLine.Find(b => b.IdNumber == idnumber);
            LineToDelete.LineExist = false;

        }
        #endregion

        #endregion Line


        #region Stations

        #region get
        public DO.Stations GetStations(int code) //check if the Stations exsis according to the code
        {
            DO.Stations stations = DataSource.ListStations.Find(b => b.Code == code && b.StationExist);
            if (stations != null)
            {
                return stations.Clone();
            }
            else
                throw new DO.WrongIDExeption(code, $"{code} התחנה לא קיימת במערכת:");

        }
        public IEnumerable<DO.Stations> GetAllStations() //return all the stations that we have
        {
            return from station in DataSource.ListStations
                   where (station.StationExist)
                   select station.Clone();
        }

        public IEnumerable<DO.Stations> GetAllStationsBy(Predicate<DO.Stations> Stationscondition) //איך כותבים??
        {
            var list = from stations in DataSource.ListStations
                       where (stations.StationExist && Stationscondition(stations))
                       select stations.Clone();
            return list;
        }
        #endregion

        #region add
        public void AddStations(DO.Stations station)
        {
            if (DataSource.ListStations.FirstOrDefault(b => b.Code == station.Code) != null) //if != null its means that this licence is allready exsis
                throw new DO.WrongIDExeption(station.Code, "התחנה לא קיימת במערכת");/////////////////////////////////////////////////////////////////
            station.StationExist = true;
            DataSource.ListStations.Add(station.Clone());
        }
        #endregion

        #region delete
        public void DeleteStations(int code)
        {
            DO.Stations stations = DataSource.ListStations.Find(b => b.Code == code && b.StationExist);
            if (stations != null)
            {
                stations.StationExist = false;

            }
            else
                throw new DO.WrongIDExeption(code, "התחנה לא קיימת במערכת");//////////////////////////////////////////////////////
        }
        #endregion

        #region update
        public void UpdateStations(DO.Stations stations1, int OldCode)
        {
            DO.Stations station = DataSource.ListStations.Find(b => b.Code == OldCode && b.StationExist);
            if (station != null)
            {
                DataSource.ListStations.Remove(station);
                DataSource.ListStations.Add(stations1.Clone());
            }
            else
                throw new DO.WrongIDExeption(OldCode, "התחנה לא קיימת במערכת");///////////////////////////////////////////////////////
        }
        #endregion
        #endregion Stations

        #region LineStation
        #region get
        public DO.LineStation GetLineStation(int Scode, int idline) //return specific stations according to code of the station and line that Passing through it
        {
            DO.LineStation linestations = DataSource.ListLineStations.Find(b => b.StationCode == Scode && b.LineId == idline && b.LineStationExist == true);
            if (linestations != null)
            {
                return linestations.Clone();
            }
            else
                throw new DO.WrongIDExeption(Scode, $"{Scode} לא נמצאו פרטים במערכת על התחנה המבוקשת");/////////////////////////////////////////////////////////

        }


        public IEnumerable<DO.LineStation> GetAllLineStationsBy(Predicate<DO.LineStation> StationsLinecondition)
        {
            var list = from stations in DataSource.ListLineStations
                       where (stations.LineStationExist && StationsLinecondition(stations))
                       select stations.Clone();
            return list;
        }
        #endregion

        #region add
        public void AddLineStations(DO.LineStation station)
        {
            DO.LineStation temp = DataSource.ListLineStations.Find(b => b.StationCode == station.StationCode && b.LineId == station.LineId && b.LineStationExist);
            if (temp != null)
                throw new DO.WrongIDExeption(station.StationCode, "  מצטערים! קוד התחנה קיים כבר במערכת");/////////////////////////////////////////////////////////////////
            DataSource.ListLineStations.Add(station.Clone());
        }
        #endregion

        #region delete
        public int DeleteStationsFromLine(int Scode, int idline)//delete Station from line (spesific)
        {
            DO.LineStation stations = DataSource.ListLineStations.Find(b => b.StationCode == Scode && b.LineId == idline && b.LineStationExist);
            if (stations != null)
            {
                stations.LineStationExist = false;
                return stations.LineStationIndex;
            }
            else
                throw new DO.WrongIDExeption(Scode, "התחנה המבוקשת לא נמצאה במערכת");//////////////////////////////////////////////////////
        }
        public void DeleteStationsFromLine(int Scode) //if station delete- we need to delete all the Lineststion
        {
            foreach (DO.LineStation item in DataSource.ListLineStations)
            {
                if (item.StationCode == Scode)
                    item.LineStationExist = false;
            }

        }
        public void DeleteStationsOfLine(int idline) //delete StationLine according lineId
        {
            foreach (DO.LineStation item in DataSource.ListLineStations)
            {
                if (item.LineId == idline)
                    item.LineStationExist = false;
            }

        }

        #endregion

        #region update
        public void UpdateLineStations(DO.LineStation linestations) //when we delete station we need delete all the line station with the same code. we use at this func
        {
            DO.LineStation station = DataSource.ListLineStations.Find(b => b.StationCode == linestations.StationCode && b.LineId == linestations.LineId && b.LineStationExist);
            if (station != null)
            {
                DataSource.ListLineStations.Remove(station);
                DataSource.ListLineStations.Add(linestations.Clone());
            }
            else
                throw new DO.WrongIDExeption(linestations.StationCode, "מצטערים! לא נמצאו פרטים על התחנה המבוקשת");///////////////////////////////////////////////////////
        }
        public void UpdateLineStationsCode(DO.LineStation linestations, int oldCode) //if the maneger change the identity code of the station we needto get the old code to find the line station and update all of them 
        {
            DO.LineStation station = DataSource.ListLineStations.Find(b => b.StationCode == linestations.StationCode && b.LineId == linestations.LineId && b.LineStationExist);
            if (station != null)
            {
                linestations.StationCode = oldCode;
                DataSource.ListLineStations.Remove(station);
                DataSource.ListLineStations.Add(linestations.Clone());
            }
            else
                throw new DO.WrongIDExeption(linestations.StationCode, "לא קיים פרטים במערכת על התחנה המבוקשת או שאחד מהנתוונים שהכנסת שגוי");///////////////////////////////////////////////////////
        }
        #endregion

        #endregion LineStation

        #region LineTrip
        #region get
        public DO.LineTrip GetLineTrip(TimeSpan start, int idline) //return specific linetrip according to start time and id line 
        {
            DO.LineTrip linetrip = DataSource.ListLineTrip.Find(b => b.KeyId == idline && b.StartAt == start && b.TripLineExist == true);
            if (linetrip != null)
            {
                return linetrip.Clone();
            }
            else
                throw new DO.WrongLineTripExeption(idline, $"{start} לא נמצאו פרטים עבור הקו המבוקש בשעה זו");/////////////////////////////////////////////////////////

        }

        //public IEnumerable<DO.LineTrip> GetAllTripline(int idline) //return all the lineTrip of specific line
        //{
        //    return from linetrip in DataSource.ListLineTrip
        //           where (linetrip.KeyId == idline && linetrip.TripLineExist == true)
        //           select linetrip.Clone();
        //}


        public IEnumerable<DO.LineTrip> GetAllLineTripsBy(Predicate<DO.LineTrip> StationsLinecondition)
        {
            var list = from linetrip in DataSource.ListLineTrip
                       where (StationsLinecondition(linetrip) && linetrip.TripLineExist == true)
                       select linetrip.Clone();
            return list;
        }
        #endregion

        #region add
        public void AddLineTrip(DO.LineTrip lineTrip)
        {
            DataSource.ListLineTrip.Add(lineTrip.Clone());
        }
        #endregion

        #region delete
        public void DeleteLineTrip(int idline) //when we delete line we need to delete his line trip
        {
            foreach (var item in DataSource.ListLineTrip)
                if (item.KeyId == idline)
                    item.TripLineExist = false;
        }
        public void DeleteLineTrip1(DO.LineTrip lineTrip) //when we want to delete specific line trip from line 
        {
            int index = DataSource.ListLineTrip.FindIndex(b => b.KeyId == lineTrip.KeyId && b.StartAt == lineTrip.StartAt && b.FinishAt == lineTrip.FinishAt);
            DataSource.ListLineTrip[index].TripLineExist = false;

        }
        #endregion

        #region update
        public void UpdatelineTrip(DO.LineTrip lineTrip)
        {
            DO.LineTrip station = DataSource.ListLineTrip.Find(b => b.KeyId == lineTrip.KeyId && lineTrip.StartAt == b.StartAt && lineTrip.TripLineExist == true);
            if (station != null)
            {
                DataSource.ListLineTrip.Remove(station);
                DataSource.ListLineTrip.Add(lineTrip.Clone());
            }
            else
                throw new DO.WrongLineTripExeption(lineTrip.KeyId, "לא נמצאו זמני נסיעות עבור קו זה");///////////////////////////////////////////////////////
        }
        #endregion

        #endregion LineTrip

        #region AdjacentStations
        #region get
        public DO.AdjacentStations GetAdjacentStations(int Scode1, int Scode2) //return specific AdjacentStations
        {
            DO.AdjacentStations linestations = DataSource.ListAdjacentStations.Find(b => b.Station1 == Scode1 && b.Station2 == Scode2&&b.AdjacExsis);
            if (linestations != null)
            {
                return linestations.Clone();
            }
            else
                throw new DO.WrongIDExeption(Scode1, "לא נמצאו פרטים במערכת עבור זוג התחנות המבוקש");/////////////////////////////////////////////////////////

        }

      

        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> StationsLinecondition)
        {
            var list = from stations in DataSource.ListAdjacentStations
                       where (StationsLinecondition(stations)&&stations.AdjacExsis)
                       select stations.Clone();
            return list;
        }
        #endregion

        #region add
        public void AddLineStations(DO.AdjacentStations adjacentStations)
        {
            DO.AdjacentStations temp = DataSource.ListAdjacentStations.Find(b => b.Station1 == adjacentStations.Station1 && b.Station2 == adjacentStations.Station2 &&b.AdjacExsis);
            if (temp != null)
                throw new DO.WrongIDExeption(adjacentStations.Station1, " התחנה עוקבת כבר קיימת במערכת");/////////////////////////////////////////////////////////////////
            DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
        }
        #endregion

        #region delete

        public void DeleteAdjacentStationseBStation(int Scode1)// if we delete station we need to delete all the adjacte station with the station we delete
        {

            var v = from item in DataSource.ListAdjacentStations
                    where ((item.Station1 == Scode1 || item.Station2 == Scode1)&&item.AdjacExsis)
                    select item.Clone();
            foreach (DO.AdjacentStations item in v)
            {
                DataSource.ListAdjacentStations.Remove(item);
            }


        }
        #endregion

        #region update
        public void UpdateAdjacentStations(DO.AdjacentStations adjacentStations) //if the maneger change data and we need  update
        {
            DO.AdjacentStations station = DataSource.ListAdjacentStations.Find(b => b.Station1 == adjacentStations.Station1 && b.Station2 == adjacentStations.Station2&&b.AdjacExsis);
            if (station != null)
            {
                DataSource.ListAdjacentStations.Remove(station);
                DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
            }
            else
                throw new DO.WrongIDExeption(adjacentStations.Station1, "לא נמצאו פרטים עבור התחנה עוקבת המבוקשת");///////////////////////////////////////////////////////
        }
        public void UpdateAdjacentStations(int code1, int code2, int codeChange, int oldCode) //if we change the number identity of the station we need update the adjace station at the change
         // so we get here the old code on the from the station that change and the new code. In addition we get the 2 code of the station(prev one) to find the station to update   
        {
            DO.AdjacentStations station = DataSource.ListAdjacentStations.Find(b => b.Station1 == code1 && b.Station2 == code2&&b.AdjacExsis);
            if (station != null)
            {
                DataSource.ListAdjacentStations.Remove(station);
                if (station.Station1 == oldCode)
                    station.Station1 = codeChange;
                else
                    station.Station2 = codeChange;

                DataSource.ListAdjacentStations.Add(station);
            }
            else
                throw new DO.WrongIDExeption(code1, "לא נמצאו פרטים עבור התחנה עוקבת המבוקשת");///////////////////////////////////////////////////////
        }
        #endregion

        #endregion AdjacentStations

        #region User

        #region get
        public DO.User GetUser(string name) //check if the user exsis according to the name
        {
            DO.User user = DataSource.ListUsers.Find(b => b.UserName == name && b.UserExist);
            if (user != null)
            {
                return user.Clone();
            }
            else
                throw new DO.WrongNameExeption(name, $"{1}השם לא קיים במערכת או אחד מהפרטים שהזנת שגוי");/////////////////////////////////////////////////////////

        }
        public IEnumerable<DO.User> GetAlluser() //return all the user that we have
        {
            return from user in DataSource.ListUsers
                   where (user.UserExist)
                   select user.Clone();
        }


        public IEnumerable<DO.User> GetAlluserBy(Predicate<DO.User> userConditions) 
        {
            var users = from u in DataSource.ListUsers
                        where (u.UserExist && userConditions(u))
                        select u.Clone();
            return users;
        }
        public DO.User getUserBy(Predicate<DO.User> userConditions)
        {
            var users = from u in DataSource.ListUsers
                        where (u.UserExist && userConditions(u))
                        select u.Clone();
            if (users.Count() > 0)
                return users.ElementAt(0);
            else
                throw new DO.WrongNameExeption();
        }
        #endregion

        #region add
        public void AddUser(DO.User user)
        {
            if (DataSource.ListUsers.FirstOrDefault(b => b.UserName == user.UserName) != null) //if != null its means that this name is allready exsis
                throw new DO.WrongNameExeption(user.UserName, "שם משתמש כבר קיים במערכת, בבקשה הכנס שם אחר");/////////////////////////////////////////////////////////////////
            DataSource.ListUsers.Add(user.Clone());
        }
        #endregion

        #region delete
        public void DeleteUser(string name)
        {
            DO.User userDelete = DataSource.ListUsers.Find(b => b.UserName == name && b.UserExist);
            if (userDelete != null)
            {
                userDelete.UserExist = false;

            }
            else
                throw new DO.WrongNameExeption(name, "לא נמצאו פרטים במערכת עבור משתמש זה");//////////////////////////////////////////////////////
        }

        #endregion


        #region update
        public void UpdateUser(DO.User user)
        {
            DO.User u = DataSource.ListUsers.Find(b => b.UserName == user.UserName && b.UserExist);
            if (u != null)
            {
                DataSource.ListUsers.Remove(u);
                DataSource.ListUsers.Add(user.Clone());
            }
            else
                throw new DO.WrongNameExeption(user.UserName, "לא נמצאו פרטים במערכת עבור משתמש זה");///////////////////////////////////////////////////////
        }
        #endregion

        #endregion User




        #region Trip



        public DO.Trip GetTrip(int id) //check if the Trip exsis according to the id
        {
            DO.Trip trip = DataSource.ListTrip.Find(b => b.Id == id && b.TripExist);
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
                   where (trip.TripExist)
                   select trip.Clone();
        }
        public IEnumerable<DO.Trip> GetAllTripLine(int line) //return all the trip that we have in thid line
        {
            return from trip in DataSource.ListTrip
                   where (trip.TripExist && trip.LineId == line)
                   select trip.Clone();
        }

        public IEnumerable<DO.Trip> GetAllTripsBy(Predicate<DO.Trip> Tripcondition) //איך כותבים??
        {
            var list = from trip in DataSource.ListTrip
                       where (trip.TripExist && Tripcondition(trip))
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
            DO.Trip stations = DataSource.ListTrip.Find(b => b.Id == id && b.TripExist);
            if (stations != null)
            {
                stations.TripExist = false;

            }
            else
                throw new DO.WrongIDExeption(id, "ID not exsis");
        }

        public void UpdateStations(DO.Trip trip)
        {
            DO.Trip t = DataSource.ListTrip.Find(b => b.Id == trip.Id && b.TripExist);
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
