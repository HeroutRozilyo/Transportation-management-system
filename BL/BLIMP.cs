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

        public IEnumerable<BO.Bus> GetAllBus() //return all the buses that working 
        {
            var v = from item in dl.GetAllBuses()
                    select busDoBoAdapter(item.Licence);
            return v;
        }

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
            busDO = (DO.Bus)bus.CopyPropertiesToNew(typeof(DO.Bus));

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
                throw new BO.BadBusLicenceException("מספר הרישוי המבוקש לא נמצא במערכת", ex);

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
                    throw new BO.BadBusLicenceException("מספר הרישוי שהוכנס אינו חוקי, בבקשה אכנס מספר רישוי בעל 8 ספרות", Convert.ToInt32(bus.Licence));
                else
                    throw new BO.BadBusLicenceException("מספר הרישוי שהוכנס אינו חוקי, בבקשה אכנס מספר רישוי חדש עם 7 ספרות", Convert.ToInt32(bus.Licence));
            }
        }

        bool checkLicence(string licences) //check if the licence number is valid
        {
            string licence = licences.Replace("-", "");
            if (licence.Length == 7 || licence.Length == 8)
                return true;
            else
                throw new BO.BadBusLicenceException("מספר הרישוי שהוכנס אינו חוקי, בבקשה אכנס מספר רישוי בעל 7 או 8 ספרות בהתאם לשנת הייצור", Convert.ToInt32(licence));
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
        //BO.Line lineDoBoAdapter(int idLine) // return the line from dl according to licence
        //{
        //    BO.Line lineBO = new BO.Line();
        //    DO.Line lineDO;
        //    IEnumerable<DO.LineStation> tempDO;
        //    IEnumerable<DO.LineStation> tempDO1;
        //    IEnumerable<DO.LineTrip> tripDO;
        //    DO.AdjacentStations adj = new DO.AdjacentStations();
        //    try
        //    {
        //        lineDO = dl.GetLine(idLine);
        //        tempDO = dl.GetAllStationsLine(idLine);
        //        tripDO = dl.GetAllTripline(idLine);
        //    }
        //    catch (DO.WrongIDExeption ex)
        //    {
        //        throw new BO.BadIdException("מזהה קו לא תקין", ex);
        //    }

        //    lineBO.TimeTravel = 0;
        //    lineDO.CopyPropertiesTo(lineBO); //go to a deep copy. all field is copied to a same field at bo.



        //    tempDO1= from st in tempDO
        //                           orderby st.LineStationIndex
        //                           select st;


        //    List<LineStation> kkk = new List<LineStation>();
        //    LineStation mmm = new LineStation();
        //    int code1, code2 = 0;
        //    for (int i = 0; i < tempDO.Count() - 1; i++)
        //    {

        //        code1 = tempDO1.ElementAt(i).StationCode;
        //        code2 = tempDO1.ElementAt(i + 1).StationCode;
        //        adj = dl.GetAdjacentStations(code1, code2);

        //        mmm = new LineStation()
        //        {
        //            LineId = tempDO.ElementAt(i).LineId,
        //            LineStationIndex = tempDO.ElementAt(i).LineStationIndex,
        //            LineStationExist = tempDO.ElementAt(i).LineStationExist,
        //            NextStation = tempDO.ElementAt(i).NextStation,
        //            PrevStation = tempDO.ElementAt(i).PrevStation,
        //            StationCode = tempDO.ElementAt(i).StationCode,
        //            DistanceFromNext = adj.Distance,
        //            TimeAverageFromNext = Convert.ToDouble(adj.TimeAverage),
        //        };
        //        kkk.Add(mmm);

        //    }
        //   lineBO.StationsOfBus = kkk.AsEnumerable();


        //    lineBO.TimeLineTrip = from st in tripDO
        //                          select (BO.LineTrip)st.CopyPropertiesToNew(typeof(BO.LineTrip));
        //    lineBO.TimeTravel = CalucateTravel(idLine);

        //    return lineBO;
        //}

        BO.Line lineDoBoAdapter(int idLine) // return the line from dl according to licence
        {
            BO.Line lineBO = new BO.Line();
            DO.Line lineDO;
            IEnumerable<DO.LineStation> tempDO;
            IEnumerable<DO.LineTrip> tripDO;
            DO.AdjacentStations adj = new DO.AdjacentStations();
            try
            {
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

            lineBO.StationsOfBus = from st in tempDO
                                   orderby st.LineStationIndex
                                   select (BO.LineStation)st.CopyPropertiesToNew(typeof(BO.LineStation));
            int code1, code2 = 0;
            for (int i = 0; i < lineBO.StationsOfBus.Count() - 1; i++)
            {
                code1 = lineBO.StationsOfBus.ElementAt(i).StationCode;
                code2 = lineBO.StationsOfBus.ElementAt(i + 1).StationCode;
                adj = dl.GetAdjacentStations(code1, code2);

                lineBO.StationsOfBus.ElementAt(i).DistanceFromNext = adj.Distance;
                lineBO.StationsOfBus.ElementAt(i).TimeAverageFromNext = Convert.ToDouble(adj.TimeAverage);

            }

            lineBO.TimeLineTrip = from st in tripDO
                                  select (BO.LineTrip)st.CopyPropertiesToNew(typeof(BO.LineTrip));
            lineBO.TimeTravel = CalucateTravel(idLine);

            return lineBO;
        }//



        #region GetLine
        public IEnumerable<BO.Line> GetAllLine() //return all the lines that working 
        {
            var v = from item in dl.GetAllLine()
                    select lineDoBoAdapter(item.IdNumber);

            return v;

        }
        public IEnumerable<BO.Line> GetLineBy(int stationCode) //return all the lines according to predicate
        {
            var v = from item in dl.GetAllLineBy(x => x.FirstStationCode == stationCode)
                    select lineDoBoAdapter(item.IdNumber);

            return v;
        }
        public IEnumerable<BO.Line> GetLineByLineCode(int LineCodeCode) //return all the lines according to predicate
        {
            var v = from item in dl.GetAllLineBy(x => x.NumberLine == LineCodeCode)
                    select lineDoBoAdapter(item.IdNumber);

            return v;
        }
        public IEnumerable<IGrouping<BO.AREA,BO.Line>>GetLinesByAreaG()
        {
             var list = from line in GetAllLine()
                       group line by line.Area into g
                       select g;
            return list;
        }

        

        public BO.Line GetLineByLine(int lineid) //return all the lines according to predicate
        {
            Line line = lineDoBoAdapter(lineid);

            return line;
        }
        public IEnumerable<BO.Line> GetLineByArea(BO.AREA area) //return all the line according to their area
        {

            IEnumerable<BO.Line> help;
            help = from item in dl.GetAllLinesArea((DO.AREA)area)
                   select lineDoBoAdapter(item.IdNumber);

            return help;
        }
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
        //public IEnumerable<BO.LineStation> AddLine(BO.Line line)
        public int AddLine(BO.Line line)

        {
            DO.Line lineDO = new DO.Line();            
            line.CopyPropertiesTo(lineDO);//do the bus to be DO

            List<BO.LineStation> adja = new List<LineStation>();
        //    BO.LineStation adjacent;

            IEnumerable<DO.LineStation> tempDO;
            tempDO = from st in line.StationsOfBus                                        //tempDO=line station
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

        //public IEnumerable<BO.LineStation> AddStationLine(BO.LineStation station) //we add station to the bus travel
        //{
        //    int index = station.LineStationIndex;
        //    DO.Line lineDO = new DO.Line();
        //    BO.Line lineBO = new BO.Line();
        //    IEnumerable<DO.LineStation> tempDO;
        //    IEnumerable<DO.LineTrip> tripDO;

        //    BO.LineStation adjacent;
        //    List<BO.LineStation> adja = new List<LineStation>();

        //    try
        //    {
        //        //creat BO line

        //        lineDO = dl.GetLine(station.LineId);  //if the bus not exsis we will have exeption from DL
        //        tempDO = dl.GetAllStationsLine(station.LineId);
        //        tripDO = dl.GetAllTripline(station.LineId);

        //        lineBO.StationsOfBus = from st in tempDO
        //                               select (BO.LineStation)st.CopyPropertiesToNew(typeof(BO.LineStation));

        //        lineBO.TimeLineTrip = from st in tripDO
        //                              select (BO.LineTrip)st.CopyPropertiesToNew(typeof(BO.LineTrip));
        //        lineBO.CopyPropertiesTo(lineDO);

        //        bool exsit = true;
        //        for (int i = 0; i < lineBO.StationsOfBus.Count(); i++)
        //        {
        //            try
        //            {
               
        //                // creat a new adj station if they not exsis yet
        //                if (lineBO.StationsOfBus.ElementAt(i).LineStationIndex == (index - 1))
        //                {
        //                    exsit = CreatAdjStations(lineBO.StationsOfBus.ElementAt(i).StationCode, station.StationCode);
        //                    if (!exsit)
        //                    {
        //                        adjacent = new LineStation();
        //                        adjacent.StationCode = lineBO.StationsOfBus.ElementAt(i).StationCode;
        //                        adjacent.NextStation = station.StationCode;
        //                        adja.Add(adjacent);

        //                    }
        //                }
        //                if(lineBO.StationsOfBus.ElementAt(i).LineStationIndex == (index + 1))
        //                {
        //                    exsit = CreatAdjStations(station.StationCode,lineBO.StationsOfBus.ElementAt(i).StationCode);
        //                    if (!exsit)
        //                    {
        //                        adjacent = new LineStation();
        //                        adjacent.StationCode = lineBO.StationsOfBus.ElementAt(i).StationCode;
        //                        adjacent.NextStation = station.StationCode;
        //                        adja.Add(adjacent);

        //                    }
        //                }

        //                //in order to update the station index at line travel
        //                if (lineBO.StationsOfBus.ElementAt(i).LineStationIndex >= index)
        //                    lineBO.StationsOfBus.ElementAt(i).LineStationIndex++;

        //            }
        //            catch (DO.WrongIDExeption ex) { string a = ""; a += ex; }

        //        }

        //    }
        //    catch (DO.WrongIDExeption ex)
        //    {
        //        throw new BO.BadIdException("ID not valid", ex);
        //    }

        //    return adja.AsEnumerable();
        //}


        public void AddOneTripLine(LineTrip line) //func that get new lineTrip and update the list at DS
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

        public void DeleteStation(int idline, int code) //delete station from the line travel
        {
          //  BO.LineStation adjacent=new LineStation();
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

            //return adjacent;

        }

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


                for (int i = 0; i < a.Count(); i++)
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
                if (toAdd == true)
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

        public void UpdateLineStationForIndexChange(BO.Line line)
        {
           // List<BO.LineStation> adja = new List<LineStation>();
           // BO.LineStation adjacent;

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
                if (oldPlaceIndex < indexChange)
                {
                    for (int i = oldPlaceIndex; i < indexChange-1 ; i++)
                    {
                        toSend = lineStationDO.ElementAt(i);
                        toSend.LineStationIndex--;
                        if (i == oldPlaceIndex)
                        {

                            if (i == 0)
                            {
                                toSend.PrevStation = 0;

                                dl.UpdateLineStations(toSend);
                            }
                            else
                            {
                                toSendTo = lineStationDO.ElementAt(i - 1);
                                toSend.PrevStation = a.ElementAt(0).PrevStation;
                                toSendTo.NextStation = lineStationDO.ElementAt(i).StationCode;
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
                                toSendTo.PrevStation = lineStationDO.ElementAt(indexChange - 2).StationCode;

                            }
                            dl.UpdateLineStations(toSend);
                        }


                    }
                    if (indexChange == lineStationDO.Count())
                    {
                        toSendTo.NextStation = 0;
                        dl.UpdateLineStations(toSendTo);
                    }
                    else
                    {
                        toSend.NextStation = lineStationDO.ElementAt(place).StationCode;
                        dl.UpdateLineStations(toSend);


                        toSendTo.StationCode = lineStationDO.ElementAt(place).StationCode;
                             toSendTo.NextStation = lineStationDO.ElementAt(indexChange - 1).StationCode;
                            toSendTo.PrevStation = lineStationDO.ElementAt(indexChange - 2).StationCode;
                           toSendTo.LineStationIndex = indexChange;
                     
                        dl.UpdateLineStations(toSendTo);




                    }
                }

                else
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
                                // dl.UpdateLineStations(toSend);
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

                            toSendTo.StationCode = lineStationDO.ElementAt(place).StationCode;
                            toSendTo.NextStation = lineStationDO.ElementAt(indexChange - 1).StationCode;
                            toSendTo.PrevStation = lineStationDO.ElementAt(indexChange - 2).StationCode;
                            toSendTo.LineStationIndex = indexChange;
                            dl.UpdateLineStations(toSendTo);
                        }


                    }

                }

                //-------------------
                //creat adjate station if we need

                IEnumerable<DO.LineStation> tempDO2;
                tempDO2 = from item in dl.GetAllStationsLine(line.IdNumber)               //the new line station
                          orderby item.LineStationIndex
                          select item;

           
                bool exsit = true;
                try
                {
                    for (int i = 0; i < tempDO2.Count() - 1; i++)   //move on the line station list send 2 adj station to creat if they not exsis yet.
                    {
                        exsit= CreatAdjStations(tempDO2.ElementAt(i).StationCode, tempDO2.ElementAt(i + 1).StationCode);
           
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

        public bool UpdateLine(BO.Line line) //we get change at line to update
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

        public bool CreatAdjStations(int station1, int station2)
        {
            double speed = 666.66;//m/s= 50 km/h
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
                adjacent.Distance = 1;
              
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

        public IEnumerable<BO.Line> GetAllLineIndStation(int StationCode)
        {
            IEnumerable<DO.LineStation> v = from item in dl.GetAllLineStationsBy(b => b.StationCode == StationCode)
                                            select item;

            var x = from s in v
                    select lineDoBoAdapter(s.LineId);
            return x;

        }


        #endregion


        #region Station

        BO.Station stationDoBoAdapter(int code) // return the station from dl according to code
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
            stationBO.Coordinate = new GeoCoordinate();
            stationBO.Coordinate.Latitude = stationDO.Coordinate.Latitude;
            stationBO.Coordinate.Longitude = stationDO.Coordinate.Longitude;
            return stationBO;
        }



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
        //public IEnumerable<BO.Line> GetAllLineIndStation(int StationCode)
        //{
        //    IEnumerable<DO.LineStation> v = from item in dl.GetAllLineStationsBy(b => b.StationCode == StationCode)
        //                                    select item;

        //    var x = from s in v
        //            select lineDoBoAdapter(s.LineId);
        //    return x;

        //}




        public void AddStation(BO.Station station)///////////////////
        {
            DO.Stations stationDO = new DO.Stations();
            station.CopyPropertiesTo(stationDO);
            stationDO.Coordinate = new GeoCoordinate(station.Coordinate.Latitude, station.Coordinate.Longitude);
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

        public void DeleteStation(int code)
        {
            try
            {
                dl.DeleteStations(code);
                dl.DeleteAdjacentStationseBStation(code);
                dl.DeleteStationsFromLine(code);
            }
            catch (DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("קוד התחנה המבוקש לא נמצא במערכת כדי למוחקו", ex);
            }
        }

        public void UpdateStation(BO.Station station,int oldCode)
        {
            int newCode = station.Code;
            DO.Stations stationDO = new DO.Stations();
            DO.Stations stationOldDO = new DO.Stations();
            BO.AdjacentStations adjacent = new BO.AdjacentStations();
            List<BO.AdjacentStations> adjactToChange = new List<BO.AdjacentStations>();
            IEnumerable<DO.AdjacentStations> adjacentStations;
   
            try
            {
                if (station.Code != oldCode)
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

                        dl.UpdateLineStationsCode(list.ElementAt(0), newCode);
                    }
                }
                    
                    adjacentStations =  dl.GetAllAdjacentStations(oldCode); //get all adjacted stations with this code station
           

                     station.CopyPropertiesTo(stationDO);
                     stationDO.Coordinate = new GeoCoordinate(station.Coordinate.Latitude, station.Coordinate.Longitude);

                  if (station.Coordinate.Latitude >= 29.3 && station.Coordinate.Latitude <= 33.5 && station.Coordinate.Longitude >= 33.7 && station.Coordinate.Longitude <= 36.3)
                  {
                    stationOldDO = dl.GetStations(oldCode);
                    dl.UpdateStations(stationDO,oldCode);
                    if(newCode!= oldCode)
                    {
        
                        for(int i=0;i< adjacentStations.Count()+1;i++)
                            dl.UpdateAdjacentStations(adjacentStations.ElementAt(0).Station1, adjacentStations.ElementAt(0).Station2, newCode, oldCode);
                    }
                      
                    if (stationOldDO.Coordinate != station.Coordinate) //if we change the place of tje station we need to ask to insert again data on distance and time travel
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

                        //    UpdateAdjacentStations

                    
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
        public void UpdateAdjac(BO.AdjacentStations adjacBO)
        {
            DO.AdjacentStations adjacDO = new DO.AdjacentStations();

            try
            {
                adjacBO.CopyPropertiesTo(adjacDO);
               
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



    
        public double CalucateTime(BO.Line line, int cod1, int cod2)
        {
            double time = 0;
            int index1 = line.StationsOfBus.FirstOrDefault(c => c.StationCode == cod1).LineStationIndex;
            int index2 = line.StationsOfBus.FirstOrDefault(c => c.StationCode == cod2).LineStationIndex;

            for (int i = index1-1; i < index2; i++)
            {
                time += line.StationsOfBus.ElementAt(i).TimeAverageFromNext;
            }
            return time;
        }

        public IEnumerable<object> TravelPath(int code1,int code2)
        {
            List<BO.Line> lineToSend =new List<Line>();
            BO.Line l;
           List<object> toReturn=new List<object>();
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

              var v=  from temp1 in stationsBO1
                      from temp2 in stationsBO2
                      where temp1.LineId == temp2.LineId && temp1.LineStationIndex < temp2.LineStationIndex
                      select temp1;

                foreach(var tt in v)//if i foun direct
                {
                    l = new BO.Line();
                    if(lineToSend.FirstOrDefault(y=>y.IdNumber==tt.LineId)==null)
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
                        ) ;
                       
                    }
               
                }

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
                                replace=z.StationCode;
                                foreach (var sh in stationsBO2)//over all the line in the station 2
                                {
                                    if (z.LineId == sh.LineId)//if i found the same line
                                        if (z.LineStationIndex < sh.LineStationIndex)//if station 1 before station 2 like i need
                                        {

                                            if (lineToSend.FirstOrDefault(n => n.IdNumber == z.LineId) == null)//line 2 not in my list yet
                                            {
                                                Line one = new BO.Line();
                                                one = lineDoBoAdapter(te.LineId);
                                                lineToSend.Add(one);

                                                l = new BO.Line();
                                                l = lineDoBoAdapter(z.LineId);
                                                lineToSend.Add(l);
                                                toReturn.Add(new
                                                {

                                                    numberLine1 = one.NumberLine,
                                                    numberLine2 = l.NumberLine,
                                                    direct =false,
                                                    replaceStation = replace,
                                                    timeTravel = CalucateTime(one,code1,replace)+ CalucateTime(l, replace, code2)

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

        public void AddUser(string name,string pas, bool admin)
        {
            DO.User userDO = new DO.User();
            userDO.UserName = name;
            userDO.Admin = admin;
            userDO.Password = pas;
            userDO.UserExist = true;

            try
            {
                dl.AddUser(userDO);
            }
            catch(DO.WrongNameExeption ex)
            {
                throw new BO.BadNameExeption("שם משתמש קיים כבר במערכת", ex);

            }

            IEnumerable<DO.User> users = dl.GetAlluser();
        }

        

        #endregion

    }
}
