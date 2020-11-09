using dotNet5781_02_4789_9647.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_02_4789_9647.Properties
{
    public class BusCompany :LineBus, IEnumerable<LineBus>
    {
        private List<LineBus> companyBus;

      //  private List<int> numbers = new List<int>();

        public BusCompany()
        {
            companyBus = new List<LineBus>();
        }

        public bool findline(LineBus line1)
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
                        return false;
                    }
                    else
                        return false;
                }
            }
            return true;
        }

        public void add(LineBus line1)
        {
            bool result = findline(line1);
            if(result)
            {
                companyBus.Add(line1);
            }
            else
                throw new ArgumentException(
                     String.Format("{0} Number line exsis allready", line1.NumberID));
        }


        public LineBus findhelp(int line1)
        {
            foreach (LineBus item in companyBus)
            {
                if (item.NumberID == line1)
                    return item;
            }
            return null;
        }




        public void delline(int idline)
        {
            LineBus a = findhelp(idline);
            if(a!=null)
            {
                companyBus.Remove(a);
            }
            else
                throw new ArgumentException(
                    String.Format("{0} Number line exsis allready", idline));
        }

        public void delline(int idbustion)










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
