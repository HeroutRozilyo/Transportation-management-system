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
    public class Drivers: INotifyPropertyChanged
    {
        BackgroundWorker worker = new BackgroundWorker();
        private TimeSpan sumTime;
        public TimeSpan SumTime
        {
            get { return sumTime; }
            set { sumTime = value; }
        }
        static Random r = new Random();
        private string name;
        private bool inTraveling;
        public Drivers myProperty   // CS0053  
        {
            get
            {
                return this;
            }
        }

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
                    throw new ArgumentOutOfRangeException("ID not valid, please enter ID with 9 digite");

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));

            }
        }
        public string Name1
        {
            get { return name; }
            set
            { name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name1"));
            }
        }
        private bool InBreak;
        public bool inBreak/////////////////////
        {
            get { return InBreak; }
            set
            {
                InBreak = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("inBreak"));

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



        public void TakeBreak()
        {
            enable = false;
             SumTime = TimeSpan.Zero;
            StringTraveling = "In Break";
            worker.RunWorkerAsync(144);
            inBreak = true;
            InTraveling = true;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("InTraveling"));
          
        }


        public void help(int a)
        {
            enable = false;
            StringTraveling = "In Travelling";
            worker.RunWorkerAsync(a);
            InTraveling = true;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("InTraveling"));

        }




        public bool InTraveling
        {
            get { return inTraveling; }
            set
            {
                inTraveling = value;
                if (InTraveling)
                {

                }
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("InTravelling"));
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
        public Drivers(int d,string num1)//constructor
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


       
        int timeToEndWork;

        public bool isTimerRun;

        private string visible;
        public string Visible
        {
            get { return visible; }
            set { visible = value; }


        }




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

            //enable = false;
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
            enable = true;
             work = 0;
            Time_left = "";
            timeToEndWork = 0;

            visible = "Hidden";
            InTraveling = false;
            inBreak = false;
            if (SumTime.TotalSeconds >= 72)//take a break//////////////
            {
                TakeBreak();
                inBreak = true;

            }
            else 
                StringTraveling = "Available for travel";

        }





    }

}

    
