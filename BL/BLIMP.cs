﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlAPI;
using DALAPI;
//using DL;
using BO;
using System.Device.Location;
using DLAPI;

namespace BL
{
    class BlImp : IBL
    {
        IDAL dl = DalFactory.GetDL();

        #region Bus
        BO.Bus busDoBoAdapter(int licence) // return the bus from dl according to licence
        {
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

            busDO.CopyPropertiesTo(busBO); //go to a deep copy. all field is copied to a same field at bo.

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
            bus.CopyPropertiesTo(busDO); //go to copy the varieble to be DO
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
        public bool DeleteBus(int licence) //delete bus according to his licence
        {
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
        public bool UpdateBus(BO.Bus bus) //update the bus at the DS
        {
            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO); //go to copy the varieble to be DO
            try
            {
                dl.UpdateBus(busDO);
            }
            catch (DO.WrongLicenceException ex)
            {
                throw new BO.BadBusLicenceException("Licence not valid", ex);
            }
            return true;
        }
        bool checkLicence(BO.Bus bus) //check if the new licence that the passenger enter is valid
        {
            string licence = "";
            licence += bus.Licence;
            if ((bus.StartingDate.Year < 2018 && licence.Length == 7) || (bus.StartingDate.Year >= 2018 && licence.Length == 8))
            {
                return true;
            }
            else
            {
                if (bus.StartingDate.Year >= 2018)
                    throw new BO.BadBusLicenceException("The new licence is not valid,\n please enter again number licence with 8 digite", bus.Licence);
                else
                    throw new BO.BadBusLicenceException("The new licence is not valid,\n please enter again number licence with 7 digite", bus.Licence);
            }
        }
        bool checkLicence(int licence) //check if the licence number is valid
        {
            string licences = "";
            licences += licence;
            if (licences.Length == 7 || licences.Length == 8)
                return true;
            else
                throw new BO.BadBusLicenceException("The new licence is not valid,\n please enter again number licence with 8 or 7 digite", licence);
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
            catch(DO.WrongIDExeption ex)
            {
                throw new BO.BadIdException("ID not valid", ex);
            }
   ///////////////
      
            lineDO.CopyPropertiesTo(lineBO); //go to a deep copy. all field is copied to a same field at bo.
            lineBO.StationsOfBus = (IEnumerable<LineStation>)tempDO;
            lineBO.TimeLineTrip = (IEnumerable<LineTrip>)tripDO;
            return lineBO;
        }
        public IEnumerable<BO.Line> GetAllLine() //return all the lines that working 
        {
            return from item in dl.GetAllLine()
                   select lineDoBoAdapter(item.IdNumber);
        }
        public IEnumerable<BO.Line> GetLineBy(int stationCode) //return all the lines according to predicate
        {
            return from item in dl.GetAllLineBy(x => x.FirstStationCode == stationCode)
                   select lineDoBoAdapter(item.IdNumber);
        }
        public IEnumerable<BO.Line> GetLineByArea(BO.AREA area) //return all the line according to their area
        {
            return from item in dl.GetAllLinesArea((DO.AREA)area)
                   select lineDoBoAdapter(item.IdNumber);
        }

        public void AddLine(BO.Line line)
        {          
            DO.Line lineDO=new DO.Line();
            //do the bus to be DO
            line.CopyPropertiesTo(lineDO);
            IEnumerable<DO.LineStation> tempDO=(IEnumerable<DO.LineStation>)line.StationsOfBus;
            IEnumerable<DO.LineStation> tempDO1 = (IEnumerable<DO.LineStation>)line.StationsOfBus;
            IEnumerable<DO.LineTrip> tripDO = (IEnumerable<DO.LineTrip>)line.TimeLineTrip;
            DO.LineStation l1 = new DO.LineStation();
            DO.LineStation l2 = new DO.LineStation();
            try
            {
               int id= dl.AddLine(lineDO);
                foreach(var item in tempDO)        ///////////////////////////לשאול את אליעזר איך כותבים את זה עם ביטוי למדה או לינק
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
                for (int i=1;i<tempDO.Count();i++) //move on the line station list send 2 adj station to creat if they not exsis yet.
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
            int adj1=-1, adj2=-1;
            try
            {
                lineDO = dl.GetLine(station.LineId);  //if the bus not exsis we will have exeption from DL
                tempDO = dl.GetAllStationsLine(station.LineId);
                tripDO = dl.GetAllTripline(station.LineId);

                //creat BO line
                lineBO.StationsOfBus = (IEnumerable<LineStation>)tempDO;
                lineBO.TimeLineTrip = (IEnumerable<LineTrip>)tripDO;
                lineBO.CopyPropertiesTo(lineDO);
                for(int i=0;i<lineBO.StationsOfBus.Count();i++)
                {
                    try
                    {
                        // check if we need to delete the adjacted station after we add between them another station
                        //find the require 2 stations
                        if (lineBO.StationsOfBus.ElementAt(i).LineStationIndex == index - 1)
                            adj1 = lineBO.StationsOfBus.ElementAt(i).StationCode;
                        if(lineBO.StationsOfBus.ElementAt(i).LineStationIndex == index)
                            adj2 = lineBO.StationsOfBus.ElementAt(i).StationCode;
                        //if we find them so check if thet=y adjacted station for another bus. if not-delete
                        if (adj1 != -1 && adj2 != -1)
                            if (dl.GetAllLineAt2Stations(adj1, adj2).Count() == 1)
                                dl.DeleteAdjacentStationse(adj1, adj2);

                        // creat a new adj station if they not exsis yet
                        if (lineBO.StationsOfBus.ElementAt(i).LineStationIndex == index - 1 || lineBO.StationsOfBus.ElementAt(i).LineStationIndex == index + 1)
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

            //for (int i = 1; i < tempDO.Count(); i++) //move on the line station list send 2 adj station to creat if they not exsis yet.
            //{
            //    try
            //    {
            //        l1 = tempDO.ElementAt(i);
            //        i++;
            //        l2 = tempDO.ElementAt(i);
            //        i--;
            //        CreatAdjStations(l1.StationCode, l2.StationCode);
            //    }
            //    catch (DO.WrongIDExeption ex) { string a = ""; a += ex; }
            //}
        }

        public void DeleteLine(int idLine)
        {

        }


        public void CreatAdjStations(int station1,int station2)
        {
            double speed = 13.89;//m/s= 50 km/h
            DO.AdjacentStations adjacent=new DO.AdjacentStations();
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
        #endregion 

    }
}
