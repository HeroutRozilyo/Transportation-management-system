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
    public class Drivers
    {
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
                    throw new ArgumentOutOfRangeException("id not valid");

            }
        }
        public string Name
        {
            get { return name; }
            set
            { name = value; }
        }
        private bool InBreak;
        public bool inBreak/////////////////////
        {
            get { return InBreak; }
            set
            {
                InBreak = value;
            }
        }

        public bool InTraveling
        {
            get { return inTraveling; }
            set
            {
                inTraveling = value;
            }
        }
        private string stringTraveling;

        public string StringTraveling
        {
            get { return stringTraveling; }
            set
            {
                if (inTraveling)
                {
                    stringTraveling = "In Travelling";
                    //worker.RunWorkerAsync(12);
                }
                else
                {
                    if (SumTime.TotalSeconds >= 72)//take a break//////////////
                    {

                        stringTraveling = "In Break";
                        inBreak = true;
                        SumTime = TimeSpan.Zero;

                    }
                    else stringTraveling = "Available for travel";


                }
            }
        }
         
         public Drivers(string num1)//constructor
        {
            name = num1;
            id = r.Next(100000000, 1000000000);
            inTraveling = false;
            sumTime = TimeSpan.Zero;
            stringTraveling = "Available for travel";

        }
        public Drivers()//constructor
        {
            name = "There no name";
            id = r.Next(100000000, 1000000000);
            inTraveling = false;
            stringTraveling = "Available for travel";
            sumTime = TimeSpan.Zero;
        }


        //public int start
        //{

        //    get
        //    {
        //        worker.RunWorkerAsync(12);
        //        return 1;
        //    }
        //}










        //public bool isTimerRun;
        //public event PropertyChangedEventHandler PropertyChanged;


        //private string _timeleft;
        //public string Time_left
        //{

        //    get
        //    {
        //        return _timeleft;
        //    }
        //    set
        //    {
        //        _timeleft = value;
        //        if (PropertyChanged != null)
        //            PropertyChanged(this, new PropertyChangedEventArgs("Time_left"));


        //    }
        //}

        //private int _work;
        //public int work
        //{
        //    get
        //    {
        //        return _work;
        //    }
        //    set
        //    {
        //        _work = value;
        //        if (PropertyChanged != null)
        //            PropertyChanged(this, new PropertyChangedEventArgs("work"));

        //    }

        //}
        //public bool Enable;
        //public bool enable
        //{
        //    get { return Enable; }
        //    set
        //    {
        //        Enable = value;
        //        if (PropertyChanged != null)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs("enable"));
        //        }

        //    }
        //}

        //private string visibility;
        //public string visible
        //{
        //    get
        //    {
        //        return visibility;
        //    }
        //    set
        //    {
        //        visibility = value;
        //        if (PropertyChanged != null)
        //            PropertyChanged(this, new PropertyChangedEventArgs("visible"));

        //    }

        //}


        //BackgroundWorker worker = new BackgroundWorker();



        //int timeToEndWork;

        //private void Worker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    enable = false;
        //    int length = (int)e.Argument;
        //    timeToEndWork = length;
        //    isTimerRun = true;

        //    visible = "Visible";

        //    for (int i = 1; i <= (length + 1); i++)
        //    {

        //        Thread.Sleep(1000);
        //        worker.ReportProgress(i * 100 / length);

        //    }
        //}


        //private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    work = (int)e.ProgressPercentage;
        //    Time_left = timeToEndWork + "s";
        //    timeToEndWork--;



        //}

        //private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{

        //    isTimerRun = false;
          
        //    work = 0;
        //    Time_left = "";
        //    timeToEndWork = 0;
        //    enable = true;
        //    visible = "Hidden";
                  

        //}
    }
}
