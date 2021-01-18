using System;
using System.ComponentModel;
using System.Threading;


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
        //----------------

        BackgroundWorker worker = new BackgroundWorker();
        static Random r = new Random(DateTime.Now.Millisecond);
        private const int MAX_KM = 20000;
        private const int FULLTANK = 1200;


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
        }

        private Status s; //the status of this bus
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


        private double newkm; //return the new km to travel
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


        private string license; //return licence
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
                    if (StartingDate.Year >= 2018)
                        throw new ArgumentException("The new licence is not valid,\n please enter again number licence with 8 digite");
                    else throw new ArgumentException("The new licence is not valid,\n please enter again number licence with 7 digite");
                }
            }
        }

        private double km; //return the sum of km thatthis bus did
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
                    throw new ArgumentException("KM must to be a positive number");
            }
        }


        private string driverOfBus;   //return the name of driver bus  
        public string NameDriver
        {
            get { return driverOfBus; }
            set
            {
                driverOfBus = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("NameDriver"));
            }
        }

        private DateTime Lasttreat; //return the last date of treat
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


        private int TimeTravel; //return the time to this travel
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

        //cobstructor to the bus with to value. we using most the time at this constructor
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
            lastTreat = new DateTime(r.Next(2020, DateTime.Today.Year), r.Next(1, DateTime.Today.Month), r.Next(1, DateTime.Today.Day));
            status = checkingStatus();
            strstartingdate = String.Format("{0}/{1}/{2}", StartingDate.Day, StartingDate.Month, StartingDate.Year);
            enable = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            visible = "Hidden";
            driverOfBus = ("");
        }

        public override string ToString()
        {
            return String.Format("license is: {0,-10}, starting date: {1}", License, StartingDate);
        }



        /// <summary>
        /// help method to our variebls
        /// </summary>

        public DateTime Last_tratment() //update to the current date
        {
            lastTreat = DateTime.Today;
            return lastTreat;
        }
        public DateTime Last_tratment(DateTime checkup) //update specific date. when we create bus who using this func
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
        } //return the date at string. in order show at data bus window.
        //
        public Status checkingStatus()//return the current status of this bus
        {
            DateTime a = lastTreat;
            if (this.Fuel != 0 && newKm_from_LastTreatment < MAX_KM && a.AddYears(1) >= DateTime.Today)
            {
                return Status.READY_TO_TRAVEL;
            }
            else return Status.Unfit;
        }

        public string fuelString() //return the fuel of bus at string, in order to show that at main window.
        {
            string str = " ";
            // int.TryParse(string a, out Fuel);
            str = "The amount of fuel at this Bus:\n";
            str += Fuel;
            str += "\n Do you want to refull ?";
            return str;
        }




        /// <summary>
        /// functions of the bus
        /// </summary>
        public void treatment()/// func that do treatment to the bus
        {

            worker.RunWorkerAsync(144);//one day

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

            worker.RunWorkerAsync(12);

            status = (Status)2;



            Fuel = FULLTANK;
        }

        public bool Take_travel(double kmTravel)//check if the bus can take the travel
        {
            if (newKm_from_LastTreatment <= MAX_KM && lastTreat.AddYears(1) >= DateTime.Today) //if he didnt need go to treatment
            {
                if (newKm_from_LastTreatment + kmTravel <= MAX_KM && Fuel - kmTravel >= 0) //if he can take this travel
                {
                    double time = 0;
                    int v = r.Next(20, 51); //velocitui of this bus
                    double t = kmTravel / (v); //time of this travel.This time is in hour

                    time = t * 0.1 * 60; //time travel at our program



                    timeTravel = (int)time;
                    // worker.RunWorkerAsync((int)(v * kmTravel * 0.1));
                    worker.RunWorkerAsync((int)time);

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
                    status = checkingStatus();
                    return false;
                }
            }
            else //if he cant travels
            {

                return false;
            }

        }




        /// <summary>
        /// thread to this bus.. 
        /// </summary>
        /// 

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

        public bool Enable; //varieble that indicate us if tohidde the buttons on the window or not. whan it equal to true the buttons work.
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


        public bool isTimerRun;
        public event PropertyChangedEventHandler PropertyChanged;


        private string _timeleft; //string that return the time left of the thread. in order to show on the window.
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

        int timeToEndWork; //in order to return the time that the thread neeed to keep going. we neeed that inorder to do the same thread at driver window
        public int TimeToEndWork
        {
            get { return timeToEndWork; }
            set { }
        }

        private int _work; //thread worker
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

            for (int i = 1; i <= (length + 1); i++)
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
            driverOfBus = "";

        }

    }

}