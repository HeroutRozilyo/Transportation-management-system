using dotNet5781_02_4789_9647.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_02_4789_9647.Properties
{
    //--------------------
    //class that keep lines

    public class BusCompany : LineBus, IEnumerable<LineBus>
    {

        private static Random r = new Random();

        private List<LineBus> companyBus;       //list of lines
        public List<LineBus> ComanyBus
        {
            get { return companyBus; }
        }



        //--------------------------
        //constructor

        public BusCompany() : base()     //deafult construcror
        {
            companyBus = new List<LineBus>();
        }

        public BusCompany(LineBus lineBus)      //constructor tjat get abus to add
        {
            bool find = findLIneAtBusConpany(lineBus);      //to check i the bus allready exsis
            while (!find)       //if the bus exsis. random a new number to the line
            {
                lineBus.NumberID = r.Next(1, 999);
                find = findLIneAtBusConpany(lineBus);
            }
            companyBus = new List<LineBus>();       //add to the list
            companyBus.Add(lineBus);
        }


        public bool findLIneAtBusConpany(LineBus line1)     //find if the line exsis at out list
        {

            foreach (LineBus item in companyBus)        //move in the list
            {
                if (item.NumberID == line1.NumberID)        //if the number id exsis, check if the 2 buses are the same lines with upside dierection
                {
                    if (item.FirstStation == line1.LastStation)
                    {
                        if (item.LastStation == line1.FirstStation)
                        {
                            return true;        //so the line that we want to add not exsis at the list. the upside line exsis.

                        }
                        else return false;
                    }
                    return false;


                }

            }
            return true;

        }

        public bool findLine_BusConpany(int iDLine)     //find if the line exsis according to the number line
        {
            foreach (LineBus item in companyBus)
            {
                if (item.NumberID == iDLine)
                    return true;
            }
            return false;
        }

        public LineBus findHelpAtBusConpany(int iDLine)     //find the line according to the number line and return the line bus
        {
            foreach (LineBus item in companyBus)
            {
                if (item.NumberID == iDLine)
                    return item;
            }
            return null;
        }

        public void addAtBusConpany(LineBus line1)      //add line to the list
        {
            bool find = findLIneAtBusConpany(line1);        //find if the line allresdy exsis

            if (find)
            {
                companyBus.Add(line1);
            }

            else new ArgumentException(string.Format("{0} NumberLine exist already", line1.NumberID));
        }


        public void deleteAtBusConpany(int iDLine)      //delete bus from the list
        {

            LineBus a = findHelpAtBusConpany(iDLine);       //find if the line exsis
            if (a != null)
            {
                companyBus.Remove(a);
            }
            else new ArgumentException(string.Format("{0} NumberLine not exist already", iDLine));

        }

        public List<int> WhichBusAtTheSTation(int id)       //find which bus path at specific station
        {
            List<int> temp = new List<int>();
            foreach (LineBus item in companyBus)
            {
                bool a = item.findStion(id);        //find if the bus path at this station
                if (a)
                    temp.Add(item.NumberID);        //if the bus have this station so add this bus to another list that keep all the station that move at the station

            }
            if (temp == null)
                throw new ArgumentException(string.Format("{0} Number of station not exist already", id));
            else
                return temp;      //return the number line at the station
        }


        public void sortBus()       //sort the list busses according to the travel time. use func comperto
        {
            companyBus.Sort();
        }


        //public BusCompany sortBus()
        //{
        //    BusCompany sortList = new BusCompany();//the sort list 
        //    BusCompany temp = new BusCompany();
        //    temp=this;//list to change
        //    LineBus smaller = new LineBus();//parameter that save the smaller line
        //    foreach (LineBus hisony in temp)
        //    {
        //        smaller = hisony;//save the this at the list
        //        foreach (LineBus item in temp)
        //        {
        //            LineBus a = smaller.Compare(item);
        //            smaller = a;

        //        }
        //        //BusStation findTHis = sortList.findHelpAtBusConpany(smaller.NumberID);
        //        //if (findTHis == null)

        //            sortList.addAtBusConpany(smaller);
        //            temp.deleteAtBusConpany(smaller.NumberID); 

        //    }
        //    return sortList;
        //}

        public IEnumerator<LineBus> GetEnumerator()
        {
            foreach (LineBus item in companyBus)
            {
                yield return item;
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }



        public LineBus this[int index]
        {
            //    get
            //    {
            //        LineBus lineBus = default(LineBus);
            //        lineBus = companyBus.Find(bus => bus.NumberID == index);

            //        if (lineBus == null)
            //        {
            //            ArgumentNullException exception = new ArgumentNullException("index", "Kav lo kayam");
            //            exception.Data["LineNumber"] = index;
            //            throw exception;
            //        }
            //        return lineBus;
            //    }
            get
            {
                if (index >= 0 && index < companyBus.Count)
                {
                    return companyBus[index];
                }
                return 
            }


        }
    }

}
