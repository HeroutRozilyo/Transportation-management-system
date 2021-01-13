using DALAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
        string linePath = @"LineXml.xml"; //XMLSerializer
        string stationPath = @"StationXml.xml"; //XMLSerializer
        string lineStationPath = @"lineStationXml.xml"; //XMLSerializer
         string lineTripPath = @"lineTripXml.xml"; //XMLSerializer
       string adjacentStationsPath = @"AdjacentStationsXml.xml"; //XMLSerializer
        string userPath = @"UserXml.xml"; //XMLSerializer


        #endregion


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
                     new XElement("Licence", bus.Licence),
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

        public bool DeleteBus(string licence)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath); //get the data from xml
            XElement bus= (from b in busRootElem.Elements()
                           where b.Element("Licence").Value == licence && Convert.ToBoolean(b.Element("BusExist").Value) == true
                           select b).FirstOrDefault();
            if (bus != null)
            {
                bus.Element("BusExist").Value = "false";
                XMLTools.SaveListToXMLElement(busRootElem, busPath);
                return true;
            }
            else
                throw new DO.WrongLicenceException(Convert.ToInt32(licence), "האוטובוס המבוקש אינו קיים במערכת");


        }

        public bool UpdateBus(Bus bus)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath); //get the data from xml
            XElement findBus = (from b in busRootElem.Elements()
                                where b.Element("Licence").Value == bus.Licence && Convert.ToBoolean(b.Element("BusExist").Value) == true
                                select b).FirstOrDefault();
            if (findBus == null) //we not find the bus
                throw new DO.WrongLicenceException(Convert.ToInt32(bus.Licence), "אוטובוס זה כבר קיים במערכת, באפשרותך לעדכן נתונים עליו במקום המתאים");
            else
            {
                findBus.Element("Licence").Value = bus.Licence;
                findBus.Element("StartingDate").Value = bus.StartingDate.ToString();
                findBus.Element("Kilometrz").Value = bus.Kilometrz.ToString();
                findBus.Element("KilometrFromLastTreat").Value = bus.KilometrFromLastTreat.ToString();
                    findBus.Element("FuellAmount").Value = bus.FuellAmount.ToString();
                findBus.Element("StatusBus").Value = bus.StatusBus.ToString();
                findBus.Element("BusExist").Value = bus.BusExist.ToString();
                findBus.Element("LastTreatment").Value = bus.LastTreatment.ToString();

                XMLTools.SaveListToXMLElement(busRootElem, busPath);


                return true;

            }

        }



        #endregion

        #region Line

      

        public Line GetLine(int idline)
        {
            XElement lineRootElem = XMLTools.LoadListFromXMLElement(linePath); //get the data from xml
            DO.Line b = (from line in lineRootElem.Elements()
                        where line.Element("IdNumber").Value == idline.ToString() && Convert.ToBoolean(line.Element("LineExist").Value) == true
                        select new DO.Line()
                        {
                            IdNumber = Convert.ToInt32(line.Element("IdNumber").Value),
                            NumberLine = Convert.ToInt32(line.Element("NumberLine").Value),
                            FirstStationCode = Convert.ToInt32(line.Element("FirstStationCode").Value),
                            LastStationCode = Convert.ToInt32(line.Element("LastStationCode").Value),
                            Area = (AREA) Enum.Parse(typeof(AREA),line.Element("Area").Value),
                            LineExist = Convert.ToBoolean(line.Element("LineExist").Value)

                        }
            ).FirstOrDefault();

            if (b == null)
                throw new DO.WrongIDExeption(idline, "  הקו המבוקש לא נמצא במערכת");
            return b;
        }

        public IEnumerable<Line> GetAllLineBy(Predicate<Line> linecondition)
        {


            XElement lineRootElem = XMLTools.LoadListFromXMLElement(linePath); //get the data from xml
            return from line in lineRootElem.Elements()
                   let l1 = new DO.Line()
                   {
                       IdNumber = Convert.ToInt32(line.Element("IdNumber").Value),
                       NumberLine = Convert.ToInt32(line.Element("NumberLine").Value),
                       FirstStationCode = Convert.ToInt32(line.Element("FirstStationCode").Value),
                       LastStationCode = Convert.ToInt32(line.Element("LastStationCode").Value),
                       Area = (AREA)Enum.Parse(typeof(AREA), line.Element("Area").Value),
                       LineExist = Convert.ToBoolean(line.Element("LineExist").Value)

                     
                   }
                   where linecondition(l1) && Convert.ToBoolean(line.Element("LineExist").Value) == true
                   select l1;

        }

        public IEnumerable<Line> GetAllLine()
        {

            XElement lineRootElem = XMLTools.LoadListFromXMLElement(linePath); //get the data from xml

            return (from line in lineRootElem.Elements()
                    where Convert.ToBoolean(line.Element("LineExist").Value) == true
                    select new DO.Line()
                    {
                        IdNumber = Convert.ToInt32(line.Element("IdNumber").Value),
                        NumberLine = Convert.ToInt32(line.Element("NumberLine").Value),
                        FirstStationCode = Convert.ToInt32(line.Element("FirstStationCode").Value),
                        LastStationCode = Convert.ToInt32(line.Element("LastStationCode").Value),
                        Area = (AREA)Enum.Parse(typeof(AREA), line.Element("Area").Value),
                        LineExist = Convert.ToBoolean(line.Element("LineExist").Value)

                    }
              );
        }

        public IEnumerable<Line> GetAllLinesArea(AREA area)
        {
            XElement lineRootElem = XMLTools.LoadListFromXMLElement(linePath); //get the data from xml

            return (from line in lineRootElem.Elements()
                    where (AREA)Enum.Parse(typeof(AREA), line.Element("Area").Value) == area && Convert.ToBoolean(line.Element("LineExist").Value) == true
                    select new DO.Line()
                    {
                        IdNumber = Convert.ToInt32(line.Element("IdNumber").Value),
                        NumberLine = Convert.ToInt32(line.Element("NumberLine").Value),
                        FirstStationCode = Convert.ToInt32(line.Element("FirstStationCode").Value),
                        LastStationCode = Convert.ToInt32(line.Element("LastStationCode").Value),
                        Area = (AREA)Enum.Parse(typeof(AREA), line.Element("Area").Value),
                        LineExist = Convert.ToBoolean(line.Element("LineExist").Value)
                    }
              );
        }

        public int AddLine(Line line)
        {  //eliezer told us that we can do here not checking because check according to idnumber is meaningless

            serials = XElement.Load(@"Serials.xml");
            int idLine = int.Parse(serials.Element("LineCounter").Value);

            serials.Element("LineCounter").Value = (++idLine).ToString();

            List<Line> lines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);
            line.IdNumber = idLine;

            lines.Add(line);
            XMLTools.SaveListToXMLSerializer(lines, linePath);

            serials.Save(@"Serials.xml");
            return idLine;
        }

        public void UpdateLine(Line line)
        {
            XElement lineRootElem = XMLTools.LoadListFromXMLElement(linePath); //get the data from xml
            XElement findLine = (from l in lineRootElem.Elements()
                                where l.Element("IdNumber").Value == line.IdNumber.ToString() && Convert.ToBoolean(l.Element("LineExist").Value) == true
                                select l).FirstOrDefault();
            if (findLine == null) //we not find the bus
                throw new DO.WrongLicenceException(Convert.ToInt32(line.IdNumber), "קו זה כבר קיים במערכת, באפשרותך לעדכן נתונים עליו במקום המתאים");
            else
            {
                findLine.Element("IdNumber").Value = line.IdNumber.ToString();
                findLine.Element("NumberLine").Value = line.NumberLine.ToString();
                findLine.Element("FirstStationCode").Value = line.FirstStationCode.ToString();
                findLine.Element("LastStationCode").Value = line.LastStationCode.ToString();
                findLine.Element("Area").Value = line.Area.ToString();
                findLine.Element("LineExist").Value = line.LineExist.ToString();
               

                XMLTools.SaveListToXMLElement(lineRootElem, linePath);


            }

        }

        public void DeleteLine(int idnumber)
        {
            XElement lineRootElem = XMLTools.LoadListFromXMLElement(linePath); //get the data from xml
            XElement line = (from l in lineRootElem.Elements()
                            where l.Element("IdNumber").Value == idnumber.ToString() && Convert.ToBoolean(l.Element("LineExist").Value) == true
                            select l).FirstOrDefault();
            if (line != null)
            {
                line.Element("LineExist").Value = "false";
                XMLTools.SaveListToXMLElement(lineRootElem, linePath);
                
            }
            else
                throw new DO.WrongLicenceException(idnumber, "הקו המבוקש אינו קיים במערכת");



        }



        #endregion


        #region Station

        public Stations GetStations(int code)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationPath); //get the data from xml
            DO.Stations b = (from station in stationRootElem.Elements()
                             where station.Element("Code").Value == code.ToString() && Convert.ToBoolean(station.Element("StationExist").Value) == true
                             select new DO.Stations()
                             {
                                 Code = Convert.ToInt32(station.Element("Code").Value),
                                 Name = station.Element("Name").Value,
                                 Address = station.Element("Address").Value,
                                 Latitude= Convert.ToDouble(station.Element("Latitude").Value),
                                 Longtitude = Convert.ToDouble(station.Element("Longtitude").Value),
                                 StationExist = Boolean.Parse(station.Element("StationExist").Value)

                             }
            ) .FirstOrDefault();

            if (b == null)
                throw new DO.WrongLicenceException(code, "התחנה המבוקש לא נמצא במערכת");
            return b;


        }

        public IEnumerable<Stations> GetAllStations()
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationPath); //get the data from xml

            return (from station in stationRootElem.Elements()
                    where Convert.ToBoolean(station.Element("StationExist").Value) == true
                    select new DO.Stations()
                    {
                        Code = Convert.ToInt32(station.Element("Code").Value),
                        Name = station.Element("Name").Value,
                        Address = station.Element("Address").Value,
                        Latitude = Convert.ToDouble(station.Element("Latitude").Value),
                        Longtitude = Convert.ToDouble(station.Element("Longtitude").Value),
                        StationExist = Boolean.Parse(station.Element("StationExist").Value)

                    }
              );

        }

        public IEnumerable<Stations> GetAllStationsBy(Predicate<Stations> Stationscondition)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationPath); //get the data from xml
            return from station in stationRootElem.Elements()
                   let b1 = new DO.Stations()
                   {
                       Code = Convert.ToInt32(station.Element("Code").Value),
                       Name = station.Element("Name").Value,
                       Address = station.Element("Address").Value,
                       Latitude = Convert.ToDouble(station.Element("Latitude").Value),
                       Longtitude = Convert.ToDouble(station.Element("Longtitude").Value),
                       StationExist = Boolean.Parse(station.Element("StationExist").Value)
                   }
                   where Stationscondition(b1) && Convert.ToBoolean(station.Element("StationExist").Value) == true
                   select b1;
        }

        public void AddStations(Stations station)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationPath); //get the data from xml
            XElement stationBus = (from b in stationRootElem.Elements()
                                   where b.Element("Code").Value == station.Code.ToString() && Convert.ToBoolean(b.Element("StationExist").Value) == true
                                   select b).FirstOrDefault();
            if (stationBus != null)
                throw new DO.WrongIDExeption(station.Code, "תחנה זו כבר קיימת במערכת, באפשרותך לעדכן נתונים עליו במקום המתאים");

            XElement stationToAdd = new XElement("Stations",
                    new XElement("Code", station.Code),
                    new XElement("Name", station.Name),
                    new XElement("Address", station.Address),
                    new XElement("Latitude", station.Latitude),
                    new XElement("Longtitude", station.Longtitude),
                    new XElement("StationExist", station.StationExist));


            stationRootElem.Add(stationToAdd);
            XMLTools.SaveListToXMLElement(stationRootElem, stationPath);
 
        }


        public void DeleteStations(int code)
        {
            List<Stations> ListStation = XMLTools.LoadListFromXMLSerializer<Stations>(stationPath);
            DO.Stations sta = ListStation.Find(p => p.Code == code&&p.StationExist==true);
            if (sta != null)
            {
                sta.StationExist = false;
            }
            else
                throw new DO.WrongIDExeption(code, "לא נמצאו פרטים עבור התחנה המבוקשת");

            XMLTools.SaveListToXMLSerializer(ListStation, stationPath);


        }

        public void UpdateStations(Stations stations, int oldCode)
        {
            List<Stations> ListStation = XMLTools.LoadListFromXMLSerializer<Stations>(stationPath);
            DO.Stations sta = ListStation.Find(p => p.Code == oldCode && p.StationExist == true);
            if (sta != null)
            {
                ListStation.Remove(sta);
                ListStation.Add(stations);
            }
            else
                throw new DO.WrongIDExeption(oldCode, "לא נמצאו פרטים עבור התחנה המבוקשת");
            XMLTools.SaveListToXMLSerializer(ListStation, stationPath);

        }

        #endregion


        #region LineStation


        public LineStation GetLineStation(int Scode, int idline)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);
            DO.LineStation sta = ListLineStation.Find((b => b.StationCode == Scode && b.LineId == idline && b.LineStationExist == true));
            if (sta != null)
                return sta;
            else
                throw new DO.WrongIDExeption(Scode, "לא נמצאו פרטים עבור התחנה המבוקשת");

        }


        public IEnumerable<LineStation> GetAllStationsLine(int idline)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            return from station in ListLineStation
                   where (station.LineId == idline && station.LineStationExist)
                   select station;

        }



        public IEnumerable<LineStation> GetAllStationsCode(int code)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            return from station in ListLineStation
                   where (station.StationCode == code)
                   select station;
        }

        public IEnumerable<LineStation> GetAllLineStationsBy(Predicate<LineStation> StationsLinecondition)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);
            return from stations in ListLineStation
                   where (stations.LineStationExist && StationsLinecondition(stations))
                   select stations;
        }


        public void AddLineStations(LineStation station)
        {
          List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);
          if(ListLineStation.FirstOrDefault(b => b.StationCode == station.StationCode && b.LineId == station.LineId && b.LineStationExist)!=null)
                throw new DO.WrongIDExeption(station.StationCode, "התחנה המבוקשת קיימת כבר במערכת");
            ListLineStation.Add(station);

            XMLTools.SaveListToXMLSerializer(ListLineStation, lineStationPath);
        }

        public int DeleteStationsFromLine(int Scode, int idline)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);
            DO.LineStation stations = ListLineStation.FirstOrDefault(b => b.StationCode == Scode && b.LineId == idline && b.LineStationExist);
            if (stations != null)
                stations.LineStationExist = false;
            else
                throw new DO.WrongIDExeption(Scode, "התחנה המבוקשת לא נמצאה במערכת");
            XMLTools.SaveListToXMLSerializer(ListLineStation, lineStationPath);
            return stations.LineStationIndex;
        }


        public void DeleteStationsFromLine(int Scode)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            foreach (DO.LineStation item in ListLineStation)
            {
                if (item.StationCode == Scode)
                    item.LineStationExist = false;
            }

            XMLTools.SaveListToXMLSerializer(ListLineStation, lineStationPath);
        }

        public void DeleteStationsOfLine(int idline)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            foreach (DO.LineStation item in ListLineStation)
            {
                if (item.LineId == idline)
                    item.LineStationExist = false;
            }

            XMLTools.SaveListToXMLSerializer(ListLineStation, lineStationPath);

        }


        public void UpdateLineStations(LineStation linestations)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);
            DO.LineStation station = ListLineStation.FirstOrDefault(b => b.StationCode == linestations.StationCode && b.LineId == linestations.LineId && b.LineStationExist);
            if(station!=null)
            {
                ListLineStation.Remove(station);
                ListLineStation.Add(linestations);
            }    
            else
                throw new DO.WrongIDExeption(linestations.StationCode, "התחנה המבוקשת לא נמצאה במערכת");
            XMLTools.SaveListToXMLSerializer(ListLineStation, lineStationPath);

        }

        public void UpdateLineStationsCode(LineStation linestations, int newCode)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);
            DO.LineStation station = ListLineStation.FirstOrDefault(b => b.StationCode == linestations.StationCode && b.LineId == linestations.LineId && b.LineStationExist);
            if (station != null)
            {
                linestations.StationCode = newCode;
                ListLineStation.Remove(station);
                ListLineStation.Add(linestations);
            }

            XMLTools.SaveListToXMLSerializer(ListLineStation, lineStationPath);




        }



        #endregion

        #region LineTrip


    
        public LineTrip GetLineTrip(TimeSpan start, int idline)

        {
            List<LineTrip> ListLineTrip1 = new List<LineTrip>
            {
                 #region LineTrip
                new LineTrip
                {
                 KeyId=1,
                 StartAt=new TimeSpan(06,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=19,
                 },
                new LineTrip
                {
                 KeyId=2,
                 StartAt=new TimeSpan(06,00,00),
                 FinishAt=new TimeSpan(24,00,00),
                 TripLineExist=true,
                 Frequency=13,
                 },
                new LineTrip
                {
                 KeyId=3,
                 StartAt=new TimeSpan(06,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=15,
                 },
                new LineTrip
                {
                 KeyId=4,
                 StartAt=new TimeSpan(06,00,00),
                 FinishAt=new TimeSpan(19,00,00),
                 TripLineExist=true,
                 Frequency=10,
                 },
                new LineTrip
                {
                 KeyId=4,
                 StartAt=new TimeSpan(19,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=30,
                 },
                new LineTrip
                {
                 KeyId=5,
                 StartAt=new TimeSpan(06,00,00),
                 FinishAt=new TimeSpan(08,30,00),
                 TripLineExist=true,
                 Frequency=3,
                 },
                new LineTrip
                {
                 KeyId=5,
                 StartAt=new TimeSpan(08,30,00),
                 FinishAt=new TimeSpan(14,00,00),
                 TripLineExist=true,
                 Frequency=60,
                 },
                new LineTrip
                {
                 KeyId=5,
                 StartAt=new TimeSpan(14,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=20,
                 },
                new LineTrip
                {
                 KeyId=6,
                 StartAt=new TimeSpan(06,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=17,
                 },
                new LineTrip
                {
                 KeyId=7,
                 StartAt=new TimeSpan(08,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=30,
                 },
                new LineTrip
                {
                 KeyId=8,
                 StartAt=new TimeSpan(01,00,00),
                 FinishAt=new TimeSpan(06,00,00),
                 TripLineExist=true,
                 Frequency=60,
                 },
                new LineTrip
                {
                 KeyId=9,
                 StartAt=new TimeSpan(05,00,00),
                 FinishAt=new TimeSpan(10,00,00),
                 TripLineExist=true,
                 Frequency=20,
                 },
                new LineTrip
                {
                 KeyId=9,
                 StartAt=new TimeSpan(10,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=40,
                 },
                new LineTrip
                {
                 KeyId=10,
                 StartAt=new TimeSpan(6,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=40,
                 }
                 #endregion LineTrip
            };
            XMLTools.SaveListToXMLSerializer(ListLineTrip1, lineTripPath);
            XElement ListLineTrip = XMLTools.LoadListFromXMLElement(lineTripPath); //get the data from xml
            DO.LineTrip b = (from line in ListLineTrip.Elements()
                             where line.Element("KeyId").Value == idline.ToString() && Convert.ToBoolean(line.Element("TripLineExist").Value) == true
                             select new DO.LineTrip
                             {
                                 KeyId = int.Parse( line.Element("KeyId").Value),
                                 StartAt = TimeSpan.Parse(line.Element("StartAt").Value),
                                 Frequency = Double.Parse(line.Element("Frequency").Value),
                                 FinishAt = TimeSpan.Parse(line.Element("FinishAt").Value),
                                 TripLineExist = Convert.ToBoolean( line.Element("TripLineExist").Value),
                               
        }).FirstOrDefault();

            if (b == null)
                throw new DO.WrongLineTripExeption(idline, $"{start} לא נמצאו פרטים עבור הקו המבוקש בשעה זו");
          return b;
               
        }

        public IEnumerable<LineTrip> GetAllTripline(int idline)
        {

            XElement a=new XElement(lineTripPath);
            List<LineTrip> ListLineTrip1 = new List<LineTrip>
            {
                 #region LineTrip
                new LineTrip
                {
                 KeyId=1,
                 StartAt=new TimeSpan(06,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=19,
                 },
                new LineTrip
                {
                 KeyId=2,
                 StartAt=new TimeSpan(06,00,00),
                 FinishAt=new TimeSpan(24,00,00),
                 TripLineExist=true,
                 Frequency=13,
                 },
                new LineTrip
                {
                 KeyId=3,
                 StartAt=new TimeSpan(06,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=15,
                 },
                new LineTrip
                {
                 KeyId=4,
                 StartAt=new TimeSpan(06,00,00),
                 FinishAt=new TimeSpan(19,00,00),
                 TripLineExist=true,
                 Frequency=10,
                 },
                new LineTrip
                {
                 KeyId=4,
                 StartAt=new TimeSpan(19,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=30,
                 },
                new LineTrip
                {
                 KeyId=5,
                 StartAt=new TimeSpan(06,00,00),
                 FinishAt=new TimeSpan(08,30,00),
                 TripLineExist=true,
                 Frequency=3,
                 },
                new LineTrip
                {
                 KeyId=5,
                 StartAt=new TimeSpan(08,30,00),
                 FinishAt=new TimeSpan(14,00,00),
                 TripLineExist=true,
                 Frequency=60,
                 },
                new LineTrip
                {
                 KeyId=5,
                 StartAt=new TimeSpan(14,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=20,
                 },
                new LineTrip
                {
                 KeyId=6,
                 StartAt=new TimeSpan(06,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=17,
                 },
                new LineTrip
                {
                 KeyId=7,
                 StartAt=new TimeSpan(08,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=30,
                 },
                new LineTrip
                {
                 KeyId=8,
                 StartAt=new TimeSpan(01,00,00),
                 FinishAt=new TimeSpan(06,00,00),
                 TripLineExist=true,
                 Frequency=60,
                 },
                new LineTrip
                {
                 KeyId=9,
                 StartAt=new TimeSpan(05,00,00),
                 FinishAt=new TimeSpan(10,00,00),
                 TripLineExist=true,
                 Frequency=20,
                 },
                new LineTrip
                {
                 KeyId=9,
                 StartAt=new TimeSpan(10,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=40,
                 },
                new LineTrip
                {
                 KeyId=10,
                 StartAt=new TimeSpan(6,00,00),
                 FinishAt=new TimeSpan(23,59,59),
                 TripLineExist=true,
                 Frequency=40,
                 }
                 #endregion LineTrip
            };
             a.Add(XMLTools.ToXML(ListLineTrip1[0]));
            for (int i = 1; i < ListLineTrip1.Count(); i++)
            {

                a.Add( XMLTools.ToXML(ListLineTrip1[i]));
            }
            XMLTools.SaveListToXMLElement(a, lineTripPath);


            XElement ListLineTrip = XMLTools.LoadListFromXMLElement(lineTripPath); //get the data from xml
            //return (from line in ListLineTrip.Elements()
            //        where line.Element("KeyId").Value == idline.ToString() && Convert.ToBoolean(line.Element("TripLineExist").Value) == true
            //        select new DO.LineTrip
            //        {
            //            KeyId = int.Parse(line.Element("KeyId").Value),
            //            StartAt = TimeSpan.Parse(line.Element("StartAt").Value),
            //            Frequency = Double.Parse(line.Element("Frequency").Value),
            //            FinishAt = TimeSpan.Parse(line.Element("FinishAt").Value),
            //            TripLineExist = Convert.ToBoolean(line.Element("TripLineExist").Value),

            //        });
            List<DO.LineTrip> b = new List<LineTrip>();
            foreach( var line in ListLineTrip.Elements())
            {
                if(line.Element("KeyId").Value == idline.ToString() && Convert.ToBoolean(line.Element("TripLineExist").Value) == true)
                {
                    b.Add(new DO.LineTrip()
                    {
                        KeyId = int.Parse(line.Element("KeyId").Value),
                        StartAt = TimeSpan.Parse(line.Element("StartAt").Value),
                        Frequency = Double.Parse(line.Element("Frequency").Value),
                        FinishAt = TimeSpan.Parse(line.Element("FinishAt").Value),
                        TripLineExist = Convert.ToBoolean(line.Element("TripLineExist").Value),

                    });
                }

            }
            return b.AsEnumerable();
        }



        public IEnumerable<LineTrip> GetAllLineTripsBy(Predicate<LineTrip> StationsLinecondition)
        {

            XElement ListLineTrip = XMLTools.LoadListFromXMLElement(lineTripPath); //get the data from xml
            return from line in ListLineTrip.Elements()
                   let l1 = new DO.LineTrip
                   {
                       KeyId = int.Parse(line.Element("KeyId").Value),
                       StartAt = TimeSpan.Parse(line.Element("StartAt").Value),
                       Frequency = Double.Parse(line.Element("Frequency").Value),
                       FinishAt = TimeSpan.Parse(line.Element("FinishAt").Value),
                       TripLineExist = Convert.ToBoolean(line.Element("TripLineExist").Value),


                   }
                   where StationsLinecondition(l1) && Convert.ToBoolean(line.Element("TripLineExist").Value) == true
                   select l1;
                    

        }

        public void AddLineTrip(LineTrip lineTrip)
        {
 


            XElement ListLineTrip = XMLTools.LoadListFromXMLElement(lineTripPath); //get the data from xml
            XElement lineToAdd = XMLTools.ToXML(lineTrip);

            ListLineTrip.Add(lineToAdd);
            XMLTools.SaveListToXMLElement(ListLineTrip, lineTripPath);

        }


        public void DeleteLineTrip(int idline)
        {    

            List<LineTrip> ListLineTrip = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            foreach (var item in ListLineTrip)
                if (item.KeyId == idline)
                    item.TripLineExist = false;
            XMLTools.SaveListToXMLSerializer(ListLineTrip, lineTripPath);

        }

        public void DeleteLineTrip1(LineTrip lineTrip)
        {
            List<LineTrip> ListLineTrip = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            int index = ListLineTrip.FindIndex(b => b.KeyId == lineTrip.KeyId && b.StartAt == lineTrip.StartAt && b.FinishAt == lineTrip.FinishAt);
            ListLineTrip[index].TripLineExist = false;
            XMLTools.SaveListToXMLSerializer(ListLineTrip, lineTripPath);

        }



        public void UpdatelineTrip(LineTrip lineTrip)
        {
            XElement ListLineTrip = XMLTools.LoadListFromXMLElement(lineTripPath); //get the data from xml
            XElement findLine = (from b in ListLineTrip.Elements()
                                where b.Element("KeyId").Value == lineTrip.KeyId.ToString() && Convert.ToBoolean(b.Element("TripLineExist").Value) == true
                                select b).FirstOrDefault();

            if (findLine == null) //we not find the bus
                throw new DO.WrongLineTripExeption(lineTrip.KeyId, "לא נמצאו זמני נסיעות עבור קו זה");
            else
            {
                findLine.Element("KeyId").Value = lineTrip.KeyId.ToString();
                findLine.Element("StartAt").Value = lineTrip.StartAt.ToString();
                findLine.Element("Frequency").Value = lineTrip.Frequency.ToString();
                findLine.Element("FinishAt").Value = lineTrip.FinishAt.ToString();
                findLine.Element("TripLineExist").Value = lineTrip.TripLineExist.ToString();

                XMLTools.SaveListToXMLElement(ListLineTrip, lineTripPath);
            }
            















        }


        #endregion

        #region AdjacentStations

        public AdjacentStations GetAdjacentStations(int Scode1, int Scode2)
        {
           
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);
            DO.AdjacentStations linestations = ListAdjacentStations.Find(b => b.Station1 == Scode1 && b.Station2 == Scode2);//|| b.Station1 == Scode2 && b.Station2 == Scode1);
            if (linestations != null)
            {
                return linestations;
            }
            else
                throw new DO.WrongIDExeption(Scode1, "לא נמצאו פרטים במערכת עבור זוג התחנות המבוקש");

        }


        public IEnumerable<AdjacentStations> GetAllAdjacentStations(int stationCode) //return all the AdjacentStations that we have for this station code
        {
           


            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);

            return from station in ListAdjacentStations
                   where (stationCode == station.Station1 || stationCode == station.Station2)
                   select station;            
        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> StationsLinecondition)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);


            return from stations in ListAdjacentStations
                   where (StationsLinecondition(stations))
                   select stations;
            
        }


    
        public void AddLineStations(DO.AdjacentStations adjacentStations)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);

            DO.AdjacentStations temp = ListAdjacentStations.Find(b => b.Station1 == adjacentStations.Station1 && b.Station2 == adjacentStations.Station2);
            if (temp != null)
                throw new DO.WrongIDExeption(adjacentStations.Station1, " התחנה עוקבת כבר קיימת במערכת");/////////////////////////////////////////////////////////////////
            ListAdjacentStations.Add(adjacentStations);

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, adjacentStationsPath);


            //
        }

        public void DeleteAdjacentStationse(int Scode1, int Scode2)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);

            DO.AdjacentStations stations = ListAdjacentStations.Find(b => b.Station1 == Scode1 && b.Station2 == Scode2);
            if (stations != null)
            {
                ListAdjacentStations.Remove(stations);
            }
            else
                throw new DO.WrongIDExeption(Scode1, "לא נמצאו פרטים עבור התחנה עוקבת המבוקשת");

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, adjacentStationsPath);


        }

        public void DeleteAdjacentStationseBStation(int Scode1)////
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);

            var v = from item in ListAdjacentStations
                    where (item.Station1 == Scode1 || item.Station2 == Scode1)
                    select item;
            foreach (DO.AdjacentStations item in v)
            {
                ListAdjacentStations.Remove(item);
            }

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, adjacentStationsPath);

        }

        public void UpdateAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);

            DO.AdjacentStations station = ListAdjacentStations.Find(b => b.Station1 == adjacentStations.Station1 && b.Station2 == adjacentStations.Station2);
            if (station != null)
            {
                ListAdjacentStations.Remove(station);
                ListAdjacentStations.Add(adjacentStations);
            }
            else
                throw new DO.WrongIDExeption(adjacentStations.Station1, "לא נמצאו פרטים עבור התחנה עוקבת המבוקשת");

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, adjacentStationsPath);

        }


        public void UpdateAdjacentStations(int code1, int code2, int codeChange, int oldCode)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);

            DO.AdjacentStations station = ListAdjacentStations.Find(b => b.Station1 == code1 && b.Station2 == code2);
            if (station != null)
            {
                ListAdjacentStations.Remove(station);
                if (station.Station1 == oldCode)
                    station.Station1 = codeChange;
                else
                    station.Station2 = codeChange;

                ListAdjacentStations.Add(station);
            }
            else
                throw new DO.WrongIDExeption(code1, "לא נמצאו פרטים עבור התחנה עוקבת המבוקשת");

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, adjacentStationsPath);

        }




        #endregion



        #region User

      
        public DO.User GetUser(string name) //check if the user exsis according to the name
        {
            List<User> ListUsers = new List<User>
            {
               #region User
                new User
                {
                    UserName="Herout",
                    Password="12345",
                    Admin=true,
                    UserExist=true,
                    MailAddress="heroot12@gmail.com"
                },

                new User
                {
                    UserName="Dafna",
                    Password="12345",
                    Admin=true,
                    UserExist=true,
                     MailAddress="da0773412369@gmail.com"

                },
                new User
                {
                    UserName="notUs",
                    Password="12345",
                    Admin=false,
                    UserExist=true,
                     MailAddress="heroot12@gmail.com"
                },
                new User
                {
                    UserName="OriyaShmoel",
                    Password="busbus123",
                    Admin=false,
                    UserExist=true,
                     MailAddress="heroot12@gmail.com"
                },
                 #endregion User
            };
            XMLTools.SaveListToXMLSerializer(ListUsers, userPath);
            List<User> userStations = XMLTools.LoadListFromXMLSerializer<User>(userPath);

            DO.User user = userStations.Find(b => b.UserName == name && b.UserExist);
            if (user != null)
            {
                return user;
            }
            else
                throw new DO.WrongNameExeption(name, $"{1}השם לא קיים במערכת או אחד מהפרטים שהזנת שגוי");/////////////////////////////////////////////////////////

        }
        public IEnumerable<DO.User> GetAlluser() //return all the user that we have
        {
            List<User> userStations = XMLTools.LoadListFromXMLSerializer<User>(userPath);

            return from user in userStations
                   where (user.UserExist)
                   select user;
        }
        public IEnumerable<DO.User> GetAlluserAdmin() //return all the user Admin we have
        {
            List<User> userStations = XMLTools.LoadListFromXMLSerializer<User>(userPath);

            return from user in userStations
                   where (user.UserExist && user.Admin)
                   select user;
        }
        public IEnumerable<DO.User> GetAlluserNAdmin() //return all the user not Admin we have
        {
            List<User> userStations = XMLTools.LoadListFromXMLSerializer<User>(userPath);

            return from user in userStations
                   where (user.UserExist && !user.Admin)
                   select user;
        }

        public IEnumerable<DO.User> GetAlluserBy(Predicate<DO.User> userConditions) //איך כותבים??
        {
            List<User> userStations = XMLTools.LoadListFromXMLSerializer<User>(userPath);

            return from u in userStations
                        where (u.UserExist && userConditions(u))
                        select u;
           
        }

        public void AddUser(DO.User user)
        {
            List<User> userStations = XMLTools.LoadListFromXMLSerializer<User>(userPath);

            if (userStations.FirstOrDefault(b => b.UserName == user.UserName) != null) //if != null its means that this name is allready exsis
                throw new DO.WrongNameExeption(user.UserName, "שם משתמש כבר קיים במערכת, בבקשה הכנס שם אחר");/////////////////////////////////////////////////////////////////
            userStations.Add(user);
            XMLTools.SaveListToXMLSerializer(userStations, userPath);


        }
        public DO.User getUserBy(Predicate<DO.User> userConditions)
        {
            List<User> userStations = XMLTools.LoadListFromXMLSerializer<User>(userPath);

            var users = from u in userStations
                        where (u.UserExist && userConditions(u))
                        select u;
            return users.ElementAt(0);
        }


        public void DeleteUser(string name)
        {
            List<User> userStations = XMLTools.LoadListFromXMLSerializer<User>(userPath);

            DO.User userDelete = userStations.Find(b => b.UserName == name && b.UserExist);
            if (userDelete != null)
            {
                userDelete.UserExist = false;

            }
            else
                throw new DO.WrongNameExeption(name, "לא נמצאו פרטים במערכת עבור משתמש זה");
            XMLTools.SaveListToXMLSerializer(userStations, userPath);

        }

        public void UpdateUser(DO.User user)
        {
            List<User> userStations = XMLTools.LoadListFromXMLSerializer<User>(userPath);

            DO.User u = userStations.Find(b => b.UserName == user.UserName && b.UserExist);
            if (u != null)
            {
                userStations.Remove(u);
                userStations.Add(user);
            }
            else
                throw new DO.WrongNameExeption(user.UserName, "לא נמצאו פרטים במערכת עבור שם זה");
            XMLTools.SaveListToXMLSerializer(userStations, userPath);

        }

        #endregion User



     

    }
}
