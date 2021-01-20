using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace BO
{
    public class ClockS 
    {
        #region singelton
        static readonly ClockS instance = new ClockS();
        static ClockS() { }

        public static ClockS Instance { get => instance; }
        #endregion

        #region clock
        /// <summary>
        /// inner clock. in order to prevenr bother at the clock work
        /// </summary>
        public class Clock
        {
            public TimeSpan Time;
            public Clock(TimeSpan time) => Time = time;//constructor

        }
        #endregion

        #region varieble
        internal volatile bool Cancel;
        private volatile Clock sClock = null;
        internal Clock SClock => sClock;
        private int rate;
        internal int Rate => rate;
        private Stopwatch stoper = new Stopwatch();
        private Action<TimeSpan> observerClock = null;
        public TimeSpan startTime;
        #endregion

        /// <summary>
        /// get the action from the delegate at UI 
        /// </summary>
        internal event Action<TimeSpan> ObserverClock
        {
            add => observerClock = value;
            remove => observerClock = null;
        }
     

       
        /// <summary>
        /// start the work of the clock
        /// </summary>
        public void Start(TimeSpan mstartTime, int mrate)
        {
            startTime = mstartTime;
            sClock = new Clock(startTime);
            rate = mrate;
            Cancel = false;
            stoper.Restart();
            new Thread(clockThread).Start();


        }

        /// <summary>
        /// update the clock when during he work
        /// </summary>
        void clockThread()
        {
            while (!Cancel)
            {
                sClock = new Clock(startTime + new TimeSpan(stoper.ElapsedTicks * rate)); //the rate we wont the clock change- to add to the current time
                observerClock(new TimeSpan(sClock.Time.Hours, sClock.Time.Minutes, sClock.Time.Seconds));//call to the event
                Thread.Sleep(100);
            }
            observerClock = null;
        }
        internal void Stop() { Cancel = true; }


    }
}



