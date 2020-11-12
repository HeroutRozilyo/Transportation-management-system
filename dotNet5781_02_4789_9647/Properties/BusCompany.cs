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
    public class BusCompany :LineBus,IEnumerable<LineBus>
    {
        private  List<LineBus> companyBus;

      //  private List<int> numbers = new List<int>();

        public BusCompany()
        {
            companyBus = new List<LineBus>();
        }
        public bool findLIne(LineBus line1)
        {
           
            foreach(LineBus item in companyBus)
            {
                if(item.NumberID==line1.NumberID)
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
        public void add(LineBus line1)
        {
            bool find = findLIne(line1);
            
            if (find)
            {
                companyBus.Add(line1);
            }

            else new ArgumentException(string.Format("{0} NumberLine exist already", line1.NumberID));
        }
        public LineBus findHelp(int iDLine)
        {
            foreach(LineBus item in companyBus)
            {
                if (item.NumberID == iDLine)
                    return item;
            }
            return null;
        }
        public void delete(int iDLine)
        {

            LineBus a = findHelp(iDLine);
            if(a!=null)
            {
                companyBus.Remove(a);
            }
            else new ArgumentException(string.Format("{0} NumberLine not exist already", iDLine));

        }
        public List<int> WhichBusAtTheSTation(int id)
        {
            List<int> temp=null;
            foreach (LineBus item in companyBus)
            {
                bool a = item.findStion(id);
                    if(a)
                    temp.Add(item.NumberID);
                
            }
            if (temp == null)
                throw new ArgumentException(string.Format("{0} Number of station not exist already", id));
            else
                return temp;
        }
        public BusCompany sortBus()
        {
            BusCompany sortList = new BusCompany();
            BusCompany temp = this;
            LineBus smaller = null;
            foreach (LineBus hisony in companyBus)
            {
                smaller = hisony;
                foreach (LineBus item in temp)
                {
                    LineBus a = smaller.Compare(item);
                    smaller = a;
                    
                }
                BusStation findTHis = sortList.findHelp(smaller.NumberID);
                if(findTHis==null)
                {
                    sortList.add(smaller);
                    temp.delete(smaller.NumberID);
                }
            }
            return sortList;
        }

        public LineBus this[int index]
        {
            set
            {
                companyBus[index] = value;
            }
            get 
            {
                return companyBus[index];
            }

        }

        public LineBus this[LineBus a]
        {
            get
            {
                for (int i = 0; i < companyBus.Count - 1; i++)
                {
                    if (companyBus[i].NumberID == a.NumberID)
                        return companyBus[i];
                }
                return null;
            }

        }




        public IEnumerator<LineBus> GetEnumerator() => new BusCompanyIEnumator();

        private class BusCompanyIEnumator : IEnumerator<LineBus>
        {
            private List<LineBus> arr;
            private int index;
            private int place;
            

            public BusCompanyIEnumator()
            {
                arr = new List<LineBus>();
                index = -1;
                place = 0;
            }

            public BusCompanyIEnumator(List<LineBus> b,int c)
            {
                arr = b;
                place = c;
                if (c > 0)
                    index = 0;
                else
                    index = -1;
            }


             public LineBus Current => arr[index];
            object IEnumerator.Current => Current;

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                index++;
                if(index>place)
                {
                    Reset();
                    return false;
                }
                return true;
            }

            public void Reset()
            {
                index = -1;
            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //IEnumerator<LineBus> IEnumerable<LineBus>.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}
    }


}
