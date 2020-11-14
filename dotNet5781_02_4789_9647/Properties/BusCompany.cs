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
    public class BusCompany : LineBus, IEnumerable<LineBus>
    {
        private List<LineBus> companyBus;
        public List<LineBus> ComanyBus
        {
            get { return companyBus; }
            set 
        }

       



        public BusCompany() :base()
        {
            companyBus = new List<LineBus>();
        }

        public BusCompany(LineBus lineBus)
        {
            companyBus = new List<LineBus>();
            companyBus.Add(lineBus);
        }


        public bool findLIneAtBusConpany(LineBus line1)
        {

            foreach (LineBus item in companyBus)
            {
                if (item.NumberID == line1.NumberID)
                {
                    if (item.FirstStation == line1.LastStation)
                    {
                        if (item.LastStation == line1.FirstStation)
                        {
                            return true;

                        }
                        else return false;
                    }
                    return false;


                }

            }
            return true;

        }

        public LineBus findHelpAtBusConpany(int iDLine)
        {
            foreach (LineBus item in companyBus)
            {
                if (item.NumberID == iDLine)
                    return item;
            }
            return null;
        }

        public void addAtBusConpany(LineBus line1)
        {
            bool find = findLIneAtBusConpany(line1);

            if (!find)
            {
                companyBus.Add(line1);
            }

            else new ArgumentException(string.Format("{0} NumberLine exist already", line1.NumberID));
        }


        public void deleteAtBusConpany(int iDLine)
        {

            LineBus a = findHelpAtBusConpany(iDLine);
            if (a != null)
            {
                companyBus.Remove(a);
            }
            else new ArgumentException(string.Format("{0} NumberLine not exist already", iDLine));

        }

        public List<int> WhichBusAtTheSTation(int id)
        {
            List<int> temp = null;
            foreach (LineBus item in companyBus)
            {
                bool a = item.findStion(id);
                if (a)
                    temp.Add(item.NumberID);

            }
            if (temp == null)
                throw new ArgumentException(string.Format("{0} Number of station not exist already", id));
            else
                return temp;
        }

        public BusCompany sortBus()
        {
            BusCompany sortList = new BusCompany();//the sort list 
            BusCompany temp = new BusCompany();
            temp=this;//list to change
            LineBus smaller = null;//parameter that save the smaller line
            foreach (LineBus hisony in temp)
            {
                smaller = hisony;//save the this at the list
                foreach (LineBus item in temp)
                {
                    LineBus a = smaller.Compare(item);
                    smaller = a;

                }
                //BusStation findTHis = sortList.findHelpAtBusConpany(smaller.NumberID);
                //if (findTHis == null)
                {
                    sortList.addAtBusConpany(smaller);
                    temp.deleteAtBusConpany(smaller.NumberID); 
                }
            }
            return sortList;
        }

        public IEnumerator<LineBus> GetEnumerator()
        {
            foreach(LineBus item in companyBus)
            {
                yield return item;
            }
           // return this.companyBus.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }



        public LineBus this[int index]
        {
            get
            {
                LineBus lineBus = default(LineBus);
                lineBus = companyBus.Find(bus => bus.NumberID == index);
                if (lineBus == null)
                {
                    ArgumentNullException exception = new ArgumentNullException("index", "Kav lo kayam");
                    exception.Data["LineNumber"] = index;
                    throw exception;
                }
                return lineBus;
            }

        }

        //public IEnumerator<LineBus> GetEnumerator() => new BusCompanyIEnumator();

        //private class BusCompanyIEnumator : IEnumerator<LineBus>
        //{
        //    private List<LineBus> arr;
        //    private int index;
        //    private int place;


        //    public BusCompanyIEnumator()
        //    {
        //        arr = new List<LineBus>();
        //        index = -1;
        //        place = 0;
        //    }

        //    public BusCompanyIEnumator(List<LineBus> b, int c)
        //    {
        //        arr = b;
        //        place = c;
        //        if (c > 0)
        //            index = 0;
        //        else
        //            index = -1;
        //    }


    //        public LineBus Current => arr[index];
    //        object IEnumerator.Current => Current;

    //        public void Dispose()
    //        {
    //            throw new NotImplementedException();
    //        }

    //        public bool MoveNext()
    //        {
    //            index++;
    //            if (index > place)
    //            {
    //                Reset();
    //                return false;
    //            }
    //            return true;
    //        }

    //        public void Reset()
    //        {
    //            index = -1;
    //        }
    //    }


    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }

    //    //IEnumerator<LineBus> IEnumerable<LineBus>.GetEnumerator()
    //    //{
    //    //    throw new NotImplementedException();
    //    //}
    //}








}
}
