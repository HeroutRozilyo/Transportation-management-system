using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;
using System;

namespace doNet5781_03B_4789_9647
{
    //public class prograss 
    //{
    //    BackgroundWorker worker; 
    //    private int v;
    //    int progras;
    //    public prograss(object vt)
    //    {
            
    //        this.v =(int)vt;
    //        worker = new BackgroundWorker();
    //        worker.DoWork += worker_Dowork;
    //        worker.ProgressChanged += worker_prograss;
    //        worker.RunWorkerCompleted += worker_Run;

    //        worker.WorkerReportsProgress = true;
    //        worker.WorkerSupportsCancellation = true;
           
    //        worker.RunWorkerAsync();
            
    //    }


    //    private void worker_Run(object sender, RunWorkerCompletedEventArgs e)
    //    {
    //        if (e.Cancelled == false)
    //        {
    //            if (e.Error != null)
    //            {
    //                MessageBox.Show("ERROR");
    //            }
    //            //else
    //            //{
    //            //    long result = (long)e.Result;
    //            //    if (result < 1000)
    //            //    {

    //            //    }
    //            //    else
    //            //    {

    //            //    }
    //            //}
    //        }


    //    }



    //    private void worker_prograss(object sender, ProgressChangedEventArgs e)
    //    {
    //        //= (int)e.ProgressPercentage;
    //    }


    //    private void worker_Dowork(object sender, DoWorkEventArgs e)
    //    {
    //        Stopwatch stopwatch = new Stopwatch();
    //        stopwatch.Start();

    //        //int lengh = (int)e.Argument;
    //        int lengh = v;

    //        for (int i = 1; i <= lengh; i++)
    //        {
    //            if (worker.CancellationPending == true)
    //            {
    //                e.Cancel = true;
    //                e.Result = stopwatch.ElapsedMilliseconds;
    //                break;
    //            }
    //            else
    //            {
    //                System.Threading.Thread.Sleep(500);
    //                worker.ReportProgress(i * 100 / lengh);
    //            }

    //        }
    //        e.Result = stopwatch.ElapsedMilliseconds;
    //    }


    //}

}

    



