using DALAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
//https://www.gov.il/he/Departments/General/gtfs_general_transit_feed_specifications

namespace DL
{
    sealed class DalXml : IDAL
    {
        private XElement serials;

        #region singelton
        static readonly DalXml instance = new DalXml();
        static DalXml() { }// static ctor to ensure instance init is done just before first usage
        DalXml() { }   // default => private
        public static DalXml Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files
        string busPath = @"BusXML.xml"; //XElement        
                                        //string studentsPath = @"StudentsXml.xml"; //XMLSerializer
                                        //string coursesPath = @"CoursesXml.xml"; //XMLSerializer
                                        //string lecturersPath = @"LecturersXml.xml"; //XMLSerializer
                                        //string lectInCoursesPath = @"LecturerInCourseXml.xml"; //XMLSerializer
                                        //string studInCoursesPath = @"StudentInCoureseXml.xml"; //XMLSerializer
        #endregion


        /*    
       public int AddBus(DO.Bus bus)
        {
            if (DataSource.ListBus.FirstOrDefault(b => b.Licence == bus.Licence) != null) //if != null its means that this licence is allready exsis
                throw new DO.WrongLicenceException(int.Parse(bus.Licence), "המספר רישוי כבר קיים במערכת");
            bus.BusExist = true;
            DataSource.ListBus.Add(bus.Clone());
            return 1;
        }
   */
        #region Bus
        public Bus GetBus(string licence)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath); //get the data from xml
            DO.Bus b = (from bus in busRootElem.Elements()
                        where bus.Element("Licence").Value == licence && Convert.ToBoolean(bus.Element("BusExist").Value) == true
                        select new DO.Bus()
                        {
                            Licence = bus.Element("Licence").Value,
                            StartingDate = DateTime.Parse(bus.Element("StartingDate").Value),
                            Kilometrz = Double.Parse(bus.Element("Kilometrz").Value),
                            KilometrFromLastTreat = Double.Parse(bus.Element("KilometrFromLastTreat").Value),
                            FuellAmount = Double.Parse(bus.Element("FuellAmount").Value),
                            StatusBus = (STUTUS)Enum.Parse(typeof(STUTUS), bus.Element("StatusBus").Value),
                            BusExist = Boolean.Parse(bus.Element("BusExist").Value),
                            LastTreatment = DateTime.Parse(bus.Element("LastTreatment").Value)
                        }
            ).FirstOrDefault();

            if (b == null)
                throw new DO.WrongLicenceException(Convert.ToInt32(licence), "האוטובוס המבוקש לא נמצא במערכת");
            return b;
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath); //get the data from xml

            return (from bus in busRootElem.Elements()
                    where Convert.ToBoolean(bus.Element("BusExist").Value) == true
                    select new DO.Bus()
                    {
                        Licence = bus.Element("Licence").Value,
                        StartingDate = DateTime.Parse(bus.Element("StartingDate").Value),
                        Kilometrz = Double.Parse(bus.Element("Kilometrz").Value),
                        KilometrFromLastTreat = Double.Parse(bus.Element("KilometrFromLastTreat").Value),
                        FuellAmount = Double.Parse(bus.Element("FuellAmount").Value),
                        StatusBus = (STUTUS)Enum.Parse(typeof(STUTUS), bus.Element("StatusBus").Value),
                        BusExist = Boolean.Parse(bus.Element("BusExist").Value),
                        LastTreatment = DateTime.Parse(bus.Element("LastTreatment").Value)
                    }
              );

        }

        public IEnumerable<Bus> GetAllBusesStusus(STUTUS stusus)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath); //get the data from xml

            return (from bus in busRootElem.Elements()
                    where (STUTUS)Enum.Parse(typeof(STUTUS), bus.Element("StatusBus").Value) == stusus && Convert.ToBoolean(bus.Element("BusExist").Value) == true
                    select new DO.Bus()
                    {
                        Licence = bus.Element("Licence").Value,
                        StartingDate = DateTime.Parse(bus.Element("StartingDate").Value),
                        Kilometrz = Double.Parse(bus.Element("Kilometrz").Value),
                        KilometrFromLastTreat = Double.Parse(bus.Element("KilometrFromLastTreat").Value),
                        FuellAmount = Double.Parse(bus.Element("FuellAmount").Value),
                        StatusBus = (STUTUS)Enum.Parse(typeof(STUTUS), bus.Element("StatusBus").Value),
                        BusExist = Boolean.Parse(bus.Element("BusExist").Value),
                        LastTreatment = DateTime.Parse(bus.Element("LastTreatment").Value)
                    }
              );
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> buscondition)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath); //get the data from xml
            return from bus in busRootElem.Elements()
                   let b1 = new DO.Bus()
                   {
                       Licence = bus.Element("Licence").Value,
                       StartingDate = DateTime.Parse(bus.Element("StartingDate").Value),
                       Kilometrz = Double.Parse(bus.Element("Kilometrz").Value),
                       KilometrFromLastTreat = Double.Parse(bus.Element("KilometrFromLastTreat").Value),
                       FuellAmount = Double.Parse(bus.Element("FuellAmount").Value),
                       StatusBus = (STUTUS)Enum.Parse(typeof(STUTUS), bus.Element("StatusBus").Value),
                       BusExist = Boolean.Parse(bus.Element("BusExist").Value),
                       LastTreatment = DateTime.Parse(bus.Element("LastTreatment").Value)
                   }
                   where buscondition(b1) && Convert.ToBoolean(bus.Element("BusExist").Value) == true
                   select b1;
        }



        public int AddBus(Bus bus)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath); //get the data from xml
            XElement findBus = (from b in busRootElem.Elements()
                                where b.Element("Licence").Value == bus.Licence && Convert.ToBoolean(b.Element("BusExist").Value) == true
                                select b).FirstOrDefault();
            if (findBus != null)
                throw new DO.WrongLicenceException(Convert.ToInt32(bus.Licence), "אוטובוס זה כבר קיים במערכת, באפשרותך לעדכן נתונים עליו במקום המתאים");
            XElement busToAdd = new XElement("Bus",
                     new XElement("StartingDate", bus.StartingDate),
                     new XElement("Kilometrz", bus.Kilometrz),
                     new XElement("KilometrFromLastTreat", bus.KilometrFromLastTreat),
                     new XElement("FuellAmount", bus.FuellAmount),
                     new XElement("StatusBus", bus.StatusBus.ToString()),
                     new XElement("BusExist", bus.BusExist),
                     new XElement("LastTreatment", bus.LastTreatment));

            busRootElem.Add(busToAdd);
            XMLTools.SaveListToXMLElement(busRootElem, busPath);
            return 1;

        }


        #endregion



        public int AddLine(Line line)
        {

            serials = XElement.Load(@"Serials.xml");

            int seialLine = int.Parse(serials.Element("LineCounter").Value);
            List<Line> lines = XMLTools.LoadListFromXMLSerializer<Line>(@"lines.xml");
            serials.Element("LineCounter").Value = (++seialLine).ToString();
            line.IdNumber = seialLine;
            lines.Add(line);
            XMLTools.SaveListToXMLSerializer(lines, @"lines.xml");
            serials.Save(@"Serials.xml");
            throw new NotImplementedException();
        }

        public void AddLineStations(LineStation station)
        {
            throw new NotImplementedException();
        }

        public void AddLineStations(AdjacentStations adjacentStations)
        {
            throw new NotImplementedException();
        }

        public void AddLineTrip(LineTrip lineTrip)
        {
            throw new NotImplementedException();
        }

        public void AddStations(Stations station)
        {
            XElement stationXml = station.ToXML();


        }

        public void AddTrip(Trip trip)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteAdjacentStationse(int Scode1, int Scode2)
        {
            throw new NotImplementedException();
        }

        public void DeleteAdjacentStationseBStation(int Scode1)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBus(string licence)
        {
            throw new NotImplementedException();
        }

        public void DeleteLine(int idnumber)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineTrip(int idline)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineTrip1(LineTrip lineTrip)
        {
            throw new NotImplementedException();
        }

        public void DeleteStations(int code)
        {
            throw new NotImplementedException();
        }

        public int DeleteStationsFromLine(int Scode, int idline)
        {
            throw new NotImplementedException();
        }

        public void DeleteStationsFromLine(int Scode)
        {
            throw new NotImplementedException();
        }

        public void DeleteStationsOfLine(int idline)
        {
            throw new NotImplementedException();
        }

        public void DeleteTrip(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string name)
        {
            throw new NotImplementedException();
        }

        public AdjacentStations GetAdjacentStations(int Scode1, int Scode2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStations(int stationCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> StationsLinecondition)
        {
            throw new NotImplementedException();
        }




        public IEnumerable<Line> GetAllLine()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineStation> GetAllLineAt2Stations(int code1, int cod2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetAllLineBy(Predicate<Line> linecondition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetAllLinesArea(AREA area)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineStation> GetAllLineStationsBy(Predicate<LineStation> StationsLinecondition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineTrip> GetAllLineTripsBy(Predicate<LineTrip> StationsLinecondition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stations> GetAllStations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stations> GetAllStationsBy(Predicate<Stations> Stationscondition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineStation> GetAllStationsCode(int code)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineStation> GetAllStationsLine(int idline)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Trip> GetAllTrip()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineTrip> GetAllTripline(int idline)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Trip> GetAllTripLine(int line)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Trip> GetAllTripsBy(Predicate<Trip> Tripcondition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAlluser()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAlluserAdmin()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAlluserBy(Predicate<User> userConditions)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAlluserNAdmin()
        {
            throw new NotImplementedException();
        }


        public Line GetLine(int idline)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetLineFields(Func<int, bool, object> generate)
        {
            throw new NotImplementedException();
        }

        public LineStation GetLineStation(int Scode, int idline)
        {
            throw new NotImplementedException();
        }

        public LineTrip GetLineTrip(TimeSpan start, int idline)
        {
            throw new NotImplementedException();
        }

        public Stations GetStations(int code)
        {
            throw new NotImplementedException();
        }

        public Trip GetTrip(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string name)
        {
            throw new NotImplementedException();
        }

        public User getUserBy(Predicate<User> userConditions)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdjacentStations(int code1, int code2, int codeChange, int oldCode)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBus(Bus buses)
        {
            throw new NotImplementedException();
        }

        public void UpdateLine(Line line)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineStations(LineStation linestations)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineStationsCode(LineStation linestations, int oldCode)
        {
            throw new NotImplementedException();
        }

        public void UpdatelineTrip(LineTrip lineTrip)
        {
            throw new NotImplementedException();
        }

        public void UpdateStations(Stations stations, int oldCode)
        {
            throw new NotImplementedException();
        }

        public void UpdateStations(Trip trip)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
