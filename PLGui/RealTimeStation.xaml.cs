using BlAPI;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PLGui
{
    /// <summary>
    /// Interaction logic for RealTimeStation.xaml
    /// </summary>
    public partial class RealTimeStation : Window, INotifyPropertyChanged
    {
        private IBL bl;
        private Station stationData1;
        private bool boolStart;
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
        public Timer timer = new Timer();
        private IEnumerable<LineTiming> lineAtStation;

        public RealTimeStation()
        {
          InitializeComponent();
        }

        public RealTimeStation(IBL bl, Station stationData1)
        {
            this.DataContext = this;
            InitializeComponent();
            this.bl = bl;
            this.stationData1 = stationData1;

           

          

            BoolStart = true;
            timerWorker = new BackgroundWorker();

            timerWorker.DoWork += (s, e) =>
            {
                workerThread = Thread.CurrentThread;
                bl.StartSimulator(startTimeSimulator, rate, (time) => timerWorker.ReportProgress(0, time));
                while (!timerWorker.CancellationPending) try { Thread.Sleep(1000000); }
                    catch (ThreadInterruptedException a)
                    {

                    }
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

        /// <summary>
        /// Responsible for updating the textBox at the updated time
        /// </summary>
        private void timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
          
            TimeSpan timeSpan = (TimeSpan)e.UserState;//userState-  member to pass more information back to the UI for updating on a progressChanged call
            TimerTextBox.Text = String.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

            lineAtStation = bl.GetLineStationLineTimer(stationData1, timeSpan);
            RealTimeStationLine.ItemsSource = lineAtStation;
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (BoolStart)
            {
                if (!TimeSpan.TryParse(TimerTextBox.Text, out startTimeSimulator) || !int.TryParse(rateTextBox.Text, out rate))
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

        private void TimerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x) || ':'.Equals(x));
        }

        private void rateTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(x => Char.IsDigit(x) || '.'.Equals(x));
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (timerWorker.IsBusy)
            {
                timerWorker.CancelAsync();
                workerThread.Interrupt();
            }
        }

    }
}
