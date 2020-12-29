using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlAPI;
using DALAPI;
//using DL;
using BO;





namespace BL
{
    class BlImp : IBL
    {
        IDAL dl = DalFactory.GetDal();

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
        

        #endregion 

    }
}
