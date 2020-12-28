using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public static class Config
    {
        //for Line
       public static int idLineCounter;
        //for trip uses
      public  static int tripUser;
        //for bus trip
      public  static int tripBus = 0;

 //       public static int IdLineCounter => ++idLineCounter;
 //        public static int TripUser => ++tripUser;
 //      public static int TripBus => ++tripBus;

        static Config()
        {
            idLineCounter = 0;
            tripUser = 0;
            tripBus = 0;
        }
    }
}
