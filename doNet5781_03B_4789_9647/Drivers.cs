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

        public bool InTraveling
        {
            get { return inTraveling; }
            set
            {
                inTraveling = value;
            }
        }
         
         public Drivers(string num1)//constructor
        {
            name = num1;
            id = r.Next(100000000, 1000000000);
            inTraveling = false;
            sumTime = TimeSpan.Zero; 

        }
        public Drivers()//constructor
        {
            name = "There no name";
            id = r.Next(100000000, 1000000000);
            inTraveling = false;
            sumTime = TimeSpan.Zero;
        }

    }
}
