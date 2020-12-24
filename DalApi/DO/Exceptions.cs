using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class WrongLicenceException : Exception
    {
        public int Licence;
        public WrongLicenceException(int licence) : base() => Licence = licence;
        public WrongLicenceException(int licence, string messege) : base(messege) => Licence = licence;
        public WrongLicenceException(int licence, string messege, Exception innerException) : base(messege, innerException) => Licence = licence;
        public override string ToString()
        {
            return base.ToString() + $",Licence not valid:{Licence} ";
        }


    }
}
