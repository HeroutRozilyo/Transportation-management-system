using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    static class Config
    {
        //for Line
        static int idLineCounter = 0;
        public static int IdLineCounter => ++idLineCounter;

        //for trip uses
        static int tripUser = 0;
        public static int TripUser => ++tripUser;

        //for bus trip
        static int tripBus = 0;
        public static int TripBus => ++tripBus;


    }
}
