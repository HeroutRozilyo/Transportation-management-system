using BlAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private IBL bl;
        private BO.User userNow=new BO.User();
        public static readonly DependencyProperty BoolStartProperty = DependencyProperty.Register("BoolStart", typeof(Boolean), typeof(UserWindow));
        private bool BoolStart
        {
            get => (bool)GetValue(BoolStartProperty);
            set => SetValue(BoolStartProperty, value);
        }

        BackgroundWorker timerWorker;
        Thread workerThread;
        TimeSpan startTimeSimulator;
        int rate;
       
        
        public UserWindow()
        {
            InitializeComponent();
        }

        public UserWindow(IBL bl, BO.User users)
        {
            InitializeComponent();
            this.bl = bl;
            userNow = users;
            NameTextBlock.Text = users.UserName;
            BoolStart = true;
            timerWorker = new BackgroundWorker();

            timerWorker.DoWork += (s, e) =>
            {
                workerThread = Thread.CurrentThread;
                bl.StartSimulator(startTimeSimulator, rate, (time) => timerWorker.ReportProgress(0, time));
                while (!timerWorker.CancellationPending)
                    Thread.Sleep(1000000);



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

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if(frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        private void Disengagement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void forward_Click(object sender, RoutedEventArgs e)
        {
            if (frame.CanGoForward)
            {
                frame.GoForward();
            }
        }

        private void accountDatiels_Click(object sender, RoutedEventArgs e)
        {

        }

        private void contantUs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void line_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new LineUser(bl));
        }

        private void station_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new User());
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if(BoolStart)
            {
                if (!TimeSpan.TryParse(TimerTextBox.Text, out startTimeSimulator) || !int.TryParse(rateTextBox.Text, out rate))
                {
                    MessageBox.Show("hh:mm:ss-בבקשה להכניס זמן בתבנית הבאה ","timer",MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                timerWorker.RunWorkerAsync();
                BoolStart = false;
                startButton.Content = "עצור";
            }
            else
            {
                workerThread.Interrupt();
                timerWorker.CancelAsync();
                
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
