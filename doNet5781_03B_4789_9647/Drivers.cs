using System;
using System.ComponentModel;
using System.Threading;

namespace doNet5781_03B_4789_9647
{
    public class Drivers : INotifyPropertyChanged
    {
        /// <summary>
        /// variebles defination
        /// </summary>
        /// 
        BackgroundWorker worker = new BackgroundWorker();


        static Random r = new Random();

        private TimeSpan sumTime; //keep the sum of time that this driver work until he go to break.
        public TimeSpan SumTime
        {
            get { return sumTime; }
            set { sumTime = value; }
        }


        private bool inTraveling;

        private int id;
        public int Id
        {
            get
            {
                return id;
            }


            set
            {
                if (value.ToString().Length == 9)
                    id = value;
                else
                    throw new ArgumentException(string.Format("ID number is incorrect, please enter ID number with only 9 digits"));

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));

            }
        }

        private string name;
        public string Name1
        {
            get { return name; }
            set
            {
                name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name1"));
            }
        }

        private string stringTraveling;
        public string StringTraveling
        {
            get { return stringTraveling; }
            set
            {

                stringTraveling = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("StringTraveling"));

            }
        }

        /// <summary>
        /// constructors
        /// </summary>

        public Drivers()//constructor
        {
            name = "There no name";
            id = r.Next(100000000, 1000000000);
            inTraveling = false;
            stringTraveling = "Available for travel";
            sumTime = TimeSpan.Zero;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            visible = "Hidden";
            enable = true;
        }
        public Drivers(string num1)//constructor
        {
            name = num1;
            id = r.Next(100000000, 1000000000);
            inTraveling = false;
            sumTime = TimeSpan.Zero;
            stringTraveling = "Available for travel";
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            visible = "Hidden";
            enable = true;

        }
        public Drivers(int d, string num1)//constructor
        {
            name = num1;
            Id = d;
            inTraveling = false;
            sumTime = TimeSpan.Zero;
            stringTraveling = "Available for travel";
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            visible = "Hidden";
            enable = true;

        }



        /// <summary>
        /// help metod to our variable
        /// </summary>

        private bool InBreak; //return if the driver is at break or not
        public bool inBreak
        {
            get { return InBreak; }
            set
            {
                InBreak = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("inBreak"));

            }
        }


        public bool InTraveling //return if the bus at travel or not
        {
            get { return inTraveling; }
            set
            {
                inTraveling = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("InTraveling"));
            }
        }

        public void TakeBreak()//func that send the driver to break if he work more that 12h= 72s at our program
        {

            enable = false;
            SumTime = TimeSpan.Zero;
            StringTraveling = "In Break";
            worker.RunWorkerAsync(144);
            inBreak = true;
            InTraveling = true;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("InTraveling"));
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("SumTime"));

        }
        public static int counter = 0;

        public void help(int a)//send thr driver to start prograss. in order to do a prograss bar at driver window when driver have a travel.
        {

            if (worker.IsBusy)//after ew closed the list Window and open
            {
            }
            else worker.RunWorkerAsync(a);//for the first time we open the window
            enable = false;
            StringTraveling = "In Travelling";

            InTraveling = true;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("InTraveling"));

        }



        /// <summary>
        /// thread to driver.. 
        /// </summary>
        /// 

        public event PropertyChangedEventHandler PropertyChanged;

        int timeToEndWork;
        public int TimeToEndWork
        {
            get { return timeToEndWork; }
            set { timeToEndWork = 0; }
        }




        public bool isTimerRun;


        public bool Enable;//varieble that indicate us if tohidde the buttons on the window or not. whan it equal to true the buttons work.
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

        private string visible; //variable that connect to the vasibility op prograss ber at main window xamle
        public string Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Visible"));
            }


        }

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


            int length = (int)e.Argument;
            timeToEndWork = length;
            isTimerRun = true;

            visible = "Visible";

            for (int i = 1; i <= (length + 1); i++)
            {

                Thread.Sleep(1000);
                if (length != 0)
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
            enable = true;
            work = 0;
            Time_left = "";
            timeToEndWork = 0;
            //
            Visible = "Hidden";
            InTraveling = false;
            inBreak = false;
            counter--;
            if (SumTime.TotalSeconds >= 72)//take a break//////////////
            {
                BackgroundWorker worker = new BackgroundWorker();
                TakeBreak();
                inBreak = true;
                Visible = "Visible";
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("inBreak"));

            }
            else
                StringTraveling = "Available for travel";

        }




        public Drivers myProperty   // CS0053  
        {
            get
            {
                return this;
            }
        }

    }

}


