using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLGui
{
   public class Timer: INotifyPropertyChanged
    {
        public TimeSpan timer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
