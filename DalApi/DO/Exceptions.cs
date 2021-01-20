using System;

namespace DO
{
    [Serializable]
    public class WrongLicenceException : Exception //when licence is wrong 
    {
        public int Licence;
        public WrongLicenceException() : base() { }
        public WrongLicenceException(int licence) : base() => Licence = licence;
        public WrongLicenceException(int licence, string messege) : base(messege) => Licence = licence;
        public WrongLicenceException(int licence, string messege, Exception innerException) : base(messege, innerException) => Licence = licence;
        public override string ToString()
        {
            return base.ToString() + $",{Licence} מספר רישוי לא תקין";
        }
    }
    [Serializable]
    public class WrongKMException : Exception //when licence is wrong 
    {
        public int Licence;
        public WrongKMException() : base() { }
        public WrongKMException(int licence) : base() => Licence = licence;
        public WrongKMException(int licence, string messege) : base(messege) => Licence = licence;
        public WrongKMException(int licence, string messege, Exception innerException) : base(messege, innerException) => Licence = licence;
        public override string ToString()
        {
            return base.ToString() + $", אינו תקין{Licence} מספר קילומטרים לאוטובוס ";
        }
    }
    [Serializable]
    public class WrongIDExeption : Exception //when the id number is wrong.. use at line, station, trip,linestation..
    {
        public int ID;
        public WrongIDExeption() : base() { }

        public WrongIDExeption(int idline) : base() => ID = idline;
        public WrongIDExeption(int idline, string messege) : base(messege) => ID = idline;
        public WrongIDExeption(int idline, string messege, Exception innerException) : base(messege, innerException) => ID = idline;
        public override string ToString()
        {
            return base.ToString() + $",{ID} מספר הקו לא תקין";
        }

    }

    [Serializable]
    public class WrongNameExeption : Exception //if name user wrong
    {
        public string Name;
        public WrongNameExeption() : base() { }

        public WrongNameExeption(string name) : base() => Name = name;
        public WrongNameExeption(string name, string messege) : base(messege) => Name = name;
        public WrongNameExeption(string name, string messege, Exception innerException) : base(messege, innerException) => Name = name;
        public override string ToString()
        {
            return base.ToString() + $",{Name} השם שהוכנס לא תקין";
        }
    }

    [Serializable]
    public class WrongLineTripExeption : Exception //if name user wrong
    {
        public int ID;
        public WrongLineTripExeption() : base() { }

        public WrongLineTripExeption(int idline) : base() => ID = idline;
        public WrongLineTripExeption(int idline, string messege) : base(messege) => ID = idline;
        public WrongLineTripExeption(int idline, string messege, Exception innerException) : base(messege, innerException) => ID = idline;
        public override string ToString()
        {
            return base.ToString() + $"{ID} מצטערים, לא נמצאו זמני נסיעה עבור ";
        }




    }
    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }




}
