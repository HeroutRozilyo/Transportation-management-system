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
        bool UpdateBus(BO.Bus bus);
        #endregion
    }
}
