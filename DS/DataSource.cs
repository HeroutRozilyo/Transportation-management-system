using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using System.Device.Location;


namespace DS
{
    public static class DataSource
    {
        public static List<Bus> ListBus;
        public static List<Stations> ListStations;
        public static List<Line> ListLine;
        public static List<LineStation> ListLineStations;
        public static List<User> ListUsers;
        public static List<LineTrip> ListLineTrip;
        public static List<BusOnTrip> ListBusOnTrip;
        public static List<Trip> ListTrip;
        public static List<AdjacentStations> ListAdjacentStations;

        static DataSource()
        {
            InitAllLists();
        }

        static void InitAllLists()
        {
            double speed = 666.66;//m/s- 40 km/h



            ListStations = new List<Stations>
            {
                #region initialization stations//איתחול תחנות
                new Stations
                {
                    Code = 73,
                    Name = "שדרות גולדה מאיר/המשורר אצ''ג",
                    Address = "שדרות גולדה מאיר ,ירושלים ",
                    Coordinate= new GeoCoordinate( 31.825302,35.188624),
                    StationExist=true,

                },
                new Stations
                {
                    Code = 76,
                    Name = "בית ספר צור באהר בנות/אלמדינה אלמונוורה",
                    Address = "רחוב:אל מדינה אל מונאוורה  עיר: ירושלים",
                     Coordinate= new GeoCoordinate(31.738425,35.228765),
                     StationExist=true,
                },
                new Stations
                {
                    Code = 77,
                    Name = "בית ספר אבן רשד/אלמדינה אלמונוורה",
                    Address = "רחוב:אל מדינה אל מונאוורה  עיר: ירושלים ",
                    Coordinate= new GeoCoordinate(31.738676,35.226704),
                    StationExist=true,
                },
                new Stations
                {
                    Code = 78,
                    Name = "שרי ישראל/יפו",
                    Address = "רחוב:שדרות שרי ישראל 15 עיר: ירושלים",
                    Coordinate= new GeoCoordinate(31.789128,35.206146),
                    StationExist=true,
                },
                new Stations
                {
                    Code = 83,
                    Name = "בטן אלהווא/חוש אל מרג",
                    Address = "רחוב:בטן אל הווא  עיר: ירושלים",
                    StationExist=true,
                     Coordinate= new GeoCoordinate(31.766358,35.240417)

                },
                new Stations
                {
                    Code = 84,
                    Name = "מלכי ישראל/הטורים",
                    Address = " רחוב:מלכי ישראל 77 עיר: ירושלים ",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.790758,35.209791)

                },
                new Stations
                {
                    Code = 85,
                    Name = "בית ספר לבנים/אלמדארס",
                    Address = "רחוב:אלמדארס  עיר: ירושלים",
                    StationExist=true,
                     Coordinate= new GeoCoordinate(31.768643,35.238509)

                },
                new Stations
                {
                    Code = 86,
                    Name = "מגרש כדורגל/אלמדארס",
                    Address = "רחוב:אלמדארס  עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.769899,35.23973)

                },
                new Stations
                {
                    Code = 88,
                    Name = "בית ספר לבנות/בטן אלהוא",
                    Address = " רחוב:בטן אל הווא  עיר: ירושלים",
                    StationExist=true,
                     Coordinate= new GeoCoordinate(31.767064,35.238443)

                },
                new Stations
                {
                    Code = 89,
                    Name = "דרך בית לחם הישה/ואדי קדום",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים ",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.765863,35.247198)

                },
                new Stations
                {
                    Code = 90,
                    Name = "גולדה/הרטום",
                    Address = "רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    StationExist=true,
                     Coordinate= new GeoCoordinate(31.799804,35.213021)

                },
                new Stations
                {
                    Code = 91,
                    Name = "דרך בית לחם הישה/ואדי קדום",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים ",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.765717,35.247102)
                    
                },
                new Stations
                {
                    Code = 93,
                    Name = "חוש סלימה 1",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.767265,35.246594)
                    
                },
                new Stations
                {
                    Code = 94,
                    Name = "דרך בית לחם הישנה ב",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.767084,35.246655)
                   
                },
                new Stations
                {
                    Code = 95,
                    Name = "דרך בית לחם הישנה א",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.768759,31.768759)
                    
                },
                new Stations
                {
                    Code = 97,
                    Name = "שכונת בזבז 2",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.77002,35.24348)
                    
                },
                new Stations
                {
                    Code = 102,
                    Name = "גולדה/שלמה הלוי",
                    Address = " רחוב:שדרות גולדה מאיר  עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.8003,35.208257)
                    
                },
                new Stations
                {
                    Code = 103,
                    Name = "גולדה/הרטום",
                    Address = " רחוב:שדרות גולדה מאיר  עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.8,35.214106)
                   
                },
                new Stations
                {
                    Code = 105,
                    Name = "גבעת משה",
                    Address = " רחוב:גבעת משה 2 עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.797708,35.217133)
                    
                },
                new Stations
                {
                    Code = 106,
                    Name = "גבעת משה",
                    Address = " רחוב:גבעת משה 3 עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.797535,35.217057)
                    
                },
                //20
                new Stations
                {
                    Code = 108,
                    Name = "עזרת תורה/עלי הכהן",
                    Address = "  רחוב:עזרת תורה 25 עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.797535,35.213728)
                    
                },
                new Stations
                {
                    Code = 109,
                    Name = "עזרת תורה/דורש טוב",
                    Address = "  רחוב:עזרת תורה 21 עיר: ירושלים ",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.796818,35.212936)
                    
                },
                new Stations
                {
                    Code = 110,
                    Name = "עזרת תורה/דורש טוב",
                    Address = " רחוב:עזרת תורה 12 עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.796129,35.212698)
                    
                },
                new Stations
                {
                    Code = 111,
                    Name = "יעקובזון/עזרת תורה",
                    Address = "  רחוב:יעקובזון 1 עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.794631,35.21161)
                    
                },
                new Stations
                {
                    Code = 112,
                    Name = "יעקובזון/עזרת תורה",
                    Address = " רחוב:יעקובזון  עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.79508,35.211684)
                   
                },
                //25
                new Stations
                {
                    Code = 113,
                    Name = "זית רענן/אוהל יהושע",
                    Address = "  רחוב:זית רענן 1 עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.796255,35.211065)
                   
                },
                new Stations
                {
                    Code = 115,
                    Name = "זית רענן/תורת חסד",
                    Address = " רחוב:זית רענן  עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.798423,35.209575)
                    
                },
                new Stations
                {
                    Code = 116,
                    Name = "זית רענן/תורת חסד",
                    Address = "  רחוב:הרב סורוצקין 48 עיר: ירושלים ",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.798689,35.208878)
                    
                },
                new Stations
                {
                    Code = 117,
                    Name = "קרית הילד/סורוצקין",
                    Address = "  רחוב:הרב סורוצקין  עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.799165,35.206918)
                    
                },
                new Stations
                {
                    Code = 119,
                    Name = "סורוצקין/שנירר",
                    Address = "  רחוב:הרב סורוצקין 31 עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.797829,35.205601)
                    
                },

                //#endregion //30
                new Stations
                {
                    Code = 1485,
                    Name = "שדרות נווה יעקוב/הרב פרדס ",
                    Address = "רחוב: שדרות נווה יעקוב  עיר:ירושלים ",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.840063,35.240062)
                    

                },
                new Stations
                {
                    Code = 1486,
                    Name = "מרכז קהילתי /שדרות נווה יעקוב",
                    Address = "רחוב:שדרות נווה יעקוב ירושלים עיר:ירושלים ",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.838481,35.23972)
                   
                },


                new Stations
                {
                    Code = 1487,
                    Name = " מסוף 700 /שדרות נווה יעקוב ",
                    StationExist=true,
            Address = "רחוב:שדרות נווה יעקב 7 עיר: ירושלים  ",
            Coordinate= new GeoCoordinate(31.837748,35.231598)
                   
                },
                new Stations
                {
                    Code = 1488,
                    Name = " הרב פרדס/אסטורהב ",
                    StationExist=true,
                    Address = "רחוב:מעגלות הרב פרדס  עיר: ירושלים רציף  ",
                    Coordinate= new GeoCoordinate(31.840279,35.246272)
                    
                },
                new Stations
                {
                    Code = 1490,
                    Name = "הרב פרדס/צוקרמן ",
                    Address = "רחוב:מעגלות הרב פרדס 24 עיר: ירושלים   ",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.843598,35.243639)
                    
                },
                new Stations
                {
                    Code = 1491,
                    Name = "ברזיל ",
                    Address = "רחוב:ברזיל 14 עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.766256,35.173)
                   
                },
                new Stations
                {
                    Code = 1492,
                    Name = "בית וגן/הרב שאג ",
                    Address = "רחוב:בית וגן 61 עיר: ירושלים ",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.76736,35.184771)
                    
                },
                new Stations
                {
                    Code = 1493,
                    Name = "בית וגן/עוזיאל ",
                    Address = "רחוב:בית וגן 21 עיר: ירושלים    ",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.770543,35.183999)
                    
                },
                new Stations
                {
                    Code = 1494,
                    Name = " קרית יובל/שמריהו לוין ",
                    Address = "רחוב:ארתור הנטקה  עיר: ירושלים    ",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.768465,35.178701)
                    
                },
                new Stations
                {
                    Code = 1510,
                    Name = " קורצ'אק / רינגלבלום ",
                    StationExist=true,
                    Address = "רחוב:יאנוש קורצ'אק 7 עיר: ירושלים",
                    Coordinate= new GeoCoordinate(31.759534,35.173688)
                   
                },
                new Stations
                {
                    Code = 1511,
                    Name = " טהון/גולומב ",
                    Address = "רחוב:יעקב טהון  עיר: ירושלים     ",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.761447,35.175929)
                   
                },
                new Stations
                {
                    Code = 1512,
                    Name = "הרב הרצוג/שח''ל ",
                    Address = "רחוב:הרב הרצוג  עיר: ירושלים רציף",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.761447,35.199936)
                    
                },
                new Stations
                {
                    Code = 1514,
                    Name = "פרץ ברנשטיין/נזר דוד ",
                    Address = "רחוב:הרב הרצוג  עיר: ירושלים רציף",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.759186,35.189336)
                },


             new Stations
            {
            Code = 1518,
            Name = "פרץ ברנשטיין/נזר דוד",
            Address = " רחוב:פרץ ברנשטיין 56 עיר: ירושלים ",
            StationExist=true,
            Coordinate= new GeoCoordinate(31.759121,35.189178)
           
        },
              new Stations
              {
            Code = 1522,
            Name = "מוזיאון ישראל/רופין",
            Address = "  רחוב:דרך רופין  עיר: ירושלים ",
            StationExist=true,
            Coordinate= new GeoCoordinate(31.774484,35.204882)
            
                },

             new Stations
                  {
             Code = 1523,
            Name = "הרצוג/טשרניחובסקי",
            Address = "   רחוב:הרב הרצוג 21  עיר: ירושלים  ",
            StationExist=true,
            Coordinate= new GeoCoordinate(31.769609,35.209732)

                },
              new Stations
                {
              Code = 1524,
            Name = "רופין/שד' הזז",
            Address = "    רחוב:הרב הרצוג  עיר: ירושלים   ",
            StationExist=true,
            Coordinate= new GeoCoordinate(31.769652,35.208248)
           
                 },
                new Stations
                {
                    Code = 121,
                    Name = "מרכז סולם/סורוצקין ",
                    Address = " רחוב:הרב סורוצקין 13 עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.796033,35.206094)
                    
                },
                new Stations
                {
                    Code = 123,
                    Name = "אוהל דוד/סורוצקין ",
                    Address = "  רחוב:הרב סורוצקין 9 עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.794958,35.205216)
                   
                },
                new Stations
                {
                    Code = 122,
                    Name = "מרכז סולם/סורוצקין ",
                    Address = "  רחוב:הרב סורוצקין 28 עיר: ירושלים",
                    StationExist=true,
                    Coordinate= new GeoCoordinate(31.79617,35.206158)
                    
                }


                #endregion
            };


            ListBus = new List<Bus>
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

            ListLine = new List<Line>
            {
                #region line
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=18,
                FirstStationCode=73,
                LastStationCode=89,
                Area=DO.AREA.CENTER,
                
                LineExist=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=10,
                FirstStationCode=85,
                LastStationCode=97,
                Area=DO.AREA.GENERAL,
                LineExist=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=5,
                FirstStationCode=122,
                LastStationCode=1511,
                Area=DO.AREA.JERUSALEM,
                LineExist=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=6,
                FirstStationCode=121,
                LastStationCode=1491,
                Area=DO.AREA.NORTH,
                LineExist=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=33,
                FirstStationCode=119,
                LastStationCode=1491,
                Area=DO.AREA.SOUTH,
                LineExist=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=67,
                FirstStationCode=110,
                LastStationCode=1486,
                Area=DO.AREA.YOSH,
                LineExist=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=24,
                FirstStationCode=97,
                LastStationCode=111,
                Area=DO.AREA.JERUSALEM,
                LineExist=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=20,
                FirstStationCode=102,
                LastStationCode=116,
                Area=DO.AREA.JERUSALEM,
                LineExist=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=27,
                FirstStationCode=85,
                LastStationCode=102,
                Area=DO.AREA.JERUSALEM,
                LineExist=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=21,
                FirstStationCode=111,
                LastStationCode=1488,
                Area=DO.AREA.JERUSALEM,
                LineExist=true,
                }

                #endregion
            };


            ListLineTrip = new List<LineTrip>
            {
                 #region LineTrip
                new LineTrip
                {
                 KeyId=1,
                 StartAt=new TimeSpan(06,00,00),
                 FinishAt=new TimeSpan(24,00,00),
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
                 FinishAt=new TimeSpan(24,00,00),
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
                 FinishAt=new TimeSpan(24,00,00),
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
                 FinishAt=new TimeSpan(24,00,00),
                 TripLineExist=true,
                 Frequency=20,
                 },
                new LineTrip
                {
                 KeyId=6,
                 StartAt=new TimeSpan(06,00,00),
                 FinishAt=new TimeSpan(24,00,00),
                 TripLineExist=true,
                 Frequency=17,
                 },
                new LineTrip
                {
                 KeyId=7,
                 StartAt=new TimeSpan(08,00,00),
                 FinishAt=new TimeSpan(24,00,00),
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
                 FinishAt=new TimeSpan(24,00,00),
                 TripLineExist=true,
                 Frequency=40,
                 },
                new LineTrip
                {
                 KeyId=10,
                 StartAt=new TimeSpan(6,00,00),
                 FinishAt=new TimeSpan(24,00,00),
                 TripLineExist=true,
                 Frequency=40,
                 }
                 #endregion LineTrip
            };

            
            ListUsers = new List<User>
            {
               #region User
                new User
                {
                    UserName="RonDavid123",
                    Password="20934h584",
                    Admin=true,
                    UserExist=true,
                },

                new User
                {
                    UserName="TheKing99",
                    Password="1qazxsw2",
                    Admin=false,
                    UserExist=true,
                },
                new User
                {
                    UserName="Michal_Work",
                    Password="21071999M",
                    Admin=true,
                    UserExist=true,
                },
                new User
                {
                    UserName="OriyaShmoel",
                    Password="busbus123",
                    Admin=false,
                    UserExist=true,
                },
                 #endregion User
            };
            

            ListLineStations = new List<LineStation>
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
            ListAdjacentStations = new List<AdjacentStations>
            {
                
#region AdjacentStations  
                #region lineId1
                new AdjacentStations
                {
                    Station1=73,
                    Station2= 76,
                   Distance=10387.6464817987,
                   TimeAverage= ((1.5*10387.6464817987)/speed),//i.5- air to ground
                },

                new AdjacentStations
                {
                    Station1=76,
                    Station2= 77,
                   Distance=10291.789644608,
                  TimeAverage= ((1.5*10291.789644608)/speed)


                },

                new AdjacentStations
                {
                    Station1=77,
                    Station2= 78,
                   Distance=5942.26478400092,
                    TimeAverage= ((1.5*5942.26478400092)/speed)


                },

                new AdjacentStations
                {
                    Station1=78,
                    Station2= 83,
                   Distance=4115.12303761144,
                    TimeAverage=((1.5*4115.12303761144)/speed)


                },

                new AdjacentStations
                {
                    Station1=83,
                    Station2= 84,
                   Distance=3971.03321849724,
                    TimeAverage=((1.5*3971.03321849724)/speed)


                },

                new AdjacentStations
                {
                    Station1=84,
                    Station2= 85,
                   Distance=3665.92895953549,
                    TimeAverage=((1.5*3665.92895953549)/speed)


                },

                new AdjacentStations
                {
                    Station1=85,
                    Station2= 86,
                   Distance=181.343172558381,
                   TimeAverage= ((1.5*181.343172558381)/speed)


                },
                new AdjacentStations
                {
                    Station1=86,
                    Station2= 88,
                   Distance=338.193775824042,
                   TimeAverage= ((1.5*338.193775824042)/speed)


                },
                new AdjacentStations
                {
                    Station1=88,
                    Station2= 89,
                   Distance=839.108713036705,
                    TimeAverage= ((1.5*839.108713036705)/speed)


                },
#endregion LineId1
                
                #region lineId2



        

                new AdjacentStations
                {
                    Station1=89,
                    Station2= 90,
                   Distance=4972.12709975181,
                    TimeAverage= ((1.5*4972.12709975181)/speed)


                },

                new AdjacentStations
                {
                    Station1=90,
                    Station2= 91,
                   Distance=4978.59764273959,
                    TimeAverage= ((1.5*4978.59764273959)/speed)


                },

                new AdjacentStations
                {
                    Station1=91,
                    Station2= 93,
                   Distance=178.858161402408,
                   TimeAverage= ((1.5*178.858161402408)/speed)


                },

                new AdjacentStations
                {
                    Station1=93,
                    Station2= 94,
                   Distance=20.9542368918975,
                    TimeAverage= ((1.5*20.9542368918975)/speed)


                },
                new AdjacentStations
                {
                    Station1=94,
                    Station2= 95,
                   Distance=329058.161860042,
                    TimeAverage= ((1.5*329058.161860042)/speed)


                },
                new AdjacentStations
                {
                    Station1=95,
                    Station2= 97,
                   Distance=328752.547331663,
                    TimeAverage= ((1.5*328752.547331663)/speed)


                },
#endregion LineId2

                #region lineId3 
                new AdjacentStations
                {
                    Station1=122,
                    Station2= 123,
                   Distance=161.658025824811,
                    TimeAverage= ((1.5*161.658025824811)/speed)
                },

                new AdjacentStations
                {
                    Station1=123,
                    Station2= 121,
                   Distance=145.638205945867,
                    TimeAverage= ((1.5*145.638205945867)/speed)


                },

                new AdjacentStations
                {
                    Station1=121,
                    Station2= 1524,
                   Distance=2943.02888382813,
                   TimeAverage= ((1.5*2943.02888382813)/speed)


                },

                new AdjacentStations
                {
                    Station1=1524,
                    Station2= 1523,
                   Distance=140.492280736979,
                   TimeAverage= ((1.5*140.492280736979)/speed)


                },

                new AdjacentStations
                {
                    Station1=1523,
                    Station2= 1522,
                   Distance=710.578511572254,
                   TimeAverage= ((1.5*710.578511572254)/speed)


                },

                new AdjacentStations
                {
                    Station1=1522,
                    Station2= 1518,
                   Distance=2265.21319232095,
                   TimeAverage= ((1.5*2265.21319232095)/speed)


                },

                new AdjacentStations
                {
                    Station1=1518,
                    Station2= 1514,
                   Distance=16.6091664944544,
                    TimeAverage= ((1.5*16.6091664944544)/speed)


                },
                new AdjacentStations
                {
                    Station1=1514,
                    Station2= 1512,
                   Distance=1034.11608174677,
                    TimeAverage= ((1.5*1034.11608174677)/speed)


                },
                new AdjacentStations
                {
                    Station1=1512,
                    Station2= 1511,
                   Distance=2271.65706974387,
                    TimeAverage= ((1.5*2271.65706974387)/speed)


                },
#endregion LineId3

                #region lineId4
                new AdjacentStations
                {
                    Station1=121,
                    Station2= 123,
                   Distance=145.638205945867,
                   TimeAverage= ((1.5*145.638205945867)/speed)
                },

                new AdjacentStations
                {
                    Station1=123,
                    Station2= 122,
                   Distance=161.658025824811,
                   TimeAverage=((1.5*161.658025824811)/speed)


                },

                new AdjacentStations
                {
                    Station1=122,
                    Station2= 1524,
                   Distance=2957.82843148296,
                  TimeAverage= ((1.5*2957.82843148296)/speed)


                },

      
    
                new AdjacentStations
                {
                    Station1=1512,
                    Station2= 1491,
                   Distance=2604.33240816743,
                   TimeAverage=((1.5*2604.33240816743)/speed)


                },
#endregion LineId4

                #region lineId5
                new AdjacentStations
                {
                    Station1=119,
                    Station2= 1485,
                   Distance=5719.48141855948,
                 TimeAverage= ((1.5*5719.48141855948)/speed)
                },

                new AdjacentStations
                {
                    Station1=1485,
                    Station2= 1486,
                   Distance=179.006776536478,
                  TimeAverage= ((1.5*179.006776536478)/speed)


                },

                new AdjacentStations
                {
                    Station1=1486,
                    Station2= 1487,
                   Distance=772.225954779688,
                   TimeAverage= ((1.5*772.225954779688)/speed)


                },

                new AdjacentStations
                {
                    Station1=1487,
                    Station2= 1488,
                   Distance=1415.66487905204,
                   TimeAverage= ((1.5*1415.66487905204)/speed)


                },

                new AdjacentStations
                {
                    Station1=1488,
                    Station2= 1490,
                   Distance=445.425376124488,
                   TimeAverage= ((1.5*445.425376124488)/speed)


                },

                new AdjacentStations
                {
                    Station1=1490,
                    Station2= 1494,
                   Distance=10374.8817694688,
                   TimeAverage= ((1.5*10374.8817694688)/speed)


                },

                new AdjacentStations
                {
                    Station1=1494,
                    Station2= 1492,
                   Distance=587.350643701609,
                   TimeAverage= ((1.5*587.350643701609)/speed)


                },
                new AdjacentStations
                {
                    Station1=1492,
                    Station2= 1493,
                   Distance=361.691515745432,
                   TimeAverage= ((1.5*361.691515745432)/speed)


                },
                new AdjacentStations
                {
                    Station1=1493,
                    Station2= 1491,
                   Distance=1144.85124079156,
                   TimeAverage= ((1.5*1144.85124079156)/speed)


                },
#endregion LineId5

                #region lineId6
                new AdjacentStations
                {
                    Station1=110,
                    Station2= 111,
                   Distance=195.920342315088,
                    TimeAverage= ((1.5*195.920342315088)/speed)
                },

                new AdjacentStations
                {
                    Station1=111,
                    Station2= 112,
                    Distance=50.4574978208662,
                    TimeAverage= ((1.5*50.4574978208662)/speed)


                },

                new AdjacentStations
                {
                    Station1=112,
                    Station2= 113,
                   Distance=143.276626873244,
                    TimeAverage= ((1.5*143.276626873244)/speed)


                },

                new AdjacentStations
                {
                    Station1=113,
                    Station2= 115,
                    Distance=279.425185419296,
                   TimeAverage= ((1.5*279.425185419296)/speed)


                },

                new AdjacentStations
                {
                    Station1=115,
                    Station2= 116,
                   Distance=72.2684666055651,
                   TimeAverage= ((1.5*72.2684666055651)/speed)


                },

                new AdjacentStations
                {
                    Station1=116,
                    Station2= 117,
                   Distance=192.809573417871,
                    TimeAverage= ((1.5*192.809573417871)/speed)


                },

                new AdjacentStations
                {
                    Station1=117,
                    Station2= 119,
                   Distance=193.971761810414,
                   TimeAverage= ((1.5*193.971761810414)/speed)


                },
  

#endregion LineId6

                #region lineId7
                new AdjacentStations
                {
                    Station1=97,
                    Station2= 102,
                   Distance=4739.1072386442,
                     TimeAverage= ((1.5*4739.1072386442)/speed)
                },

                new AdjacentStations
                {
                    Station1=102,
                    Station2= 103,
                   Distance=554.235295622813,
                    TimeAverage= ((1.5*554.235295622813)/speed)


                },

                new AdjacentStations
                {
                    Station1=103,
                    Station2= 105,
                   Distance=383.45864939499,
                     TimeAverage= ((1.5*383.45864939499)/speed)

                },

                new AdjacentStations
                {
                    Station1=105,
                    Station2= 106,
                   Distance=20.551585590077,
                    TimeAverage= ((1.5*20.551585590077)/speed)


                },

                new AdjacentStations
                {
                    Station1=106,
                    Station2= 108,
                   Distance=314.882994150425,
                    TimeAverage= ((1.5*314.882994150425)/speed)


                },

                new AdjacentStations
                {
                    Station1=108,
                    Station2= 109,
                   Distance=109.450560373063,
                     TimeAverage= ((1.5*109.450560373063)/speed)


                },

                new AdjacentStations
                {
                    Station1=109,
                    Station2= 110,
                   Distance=79.9157986778367,
                     TimeAverage= ((1.5*79.9157986778367)/speed)

                },
                new AdjacentStations
                {
                    Station1=110,
                    Station2= 112,
                   Distance=151.091747503085,
                     TimeAverage= ((1.5*151.091747503085)/speed)


                },
                new AdjacentStations
                {
                    Station1=112,
                    Station2= 111,
                   Distance=50.4574978208662,
                     TimeAverage= ((1.5*50.4574978208662)/speed)


                },
#endregion LineId7

                #region lineId8
   
         

      
             
           

       
                new AdjacentStations
                {
                    Station1=112,
                    Station2= 116,
                   Distance=481.422062665161,
                    TimeAverage= ((1.5*481.422062665161)/speed)


                },
#endregion LineId8

                #region lineId9
          
      

      
        
   
                new AdjacentStations
                {
                    Station1=95,
                    Station2= 102,
                   Distance=325385.905757283,
                    TimeAverage= ((1.5*325385.905757283)/speed)


                },
#endregion LineId9

                #region lineId10
   
   

     

    


                new AdjacentStations
                {
                    Station1=1486,
                    Station2= 1488,
                   Distance=650.976014538566,
                     TimeAverage= ((1.5*650.976014538566)/speed)


                },
#endregion LineId10

                    };
            #endregion AdjacentStations

        }
    }

}
