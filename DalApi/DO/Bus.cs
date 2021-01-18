using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Bus entity - showing the existing buses in the company
    /// </summary>
    public class Bus
   {
        /// <summary>
        /// Bus license plate - unique ID number
        /// </summary>
        public string Licence { get; set; }

        /// <summary>
        /// Date of entry into service in the company
        /// </summary>
        public DateTime StartingDate { get; set; }

        /// <summary>
        /// The total kilometers of the bus
        /// </summary>
        public double Kilometrz { get; set; }

        /// <summary>
        /// The total mileage of the bus since the last treatment
        /// </summary>
        public double KilometrFromLastTreat { get; set; }

        /// <summary>
        /// The amount of fuel on the bus
        /// </summary>
        public double FuellAmount { get; set; }

        /// <summary>
        /// Bus status (in the Enum file)
        /// </summary>
        public STUTUS StatusBus { get; set; }

        /// <summary>
        /// A bus exists in the system or is deleted
        /// </summary>
        public bool BusExist { get; set; }

        /// <summary>
        /// Last date the bus was  treatmented
        /// </summary>
        public DateTime LastTreatment { get; set; }



    }
}
