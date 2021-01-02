using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlAPI;
using DALAPI;
//using DL;
using BO;
using System.Device.Location;

namespace BL
{
    class BlImp : IBL
    {
        private IDAL dl = DalFactory.GetDL();

        #region Bus


        BO.Bus busDoBoAdapter(string licence_) // return the bus from dl according to licence
        {
            string licence = (licence_).Replace("-", "");
            bool okey = checkLicence(licence);
            BO.Bus busBO = new BO.Bus();
            DO.Bus busDO;
            try //get the list from DL
            {
                busDO = dl.GetBus(licence);
            }
            catch (DO.WrongLicenceException ex)
            {
                throw new BO.BadBusLicenceException("Licence is illegal", ex);
            }

            busDO.CopyPropertiesTo(busBO);
            // CopyToBo(busBO, busDO);

            string firstpart, middlepart, endpart, result;
            if (licence_.Length == 7)
            {
                // xx-xxx-xx
                firstpart = licence.Substring(0, 2);
                middlepart = licence.Substring(2, 3);
                endpart = licence.Substring(5, 2);
                result = String.Format("{0}-{1}-{2}", firstpart, middlepart, endpart);
            }
            else
            {
                // xxx-xx-xxx
                firstpart = licence_.Substring(0, 3);
                middlepart = licence_.Substring(3, 2);
                endpart = licence_.Substring(5, 3);
                result = String.Format("{0}-{1}-{2}", firstpart, middlepart, endpart);
            }

            busBO.Licence = result;
            return busBO;

        }

        public IEnumerable<BO.Bus> GetAllBus() //return all the buses that working 
        {
            return from item in dl.GetAllBuses()
                   select busDoBoAdapter(item.Licence);

        }
        //public IEnumerable<BO.Bus> GetBusByline(int licence) //return all the bus according to predicate
        //{
        //    bool okey = checkLicence(licence);
        //    return from item in dl.GetAllBusesBy(x => x.Licence == licence)
        //           select busDoBoAdapter(item.Licence);
        //}
        public IEnumerable<BO.Bus> GetBusByStatus(BO.STUTUS stutus) //return all the bus according to their stutus
        {
            return from item in dl.GetAllBusesStusus((DO.STUTUS)stutus)
                   select busDoBoAdapter(item.Licence);
        }

        public int AddBus(BO.Bus bus)
        {
            bool okey = checkLicence(bus); //if the licence not goot this func will throw exeption

            DO.Bus busDO = new DO.Bus();
            bus.BusExsis = true;
            bus.StatusBus = STUTUS.READT_TO_TRAVEL;
            string te = bus.Licence.Replace("-", "");
            busDO.Licence = te;
            //CopyToBo(bus, busDO);

            try
            {
                dl.AddBus(busDO);
            }
            catch (DO.WrongLicenceException ex)
            {
                throw new BO.BadBusLicenceException("Licence not valid", ex);

            }
            return 1;
        }
        public bool DeleteBus(string licence_) //delete bus according to his licence
        {
            string licence = licence_.Replace("-", "");
            bool okey = checkLicence(licence);
            try
            {
                dl.DeleteBus(licence);
            }
            catch (DO.WrongLicenceException ex)
            {
                throw new BO.BadBusLicenceException("Licence not valid", ex);

            }
            return true;
        }
        public BO.Bus UpdateBus(BO.Bus bus) //update the bus at the DS
        {
            string te = bus.Licence.Replace("-", "");

            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO); //go to copy the varieble to be DO
            busDO.Licence = te;
            try
            {
                dl.UpdateBus(busDO);
            }
            catch (DO.WrongLicenceException ex)
            {
                throw new BO.BadBusLicenceException("Licence not valid", ex);
            }
            IEnumerable<DO.Bus> temp = dl.GetAllBuses();
            return bus;
        }

        bool checkLicence(BO.Bus bus) //check if the new licence that the passenger enter is valid
        {
            string licence = bus.Licence.Replace("-", "");

            if ((bus.StartingDate.Year < 2018 && licence.Length == 7) || (bus.StartingDate.Year >= 2018 && licence.Length == 8))
            {
                return true;
            }
            else
            {
                if (bus.StartingDate.Year >= 2018)
                    throw new BO.BadBusLicenceException("The new licence is not valid,\n please enter again number licence with 8 digite", Convert.ToInt32(bus.Licence));
                else
                    throw new BO.BadBusLicenceException("The new licence is not valid,\n please enter again number licence with 7 digite", Convert.ToInt32(bus.Licence));
            }
        }

        bool checkLicence(string licences) //check if the licence number is valid
        {
            string licence = licences.Replace("-", "");
            if (licence.Length == 7 || licence.Length == 8)
                return true;
            else
                throw new BO.BadBusLicenceException("The new licence is not valid,\n please enter again number licence with 8 or 7 digite", Convert.ToInt32(licence));
        }




        public BO.Bus Refuelling(BO.Bus bus) //update the new fuel
        {
            //  worker.RunWorkerAsync(12);

            bus.StatusBus = (STUTUS)2;
            bus.FuellAmount = 1200;

            UpdateBus(bus);
            return bus;
        }

        public BO.Bus treatment(BO.Bus bus)/// func that do treatment to the bus
        {

            //   worker.RunWorkerAsync(144);//one day

            bus.StatusBus = (STUTUS)3;
            bus.LastTreatment = DateTime.Today;

            //   strLastTreat = String.Format("{0}/{1}/{2}", this.lastTreat.Day, this.lastTreat.Month, this.lastTreat.Year);

            bus.KilometrFromLastTreat = 0;
            if (bus.FuellAmount <= 1200)
            {
                bus.FuellAmount = 1200;
            }
            UpdateBus(bus);

            return bus;
        }



        #endregion

        #region Line
        BO.Line lineDoBoAdapter(int idLine) // return the line from dl according to licence
        {
            BO.Line lineBO = new BO.Line();
            DO.Line lineDO;
            IEnumerable<DO.LineStation> tempDO;
            IEnumerable<DO.LineTrip> tripDO;
            try
            {
                lineDO = dl.GetLine(idLine);
                tempDO = dl.GetAllStationsLine(idLine);
                tripDO = dl.GetAllTripline(idLine);
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("ID not valid", ex);
            }

            lineBO.TimeTravel = 0;
            lineDO.CopyPropertiesTo(lineBO); //go to a deep copy. all field is copied to a same field at bo.

            lineBO.StationsOfBus = from st in tempDO
                                   select (BO.LineStation)st.CopyPropertiesToNew(typeof(BO.LineStation));
            lineBO.TimeLineTrip = from st in tripDO
                                  select (BO.LineTrip)st.CopyPropertiesToNew(typeof(BO.LineTrip));

            return lineBO;
        }
        public IEnumerable<BO.Line> GetAllLine() //return all the lines that working 
        {
            var v = from item in dl.GetAllLine()
                    select lineDoBoAdapter(item.IdNumber);
            foreach (var temp in v)
            {

                //temp.StationsOfBus = from st in dl.GetAllStationsLine(temp.IdNumber)
                //                     select (BO.LineStation)temp.CopyPropertiesToNew(typeof(BO.LineStation));

                temp.StationsOfBus = from st in dl.GetAllStationsLine(temp.IdNumber)
                                     orderby st.LineStationIndex
                                     select (BO.LineStation)st.CopyPropertiesToNew(typeof(BO.LineStation));



                temp.TimeLineTrip = from st in dl.GetAllTripline(temp.IdNumber)
                                    select (BO.LineTrip)st.CopyPropertiesToNew(typeof(BO.LineTrip));


                temp.TimeTravel = CalucateTravel(temp.IdNumber);
            }

            return v;

        }
        public IEnumerable<BO.Line> GetLineBy(int stationCode) //return all the lines according to predicate
        {
            var v = from item in dl.GetAllLineBy(x => x.FirstStationCode == stationCode)
                    select lineDoBoAdapter(item.IdNumber);
            foreach (var temp in v)
            {

                temp.StationsOfBus = from st in dl.GetAllStationsLine(temp.IdNumber)
                                     orderby st.LineStationIndex
                                     select (BO.LineStation)st.CopyPropertiesToNew(typeof(BO.LineStation));

                temp.TimeLineTrip = from st in dl.GetAllTripline(temp.IdNumber)
                                    select (BO.LineTrip)st.CopyPropertiesToNew(typeof(BO.LineTrip));

                temp.TimeTravel = CalucateTravel(temp.IdNumber);
            }
            return v;
        }
        public IEnumerable<BO.Line> GetLineByArea(BO.AREA area) //return all the line according to their area
        {
            var v = from item in dl.GetAllLinesArea((DO.AREA)area)
                    select lineDoBoAdapter(item.IdNumber);
            foreach (var temp in v)
            {

                temp.StationsOfBus = from st in dl.GetAllStationsLine(temp.IdNumber)
                                     orderby st.LineStationIndex
                                     select (BO.LineStation)st.CopyPropertiesToNew(typeof(BO.LineStation));


                temp.TimeLineTrip = from st in dl.GetAllTripline(temp.IdNumber)
                                    select (BO.LineTrip)st.CopyPropertiesToNew(typeof(BO.LineTrip));

                temp.TimeTravel = CalucateTravel(temp.IdNumber);
            }
            return v;
        }

        public void AddLine(BO.Line line)
        {
            DO.Line lineDO = new DO.Line();
            //do the bus to be DO
            line.CopyPropertiesTo(lineDO);

            IEnumerable<DO.LineStation> tempDO;

            tempDO = from st in line.StationsOfBus    ///לא בטוח נכונה
                     select (DO.LineStation)st.CopyPropertiesToNew(typeof(DO.LineStation));

            IEnumerable<DO.LineStation> tempDO1;

            tempDO1 = from item in tempDO
                      select new DO.LineStation
                      {
                          LineId = item.LineId,
                          LineStationExsis = item.LineStationExsis,
                          LineStationIndex = item.LineStationIndex,
                          NextStation = item.NextStation,
                          PrevStation = item.PrevStation,
                          StationCode = item.StationCode
                      };
            //tempDO1 = from st in line.StationsOfBus    ///לא בטוח נכונה
            //         select (DO.LineStation)st.CopyPropertiesToNew(typeof(DO.LineStation));


            IEnumerable<DO.LineTrip> tripDO;
            tripDO = from st in line.TimeLineTrip    ///לא בטוח נכונה
                     select (DO.LineTrip)st.CopyPropertiesToNew(typeof(DO.LineTrip));


            DO.LineStation l1 = new DO.LineStation();
            DO.LineStation l2 = new DO.LineStation();
            try
            {
                int id = dl.AddLine(lineDO);
                foreach (var item in tempDO)        ///////////////////////////לשאול את אליעזר איך כותבים את זה עם ביטוי למדה או לינק
                {
                    item.LineId = id;
                    try //in case that the we have to same station in mistake.
                    {
                        dl.AddLineStations(item);
                    }
                    catch (DO.WrongIDExeption ex) { string a = ""; a += ex; }

                }
                foreach (var item in tripDO)        ///////////////////////////לשאול את אליעזר איך כותבים את זה עם ביטוי למדה או לינק
                {
                    item.KeyId = id;
                    try
                    {
                        dl.AddLineTrip(item);
                    }
                    catch (DO.WrongIDExeption ex) { string a = ""; a += ex; }

                }
                //sorted the line station according to their index.
                tempDO1 = from item in tempDO          ///////////////לוודא שעובד
                          orderby item.LineStationIndex
                          select item;
                for (int i = 0; i < tempDO.Count(); i++) //move on the line station list send 2 adj station to creat if they not exsis yet.
                {
                    //if we have this both station at list adj station so we have throw. we catch the throw here in order tocontinue at the for.
                    try
                    {
                        l1 = tempDO1.ElementAt(i);
                        i++;
                        l2 = tempDO1.ElementAt(i);
                        i--;
                        CreatAdjStations(l1.StationCode, l2.StationCode);
                    }
                    catch (DO.WrongIDExeption ex) { string a = ""; a += ex; }
                }
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("ID not valid", ex);
            }
        }

        public void AddStationLine(BO.LineStation station) //we add station to the bus travel
        {
            int index = station.LineStationIndex;
            DO.Line lineDO = new DO.Line();
            BO.Line lineBO = new BO.Line();
            IEnumerable<DO.LineStation> tempDO;
            IEnumerable<DO.LineTrip> tripDO;
            int adj1 = -1, adj2 = -1;

            try
            {
                //creat BO line

                lineDO = dl.GetLine(station.LineId);  //if the bus not exsis we will have exeption from DL
                tempDO = dl.GetAllStationsLine(station.LineId);
                tripDO = dl.GetAllTripline(station.LineId);

                lineBO.StationsOfBus = from st in tempDO
                                       select (BO.LineStation)st.CopyPropertiesToNew(typeof(BO.LineStation));

                lineBO.TimeLineTrip = from st in tripDO
                                      select (BO.LineTrip)st.CopyPropertiesToNew(typeof(BO.LineTrip));
                lineBO.CopyPropertiesTo(lineDO);

                for (int i = 0; i < lineBO.StationsOfBus.Count(); i++)
                {
                    try
                    {
                        // check if we need to delete the adjacted station after we add between them another station
                        //find the require 2 stations

                        if (lineBO.StationsOfBus.ElementAt(i).LineStationIndex == (index - 1))
                            adj1 = lineBO.StationsOfBus.ElementAt(i).StationCode;
                        if (lineBO.StationsOfBus.ElementAt(i).LineStationIndex == index)
                            adj2 = lineBO.StationsOfBus.ElementAt(i).StationCode;
                        //if we find them so check if they adjacted station for another bus. if not-delete
                        if (adj1 != -1 && adj2 != -1)
                            if (dl.GetAllLineAt2Stations(adj1, adj2).Count() == 1)
                                dl.DeleteAdjacentStationse(adj1, adj2);

                        // creat a new adj station if they not exsis yet
                        if (lineBO.StationsOfBus.ElementAt(i).LineStationIndex == (index - 1) || lineBO.StationsOfBus.ElementAt(i).LineStationIndex == (index + 1))
                        {
                            CreatAdjStations(lineBO.StationsOfBus.ElementAt(i).StationCode, station.StationCode);
                        }

                        //in order to update the station index at line travel
                        if (lineBO.StationsOfBus.ElementAt(i).LineStationIndex >= index)
                            lineBO.StationsOfBus.ElementAt(i).LineStationIndex++;


                    }
                    catch (DO.WrongIDExeption ex) { string a = ""; a += ex; }

                }


            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("ID not valid", ex);
            }


        }

        public void DeleteLine(int idLine)
        {
            try
            {
                dl.DeleteLine(idLine);
                dl.DeleteLineTrip(idLine);
                dl.DeleteStationsOfLine(idLine);
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("ID not valid", ex);
            }
        }
        public void DeleteStation(int idline, int code) //delete station from the line travel
        {
            try
            {
                int index = dl.DeleteStationsFromLine(code, idline);

                IEnumerable<DO.LineStation> tempDO;
                tempDO = dl.GetAllStationsLine(idline);
                int adj1 = -1, adj2 = -1;

                foreach (var item in tempDO)
                {
                    if (item.LineStationIndex == (index - 1))
                        adj1 = item.StationCode;
                    if (item.LineStationIndex == (index + 1))
                        adj2 = item.StationCode;

                    if (adj1 != -1 && adj2 != -1)
                    {
                        try
                        {
                            if (dl.GetAllLineAt2Stations(adj1, code).Count() == 1)
                                dl.DeleteAdjacentStationse(adj1, code);
                            if (dl.GetAllLineAt2Stations(code, adj2).Count() == 1)
                                dl.DeleteAdjacentStationse(code, adj2);

                            CreatAdjStations(adj1, adj2);
                        }
                        catch (DO.WrongIDExeption ex) { string a = ""; a += ex; }


                    }

                    if (item.LineStationIndex > index)
                        item.LineStationIndex--;

                }
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("ID not valid", ex);
            }


        }


        public bool UpdateLine(BO.Line line)
        {
            DO.Line lineDO = new DO.Line();
            line.CopyPropertiesTo(lineDO);

            IEnumerable<DO.LineStation> tempDO;
            tempDO = from st in line.StationsOfBus
                     select (DO.LineStation)st.CopyPropertiesToNew(typeof(DO.LineStation));

            IEnumerable<DO.LineTrip> tripDO;
            tripDO = from st in line.TimeLineTrip
                     select (DO.LineTrip)st.CopyPropertiesToNew(typeof(DO.LineTrip));


            IEnumerable<DO.LineStation> tempDO1;
            IEnumerable<DO.LineStation> tempDO2;

            try
            {
                dl.UpdateLine(lineDO);

                //for add update on line stations
                tempDO1 = from item in dl.GetAllStationsLine(line.IdNumber) //the oldest line station
                          orderby item.LineStationIndex
                          select item;
                tempDO2 = from item in tempDO //the new line station
                          orderby item.LineStationIndex
                          select item;
                for (int i = 0; i < tempDO.Count(); i++)
                {
                    if (tempDO1.ElementAt(i).StationCode != tempDO2.ElementAt(i).StationCode)
                    {
                        if (i == 0)
                        {
                            tempDO2.ElementAt(i).PrevStation = 0;
                            tempDO2.ElementAt(i).NextStation = tempDO2.ElementAt(i + 1).StationCode;
                            dl.UpdateStations(tempDO2.ElementAt(i));
                        }
                        if (i == tempDO.Count() - 1)
                        {
                            tempDO2.ElementAt(i).PrevStation = tempDO2.ElementAt(i - 1).StationCode;
                            tempDO2.ElementAt(i).NextStation = 0;
                            dl.UpdateStations(tempDO2.ElementAt(i));
                        }
                        else
                        {
                            tempDO2.ElementAt(i).PrevStation = tempDO2.ElementAt(i - 1).StationCode;
                            tempDO2.ElementAt(i).NextStation = tempDO2.ElementAt(i + 1).StationCode;
                            dl.UpdateStations(tempDO2.ElementAt(i));

                        }

                    }

                }

                //for update the line trip

                for (int i = 1; 0 < tripDO.Count(); i++)
                {
                    AddOneTripLine(tripDO.ElementAt(i));
                }

                line.TimeTravel = CalucateTravel(line.IdNumber);
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("ID not valid", ex);
            }

            return true;
        }

        public void AddOneTripLine(DO.LineTrip line) //func that get new lineTrip and update the list at DS
        {

            DO.LineTrip lineTrip = new DO.LineTrip();
            DO.LineTrip temp = new DO.LineTrip();

            IEnumerable<DO.LineTrip> tripDO1;
            tripDO1 = from item in dl.GetAllTripline(line.KeyId) //the oldest line trip
                      orderby item.StartAt
                      select item;
            for (int i = 0; 0 < tripDO1.Count(); i++)
            {
                temp = tripDO1.ElementAt(i);
                if (temp.StartAt <= line.StartAt && temp.FinishAt > line.StartAt)
                {
                    lineTrip.StartAt = tripDO1.ElementAt(i).StartAt;
                    lineTrip.TripLineExsis = true;
                    lineTrip.KeyId = tripDO1.ElementAt(i).KeyId;
                    lineTrip.Frequency = tripDO1.ElementAt(i).Frequency;
                    lineTrip.FinishAt = line.StartAt;

                    dl.UpdatelineTrip(lineTrip);

                    dl.DeleteLineTrip1(temp);
                    dl.AddLineTrip(lineTrip);
                }
                if (temp.FinishAt > line.FinishAt)
                {
                    lineTrip.StartAt = line.FinishAt;
                    lineTrip.TripLineExsis = true;
                    lineTrip.KeyId = tripDO1.ElementAt(i).KeyId;
                    lineTrip.Frequency = tripDO1.ElementAt(i).Frequency;
                    lineTrip.FinishAt = tripDO1.ElementAt(i).FinishAt;

                    dl.DeleteLineTrip1(temp);
                    dl.AddLineTrip(lineTrip);

                    break;

                }
                if (temp.StartAt >= line.StartAt && temp.FinishAt <= line.FinishAt)
                    dl.DeleteLineTrip1(tripDO1.ElementAt(i));
            }

        }

        public void CreatAdjStations(int station1, int station2)
        {
            double speed = 13.89;//m/s= 50 km/h
            DO.AdjacentStations adjacent = new DO.AdjacentStations();
            adjacent.Station1 = station1;
            adjacent.Station2 = station2;

            DO.Stations ST1 = new DO.Stations();
            DO.Stations ST2 = new DO.Stations();
            try
            {
                // in order to freat adj station we need the "real" station in order to calucate distance and travel time.
                ST1 = dl.GetStations(station1);
                ST2 = dl.GetStations(station2);
                double d = (ST1.Coordinate).GetDistanceTo((ST2.Coordinate));
                adjacent.Distance = d;

                adjacent.TimeAverage = TimeSpan.FromSeconds((1.5 * d) / speed);
                dl.AddLineStations(adjacent);

            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("ID not valid", ex);
            }

        }


        public double CalucateTravel(int lineId)
        {
            IEnumerable<DO.LineStation> tempDO;
            DO.AdjacentStations adj = new DO.AdjacentStations();
            double sum = 0;
            tempDO = dl.GetAllStationsLine(lineId);
            var v = from item in tempDO
                    orderby item.LineStationIndex
                    select item;
            for (int i = 0; i < (tempDO.Count() - 1); i++)
            {
                adj = dl.GetAdjacentStations(v.ElementAt(i).StationCode, v.ElementAt((i + 1)).StationCode);
                sum += adj.TimeAverage.TotalMinutes;
            }

            return sum;


        }

        public IEnumerable<object> DetailsOfStation(IEnumerable<LineStation> lineStations)
        {
            return from itemStation in dl.GetAllStations()
                   from itemLineStation in lineStations
                   where itemStation.Code == itemLineStation.StationCode
                   select new
                   {
                       StationCode = itemLineStation.StationCode,
                       LineStationIndex = itemLineStation.LineStationIndex,
                       LineStationExsis = itemLineStation.LineStationExsis,
                       Name = itemStation.Name,
                       Address = itemStation.Address,
                       Coordinate = itemStation.Coordinate,
                   };

        }

        #endregion


        #region Station

        BO.Station stationDoBoAdapter(int code) // return the station from dl according to code
        {
            BO.Station stationBO = new BO.Station();
            DO.Stations stationDO;
            IEnumerable<DO.LineStation> tempDO;

            try
            {
                stationDO = dl.GetStations(code);
                tempDO = dl.GetAllStationsCode(code);
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("ID not valid", ex);
            }


            stationDO.CopyPropertiesTo(stationBO); //go to a deep copy. all field is copied to a same field at bo.

            stationBO.LineAtStation = from st in tempDO
                                      select (BO.LineStation)st.CopyPropertiesToNew(typeof(BO.LineStation));


            return stationBO;
        }



        public IEnumerable<BO.Station> GetAllStations()
        {
            var v = from item in dl.GetAllStations()
                    select stationDoBoAdapter(item.Code);
            foreach (var temp in v)
            {
                temp.LineAtStation = from st in dl.GetAllStationsCode(temp.Code)
                                     select (BO.LineStation)st.CopyPropertiesToNew(typeof(BO.LineStation));
            }
            return v;

        }

        public void AddLine(BO.Station station)///////////////////
        {
            DO.Stations stationDO = new DO.Stations();
            DO.LineStation lineStationDO;
            station.CopyPropertiesTo(stationDO);

            dl.AddStations(stationDO);



        }



        #endregion
    }
}
