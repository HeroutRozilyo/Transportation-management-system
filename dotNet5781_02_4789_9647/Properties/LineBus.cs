﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4789_9647.Properties
{
    public class LineBus 
    {

        private List<BusStation> busstations = new List<BusStation>();
        public List<BusStation> BusStations
        {
            get
            {
                List<BusStation> temp = new List<BusStation>(busstations);
                return temp;
            }
        }

        //public readonly List<BusStation> busStations;

        public LineBus()
        {
            //busStations = new List<BusStation>();
        }

        /// <summary>
        /// Line number
        /// </summary>
        public int NumberID { get; set; }
        public BusStation FirstStation { get; private set; }
        public BusStation LastStation { get; private set; }
        public Zone Zone { get; set; }

        public Enum area { get; set; }

        public void AddLast(BusStation busStation)
        {
            busstations.Add(busStation);
            LastStation = busstations[busstations.Count - 1];
        }
        public void AddFirst(BusStation busStation)
        {
            busstations.Insert(0, busStation);
            FirstStation = busstations[0];
        }
        public void Add(int index, BusStation busStation)
        {
            if (index == 0)
            {
                AddFirst(busStation);
            }
            else
            {
                if (index > busstations.Count)
                {
                    throw new ArgumentOutOfRangeException("index", "index should be less than or equal to" + busstations.Count);
                }
                if (index == busstations.Count)
                {
                     busstations.Insert(index, busStation);
                    LastStation = busstations[busstations.Count - 1];
                    //AddLast(busStation);
                }
                else 
                {
                    busstations.Insert(index, busStation);
                }


            }
        }

        public override string ToString()
        {
            string result=" ";
            for(int i=0;i<busstations.Count-1;i++)
            {
                result+= busstations[i].BusStationKey+"  ";

            }

            return "Bus Line: "+NumberID, ",Area: "+area, ",Statins of the bus: "+result;
               
        }

         public void DelLast(BusStation busStation)
        {
              busstations.Add(busStation);
            LastStation = busstations[busstations.Count - 1];
        }
          public void DelFirst(BusStation busStation)
        {
        }
        public void Del(int index, BusStation busStation)
        {
        }

       
    }

}
