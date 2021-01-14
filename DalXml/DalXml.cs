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
            List<LineStation> ListLineStations1 = new List<LineStation>
            {
                #region line station
                //line number 18
                new LineStation
                {
                    LineId=1,
                    StationCode=73,
                    LineStationIndex=1,
                    LineStationExist=true,
                    PrevStation=0,
                    NextStation=76,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=76,
                    LineStationIndex=2,
                    LineStationExist=true,
                    PrevStation=73,
                    NextStation=77,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=77,
                    LineStationIndex=3,
                    LineStationExist=true,
                    PrevStation=76,
                    NextStation=78,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=78,
                    LineStationIndex=4,
                    LineStationExist=true,
                    PrevStation=77,
                    NextStation=83,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=83,
                    LineStationIndex=5,
                    LineStationExist=true,
                    PrevStation=78,
                    NextStation=84,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=84,
                    LineStationIndex=6,
                    LineStationExist=true,
                    PrevStation=83,
                    NextStation=85,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=85,
                    LineStationIndex=7,
                    LineStationExist=true,
                    PrevStation=84,
                    NextStation=86,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=86,
                    LineStationIndex=8,
                    LineStationExist=true,
                    PrevStation=85,
                    NextStation=88,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=88,
                    LineStationIndex=9,
                    LineStationExist=true,
                    PrevStation=86,
                    NextStation=89,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=89,
                    LineStationIndex=10,
                    LineStationExist=true,
                    PrevStation=88,
                    NextStation=0,
                },

             //line 10
                new LineStation
                {
                    LineId=2,
                    StationCode=85,
                    LineStationIndex=1,
                    LineStationExist=true,
                    PrevStation=0,
                    NextStation=86,
                },
                new LineStation
                {
                    LineId=2,
                    StationCode=86,
                    LineStationIndex=2,
                    LineStationExist=true,
                    PrevStation=85,
                    NextStation=88,
                },
                new LineStation
                {
                    LineId=2,
                    StationCode=88,
                    LineStationIndex=3,
                    LineStationExist=true,
                    PrevStation=86,
                    NextStation=89,
                },
                new LineStation
                {
                    LineId=2,
                    StationCode=89,
                    LineStationIndex=4,
                    LineStationExist=true,
                    PrevStation=88,
                    NextStation=90,
                },
                new LineStation
                {
                    LineId=2,
                    StationCode=90,
                    LineStationIndex=5,
                    LineStationExist=true,
                    PrevStation=89,
                    NextStation=91,
                },
                new LineStation
                {
                    LineId=2,
                    StationCode=91,
                    LineStationIndex=6,
                    LineStationExist=true,
                    PrevStation=90,
                    NextStation=93,
                },
                new LineStation
                {
                    LineId=2,
                    StationCode=93,
                    LineStationIndex=7,
                    LineStationExist=true,
                    PrevStation=91,
                    NextStation=94,
                },
                new LineStation
                {
                    LineId=2,
                    StationCode=94,
                    LineStationIndex=8,
                    LineStationExist=true,
                    PrevStation=93,
                    NextStation=95,
                },
                new LineStation
                {
                    LineId=2,
                    StationCode=95,
                    LineStationIndex=9,
                    LineStationExist=true,
                    PrevStation=94,
                    NextStation=97,
                },
                new LineStation
                {
                    LineId=2,
                    StationCode=97,
                    LineStationIndex=10,
                    LineStationExist=true,
                    PrevStation=95,
                    NextStation=0,
                }, 
             //line 5         
                new LineStation
                {
                    LineId=3,
                    StationCode=122,
                    LineStationIndex=1,
                    LineStationExist=true,
                    PrevStation=0,
                    NextStation=123,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=123,
                    LineStationIndex=2,
                    LineStationExist=true,
                    PrevStation=122,
                    NextStation=121,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=121,
                    LineStationIndex=3,
                    LineStationExist=true,
                    PrevStation=123,
                    NextStation=1524,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=1524,
                    LineStationIndex=4,
                    LineStationExist=true,
                    PrevStation=121,
                    NextStation=1523,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=1523,
                    LineStationIndex=5,
                    LineStationExist=true,
                    PrevStation=1524,
                    NextStation=1522,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=1522,
                    LineStationIndex=6,
                    LineStationExist=true,
                    PrevStation=1523,
                    NextStation=1518,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=1518,
                    LineStationIndex=7,
                    LineStationExist=true,
                    PrevStation=1522,
                    NextStation=1514,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=1514,
                    LineStationIndex=8,
                    LineStationExist=true,
                    PrevStation=1518,
                    NextStation=1512,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=1512,
                    LineStationIndex=9,
                    LineStationExist=true,
                    PrevStation=1514,
                    NextStation=1511,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=1511,
                    LineStationIndex=10,
                    LineStationExist=true,
                    PrevStation=1512,
                    NextStation=0,
                },

                //line=6
                new LineStation
                {
                    LineId=4,
                    StationCode=121,
                    LineStationIndex=1,
                    LineStationExist=true,
                    PrevStation=0,
                    NextStation=123,
                },
                new LineStation
                {
                    LineId=4,
                    StationCode=123,
                    LineStationIndex=2,
                    LineStationExist=true,
                    PrevStation=121,
                    NextStation=122,
                },
                new LineStation
                {
                    LineId=4,
                    StationCode=122,
                    LineStationIndex=3,
                    LineStationExist=true,
                    PrevStation=123,
                    NextStation=1524,
                },
                new LineStation
                {
                    LineId=4,
                    StationCode=1524,
                    LineStationIndex=4,
                    LineStationExist=true,
                    PrevStation=122,
                    NextStation=1523,
                },
                new LineStation
                {
                    LineId=4,
                    StationCode=1523,
                    LineStationIndex=5,
                    LineStationExist=true,
                    PrevStation=1524,
                    NextStation=1522,
                },
                new LineStation
                {
                    LineId=4,
                    StationCode=1522,
                    LineStationIndex=6,
                    LineStationExist=true,
                    PrevStation=1523,
                    NextStation=1518,
                },
                new LineStation
                {
                    LineId=4,
                    StationCode=1518,
                    LineStationIndex=7,
                    LineStationExist=true,
                    PrevStation=1522,
                    NextStation=1514,
                },
                new LineStation
                {
                    LineId=4,
                    StationCode=1514,
                    LineStationIndex=8,
                    LineStationExist=true,
                    PrevStation=1518,
                    NextStation=1512,
                },
                new LineStation
                {
                    LineId=4,
                    StationCode=1512,
                    LineStationIndex=9,
                    LineStationExist=true,
                    PrevStation=1514,
                    NextStation=1491,
                },
                new LineStation
                {
                    LineId=4,
                    StationCode=1491,
                    LineStationIndex=10,
                    LineStationExist=true,
                    PrevStation=1512,
                    NextStation=0,
                }, 

                //line=33
                new LineStation
                {
                    LineId=5,
                    StationCode=119,
                    LineStationIndex=1,
                    LineStationExist=true,
                    PrevStation=0,
                    NextStation=1485,
                },
                new LineStation
                {
                    LineId=5,
                    StationCode=1485,
                    LineStationIndex=2,
                    LineStationExist=true,
                    PrevStation=119,
                    NextStation=1486,
                },
                new LineStation
                {
                    LineId=5,
                    StationCode=1486,
                    LineStationIndex=3,
                    LineStationExist=true,
                    PrevStation=1485,
                    NextStation=1487,
                },
                new LineStation
                {
                    LineId=5,
                    StationCode=1487,
                    LineStationIndex=4,
                    LineStationExist=true,
                    PrevStation=1486,
                    NextStation=1488,
                },
                new LineStation
                {
                    LineId=5,
                    StationCode=1488,
                    LineStationIndex=5,
                    LineStationExist=true,
                    PrevStation=1487,
                    NextStation=1490,
                },
                new LineStation
                {
                    LineId=5,
                    StationCode=1490,
                    LineStationIndex=6,
                    LineStationExist=true,
                    PrevStation=1488,
                    NextStation=1494,
                },
                new LineStation
                {
                    LineId=5,
                    StationCode=1494,
                    LineStationIndex=7,
                    LineStationExist=true,
                    PrevStation=1490,
                    NextStation=1492,
                },
                new LineStation
                {
                    LineId=5,
                    StationCode=1492,
                    LineStationIndex=8,
                    LineStationExist=true,
                    PrevStation=1494,
                    NextStation=1493,
                },
                new LineStation
                {
                    LineId=5,
                    StationCode=1493,
                    LineStationIndex=9,
                    LineStationExist=true,
                    PrevStation=1492,
                    NextStation=1491,
                },
                new LineStation
                {
                    LineId=5,
                    StationCode=1491,
                    LineStationIndex=10,
                    LineStationExist=true,
                    PrevStation=1493,
                    NextStation=0,
                },

                //line=67,
                new LineStation
                {
                    LineId=6,
                    StationCode=110,
                    LineStationIndex=1,
                    LineStationExist=true,
                    PrevStation=0,
                    NextStation=111,
                },
                new LineStation
                {
                    LineId=6,
                    StationCode=111,
                    LineStationIndex=2,
                    LineStationExist=true,
                    PrevStation=110,
                    NextStation=112,
                },
                new LineStation
                {
                    LineId=6,
                    StationCode=112,
                    LineStationIndex=3,
                    LineStationExist=true,
                    PrevStation=111,
                    NextStation=113,
                },
                new LineStation
                {
                    LineId=6,
                    StationCode=113,
                    LineStationIndex=4,
                    LineStationExist=true,
                    PrevStation=112,
                    NextStation=115,
                },
                new LineStation
                {
                    LineId=6,
                    StationCode=115,
                    LineStationIndex=5,
                    LineStationExist=true,
                    PrevStation=113,
                    NextStation=116,
                },
                new LineStation
                {
                    LineId=6,
                    StationCode=116,
                    LineStationIndex=6,
                    LineStationExist=true,
                    PrevStation=115,
                    NextStation=117,
                },
                new LineStation
                {
                    LineId=6,
                    StationCode=117,
                    LineStationIndex=7,
                    LineStationExist=true,
                    PrevStation=116,
                    NextStation=119,
                },
                new LineStation
                {
                    LineId=6,
                    StationCode=119,
                    LineStationIndex=8,
                    LineStationExist=true,
                    PrevStation=117,
                    NextStation=1485,
                },
                new LineStation
                {
                    LineId=6,
                    StationCode=1485,
                    LineStationIndex=9,
                    LineStationExist=true,
                    PrevStation=119,
                    NextStation=1486,
                },
                new LineStation
                {
                    LineId=6,
                    StationCode=1486,
                    LineStationIndex=10,
                    LineStationExist=true,
                    PrevStation=1485,
                    NextStation=0,
                },

                //line=24,
                new LineStation
                {
                    LineId=7,
                    StationCode=97,
                    LineStationIndex=1,
                    LineStationExist=true,
                    PrevStation=0,
                    NextStation=102,
                },
                new LineStation
                {
                    LineId=7,
                    StationCode=102,
                    LineStationIndex=2,
                    LineStationExist=true,
                    PrevStation=97,
                    NextStation=103,
                },
                new LineStation
                {
                    LineId=7,
                    StationCode=103,
                    LineStationIndex=3,
                    LineStationExist=true,
                    PrevStation=102,
                    NextStation=105,
                },
                new LineStation
                {
                    LineId=7,
                    StationCode=105,
                    LineStationIndex=4,
                    LineStationExist=true,
                    PrevStation=103,
                    NextStation=106,
                },
                new LineStation
                {
                    LineId=7,
                    StationCode=106,
                    LineStationIndex=5,
                    LineStationExist=true,
                    PrevStation=105,
                    NextStation=108,
                },
                new LineStation
                {
                    LineId=7,
                    StationCode=108,
                    LineStationIndex=6,
                    LineStationExist=true,
                    PrevStation=106,
                    NextStation=109,
                },
                new LineStation
                {
                    LineId=7,
                    StationCode=109,
                    LineStationIndex=7,
                    LineStationExist=true,
                    PrevStation=108,
                    NextStation=110,
                },
                new LineStation
                {
                    LineId=7,
                    StationCode=110,
                    LineStationIndex=8,
                    LineStationExist=true,
                    PrevStation=109,
                    NextStation=112,
                },
                new LineStation
                {
                    LineId=7,
                    StationCode=112,
                    LineStationIndex=9,
                    LineStationExist=true,
                    PrevStation=110,
                    NextStation=111,
                },
                new LineStation
                {
                    LineId=7,
                    StationCode=111,
                    LineStationIndex=10,
                    LineStationExist=true,
                    PrevStation=112,
                    NextStation=0,
                },
                
                //  NumberLine=20
                new LineStation
                {
                    LineId=8,
                    StationCode=102,
                    LineStationIndex=1,
                    LineStationExist=true,
                    PrevStation=0,
                    NextStation=103,
                },
                new LineStation
                {
                    LineId=8,
                    StationCode=103,
                    LineStationIndex=2,
                    LineStationExist=true,
                    PrevStation=102,
                    NextStation=105,
                },
                new LineStation
                {
                    LineId=8,
                    StationCode=105,
                    LineStationIndex=3,
                    LineStationExist=true,
                    PrevStation=103,
                    NextStation=106,
                },
                new LineStation
                {
                    LineId=8,
                    StationCode=106,
                    LineStationIndex=4,
                    LineStationExist=true,
                    PrevStation=105,
                    NextStation=108,
                },
                new LineStation
                {
                    LineId=8,
                    StationCode=108,
                    LineStationIndex=5,
                    LineStationExist=true,
                    PrevStation=106,
                    NextStation=109,
                },
                new LineStation
                {
                    LineId=8,
                    StationCode=109,
                    LineStationIndex=6,
                    LineStationExist=true,
                    PrevStation=108,
                    NextStation=110,
                },
                new LineStation
                {
                    LineId=8,
                    StationCode=110,
                    LineStationIndex=7,
                    LineStationExist=true,
                    PrevStation=109,
                    NextStation=111,
                },
                new LineStation
                {
                    LineId=8,
                    StationCode=111,
                    LineStationIndex=8,
                    LineStationExist=true,
                    PrevStation=110,
                    NextStation=112,
                },
                new LineStation
                {
                    LineId=8,
                    StationCode=112,
                    LineStationIndex=9,
                    LineStationExist=true,
                    PrevStation=111,
                    NextStation=116,
                },
                new LineStation
                {
                    LineId=8,
                    StationCode=116,
                    LineStationIndex=10,
                    LineStationExist=true,
                    PrevStation=112,
                    NextStation=0,
                },

                //line=27
                new LineStation
                {
                    LineId=9,
                    StationCode=85,
                    LineStationIndex=1,
                    LineStationExist=true,
                    PrevStation=0,
                    NextStation=86,
                },
                new LineStation
                {
                    LineId=9,
                    StationCode=86,
                    LineStationIndex=2,
                    LineStationExist=true,
                    PrevStation=85,
                    NextStation=88,
                },
                new LineStation
                {
                    LineId=9,
                    StationCode=88,
                    LineStationIndex=3,
                    LineStationExist=true,
                    PrevStation=86,
                    NextStation=89,
                },
                new LineStation
                {
                    LineId=9,
                    StationCode=89,
                    LineStationIndex=4,
                    LineStationExist=true,
                    PrevStation=88,
                    NextStation=90,
                },
                new LineStation
                {
                    LineId=9,
                    StationCode=90,
                    LineStationIndex=5,
                    LineStationExist=true,
                    PrevStation=89,
                    NextStation=91,
                },
                new LineStation
                {
                    LineId=9,
                    StationCode=91,
                    LineStationIndex=6,
                    LineStationExist=true,
                    PrevStation=90,
                    NextStation=93,
                },
                new LineStation
                {
                    LineId=9,
                    StationCode=93,
                    LineStationIndex=7,
                    LineStationExist=true,
                    PrevStation=91,
                    NextStation=94,
                },
                new LineStation
                {
                    LineId=9,
                    StationCode=94,
                    LineStationIndex=8,
                    LineStationExist=true,
                    PrevStation=93,
                    NextStation=95,
                },
                new LineStation
                {
                    LineId=9,
                    StationCode=95,
                    LineStationIndex=9,
                    LineStationExist=true,
                    PrevStation=94,
                    NextStation=102,
                },
                new LineStation
                {
                    LineId=9,
                    StationCode=102,
                    LineStationIndex=10,
                    LineStationExist=true,
                    PrevStation=95,
                    NextStation=0,
                },

                //line=21,
                new LineStation
                {
                    LineId=10,
                    StationCode=111,
                    LineStationIndex=1,
                    LineStationExist=true,
                    PrevStation=0,
                    NextStation=112,
                },
                new LineStation
                {
                    LineId=10,
                    StationCode=112,
                    LineStationIndex=2,
                    LineStationExist=true,
                    PrevStation=111,
                    NextStation=113,
                },
                new LineStation
                {
                    LineId=10,
                    StationCode=113,
                    LineStationIndex=3,
                    LineStationExist=true,
                    PrevStation=112,
                    NextStation=115,
                },
                new LineStation
                {
                    LineId=10,
                    StationCode=115,
                    LineStationIndex=4,
                    LineStationExist=true,
                    PrevStation=113,
                    NextStation=116,
                },
                new LineStation
                {
                    LineId=10,
                    StationCode=116,
                    LineStationIndex=5,
                    LineStationExist=true,
                    PrevStation=115,
                    NextStation=117,
                },
                new LineStation
                {
                    LineId=10,
                    StationCode=117,
                    LineStationIndex=6,
                    LineStationExist=true,
                    PrevStation=116,
                    NextStation=119,
                },
                new LineStation
                {
                    LineId=10,
                    StationCode=119,
                    LineStationIndex=7,
                    LineStationExist=true,
                    PrevStation=117,
                    NextStation=1485,
                },
                new LineStation
                {
                    LineId=10,
                    StationCode=1485,
                    LineStationIndex=8,
                    LineStationExist=true,
                    PrevStation=119,
                    NextStation=1486,
                },
                new LineStation
                {
                    LineId=10,
                    StationCode=1486,
                    LineStationIndex=9,
                    LineStationExist=true,
                    PrevStation=1485,
                    NextStation=1488,
                },
                new LineStation
                {
                    LineId=10,
                    StationCode=1488,
                    LineStationIndex=10,
                    LineStationExist=true,
                    PrevStation=1486,
                    NextStation=0,
                },

               #endregion

            };
            XMLTools.SaveListToXMLSerializer(ListLineStations1, lineStationPath);
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
            #region
            //XElement a = new XElement(lineTripPath);
            //List<LineTrip> ListLineTrip1 = new List<LineTrip>
            //{
            //     #region LineTrip
            //    new LineTrip
            //    {
            //     KeyId=1,
            //     StartAt=new TimeSpan(06,00,00),
            //     FinishAt=new TimeSpan(23,59,59),
            //     TripLineExist=true,
            //     Frequency=19,
            //     },
            //    new LineTrip
            //    {
            //     KeyId=2,
            //     StartAt=new TimeSpan(06,00,00),
            //     FinishAt=new TimeSpan(24,00,00),
            //     TripLineExist=true,
            //     Frequency=13,
            //     },
            //    new LineTrip
            //    {
            //     KeyId=3,
            //     StartAt=new TimeSpan(06,00,00),
            //     FinishAt=new TimeSpan(23,59,59),
            //     TripLineExist=true,
            //     Frequency=15,
            //     },
            //    new LineTrip
            //    {
            //     KeyId=4,
            //     StartAt=new TimeSpan(06,00,00),
            //     FinishAt=new TimeSpan(19,00,00),
            //     TripLineExist=true,
            //     Frequency=10,
            //     },
            //    new LineTrip
            //    {
            //     KeyId=4,
            //     StartAt=new TimeSpan(19,00,00),
            //     FinishAt=new TimeSpan(23,59,59),
            //     TripLineExist=true,
            //     Frequency=30,
            //     },
            //    new LineTrip
            //    {
            //     KeyId=5,
            //     StartAt=new TimeSpan(06,00,00),
            //     FinishAt=new TimeSpan(08,30,00),
            //     TripLineExist=true,
            //     Frequency=3,
            //     },
            //    new LineTrip
            //    {
            //     KeyId=5,
            //     StartAt=new TimeSpan(08,30,00),
            //     FinishAt=new TimeSpan(14,00,00),
            //     TripLineExist=true,
            //     Frequency=60,
            //     },
            //    new LineTrip
            //    {
            //     KeyId=5,
            //     StartAt=new TimeSpan(14,00,00),
            //     FinishAt=new TimeSpan(23,59,59),
            //     TripLineExist=true,
            //     Frequency=20,
            //     },
            //    new LineTrip
            //    {
            //     KeyId=6,
            //     StartAt=new TimeSpan(06,00,00),
            //     FinishAt=new TimeSpan(23,59,59),
            //     TripLineExist=true,
            //     Frequency=17,
            //     },
            //    new LineTrip
            //    {
            //     KeyId=7,
            //     StartAt=new TimeSpan(08,00,00),
            //     FinishAt=new TimeSpan(23,59,59),
            //     TripLineExist=true,
            //     Frequency=30,
            //     },
            //    new LineTrip
            //    {
            //     KeyId=8,
            //     StartAt=new TimeSpan(01,00,00),
            //     FinishAt=new TimeSpan(06,00,00),
            //     TripLineExist=true,
            //     Frequency=60,
            //     },
            //    new LineTrip
            //    {
            //     KeyId=9,
            //     StartAt=new TimeSpan(05,00,00),
            //     FinishAt=new TimeSpan(10,00,00),
            //     TripLineExist=true,
            //     Frequency=20,
            //     },
            //    new LineTrip
            //    {
            //     KeyId=9,
            //     StartAt=new TimeSpan(10,00,00),
            //     FinishAt=new TimeSpan(23,59,59),
            //     TripLineExist=true,
            //     Frequency=40,
            //     },
            //    new LineTrip
            //    {
            //     KeyId=10,
            //     StartAt=new TimeSpan(6,00,00),
            //     FinishAt=new TimeSpan(23,59,59),
            //     TripLineExist=true,
            //     Frequency=40,
            //     }
            //     #endregion LineTrip
            //};
            //a.Add(XMLTools.ToXML(ListLineTrip1[0]));
            //for (int i = 1; i < ListLineTrip1.Count(); i++)
            //{

            //    a.Add(XMLTools.ToXML(ListLineTrip1[i]));
            //}
            //XMLTools.SaveListToXMLElement(a, lineTripPath);
            #endregion

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
            XElement ListLineTrip = XMLTools.LoadListFromXMLElement(lineTripPath);
           // List<LineTrip> ListLineTrip = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            foreach (var item in ListLineTrip.Elements())
                if (item.Element("KeyId").Value == idline.ToString())
                    item.Element("TripLineExist").Value = "false";
            XMLTools.SaveListToXMLElement(ListLineTrip, lineTripPath);

        }

        public void DeleteLineTrip1(LineTrip lineTrip)
        {
            XElement ListLineTrip = XMLTools.LoadListFromXMLElement(lineTripPath);
            //List<LineTrip> ListLineTrip = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);
            foreach (var item in ListLineTrip.Elements())
                if (item.Element("KeyId").Value == lineTrip.KeyId.ToString()&& item.Element("StartAt").Value == lineTrip.StartAt.ToString())
                    item.Element("TripLineExist").Value = "false";
            XMLTools.SaveListToXMLElement(ListLineTrip, lineTripPath);
            //int index = ListLineTrip.FindIndex(b => b.KeyId == lineTrip.KeyId && b.StartAt == lineTrip.StartAt && b.FinishAt == lineTrip.FinishAt);
            //ListLineTrip[index].TripLineExist = false;
            //XMLTools.SaveListToXMLSerializer(ListLineTrip, lineTripPath);

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
            DO.AdjacentStations linestations = ListAdjacentStations.Find(b => b.Station1 == Scode1 && b.Station2 == Scode2&&b.AdjacExsis);//|| b.Station1 == Scode2 && b.Station2 == Scode1);
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
                   where ((stationCode == station.Station1 || stationCode == station.Station2)&&station.AdjacExsis)
                   select station;            
        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> StationsLinecondition)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);


            return from stations in ListAdjacentStations
                   where (StationsLinecondition(stations)&&stations.AdjacExsis)
                   select stations;
            
        }


    
        public void AddLineStations(DO.AdjacentStations adjacentStations)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);

            DO.AdjacentStations temp = ListAdjacentStations.Find(b => b.Station1 == adjacentStations.Station1 && b.Station2 == adjacentStations.Station2&&adjacentStations.AdjacExsis);
            if (temp != null)
                throw new DO.WrongIDExeption(adjacentStations.Station1, " התחנה עוקבת כבר קיימת במערכת");
            ListAdjacentStations.Add(adjacentStations);

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, adjacentStationsPath);


        }

        public void DeleteAdjacentStationse(int Scode1, int Scode2)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);

            DO.AdjacentStations stations = ListAdjacentStations.Find(b => b.Station1 == Scode1 && b.Station2 == Scode2&&b.AdjacExsis);
            if (stations != null)
            {
                stations.AdjacExsis = false;
              //  ListAdjacentStations.Remove(stations);
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
