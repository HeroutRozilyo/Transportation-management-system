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
        // static public int GlobalKM { get; private set; }

        static Random r = new Random(DateTime.Now.Millisecond);


        private const int MAX_KM = 20000;
        private const int FULLTANK = 1200;

        public DateTime StartingDate
        {
            set;
            get;

        }



        BackgroundWorker worker = new BackgroundWorker();
        
        public bool isTimerRun;
        public event PropertyChangedEventHandler PropertyChanged;

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            enable = false;
            int length = (int)e.Argument;
            timeToEndWork = length;
            isTimerRun = true;
            //timer = "שניות " + timeToEndWork + " נשארו עוד";

            for (int i =0; i <= length; i++)
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
            status = (Status)0;
            work = 0;
            Time_left = "";
            timeToEndWork = 0;
            enable = true;
        }



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

                //NotifyPropertyChanged("Timeleft");
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


        



        private string license;
        private int km;
        int timeToEndWork;



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




        //------
        // set and get from our variable

        private int f;
        public int Fuel
        {
            get { return f; }
            set
            {
                f = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Fuel"));
            }
        }



        private int newkm;
        public int newKm_from_LastTreatment
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



  
        public int Km
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
            
        public string strstartingdate
        {
            set
            {
                strLastTreat = value;
             
            }
            get 
            {
                string result= " ";
                result = String.Format("{0}/{1}/{2}", StartingDate.Day, StartingDate.Month,StartingDate.Year);
                return result;
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

                    throw new Exception("license not valid");/////////////////////////////////////////////////////////////!!!!!!!!!!!!!!!!!!!!!
                
                }
            }
        }


        public Bus() //defult constructor
        {
            int num = 10000000;
            string a = num.ToString();

            status = (Status)0;
            StartingDate = DateTime.Today;
            License=a;

            lastTreat = Last_tratment();
            Fuel = FULLTANK;
            newKm_from_LastTreatment = 0;
            km = 0;
            strstartingdate = String.Format("{0}/{1}/{2}", StartingDate.Day, StartingDate.Month, StartingDate.Year);
            enable = true;
        }

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
            lastTreat = new DateTime(2020, r.Next(1, DateTime.Today.Month), r.Next(1, DateTime.Today.Day));
            status = checkingStatus();
            strstartingdate = String.Format("{0}/{1}/{2}", StartingDate.Day, StartingDate.Month, StartingDate.Year);
            enable = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

        }

        public Status checkingStatus()
        {
            if (this.Fuel != 0 && newKm_from_LastTreatment < MAX_KM && lastTreat.AddYears(1) >= DateTime.Today)
            {
                return Status.READY_TO_TRAVEL;
            }
            else return Status.Unfit;
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
            strLastTreat = a.strLastTreat;

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

            worker.RunWorkerAsync(144);

            status = (Status)3;
            this.Last_tratment();
            
            strLastTreat= String.Format("{0}/{1}/{2}", this.lastTreat.Day, this.lastTreat.Month, this.lastTreat.Year);

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

            //.Background = Brushes.Orange;

            worker.RunWorkerAsync(12);
            
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
            if (newKm_from_LastTreatment < MAX_KM && lastTreat.AddYears(1)>=DateTime.Today) //if he didnt need go to treatment
            {
                if (newKm_from_LastTreatment + kmTravel < MAX_KM && Fuel - kmTravel > 0) //if he can take this travel
                {
                    int time =0;
                    int v = r.Next(20, 51); //velocitui of this bus
                    int t = kmTravel / v; //time of this travel.This time is in hour

                    time =(int)(t/3.6); //time travel at our program
                    t = (int)(time / 60);


                    worker.RunWorkerAsync((int)(v*kmTravel*0.1)); //=============================================


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
                //this.treatment();
                return false;
            }
        
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