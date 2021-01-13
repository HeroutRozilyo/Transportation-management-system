﻿using DALAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Device.Location;
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
        string linePath = @"LineXml.xml"; //XMLSerializer
        string stationPath = @"StationXml.xml"; //XMLSerializer
        string lineStationPath = @"lineStationXml.xml"; //XMLSerializer
         string lineTripPath = @"lineTripXml.xml"; //XMLSerializer
       string adjacentStationsPath = @"AdjacentStationsXml.xml"; //XMLSerializer
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

           List<DO.Bus> ListBus = new List<Bus>
            {
                #region initialization buses
                new Bus
                {
                    Licence="5267008",
                    StartingDate= new DateTime(2013, 02, 05),
                    Kilometrz=22000,
                    KilometrFromLastTreat=2000,
                    FuellAmount=200,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    LastTreatment=new DateTime(12/12/2020),
                    BusExist=true
                },


                new Bus
                {
                    Licence="2784562",
                    StartingDate= new DateTime(2014, 03, 05),
                    Kilometrz=22000,
                    KilometrFromLastTreat=2000,
                    FuellAmount=700,
                       LastTreatment=new DateTime(12/12/2020),
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true

                },
                new Bus
                {
                    Licence="12345678",
                    StartingDate= new DateTime(2019, 02, 05),
                       LastTreatment=new DateTime(12/12/2020),
                    Kilometrz=10000,
                    KilometrFromLastTreat=10000,
                    FuellAmount=340,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },
                new Bus
                {
                    //
                    Licence="5267408",
                    StartingDate= new DateTime(2013, 02, 05),
                       LastTreatment=new DateTime(12/12/2020),
                    Kilometrz=22000,
                    KilometrFromLastTreat=2000,
                    FuellAmount=200,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },
                new Bus
                {
                    Licence="1234567",
                    StartingDate= new DateTime(2013, 09, 21),
                       LastTreatment=new DateTime(12/12/2020),
                    Kilometrz=70000,
                    KilometrFromLastTreat=1500.34,
                    FuellAmount=643.98,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },
                new Bus
                {
                    Licence="7654321",
                    StartingDate= new DateTime(2013, 02, 05),
                    Kilometrz=22000,
                       LastTreatment=new DateTime(12/12/2020),
                    KilometrFromLastTreat=2000,
                    FuellAmount=200,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },
                new Bus
                {
                    Licence="5463728",
                    StartingDate= new DateTime(2013, 06, 20),
                       LastTreatment=new DateTime(12/12/2020),
                    Kilometrz=22000,
                    KilometrFromLastTreat=78500,
                    FuellAmount=350,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },
                new Bus
                {
                    Licence="8216542",
                    StartingDate= new DateTime(2010, 04, 15),
                    Kilometrz=100000,
                       LastTreatment=new DateTime(12/12/2020),
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },
                new Bus
                {
                    Licence="34509814",
                    StartingDate= new DateTime(2019, 02, 20),
                       LastTreatment=new DateTime(12/12/2020),
                    Kilometrz=10500,
                    KilometrFromLastTreat=1400,
                    FuellAmount=300,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },
                new Bus
                {
                    Licence="10926574",
                    StartingDate= new DateTime(2020, 04, 15),
                       LastTreatment=new DateTime(12/12/2020),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },
               new Bus
                {
                    Licence="1192657",
                    StartingDate= new DateTime(2010, 12, 15),
                       LastTreatment=new DateTime(12/12/2020),
                    Kilometrz=28970,
                    KilometrFromLastTreat=8970,
                    FuellAmount=1000,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },
             new Bus
                {
                    Licence="1265473",
                    StartingDate= new DateTime(2009, 07, 18),
                       LastTreatment=new DateTime(12/12/2020),
                    Kilometrz=20000,
                    KilometrFromLastTreat=100,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                      BusExist=true
                },
                new Bus
                {
                    Licence="89712365",
                    StartingDate= new DateTime(2020, 03, 15),
                       LastTreatment=new DateTime(12/12/2020),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },
                new Bus
                {
                    Licence="1778328",
                    StartingDate= new DateTime(2010, 02, 15),
                       LastTreatment=new DateTime(12/12/2020),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },
              new Bus
                {
                    Licence="5059589",
                    StartingDate= new DateTime(1999, 04, 15),
                       LastTreatment=new DateTime(12/12/2020),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },
                                                                                                                          new Bus
                {
                    Licence="12845999",
                    StartingDate= new DateTime(2020, 01, 15),
                       LastTreatment=new DateTime(12/12/2020),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },
                                                                                                                          new Bus
                {
                    Licence="2000000",
                       LastTreatment=new DateTime(12/12/2020),
                    StartingDate= new DateTime(2012, 07, 25),
                    Kilometrz=109283,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },
                                                                                                                      new Bus
                {
                    Licence="11119999",
                    StartingDate= new DateTime(2020, 08, 15),
                       LastTreatment=new DateTime(12/12/2020),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },
                                                                                                                            new Bus
                {
                    Licence="8576669",
                    StartingDate= new DateTime(2017, 04, 15),
                       LastTreatment=new DateTime(12/12/2020),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                     BusExist=true
                },
                                                                                                                          new Bus
                {
                   Licence="10928300",
                    StartingDate= new DateTime(2020, 09, 22),
                       LastTreatment=new DateTime(12/12/2020),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExist=true
                },

                #endregion
            };
            XMLTools.SaveListToXMLSerializer(ListBus, busPath);
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
            List<LineTrip> ListLineTrip = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            DO.LineTrip linetrip = ListLineTrip.Find(b => b.KeyId == idline && b.StartAt == start && b.TripLineExist == true);
            if (linetrip != null)
            {
                return linetrip;
            }
            else
                throw new DO.WrongLineTripExeption(idline, $"{start} לא נמצאו פרטים עבור הקו המבוקש בשעה זו");
        }

        public IEnumerable<LineTrip> GetAllTripline(int idline)
        {
            List<LineTrip> ListLineTrip = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            return from linetrip in ListLineTrip
                   where (linetrip.KeyId == idline && linetrip.TripLineExist == true)
                   select linetrip;

        }

        public IEnumerable<LineTrip> GetAllLineTripsBy(Predicate<LineTrip> StationsLinecondition)
        {
            List<LineTrip> ListLineTrip = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            return from linetrip in ListLineTrip
                   where (StationsLinecondition(linetrip) && linetrip.TripLineExist == true)
                   select linetrip;
            

        }

        public void AddLineTrip(LineTrip lineTrip)
        {
            List<LineTrip> ListLineTrip = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);
            ListLineTrip.Add(lineTrip);
            XMLTools.SaveListToXMLSerializer(ListLineTrip, lineTripPath);

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
            List<LineTrip> ListLineTrip = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);


            DO.LineTrip station = ListLineTrip.Find(b => b.KeyId == lineTrip.KeyId && lineTrip.StartAt == b.StartAt && lineTrip.TripLineExist == true);
            if (station != null)
            {
                ListLineTrip.Remove(station);
                ListLineTrip.Add(lineTrip);
            }
            else
                throw new DO.WrongLineTripExeption(lineTrip.KeyId, "לא נמצאו זמני נסיעות עבור קו זה");
            XMLTools.SaveListToXMLSerializer(ListLineTrip, lineTripPath);

        }


        #endregion

        #region AdjacentStations
        /*
   
         
         
         */

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




    
        

        public void AddTrip(Trip trip)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
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





  
  
  

     
 
   


        public IEnumerable<Trip> GetAllTrip()
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


     

        public IEnumerable<object> GetLineFields(Func<int, bool, object> generate)
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
