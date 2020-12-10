using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;
using System.ComponentModel;
using System.Windows;
using System.Diagnostics;


namespace doNet5781_03B_4789_9647
{

    public enum Status
    {
        READY_TO_TRAVEL, MIDDLE_TRAVEL, RE_FULLING, TREATMENT, Unfit
    }
    public class Bus : INotifyPropertyChanged
    {
        //----------
        // Variable definition

        BackgroundWorker worker = new BackgroundWorker();
        static Random r = new Random(DateTime.Now.Millisecond);
        private const int MAX_KM = 20000;
        private const int FULLTANK = 1200;
        int timeToEndWork;
        public int TimeToEndWork
        { get { return timeToEndWork; } set { } }

        private string driverOfBus;
        public string DriverOfBus
        {
            get { return driverOfBus; }
            set { driverOfBus = value; }
        }
        
        public string NameDriver 
        {
            get { return driverOfBus; }
            set { this.driverOfBus = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("NameDriver"));
            }
        }
        //public bool driverWorking;
        //public bool DriverWorking
        //{
        //    get { return driverOfBus.InTraveling; }
        //    set
        //    {
        //        this.driverOfBus.InTraveling = value;
        //        if (PropertyChanged != null)
        //            PropertyChanged(this, new PropertyChangedEventArgs("DriverWorking"));
        //    }
        //}


        //variable that connect to the vasibility op prograss ber at main window xamle 
        private string visibility;
        public string visible
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("visible"));

            }
        
        
        }


        //cobstructor to the bus wuth to value. we using most the time at this constructor
        public Bus(int num, DateTime date)
        {
            string a = num.ToString();
            StartingDate = date;
            License = a;
            Fuel = r.Next(FULLTANK);
            do
            {
                newKm_from_LastTreatment = r.Next(20000);
                km = r.Next(30000);
            } while (km < newKm_from_LastTreatment);
            //lastTreat = new DateTime(r.Next(200,, r.Next(1, DateTime.Today.Month), r.Next(1, DateTime.Today.Day));
            lastTreat = new DateTime(r.Next(2020,DateTime.Today.Year), r.Next(1, DateTime.Today.Month), r.Next(1, DateTime.Today.Day));
            status = checkingStatus();
            strstartingdate = String.Format("{0}/{1}/{2}", StartingDate.Day, StartingDate.Month, StartingDate.Year);
            enable = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            visible = "Hidden";
            driverOfBus =("");
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
            strstartingdate = String.Format("{0}/{1}/{2}", StartingDate.Day, StartingDate.Month, StartingDate.Year);
            enable = true;
            visible = "Hidden";
            driverOfBus = ("");
        }





        public DateTime StartingDate { set; get; }
        public string strstartingdate
        {
            set
            {
                strLastTreat = value;

            }
            get
            {
                string result = " ";
                result = String.Format("{0}/{1}/{2}", StartingDate.Day, StartingDate.Month, StartingDate.Year);
                return result;
            }
        } //return thw stating date at string




        private Status s;
        public Status status
        {
            get { return s; }
            set
            {
                s = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("status"));
            }
        }

        /// <summary>
        /// Date last treat func
        /// </summary>
        private DateTime Lasttreat;
        public DateTime lastTreat
        {
            get { return Lasttreat; }
            set
            {
                Lasttreat = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("lastTreat"));
            }
        }
        //------------
        ////update the date time after treatment
        ///
        public DateTime Last_tratment() //update to the current date
        {
            lastTreat = DateTime.Today;
            return lastTreat;
        }
        public DateTime Last_tratment(DateTime checkup) //update specific date. when we creat bus who using this func
        {
            lastTreat = checkup;
            return lastTreat;
        }
        public string strLastTreat
        {
            set
            {

            }
            get
            {
                string result = " ";
                result = String.Format("{0}/{1}/{2}", lastTreat.Day, lastTreat.Month, lastTreat.Year);
                return result;
            }
        }

        private double f;
        public double Fuel
        {
            get { return f; }
            set
            {
                f = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Fuel"));
            }
        }


        private double newkm;
        public double newKm_from_LastTreatment
        {
            get { return newkm; }
            set
            {
                newkm = value;
                //km += newkm;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("newKm_from_LastTreatment"));
            }
        }

        private bool fuelldelek;
        public bool Fuelldelek
        {
            get { return fuelldelek; }
            set 
            {
                fuelldelek = value;
                if (Fuel == FULLTANK) fuelldelek = true;
                else fuelldelek = true;
            }
}

        private double km;
        public double Km
        {
            get { return km; }
            set
            {
                if (value >= 0)
                {
                    km = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Km"));
                }
                else
                    throw new ArgumentOutOfRangeException("KM must to be a positive number");
            }
        }

    
        public bool Enable;
        public bool enable
        {
            get { return Enable; }
            set
            {
                Enable = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("enable"));
                }

            }
        }

        

        private string license;
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

                    ///throw new Exception("The new licence is not valid,\n please enter again number licence with 8 digite");
                   throw new ArgumentException("The new licence is not valid,\n please enter again number licence with 8 digite");
                }
            }
        }



        public override string ToString()
        {
            return String.Format("license is: {0,-10}, starting date: {1}", License, StartingDate);
        }



        public Status checkingStatus()
        {
            DateTime a = lastTreat;
            if (this.Fuel != 0 && newKm_from_LastTreatment < MAX_KM && a.AddYears(1) >= DateTime.Today)
            {
                return Status.READY_TO_TRAVEL;
            }
            else return Status.Unfit;
        }           

        //return the fuel of bus at string, in order to show that at main window.
        public string fuelString()
        {
            string str = " ";
            // int.TryParse(string a, out Fuel);
            str = "The amount of fuel at this Bus:\n";
            str += Fuel;
            str += "\n Do you want to refull ?";
            return str;
        }


        /// <summary>
        /// func that do treatment to the bus
        /// </summary>
        public void treatment()
        {

            worker.RunWorkerAsync(144);

            status = (Status)3;
            this.Last_tratment();

            strLastTreat = String.Format("{0}/{1}/{2}", this.lastTreat.Day, this.lastTreat.Month, this.lastTreat.Year);

            newKm_from_LastTreatment = 0;
            if (this.Fuel <= 1200)
            {
                Fuel = FULLTANK;
            }
        }


        public void Refuelling() //update the new fuel
        {

            //.Background = Brushes.Orange;

            worker.RunWorkerAsync(12);

            status = (Status)2;

            fuelldelek = true;
            Fuel = FULLTANK;
        }

        private int Helptime;
        public int helptime
        {
            get { return Helptime; }
            set { Helptime = value; }
        
        
        }
        private int TimeTravel;
        public int timeTravel
        {
            get
            {
                return TimeTravel;
            }
            set
            {
                TimeTravel = value;
            }
        }



        /// <summary>
        /// check if the bus can take the travel
        /// </summary>
        /// <param name=//"kmTravel= the km to the new travel>
        /// <returns></returns>
        public bool Take_travel(double kmTravel)
        {
            if (newKm_from_LastTreatment <= MAX_KM && lastTreat.AddYears(1) >= DateTime.Today) //if he didnt need go to treatment
            {
                if (newKm_from_LastTreatment + kmTravel <= MAX_KM && Fuel - kmTravel >= 0) //if he can take this travel
                {
                    double time = 0;
                    int v = r.Next(20, 51); //velocitui of this bus
                    double t = kmTravel / (v); //time of this travel.This time is in hour

                    time = t *0.1*60; //time travel at our program



                    timeTravel = (int)time;
                    // worker.RunWorkerAsync((int)(v * kmTravel * 0.1));
                    worker.RunWorkerAsync((int)time);

                    ///------------
                    ///update the new data
                    newKm_from_LastTreatment += kmTravel;
                    Fuel -= kmTravel;
                    km += kmTravel;
                    status = (Status)1;
                    fuelldelek = false;
                    //driverOfBus.SumTime += TimeSpan.FromSeconds(time);
                    return true;
                }
                else //if he cant take this travel
                {
                    status = checkingStatus();
                    return false;
                }
            }
            else //if he cant travels
            {
                
                return false;
            }

        }




        public bool isTimerRun;
        public event PropertyChangedEventHandler PropertyChanged;


        private string _timeleft;
        public string Time_left
        {

            get
            {
                return _timeleft;
            }
            set
            {
                _timeleft = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Time_left"));


            }
        }

        private int _work;
        public int work
        {
            get
            {
                return _work;
            }
            set
            {
                _work = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("work"));

            }

        }



        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            enable = false;
            int length = (int)e.Argument;
            timeToEndWork = length;
            isTimerRun = true;

            visible = "Visible";

            for (int i = 1; i <= (length+1); i++)
            {

                Thread.Sleep(1000);
                worker.ReportProgress(i * 100 / length);

            }
        }


        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            work = (int)e.ProgressPercentage;
            Time_left = timeToEndWork + "s";
            timeToEndWork--;
            helptime = timeToEndWork-1;


        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            isTimerRun = false;
            status = checkingStatus();
            work = 0;
            Time_left = "";
            timeToEndWork = 0;
            enable = true;
            visible = "Hidden";
           // driverOfBus.InTraveling = false;
            //if (driverOfBus.SumTime.TotalSeconds >= 72)//take a break//////////////
            //{
            //    driverOfBus.inBreak = true;
               
            //}
            driverOfBus = "";

        }





    }

}