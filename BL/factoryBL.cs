using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public static class factoryBL
    {
        public static IBL GetBl()
        {
            return new BL.BlImp1();
        }

    }
}
