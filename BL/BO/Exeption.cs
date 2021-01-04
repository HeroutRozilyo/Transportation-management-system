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
        public BadBusLicenceException(string message,int licence) : base(message) => ID = licence;
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

    [Serializable]
    public class BadCoordinateException : Exception
    {
        public int cordinate;
        public string cordinateS;
        public BadCoordinateException(string geo, string v) : base() =>cordinateS=geo;
        public BadCoordinateException(int geo, string messege) : base(messege) => cordinate = geo;
        public BadCoordinateException(int geo, string messege, Exception innerException) : base(messege, innerException) => cordinate = geo;
        public override string ToString()
        {
            return base.ToString() + $",ID line not valid:{cordinate} ";
        }

    }






}
