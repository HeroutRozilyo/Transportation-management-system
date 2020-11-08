using dotNet5781_02_4789_9647.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_02_4789_9647.Properties
{
    public class BusCompany :IEnumerable<LineBus>
    {
        private List<LineBus> companyBus;

      //  private List<int> numbers = new List<int>();

        public BusCompany()
        {
            companyBus = new List<LineBus>();
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
