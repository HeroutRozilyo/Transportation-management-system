using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
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
            BO.Bus busBO = new BO.Bus(); 
            DO.Bus busDO;
            try
            {
                busDO = dl.GetBus(licence);
            }
            catch(DO.WrongLicenceException ex)
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

        public IEnumerable<BO.Bus> GetBusBy(int line)
        {
            return from item in dl.GetAllBusesBy(x => x.Licence == line)
                   select busDoBoAdapter(item.Licence);
        }





        #endregion
    }
}
