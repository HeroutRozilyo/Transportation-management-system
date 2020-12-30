using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BlAPI
{
    public static class factoryBL
    {
        public static IBL GetBL(/*string type*/)
        {
            
            
                return new BlImp();
            
        }

    }
}
