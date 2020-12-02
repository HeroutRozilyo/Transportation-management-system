using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;



namespace dotNet5781_03b_4789_9647
{
  

   
        public enum Status
        {
            READY_TO_TRAVEL, MIDDLE_TRAVEL, RE_FULLING, TREATMENT
        }
        public class Bus
        {
            //----------
            // Variable definition
            // static public int GlobalKM { get; private set; }

            static Random r = new Random(DateTime.Now.Millisecond);


            private const int MAX_KM = 20000;
            private const int FULLTANK = 1200;

            public DateTime StartingDate { get; set; }





            private string license;
            private int km;
            //  private int NewKm_from_LastTreatment;


            public Status status { get; set; }

            public DateTime lastTreat { get; set; }


            //------
            // set and get from our variable

            public int Fuel { get; set; }

            public int newKm_from_LastTreatment
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
                    else
                        throw new ArgumentOutOfRangeException("KM must to be a positive number");
                }
            }

            public string License
            {
                //----
                //in order to find if our license need be 7 or 8 numbers. according to yesr of product.
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

                set
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

            public Bus() //defult constructor
            {
                int num = 10000000;
                string a = num.ToString();

                status = (Status)0;
                StartingDate = DateTime.Today;
                License = a;

                lastTreat = Last_tratment();
                Fuel = FULLTANK;
                newKm_from_LastTreatment = 0;
                km = 0;

            }

            public Bus(int num, DateTime date)
            {
                string a = num.ToString();
                License = a;
                StartingDate = date;

                Fuel = r.Next(FULLTANK);
                newKm_from_LastTreatment = r.Next(20000);
                km = r.Next(30000);
                lastTreat = new DateTime(2020, r.Next(1, DateTime.Today.Month), r.Next(1, DateTime.Today.Day));
                status = (Status)0;

            }


            public Bus(Bus a)
            {
                license = a.license;
                StartingDate = a.StartingDate;
                Fuel = a.Fuel;
                status = a.status;
                km = a.km;
                newKm_from_LastTreatment = a.newKm_from_LastTreatment;
                lastTreat = a.lastTreat;
            }

            public override string ToString()
            {
                return String.Format("license is: {0,-10}, starting date: {1}", License, StartingDate);
            }



            /// <summary>
            /// func that do treatment to the bus
            /// </summary>
            public void treatment()
            {
                status = (Status)3;
                this.Last_tratment();

                newKm_from_LastTreatment = 0;
                if (this.Fuel <= 1200)
                {
                    Fuel = FULLTANK;
                }
            }

            //------------
            ////update the date time after treatment
            ///
            public DateTime Last_tratment()
            {
                lastTreat = DateTime.Today;
                return lastTreat;
            }
            public DateTime Last_tratment(DateTime checkup)
            {
                lastTreat = checkup;
                return lastTreat;
            }

            public void Refuelling() //update the new fuel
            {
                status = (Status)2;
                Fuel = FULLTANK;
            }

            /// <summary>
            /// check if the bus can take the travel
            /// </summary>
            /// <param name=//"kmTravel= the km to the new travel>
            /// <returns></returns>
            public bool Take_travel(int kmTravel)
            {
                if (newKm_from_LastTreatment < MAX_KM && lastTreat.AddYears(1) >= DateTime.Today) //if he didnt need go to treatment
                {
                    if (newKm_from_LastTreatment + kmTravel < MAX_KM && Fuel - kmTravel > 0) //if he can take this travel
                    {
                        ///------------
                        ///update the new data
                        newKm_from_LastTreatment += kmTravel;
                        Fuel -= kmTravel;
                        km += kmTravel;
                        status = (Status)1;
                        return true;
                    }
                    else //if he cant take this travel
                    {
                        status = (Status)0;
                        return false;
                    }
                }
                else //if he cant travels
                {
                    this.treatment();
                    return false;
                }

            }

            public static implicit operator Bus(Grid v)
            {
                throw new NotImplementedException();
            }

            public string fuelString()
            {
                string str = " ";
                // int.TryParse(string a, out Fuel);
                str = "The amount of fuel at this Bus:\n";
                str += Fuel;
                str += "\n Do you want to refull ?";
                return str;
            }
        }

    }
