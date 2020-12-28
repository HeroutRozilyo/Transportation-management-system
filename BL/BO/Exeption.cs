using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class BadBusLicenceException : Exception
    {
        public int ID;
        public BadBusLicenceException(string message, Exception innerException) :
            base(message, innerException) => ID = ((DO.WrongLicenceException)innerException).Licence;
        public override string ToString() => base.ToString() + $", bad Licence id: {ID}";
    }

    [Serializable]
    public class BadIdException : Exception
    {
        public int ID;
        public BadIdException(string message, Exception innerException) :
            base(message, innerException) => ID = ((DO.WrongIDExeption)innerException).ID;
        public override string ToString() => base.ToString() + $", bad identity number: {ID}";
    }





}
