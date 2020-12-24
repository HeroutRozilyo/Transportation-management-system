using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
   public class Bus
   {
        public int Licence { get; set; }
        public DateTime StartingDate { get; set; }
        public double Kilometrz { get; set; }
        public double KilometrFromLastTreat { get; set; }
        public double FuellAmount { get; set; }
        public STUTUS StatusBus { get; set; }
        public bool BusExsis { get; set; }


   }
}
