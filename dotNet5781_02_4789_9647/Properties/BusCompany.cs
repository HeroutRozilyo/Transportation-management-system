using dotNet5781_02_4789_9647.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4789_9647
{
    public class BusCompany :IEnumerable
    {
        private List<LineBus> companyBus;

        public BusCompany()
        {
            companyBus = new List<LineBus>();
        }

        IEnumerator GetEnumerator()
        {

        }

        public interface IEnumerator
        {
            object Current { get; } //need to return the obj
            bool MoveNext(); //current will point to the right place
            void Reset();


        }





    }


}
