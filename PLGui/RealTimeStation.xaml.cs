using BlAPI;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace PLGui
{
    /// <summary>
    /// window of the simulation at oue program
    /// </summary>
    public partial class RealTimeStation : Window, INotifyPropertyChanged
    {
        #region varieble
        private IBL bl;
        private Station stationData1;
        private bool boolStart; //in order to  know if the button need be on start or stop
        public bool BoolStart
        {
            get => boolStart;
            set
            {
                boolStart = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("BoolStart")); ;
            }
        }

        BackgroundWorker timerWorker;
        Thread workerThread;
        TimeSpan startTimeSimulator;
        int rate;
        public event PropertyChangedEventHandler PropertyChanged;
        private List<LineTiming> lineAtStation = new List<LineTiming>();
        private List<int> goodLine = new List<int>();
        private IEnumerable<Object> yellowBoard;
        #endregion

        #region constructor
        [Obsolete("notuse",true)]
        public RealTimeStation()
        {
            InitializeComponent();
        }

        //stationData1=the station that the user choose
        public RealTimeStation(IBL bl, Station stationData1)
        {
            this.DataContext = this;
            InitializeComponent();
            this.bl = bl;
            this.stationData1 = stationData1;

            yellowBoard = bl.GetYellowBoard(stationData1);
            yellow.ItemsSource = yellowBoard;
            StationCode.Text = stationData1.Code.ToString();

            //simulator clock make the BackgroundWorker
            BoolStart = true;
            timerWorker = new BackgroundWorker();
            timerWorker.DoWork += (s, e) =>
            {
                workerThread = Thread.CurrentThread;
                bl.StartSimulator(startTimeSimulator, rate, (time) => timerWorker.ReportProgress(0, time));
                while (!timerWorker.CancellationPending)
                    try { Thread.Sleep(1000000); }
                    catch (ThreadInterruptedException) { }
                    
            };
            timerWorker.ProgressChanged += timer_ProgressChanged;
            timerWorker.RunWorkerCompleted += (s, e) =>
            {
                BoolStart = true;
                startButton.Content = "התחלה";
                bl.StopSimulator();
            };
            timerWorker.WorkerReportsProgress = true;
            timerWorker.WorkerSupportsCancellation = true;
            
        }
        #endregion

        #region simulator
        /// <summary>
        /// Responsible for updating the textBox at the updated time
        /// </summary>
        private void timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            TimeSpan timeSpan = (TimeSpan)e.UserState;//userState-  member to pass more information back to the UI for updating on a progressChanged call
            TimerTextBox.Text = String.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            lineAtStation = bl.GetLineStationLineTimer(stationData1, timeSpan).ToList(); //all the frequency line arrivle 
            int ListCount = (lineAtStation.Count() > 5) ? 5 : lineAtStation.Count(); //the first five line
            RealTimeStationLine.ItemsSource = lineAtStation.GetRange(0, ListCount);

        }

        /// <summary>
        /// to start simulation clock
        /// </summary>
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (BoolStart)
            {
                if (!TimeSpan.TryParse(TimerTextBox.Text, out startTimeSimulator) || !int.TryParse(rateTextBox.Text, out rate)) //check if the time input is wrong
                {
                    MessageBox.Show("hh:mm:ss-בבקשה להכניס זמן בתבנית הבאה ", "timer", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                timerWorker.RunWorkerAsync();
                BoolStart = false;
                startButton.Content = "עצור";
            }
            else
            {
                timerWorker.CancelAsync();
                workerThread.Interrupt();
            }
        }
        #endregion

        #region textbox input correct
        /// <summary>
        /// for the timer
        /// </summary>
        private void TimerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x) || ':'.Equals(x));
        }

        /// <summary>
        /// for the rate
        /// </summary>
        private void rateTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x) || '.'.Equals(x));
        }

        #endregion

        #region moreFunc
        /// <summary>
        /// when we close the window during the clock work ae need close the prograss
        /// </summary>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (timerWorker.IsBusy)
            {
                workerThread.Abort();
                timerWorker.CancelAsync();
                workerThread.Interrupt();
                bl.StopSimulator();
            }

        }
        #endregion

    }
}
