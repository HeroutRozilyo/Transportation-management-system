using System;

namespace BO
{
    public class Bus
    {
        public string Licence { get; set; }
        public DateTime StartingDate { get; set; }
        public double Kilometrz { get; set; }
        public double KilometrFromLastTreat { get; set; }
        public double FuellAmount { get; set; }
        public STUTUS StatusBus { get; set; }
        public bool BusExsis { get; set; }
        public DateTime LastTreatment { get; set; }




    }
}
