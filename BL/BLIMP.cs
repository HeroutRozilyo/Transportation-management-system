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
        static Random random = new Random(DateTime.Now.Millisecond);
        private IDAL dl = DalFactory.GetDL();
        #region singelton
        static readonly BlImp instance = new BlImp();
        static BlImp() { }
        public static BlImp Instance { get => instance; }

        #endregion

        #region Bus

        /// <summary>
        ///  return the busBO according to licence
        /// </summary>
        /// <param name="licence_"=id number>
        /// <returns></returns>
        BO.Bus busDoBoAdapter(string licence_)
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
                throw new BO.BadBusLicenceException("מספר רישוי לא חוקי", ex);
            }

            busDO.CopyPropertiesTo(busBO);
            busBO.BusExsis = true;


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

        //return all the buses that working 
        public IEnumerable<BO.Bus> GetAllBus()
        {
            var v = from item in dl.GetAllBuses()
                    select busDoBoAdapter(item.Licence);
            return v;
        }

        //return all the bus according to their stutus
        public IEnumerable<BO.Bus> GetBusByStatus(BO.STUTUS stutus)
        {
            return from item in dl.GetAllBusesStusus((DO.STUTUS)stutus)
                   select busDoBoAdapter(item.Licence);
        }

        //if the licence not goot this func will throw exeption
        public int AddBus(BO.Bus bus)
        {
            bool okey = checkLicence(bus);

            DO.Bus busDO = new DO.Bus();
            bus.BusExsis = true;
            bus.StatusBus = STUTUS.READT_TO_TRAVEL;
            string te = bus.Licence.Replace("-", "");
            busDO.Licence = te;
            busDO = (DO.Bus)bus.CopyPropertiesToNew(typeof(DO.Bus));
            busDO.BusExist = true;
            try
            {
                dl.AddBus(busDO);
            }
            catch (DO.WrongLicenceException ex)
            {
                throw new BO.BadBusLicenceException("מספר רישוי לא חוקי או שקיים כבר במערכת", ex);

            }
            return 1;
        }

        //delete bus according to his licence
        public bool DeleteBus(string licence_)
        {
            string licence = licence_.Replace("-", "");
            bool okey = checkLicence(licence);
            try
            {
                dl.DeleteBus(licence);
            }
            catch (DO.WrongLicenceException ex)
            {
                throw new BO.BadBusLicenceException("מספר הרישוי המבוקש לא נמצא במערכת", ex);

            }
            return true;
        }

        //updat bus
        public BO.Bus UpdateBus(BO.Bus bus)
        {
            string te = bus.Licence.Replace("-", "");

            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO); //go to copy the varieble to be DO
            busDO.Licence = te;
            try
            {
                busDO.BusExist = true;
                dl.UpdateBus(busDO);
            }
            catch (DO.WrongLicenceException ex)
            {
                throw new BO.BadBusLicenceException("מספר רישוי לא חוקי או שלא נמצאו פרטים במערכת עבורו", ex);
            }
            IEnumerable<DO.Bus> temp = dl.GetAllBuses();
            return bus;
        }

        //check if the new licence that the passenger enter is valid
        bool checkLicence(BO.Bus bus)
        {
            string licence = bus.Licence.Replace("-", "");

            if ((bus.StartingDate.Year < 2018 && licence.Length == 7) || (bus.StartingDate.Year >= 2018 && licence.Length == 8))
            {
                return true;
            }
            else
            {
                if (bus.StartingDate.Year >= 2018)
                    throw new BO.BadBusLicenceException("מספר הרישוי שהוכנס אינו חוקי, בבקשה אכנס מספר רישוי בעל 8 ספרות", Convert.ToInt32(bus.Licence));
                else
                    throw new BO.BadBusLicenceException("מספר הרישוי שהוכנס אינו חוקי, בבקשה אכנס מספר רישוי חדש עם 7 ספרות", Convert.ToInt32(bus.Licence));
            }
        }

        //check if the licence number is valid
        bool checkLicence(string licences)
        {
            string licence = licences.Replace("-", "");
            if (licence.Length == 7 || licence.Length == 8)
                return true;
            else
                throw new BO.BadBusLicenceException("מספר הרישוי שהוכנס אינו חוקי, בבקשה אכנס מספר רישוי בעל 7 או 8 ספרות בהתאם לשנת הייצור", Convert.ToInt32(licence));
        }



        //update the new fuel
        public BO.Bus Refuelling(BO.Bus bus)
        {
            //  worker.RunWorkerAsync(12);

            bus.StatusBus = (STUTUS)2;
            bus.FuellAmount = 1200;

            UpdateBus(bus);
            return bus;
        }

        /// func that do treatment to the bus
        public BO.Bus treatment(BO.Bus bus)
        {



            bus.StatusBus = (STUTUS)3;
            bus.LastTreatment = DateTime.Today;



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
        /// <summary>
        ///  return the line according his id number
        /// </summary>
        /// <identity number="idLine"></>
        /// <returns></returns>
        BO.Line lineDoBoAdapter(int idLine)
        {
            BO.Line lineBO = new BO.Line();
            DO.Line lineDO;
            IEnumerable<DO.LineStation> tempDO;
            IEnumerable<DO.LineStation> tempDO1;
            IEnumerable<DO.LineTrip> tripDO;
            DO.AdjacentStations adj = new DO.AdjacentStations();
            try
            {//
                lineDO = dl.GetLine(idLine);
                tempDO = dl.GetAllStationsLine(idLine);
                tripDO = dl.GetAllTripline(idLine);
            }
            catch (DO.WrongIDExeption ex)
            {
               throw new BO.BadIdException("מזהה קו לא תקין", ex);            
            }

            lineBO.TimeTravel = 0;
            lineDO.CopyPropertiesTo(lineBO); //go to a deep copy. all field is copied to a same field at bo.

            tempDO1 = from st in tempDO     //order the list of line station from DO
                      orderby st.LineStationIndex
                      select st;

            //creat BO line station. first we insert the data t list ant calucate the time and distane ans after make to ienumerable

            List<LineStation> lineStations = new List<LineStation>();
            LineStation line = new LineStation();
            int code1, code2 = 0;
            bool timtum = false;
            for (int i = 0; i < tempDO.Count() - 1; i++)
            {
                timtum = true;
                code1 = tempDO1.ElementAt(i).StationCode;
                code2 = tempDO1.ElementAt(i + 1).StationCode;
                adj = dl.GetAdjacentStations(code1, code2);

                line = new LineStation()
                {
                    LineId = tempDO1.ElementAt(i).LineId,
                    LineStationIndex = tempDO1.ElementAt(i).LineStationIndex,
                    LineStationExist = tempDO1.ElementAt(i).LineStationExist,
                    NextStation = tempDO1.ElementAt(i).NextStation,
                    PrevStation = tempDO1.ElementAt(i).PrevStation,
                    StationCode = tempDO1.ElementAt(i).StationCode,
                    DistanceFromNext = adj.Distance,
                    TimeAverageFromNext = Convert.ToDouble(adj.TimeAverage),
                };
                lineStations.Add(line);

            }
            if (timtum)
            {
                line = new LineStation()  //restart the last linebus
                {
                    LineId = tempDO1.ElementAt(tempDO.Count() - 1).LineId,
                    LineStationIndex = tempDO1.ElementAt(tempDO.Count() - 1).LineStationIndex,
                    LineStationExist = tempDO1.ElementAt(tempDO.Count() - 1).LineStationExist,
                    NextStation = tempDO1.ElementAt(tempDO.Count() - 1).NextStation,
                    PrevStation = tempDO1.ElementAt(tempDO.Count() - 1).PrevStation,
                    StationCode = tempDO1.ElementAt(tempDO.Count() - 1).StationCode,
                    DistanceFromNext = 0,
                    TimeAverageFromNext = 0,
                };
                lineStations.Add(line);
            }
            lineBO.StationsOfBus = lineStations.AsEnumerable();


            lineBO.TimeLineTrip = from st in tripDO
                                  select (BO.LineTrip)st.CopyPropertiesToNew(typeof(BO.LineTrip));
            lineBO.TimeTravel = CalucateTravel(idLine);

            return lineBO;
        }


        #region GetLine
        //return all the lines that working 
        public IEnumerable<BO.Line> GetAllLine()
        {
            var v = from item in dl.GetAllLine()
                    select lineDoBoAdapter(item.IdNumber);

            return v;

        }
        //return all the lines according to predicate
        public IEnumerable<BO.Line> GetLineBy(int stationCode)
        {
            var v = from item in dl.GetAllLineBy(x => x.FirstStationCode == stationCode)
                    select lineDoBoAdapter(item.IdNumber);

            return v;
        }

        //return all the lines according to predicate
        public IEnumerable<BO.Line> GetLineByLineCode(int LineCodeCode)
        {
            var v = from item in dl.GetAllLineBy(x => x.NumberLine == LineCodeCode)
                    select lineDoBoAdapter(item.IdNumber);

            return v;
        }
        public IEnumerable<IGrouping<BO.AREA, BO.Line>> GetLinesByAreaG()
        {
            var list = from line in GetAllLine()
                       group line by line.Area into g
                       select g;
            return list;
        }


        //return all the lines according to predicate
        public BO.Line GetLineByLine(int lineid)
        {
            Line line = lineDoBoAdapter(lineid);

            return line;
        }
        //return all the line according to their area
        public IEnumerable<BO.Line> GetLineByArea(BO.AREA area)
        {

            IEnumerable<BO.Line> help;
            help = from item in dl.GetAllLinesArea((DO.AREA)area)
                   select lineDoBoAdapter(item.IdNumber);

            return help;
        }

        /// <summary>
        /// in UI we want to insert data on station that have more fields so we creat object to return
        /// </summary>
        /// <ienumareble with list of linestation from UI="lineStations"></param>
        /// <returns></returns>
        public IEnumerable<object> DetailsOfStation(IEnumerable<LineStation> lineStations)
        {
            var v = from itemStation in dl.GetAllStations()
                    from itemLineStation in lineStations
                    where itemStation.Code == itemLineStation.StationCode
                    select new
                    {
                        StationCode = itemLineStation.StationCode,
                        LineStationIndex = itemLineStation.LineStationIndex,
                        LineStationExist = itemLineStation.LineStationExist,
                        Name = itemStation.Name,
                        Address = itemStation.Address,
                        //Coordinate = itemStation.Coordinate,
                    };
            return from item in v
                   orderby item.LineStationIndex
                   select item;

        }

        #endregion

        #region Add
        public int AddLine(BO.Line line)

        {
            DO.Line lineDO = new DO.Line();
            line.CopyPropertiesTo(lineDO);//do the bus to be DO

            List<BO.LineStation> adja = new List<LineStation>();

            //tempDO=line station
            IEnumerable<DO.LineStation> tempDO;
            tempDO = from st in line.StationsOfBus
                     select (DO.LineStation)st.CopyPropertiesToNew(typeof(DO.LineStation));


            DO.LineStation l1 = new DO.LineStation();
            DO.LineStation l2 = new DO.LineStation();
            int id = 0;
            try
            {
                id = dl.AddLine(lineDO);
                foreach (var item in tempDO)
                {
                    item.LineId = id;
                    try //in case that the we have 2 same station in mistake.
                    {
                        dl.AddLineStations(item);
                    }
                    catch (DO.WrongIDExeption ex) { string a = ""; a += ex; }

                }


                //sorted the line station according to their index.
                IEnumerable<DO.LineStation> tempDO1;
                tempDO1 = from item in dl.GetAllStationsLine(id)
                          orderby item.LineStationIndex
                          select item;

                bool exsit = true;
                for (int i = 0; i < tempDO1.Count() - 1; i++) //move on the line station list send 2 adj station to creat if they not exsis yet.
                {
                    try //if return false its mean that we need to add this adjact station to our DS.
                    {
                        exsit = CreatAdjStations(tempDO1.ElementAt(i).StationCode, tempDO1.ElementAt(i + 1).StationCode);

                    }
                    catch (DO.WrongIDExeption ex)
                    { string a = ""; a += ex; }

                }

                line.TimeTravel = CalucateTravel(id);
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("מזהה קו לא תקין או שקיים כבר במערכת", ex);
            }
            //return adja.AsEnumerable();
            return id;
        }


        /// <summary>
        /// //func that get new lineTrip and update the list at DS
        /// </summary>
        /// <param name="line"></param>
        public void AddOneTripLine(LineTrip line)
        {
            try
            {
                DO.LineTrip lineTrip = new DO.LineTrip();
                DO.LineTrip temp = new DO.LineTrip();
                bool toAdd = false;
                IEnumerable<DO.LineTrip> tripDO1;
                tripDO1 = from item in dl.GetAllTripline(line.KeyId) //the oldest line trip
                          orderby item.StartAt
                          select item;

                if (tripDO1.Count() == 0)
                    toAdd = true;

                //move on the line trip that we have and find if we can insert the new one

                for (int i = 0; i < tripDO1.Count(); i++)
                {
                    temp = tripDO1.ElementAt(i);
                    if ((line.StartAt <= temp.StartAt && line.FinishAt <= temp.StartAt || line.StartAt >= temp.FinishAt && line.FinishAt >= temp.FinishAt) && (line.FinishAt != temp.FinishAt || line.StartAt != temp.StartAt))
                    {
                        toAdd = true;
                    }
                    else
                    {
                        toAdd = false;
                        break;
                    }


                }

                //restart the data because if we add a night line we add to the hour 24 houre inorder to do a correct check
                if (toAdd == true)
                {
                    if (line.FinishAt.Days > 0)
                    {
                        int hour = line.FinishAt.Days - line.FinishAt.Days + line.FinishAt.Hours;

                        TimeSpan toChange = new TimeSpan(hour, 0, 0);
                        line.FinishAt = toChange;
                    }
                    line.CopyPropertiesTo(lineTrip);
                    dl.AddLineTrip(lineTrip);
                }
                else
                {
                    throw new BO.BadLineTripExeption("זמני הלוח תפוסים,אנא הכנס זמנים חדשים", line.KeyId);
                }
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("מזהה קו לא תקין", ex);
            }
        }


        #endregion

        #region Delete
        /// <summary>
        /// get a identity number of the line and delete all the place that we have data on them
        /// </summary>
        /// <param name="idLine"></param>
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
                throw new BO.BadIdException("מזהה קו לא תקין או שלא נמצאו פרטים עליו במערכת", ex);
            }
        }

        //delete station from the line travel
        public void DeleteStation(int idline, int code)
        {

            try
            {
                int index = dl.DeleteStationsFromLine(code, idline);
                IEnumerable<DO.LineStation> tempDO;
                tempDO = dl.GetAllStationsLine(idline).ToList();

                //when we delete station from the line path we need updat the new adjacte station thet creat and update index stations.
                int adj1 = -1, adj2 = -1;
                foreach (var item in tempDO)
                {
                    if (item.LineStationIndex == (index - 1))
                        adj1 = item.StationCode;
                    if (item.LineStationIndex == (index + 1))
                        adj2 = item.StationCode;

                    bool exsit = true;
                    if (adj1 != -1 && adj2 != -1)
                    {
                        exsit = CreatAdjStations(adj1, adj2);

                    }

                    if (item.LineStationIndex > index)
                    {
                        item.LineStationIndex--;
                        dl.UpdateLineStations(item);

                    }

                }

            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("מזהה קו לא תקין", ex);
            }


        }

        //delete line trip from the line
        public void DeleteLineTrip(BO.LineTrip toDel)
        {
            try
            {
                DO.LineTrip lineTrip = new DO.LineTrip();
                toDel.CopyPropertiesTo(lineTrip);
                dl.DeleteLineTrip1(lineTrip);
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("מזהה קו לא תקין", ex);
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// update time at specific line trip
        /// </summary>
        /// <param name="oldTripLineIndex=we get from UI the place at the list of the line trip that we want to update>
        /// <param name="newLineTrip"= tje new time to the line trip>
        /// <returns></returns>
        public bool UpdateLineTrip(int oldTripLineIndex, BO.LineTrip newLineTrip)
        {
            try
            {
                DO.LineTrip lineTrip = new DO.LineTrip();
                DO.LineTrip temp = new DO.LineTrip();
                bool toAdd = true;
                IEnumerable<DO.LineTrip> tripDO1;
                tripDO1 = from item in dl.GetAllTripline(newLineTrip.KeyId) //the oldest line trip
                          orderby item.StartAt
                          select item;


                List<DO.LineTrip> a = tripDO1.ToList();
                for (int i = 0; i < a.Count(); i++) //check the new time of the line trip, if they okey
                {
                    if (i == oldTripLineIndex)
                        continue;
                    temp = tripDO1.ElementAt(i);

                    if ((newLineTrip.StartAt <= temp.StartAt && newLineTrip.FinishAt <= temp.StartAt || newLineTrip.StartAt >= temp.FinishAt && newLineTrip.FinishAt >= temp.FinishAt) && (newLineTrip.FinishAt != temp.FinishAt || newLineTrip.StartAt != temp.StartAt))
                    {
                        toAdd = true;
                    }
                    else
                    {
                        toAdd = false;
                        break;
                    }

                }
                if (toAdd == true) //if allright and we can update the line trip at our list
                {
                    temp = tripDO1.ElementAt(oldTripLineIndex);
                    newLineTrip.TripLineExist = true;
                    if (newLineTrip.FinishAt.Days > 0)
                    {
                        int hour = newLineTrip.FinishAt.Days - newLineTrip.FinishAt.Days + newLineTrip.FinishAt.Hours;
                        TimeSpan toChange = new TimeSpan(hour, 0, 0);
                        newLineTrip.FinishAt = toChange;
                    }

                    newLineTrip.CopyPropertiesTo(lineTrip);
                    dl.DeleteLineTrip1(temp);
                    dl.AddLineTrip(lineTrip);
                }
                else
                {
                    throw new BO.BadLineTripExeption("זמני הלוח תפוסים,אנא הכנס זמנים חדשים", newLineTrip.KeyId);
                }
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("מזהה קו לא תקין", ex);
            }
            return true;
        }

        /// <summary>
        /// update data on statin line
        /// </summary>
        /// <param name="line"= the line with the data to update>
        /// <returns></returns>
        public bool UpdateLineStation(BO.Line line)
        {
            IEnumerable<DO.LineStation> lineStationDO;

            lineStationDO = from st in line.StationsOfBus
                            select (DO.LineStation)st.CopyPropertiesToNew(typeof(DO.LineStation));
            try
            {
                foreach (var item in lineStationDO)
                {
                    try
                    {
                        dl.UpdateLineStations(item);

                    }
                    catch (DO.WrongIDExeption ex) //when we inser to here its indicate that this is a new station that ae addd to the line.
                    {
                        string a = ""; a += ex;
                        dl.AddLineStations(item);
                    }

                }
                bool exsit = true;

                for (int i = 0; i < lineStationDO.Count() - 1; i++)
                {
                    try //if return false its mean that we need to add this adjact station to our DS.
                    {
                        exsit = CreatAdjStations(lineStationDO.ElementAt(i).StationCode, lineStationDO.ElementAt(i + 1).StationCode);

                    }
                    catch (DO.WrongIDExeption ex)
                    { string a = ""; a += ex; }



                }
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("מזהה קו לא תקין", ex);
            }

            return true;

        }


        /// <summary>
        /// if we change index of the line station at the list so we need update all the data at the station that affect from this
        /// </summary>
        /// <param name="line"></param>
        public void UpdateLineStationForIndexChange(BO.Line line)
        {

            //keep the list of station
            IEnumerable<DO.LineStation> lineStationDO;
            lineStationDO = from st in line.StationsOfBus// רשימת הליין תורגמה לדו                  
                            select (DO.LineStation)st.CopyPropertiesToNew(typeof(DO.LineStation));


            int place = line.StationsOfBus.Count() - 1; //the place of the station that we change her index
            int indexChange = line.StationsOfBus.ElementAt(place).LineStationIndex; //the place of where we want to insert the station

            DO.LineStation lineDOPlace = new DO.LineStation();

            //in order to find the place of this station beffore the diffrences
            IEnumerable<DO.LineStation> OldtationDO;
            OldtationDO = from st in dl.GetAllStationsLine(line.IdNumber)
                          orderby st.LineStationIndex
                          select st;

            IEnumerable<DO.LineStation> a = from v in OldtationDO
                                            let cod = line.StationsOfBus.ElementAt(place).StationCode
                                            where v.StationCode == cod
                                            select v;
            int oldPlaceIndex = a.ElementAt(0).LineStationIndex - 1;

            try
            {
                DO.LineStation toSend = new DO.LineStation();
                DO.LineStation toSendTo = new DO.LineStation();
                if (oldPlaceIndex < indexChange)     //if the new place is bigger then the old place
                {
                    for (int i = oldPlaceIndex; i < indexChange - 1; i++)
                    {
                        toSend = lineStationDO.ElementAt(i);
                        toSend.LineStationIndex--;
                        if (i == oldPlaceIndex)
                        {

                            if (i == 0)
                            {
                                toSend.PrevStation = 0;
                                toSend.NextStation = lineStationDO.ElementAt(i + 1).StationCode;
                                dl.UpdateLineStations(toSend);
                            }
                            else
                            {
                                toSendTo = lineStationDO.ElementAt(i - 1);
                                toSend.PrevStation = a.ElementAt(0).PrevStation;
                                toSendTo.NextStation = lineStationDO.ElementAt(i).StationCode;
                                toSendTo.NextStation = toSend.StationCode;
                                dl.UpdateLineStations(toSendTo);

                                dl.UpdateLineStations(toSend);
                            }

                        }
                        else
                        {
                            if (i == indexChange - 2)
                            {
                                toSendTo = lineStationDO.ElementAt(place);

                                toSend.NextStation = a.ElementAt(0).StationCode;

                                //toSend.PrevStation = a.ElementAt(0).StationCode;
                                toSendTo.PrevStation = lineStationDO.ElementAt(indexChange - 2).StationCode;


                            }
                            dl.UpdateLineStations(toSend);
                        }


                    }
                    if (indexChange == lineStationDO.Count())
                    {
                        toSendTo = a.ElementAt(0);
                        toSendTo.LineStationIndex = indexChange;
                        toSendTo.PrevStation = a.ElementAt(0).NextStation;
                        toSendTo.NextStation = 0;
                        dl.UpdateLineStations(toSendTo);
                    }
                    else
                    {
                        //   toSend = lineStationDO.ElementAt(place-2);
                        //  toSend.PrevStation = lineStationDO.ElementAt(place).StationCode;
                        toSend = lineStationDO.ElementAt(place - 1);
                        toSend.PrevStation = lineStationDO.ElementAt(place).StationCode;
                        dl.UpdateLineStations(toSend);


                        toSendTo.StationCode = lineStationDO.ElementAt(place).StationCode;
                        toSendTo.NextStation = lineStationDO.ElementAt(indexChange - 1).StationCode;
                        toSendTo.PrevStation = lineStationDO.ElementAt(indexChange - 2).StationCode;
                        toSendTo.LineStationIndex = indexChange;

                        dl.UpdateLineStations(toSendTo);




                    }
                }

                else //if the new place smaller then the old place 
                {
                    for (int i = indexChange - 1; i < oldPlaceIndex; i++)
                    {
                        toSend = lineStationDO.ElementAt(i);
                        toSend.LineStationIndex++;
                        if (i == indexChange - 1)
                        {

                            if (i == 0)
                            {
                                toSendTo = lineStationDO.ElementAt(place);
                                toSendTo.PrevStation = 0;
                                toSendTo.NextStation = toSend.StationCode;

                                toSend.PrevStation = lineStationDO.ElementAt(place).StationCode;
                                dl.UpdateLineStations(toSendTo);

                            }
                            else
                            {
                                toSendTo = lineStationDO.ElementAt(place);
                                toSendTo.PrevStation = lineStationDO.ElementAt(i - 1).StationCode;
                                toSendTo.NextStation = lineStationDO.ElementAt(i).StationCode;
                                toSend.PrevStation = a.ElementAt(0).StationCode;
                                dl.UpdateLineStations(toSendTo);
                                // dl.UpdateLineStations(toSend);
                                toSendTo = lineStationDO.ElementAt(i - 1);
                                toSendTo.NextStation = a.ElementAt(0).StationCode;
                                dl.UpdateLineStations(toSendTo);
                                dl.UpdateLineStations(toSend);
                            }


                        }
                        else
                        {
                            if (i == oldPlaceIndex - 1)
                            {
                                if (oldPlaceIndex == lineStationDO.Count() - 1)
                                {

                                    toSend.NextStation = 0;

                                }
                                else
                                {
                                    toSendTo = lineStationDO.ElementAt(i + 1);
                                    toSendTo.PrevStation = lineStationDO.ElementAt(i).StationCode;

                                    toSend.NextStation = lineStationDO.ElementAt(i + 1).StationCode;

                                    dl.UpdateLineStations(toSendTo);
                                }

                            }

                            dl.UpdateLineStations(toSend);

                            toSendTo = lineStationDO.ElementAt(oldPlaceIndex - 1);
                            toSendTo.NextStation = lineStationDO.ElementAt(oldPlaceIndex).StationCode;
                            toSendTo.LineStationIndex = oldPlaceIndex + 1;

                            dl.UpdateLineStations(toSendTo);
                        }


                    }

                }


                //-------------------
                //creat adjate station if we need

                IEnumerable<DO.LineStation> tempDO2;
                tempDO2 = from item in dl.GetAllStationsLine(line.IdNumber)      //the new line station
                          orderby item.LineStationIndex
                          select item;


                bool exsit = true;
                try
                {
                    for (int i = 0; i < tempDO2.Count() - 1; i++)   //move on the line station list send 2 adj station to creat if they not exsis yet.
                    {
                        exsit = CreatAdjStations(tempDO2.ElementAt(i).StationCode, tempDO2.ElementAt(i + 1).StationCode);

                    }
                }
                catch (DO.WrongIDExeption x)
                {
                    string o = ""; o += x;
                }


            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("מזהה קו לא תקין", ex);
            }



        }


        /// <summary>
        /// //we get change at line to update. if he delete station or addd stations we get all the new change here
        /// and we need move on the new list atation and update the prev and next fields
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
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
                            dl.UpdateLineStations(tempDO2.ElementAt(i));
                        }
                        if (i == tempDO.Count() - 1)
                        {
                            tempDO2.ElementAt(i).PrevStation = tempDO2.ElementAt(i - 1).StationCode;
                            tempDO2.ElementAt(i).NextStation = 0;
                            dl.UpdateLineStations(tempDO2.ElementAt(i));
                        }
                        else
                        {
                            tempDO2.ElementAt(i).PrevStation = tempDO2.ElementAt(i - 1).StationCode;
                            tempDO2.ElementAt(i).NextStation = tempDO2.ElementAt(i + 1).StationCode;
                            dl.UpdateLineStations(tempDO2.ElementAt(i));

                        }

                    }

                }


            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("ID not valid", ex);
            }

            return true;
        }

        #endregion

        /// <summary>
        /// creat a adjacted station according to 2 code station
        /// we calucate the time travel between the station according to random num between 1-1.5 and speed a minute that 
        /// calucate km/h==m/m. 
        /// </summary>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <returns></returns>
        public bool CreatAdjStations(int station1, int station2)
        {
            double speed = 666.66;//m/s= 50 km/h
            DO.AdjacentStations adjacent = new DO.AdjacentStations();
            adjacent.Station1 = station1;
            adjacent.Station2 = station2;
            adjacent.AdjacExsis = true;
            DO.Stations ST1 = new DO.Stations();
            DO.Stations ST2 = new DO.Stations();
            try
            {
                // in order to freat adj station we need the "real" station in order to calucate distance and travel time.
                ST1 = dl.GetStations(station1);
                ST2 = dl.GetStations(station2);
                GeoCoordinate CoordinateST1 = new GeoCoordinate(ST1.Latitude, ST1.Longtitude);
                GeoCoordinate CoordinateST2 = new GeoCoordinate(ST2.Latitude, ST2.Longtitude);

                double d = (CoordinateST1).GetDistanceTo((CoordinateST2));
                adjacent.Distance = d;

                adjacent.TimeAverage = (((random.NextDouble() + 1) * d) / speed);
                dl.AddLineStations(adjacent);


                return true;
            }
            catch (DO.WrongIDExeption ex)
            {
                string a = ""; a += ex;


            }
            return false;

        }


        /// <summary>
        /// calucate the sum of all the travel
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
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
                sum += adj.TimeAverage;

            }

            return sum;

        }

        /// <summary>
        /// update adjacted station
        /// </summary>
        /// <param name="line"></param>
        public void AddAdjactStation(BO.LineStation line)
        {
            DO.AdjacentStations stations = new DO.AdjacentStations();
            stations.Station1 = line.StationCode;
            stations.Station2 = line.NextStation;
            stations.Distance = line.DistanceFromNext;
            stations.TimeAverage = line.TimeAverageFromNext;

            try
            {
                dl.UpdateAdjacentStations(stations);
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("בעיה", ex);
            }

        }

        /// <summary>
        /// return all the line at the station
        /// </summary>
        /// <param name="StationCode"></param>
        /// <returns></returns>
        public IEnumerable<BO.Line> GetAllLineIndStation(int StationCode)
        {
            try
            {


                IEnumerable<DO.LineStation> v = from item in dl.GetAllLineStationsBy(b => b.StationCode == StationCode)
                                                select item;

                var x = from s in v
                        select lineDoBoAdapter(s.LineId);
                return x;
            }
            catch(BO.BadIdException ex) { }
            return null;
        }


        #endregion


        #region Station

        // return the station from dl according to code
        BO.Station stationDoBoAdapter(int code)
        {
            BO.Station stationBO = new BO.Station();
            DO.Stations stationDO;
            IEnumerable<DO.LineStation> tempDO;
            IEnumerable<DO.AdjacentStations> adjactDO;

            try
            {
                stationDO = dl.GetStations(code);
                tempDO = dl.GetAllStationsCode(code);
                adjactDO = dl.GetAllAdjacentStations(code);
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("מזהה תחנה לא חוקי", ex);
            }


            stationDO.CopyPropertiesTo(stationBO); //go to a deep copy. all field is copied to a same field at bo.

            stationBO.LineAtStation = from st in tempDO
                                      select (BO.LineStation)st.CopyPropertiesToNew(typeof(BO.LineStation));
            stationBO.StationAdjacent = from ad in adjactDO
                                        select (BO.AdjacentStations)ad.CopyPropertiesToNew(typeof(BO.AdjacentStations));
            stationBO.Coordinate = new GeoCoordinate(stationDO.Latitude, stationDO.Longtitude);

            return stationBO;
        }


        /// <summary>
        /// return all the stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.Station> GetAllStations()
        {
            try
            {
                var v = from item in dl.GetAllStations()
                        orderby item.Code
                        select stationDoBoAdapter(item.Code);
                foreach (var temp in v)
                {
                    temp.LineAtStation = from st in dl.GetAllStationsCode(temp.Code)
                                         select (BO.LineStation)st.CopyPropertiesToNew(typeof(BO.LineStation));
                    temp.StationAdjacent = from ad in dl.GetAllAdjacentStations(temp.Code)
                                           select (BO.AdjacentStations)ad.CopyPropertiesToNew(typeof(BO.AdjacentStations));

                }
                return v;

            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("מזהה תחנה לא חוקי", ex);
            }
        }

        //return station
        public BO.Station GetStationByCode(int Code)
        {
            try
            {
                BO.Station toReturn = stationDoBoAdapter(Code);
                return toReturn;
            }
            catch (BO.BadIdException ex)
            {
                string a = ""; a += ex;
                return null;
            }
        }




        /// <summary>
        /// add new station.find if we have this code station and if yes so throw
        /// </summary>
        /// <param name="station"></param>
        /// 
        public void AddStation(BO.Station station)///////////////////
        {
            DO.Stations stationDO = new DO.Stations();
            station.CopyPropertiesTo(stationDO);
            stationDO.Latitude = station.Coordinate.Latitude;
            stationDO.Longtitude = station.Coordinate.Longitude;
            try
            {
                var a = from item in dl.GetAllStations()
                        where station.Code == item.Code
                        select item;
                if (a.ToList().Count() != 0)
                    throw new BO.BadIdException("קוד תחנה כבר קיים במערכת ", station.Code);
                if (station.Coordinate.Latitude >= 33.7 && station.Coordinate.Latitude <= 36.3 && station.Coordinate.Longitude >= 29.3 && station.Coordinate.Longitude <= 33.5)
                    dl.AddStations(stationDO);
                else
                    throw new BO.BadCoordinateException((station.Coordinate).ToString(), "הקורדינטה שהוכנסה אינה תקינה");
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("מזהה קו לא חוקי", ex);
            }
        }


        /// <summary>
        /// when we delete station we delete all the appirence of this station
        /// </summary>
        /// <param name="code"></param>
        public void DeleteStation(int code, IEnumerable<LineStation> a)
        {
            try
            {
                List<LineStation> temp = a.ToList();
                dl.DeleteStations(code);
                for (int i = 0; i < temp.Count(); i++)
                {
                    DeleteStation(temp[i].LineId, code);
                }
                dl.DeleteAdjacentStationseBStation(code);
                dl.DeleteStationsFromLine(code);
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("קוד התחנה המבוקש לא נמצא במערכת כדי למוחקו", ex);
            }
        }

        /// <summary>
        /// update date station. he can change all the data. if he change the cod of the station so we need go to all 
        /// the place that we keep data on this station and update them
        /// </summary>
        /// <param name="station"></param>
        /// <param name="oldCode"></param>
        public void UpdateStation(BO.Station station, int oldCode)
        {
            int newCode = station.Code;
            DO.Stations stationDO = new DO.Stations();
            DO.Stations stationOldDO = new DO.Stations();
            BO.AdjacentStations adjacent = new BO.AdjacentStations();
            List<BO.AdjacentStations> adjactToChange = new List<BO.AdjacentStations>();
            IEnumerable<DO.AdjacentStations> adjacentStations;

            try
            {
                if (station.Code != oldCode) //if we change the code station
                {
                    var a = from item in dl.GetAllStations()
                            where station.Code == item.Code
                            select item;
                    if (a.ToList().Count() != 0)
                        throw new BO.BadIdException("קוד תחנה כבר קיים במערכת ", station.Code);
                    IEnumerable<DO.LineStation> list = from item in dl.GetAllStationsCode(oldCode)
                                                       select item;

                    for (int i = 0; i < list.Count() + 1; i++)
                    {

                        dl.UpdateLineStationsCode(list.ElementAt(i), newCode);
                    }


                }

                adjacentStations = dl.GetAllAdjacentStations(oldCode); //get all adjacted stations with this code station


                station.CopyPropertiesTo(stationDO);
                stationDO.Latitude = station.Coordinate.Latitude;
                stationDO.Longtitude = station.Coordinate.Longitude;

                if (station.Coordinate.Latitude >= 29.3 && station.Coordinate.Latitude <= 33.5 && station.Coordinate.Longitude >= 33.7 && station.Coordinate.Longitude <= 36.3)
                {
                    stationOldDO = dl.GetStations(oldCode);
                    dl.UpdateStations(stationDO, oldCode);
                    if (newCode != oldCode)
                    {

                        for (int i = 0; i < adjacentStations.Count(); i++)
                            dl.UpdateAdjacentStations(adjacentStations.ElementAt(i).Station1, adjacentStations.ElementAt(i).Station2, newCode, oldCode);
                    }

                    if (stationOldDO.Latitude != station.Coordinate.Latitude && stationOldDO.Longtitude != station.Coordinate.Longitude) //if we change the place of tje station we need to ask to insert again data on distance and time travel
                    {
                        adjacentStations = dl.GetAllAdjacentStations(oldCode);
                        foreach (var item in adjacentStations)
                        {
                            if (item.Station1 == oldCode) //if this station is the first station
                            {
                                stationOldDO = dl.GetStations(item.Station2);

                                adjacent.Station1 = oldCode;
                                adjacent.Station2 = stationOldDO.Code;

                            }
                            else
                            {
                                stationOldDO = dl.GetStations(item.Station1);

                                adjacent.Station1 = stationOldDO.Code;
                                adjacent.Station2 = oldCode;

                            }

                            adjactToChange.Add(adjacent);




                        }

                    }

                }
                else
                    throw new BO.BadCoordinateException(Convert.ToInt32(station.Coordinate.Latitude), Convert.ToInt32(station.Coordinate.Longitude), "הקורדינטה אינה נכונה");



            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("קוד התחנה אינו חוקי", ex);
            }



        }

        //update data at adjacte station
        public void UpdateAdjac(BO.AdjacentStations adjacBO)
        {
            DO.AdjacentStations adjacDO = new DO.AdjacentStations();

            try
            {
                adjacBO.CopyPropertiesTo(adjacDO);
                adjacDO.AdjacExsis = true;
                dl.UpdateAdjacentStations(adjacDO);

            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("קוד התחנה אינו חוקי או אינו נמצא במערכת", ex);
            }

        }
        #endregion

        #region User
        BO.User userDoBoAdapter(string name)
        {
            BO.User userBO = new BO.User();
            DO.User userDO;

            try
            {
                userDO = dl.GetUser(name);
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("ID not valid", ex);
            }


            userDO.CopyPropertiesTo(userBO); //go to a deep copy. all field is copied to a same field at bo.

            return userBO;
        }
        public BO.User getUserByEmail(string email)
        {
            BO.User temp = new User();
            dl.getUserBy(b => b.MailAddress == email).CopyPropertiesTo(temp);
            return temp;
        }
        public IEnumerable<BO.User> GetAllUsers()
        {
            var v = from us in dl.GetAlluser()
                    where us.UserExist
                    select (BO.User)us.CopyPropertiesToNew(typeof(BO.User));
            return v;
        }
        public bool EmailExsit(string mail)
        {
            var v = getUserByEmail(mail);
            if (v != null)
                return true;
            return false;
        }
        public bool findUser(BO.User a)
        {
            try
            {
                User specUser = new User();
                (dl.GetUser(a.UserName)).CopyPropertiesTo(specUser);
                if (specUser.UserName != "" && specUser.Password == a.Password)
                {
                    return specUser.Admin;
                }
                else throw new BO.BadNameExeption("אחד הנתונים שגויים", a.UserName);
            }
            catch (DO.WrongNameExeption b)
            {
                throw new BadNameExeption(b.Message, a.UserName);
            }
        }



        ///// <summary>
        ///// calucate time travel between 2 stations
        ///// </summary>
        ///// <param name="line"></the line that travel between these 2 stations>
        ///// <param name="cod1"></station 1>
        ///// <param name="cod2"></staton 2>
        ///// <returns></returns>
        public double CalucateTime(BO.Line line, int cod1, int cod2)
        {
            double time = 0;
            int index1 = line.StationsOfBus.FirstOrDefault(c => c.StationCode == cod1).LineStationIndex;
            int index2 = line.StationsOfBus.FirstOrDefault(c => c.StationCode == cod2).LineStationIndex;

            for (int i = index1; i < index2; i++)
            {
                time += line.StationsOfBus.ElementAt(i).TimeAverageFromNext;
            }
            return time;
        }


        //    /// <summary>
        //    /// find path between 2 stations. we enable to return path onlly if we need travel with one line or 2, more is a ling long algoritem
        //    /// firstly we fing if we have the same line at the 2 stations accordong to identity number and check that the first atation index at the line travel smaller then the secons
        //    /// after we search if we have a travel with 2 line.
        //    /// </summary>
        //    /// <param name="code1"></begin station>
        //    /// <param name="code2"></end station>
        //    /// <returns></returns>
        public IEnumerable<object> TravelPath(int code1, int code2)
        {
            List<BO.Line> lineToSend = new List<Line>();
            BO.Line l;
            List<object> toReturn = new List<object>();
            IEnumerable<BO.LineStation> stationsBO1;
            IEnumerable<BO.LineStation> stationsBO2;
            try
            {

                stationsBO1 = from item1 in dl.GetAllLineStationsBy(b => b.StationCode == code1)  //get all the line that move at the first station. 
                              orderby item1.LineId
                              select (BO.LineStation)item1.CopyPropertiesToNew((typeof(BO.LineStation)));
                stationsBO2 = from item2 in dl.GetAllLineStationsBy(b => b.StationCode == code2) //get all the kine that move at the second station
                              orderby item2.LineId
                              select (BO.LineStation)item2.CopyPropertiesToNew((typeof(BO.LineStation)));

                var v = from temp1 in stationsBO1
                        from temp2 in stationsBO2
                        where temp1.LineId == temp2.LineId && temp1.LineStationIndex < temp2.LineStationIndex
                        select temp1;

                foreach (var tt in v)//if i foun direct
                {
                    l = new BO.Line();
                    if (lineToSend.FirstOrDefault(y => y.IdNumber == tt.LineId) == null)
                    {
                        l = lineDoBoAdapter(tt.LineId);
                        lineToSend.Add(l);
                        toReturn.Add(new
                        {

                            numberLine1 = l.NumberLine,
                            numberLine2 = l.NumberLine,
                            direct = true,
                            replaceStation = code2,
                            timeTravel = l.TimeTravel

                        }
                        );

                    }

                }
                bool equal = false;
                int replace;
                //   if (v.Count()<1)
                {
                    foreach (var co1 in stationsBO1)//over all the line in the first Station
                    {
                        var temp = dl.GetAllStationsLine(co1.LineId);//all the staion for this line
                        foreach (var te in temp)//over the staion list
                        {
                            var d = dl.GetAllLineStationsBy(b => b.StationCode == te.StationCode);//gets all the line in this staion
                            foreach (var z in d)//over al the line in this spesific line
                            {
                                replace = z.StationCode;
                                foreach (var sh in stationsBO2)//over all the line in the station 2
                                {
                                    if (z.LineId == sh.LineId)//if i found the same line
                                        if (z.LineStationIndex < sh.LineStationIndex)//if station 1 before station 2 like i need
                                        {

                                            if (lineToSend.FirstOrDefault(n => n.IdNumber == z.LineId) == null)//line 2 not in my list yet
                                            {
                                                l = new BO.Line();
                                                Line one = lineDoBoAdapter(te.LineId);
                                                lineToSend.Add(one);
                                                l = new BO.Line();
                                                l = lineDoBoAdapter(z.LineId);
                                                lineToSend.Add(l);

                                                if (one.IdNumber == l.IdNumber)
                                                    equal = true;
                                                else
                                                    equal = false;



                                                toReturn.Add(new
                                                {

                                                    numberLine1 = one.NumberLine,
                                                    numberLine2 = l.NumberLine,


                                                    direct = equal,
                                                    replaceStation = replace,
                                                    timeTravel = CalucateTime(one, code1, replace) + CalucateTime(l, replace, code2)

                                                });




                                            }

                                        }
                                }
                            }
                        }
                    }

                }





                return toReturn.AsEnumerable();

            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("ID not valid", ex);
            }
        }

        public void AddUser(string name, string pas, bool admin, string email)
        {
            DO.User userDO = new DO.User();
            userDO.UserName = name;
            userDO.Admin = admin;
            userDO.Password = pas;
            userDO.UserExist = true;
            userDO.MailAddress = email;

            try
            {
                dl.AddUser(userDO);
            }
            catch (DO.WrongNameExeption ex)
            {
                throw new BO.BadNameExeption("שם משתמש קיים כבר במערכת", ex);

            }

            IEnumerable<DO.User> users = dl.GetAlluser();
        }



        #endregion
        #region Simulator
        public void StartSimulator(TimeSpan startTime, int Rate, Action<TimeSpan> updateTime)
        {

            ClockS.Instance.ObserverClock += updateTime;
            ClockS.Instance.Start(startTime, Rate);
        }
        public void StopSimulator()
        {
            ClockS.Instance.Stop();
        }

        #endregion
        #region LineTiming
        public IEnumerable<BO.LineTiming> GetLineStationLineTimer(Station station, TimeSpan timeStart)
        {
            List<BO.LineTiming> lineTimings = new List<BO.LineTiming>();
            foreach (var item in station.LineAtStation)//over the line in this station
            {
                IEnumerable<DO.LineTrip> lineTrips = dl.GetAllLineTripsBy(b => b.KeyId == item.LineId);
                DO.Line temp = dl.GetLine(item.LineId);
                foreach (DO.LineTrip lineTrip in lineTrips)//over the line trip of the line
                {
                    TimeSpan ExitTime = lineTrip.StartAt;
                    int a = Convert.ToInt32(CalucateTime(lineDoBoAdapter(item.LineId), temp.FirstStationCode, station.Code));
                    if (a == 0) continue;
                    TimeSpan b = new TimeSpan(0, a, 0);

                    while (ExitTime <= lineTrip.FinishAt)
                    {

                        BO.LineTiming lineTiming = new BO.LineTiming()
                        {
                            LineId = item.LineId,
                            LineNumber = temp.NumberLine,
                            LastStation = dl.GetStations(temp.LastStationCode).Name,
                            tripStart = TimeSpan.Parse(ExitTime.ToString()),
                            ExpectedTimeArrive = ExitTime +b -  TimeSpan.Parse(timeStart.ToString().Substring(0, 8)),

                        };
                      

                        lineTiming.ExpectedTimeArrive = TimeSpan.Parse(lineTiming.ExpectedTimeArrive.ToString().Substring(0, 8));
                        ExitTime += TimeSpan.FromMinutes(lineTrip.Frequency);
                        if (lineTiming.ExpectedTimeArrive + lineTiming.tripStart >= timeStart)
                        {

                            lineTimings.Add(lineTiming);
                        }


                    }
                }
            }
           IEnumerable<LineTiming> lineTimings1= lineTimings.OrderBy(b => b.ExpectedTimeArrive);
            List<LineTiming> toReturn=new List<LineTiming>();
            List<int> goodLine = new List<int>();
            foreach (var item in lineTimings1)
            {
                int index = goodLine.FindIndex(b => b.ToString() == item.LineId.ToString());
                if (index == -1)
                {
                    toReturn.Add(item);
                    goodLine.Add(item.LineId);
                }

            }
            return toReturn.AsEnumerable();
        }
        #endregion



    }
}
