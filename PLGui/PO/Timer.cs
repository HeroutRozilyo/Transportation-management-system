using System;
using System.ComponentModel;

namespace PLGui
{
    public class Timer : INotifyPropertyChanged
    {
        public TimeSpan timer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
