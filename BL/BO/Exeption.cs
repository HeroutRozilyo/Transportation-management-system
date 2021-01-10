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
        public BadBusLicenceException() : base() { }
        public BadBusLicenceException(string message,int licence) : base(message) => ID = licence;
        public BadBusLicenceException(string message, Exception innerException) :
            base(message, innerException) => ID = ((DO.WrongLicenceException)innerException).Licence;
        public override string ToString() => base.ToString() + $",{ID} מספר רישוי שגוי   ";
    }

    [Serializable]
    public class BadIdException : Exception
    {
        public int ID;
        public BadIdException() : base() { }
        public BadIdException(string message, int id) : base() => ID = id;
        public BadIdException(string message, Exception innerException) :
            base(message, innerException) => ID = ((DO.WrongIDExeption)innerException).ID;
        public override string ToString() => base.ToString() + $",  {ID} מצטערים! המספר זיהוי שגוי";
    }

    [Serializable]
    public class BadCoordinateException : Exception
    {
        public int cordinate;
        public string cordinateS;
        public BadCoordinateException() : base() { }
        public BadCoordinateException(string geo, string v) : base() =>cordinateS=geo;
        public BadCoordinateException(int geo, string messege) : base(messege) => cordinate = geo;
        public BadCoordinateException(int geo,int a, string messege) : base(messege) => cordinate = geo;
        public BadCoordinateException(int geo, string messege, Exception innerException) : base(messege, innerException) => cordinate = geo;
        public override string ToString()
        {
            return base.ToString() + $",{cordinate} הקורדינטות שהוכנסו אינן חוקיות";
        }

    }
    [Serializable]
    public class BadLineTripExeption : Exception //if name user wrong
    {
        public int ID;
        public BadLineTripExeption() : base() { }
        public BadLineTripExeption(string message, int licence) : base(message) => ID = licence;
        public BadLineTripExeption(string message, Exception innerException) :
            base(message, innerException) => ID = ((DO.WrongLineTripExeption)innerException).ID;
        public override string ToString() => base.ToString() + $",{ID}  הלוח זמנים המבוקש לא נמצא ";
    }

    [Serializable]
    public class BadNameExeption : Exception //if name user wrong
    {
        public string NAME;
        public BadNameExeption() : base() { }
        public BadNameExeption(string message, string name) : base(message) => NAME =name ;
        public BadNameExeption(string message, Exception innerException) :
            base(message, innerException) => NAME = ((DO.WrongNameExeption)innerException).Name;
        public override string ToString() => base.ToString() + $",{NAME}  שם משתמש לא חוקי ";
    }





}
