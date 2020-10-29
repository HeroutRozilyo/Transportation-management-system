using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_9647_4789
{
      public class Bus
    {
        static public int GlobalKM { get; private set; }
        private const int FULLTANK = 1200;

        public readonly DateTime StartingDate;
        private string license;
        private int km;
        private int NweKm;

        public DateTime Checkup { get;  set; }
        public int Fuel { get; set; }
        public int NEWKm
        {
            get; set;
        }
        public int Km 
        {
            get { return km; }
            set
            {
                if (value >= 0)
                {
                    km = value;
                }
            }
        }
        public string License
        {
            get
            {
                string firstpart, middlepart, endpart;
                string result;
                if (license.Length == 7)
                {
                    // xx-xxx-xx
                    firstpart = license.Substring(0, 2);
                    middlepart = license.Substring(2, 3);
                    endpart = license.Substring(5, 2);
                    result = String.Format("{0}-{1}-{2}", firstpart, middlepart, endpart);
                }
                else
                {
                    // xxx-xx-xxx
                    firstpart = license.Substring(0, 3);
                    middlepart = license.Substring(3, 2);
                    endpart = license.Substring(5, 3);
                    result = String.Format("{0}-{1}-{2}", firstpart, middlepart, endpart);
                }
                return result;
            }

            private set
            {
                if ((StartingDate.Year < 2018 && value.Length == 7) || (StartingDate.Year >= 2018 && value.Length == 8))
                {
                    license = value;
                }
                else
                {
                    throw new Exception("license not valid");
                }
            }
        }

        public Bus()
        {
            Console.WriteLine("give Starting date");
            bool success = DateTime.TryParse(Console.ReadLine(), out StartingDate);
            if (success == false)
            {
                throw new Exception("invalid DateTime string format");
            }
            Console.WriteLine("give license number");
            License = Console.ReadLine();
            
        }
        public Bus(Bus a)
        {
            license = a.license;
            StartingDate = a.StartingDate;
        }

        public override string ToString()
        {
            return String.Format("license is: {0,-10}, starting date: {1}", License, StartingDate);
        }
        public DateTime Maintenance()
        {
            Checkup = DateTime.Today;
            return Checkup;
        }
        public DateTime Maintenance(DateTime checkup)
        {
            Checkup = checkup;
            return Checkup;
        }

        public void Refuelling(int fuel)
        {
            Fuel = FULLTANK;
        }


    }
}
