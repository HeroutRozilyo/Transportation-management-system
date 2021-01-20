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
        /// inner clock. in order to prevenr bother at the clock work,Prevent disruptions to the clock
        /// </summary>
        public class Clock
        {

            public TimeSpan Time;
            public Clock(TimeSpan time) => Time = time;//constructor

        }
        #endregion

        #region varieble
        /// <summary>
        /// the Rate of the clock
        /// </summary>
        private int rate;
        internal int Rate => rate;
        /// <summary>
        /// The watch itself - with a soft stop
        /// </summary>
        private volatile Clock sClock = null;
        internal Clock SClock => sClock;

        /// <summary>
        /// Runs the process itself
        /// </summary>
        private Stopwatch stoper = new Stopwatch();

        /// <summary>
        /// An action record that communicates with the delagete in the ui layer
        /// </summary>
        private Action<TimeSpan> observerClock = null;

        /// <summary>
        /// the start time of the clock
        /// </summary>
        public TimeSpan startTime;

        /// <summary>
        /// Stop the clock - soft stop
        /// </summary>
        internal volatile bool Cancel;
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
            stoper.Restart();// start the action
            new Thread(clockThread).Start();//start the theard


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



