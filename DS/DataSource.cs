using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


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

            ListStations = new List<Stations>
            {
                #region initialization stations//איתחול תחנות
                new Stations
                {
                    Code = 73,
                    Name = "שדרות גולדה מאיר/המשורר אצ''ג",
                    Address = "רחוב:שדרות גולדה מאיר  עיר: ירושלים ",
                    Latitude = 31.825302,
                    Longtitude = 35.188624
                },
                new Stations
                {
                    Code = 76,
                    Name = "בית ספר צור באהר בנות/אלמדינה אלמונוורה",
                    Address = "רחוב:אל מדינה אל מונאוורה  עיר: ירושלים",
                    Latitude = 31.738425,
                    Longtitude = 35.228765
                },
                new Stations
                {
                    Code = 77,
                    Name = "בית ספר אבן רשד/אלמדינה אלמונוורה",
                    Address = "רחוב:אל מדינה אל מונאוורה  עיר: ירושלים ",
                    Latitude = 31.738676,
                    Longtitude = 35.226704
                },
                new Stations
                {
                    Code = 78,
                    Name = "שרי ישראל/יפו",
                    Address = "רחוב:שדרות שרי ישראל 15 עיר: ירושלים",
                    Latitude = 31.789128,
                    Longtitude = 35.206146
                },
                new Stations
                {
                    Code = 83,
                    Name = "בטן אלהווא/חוש אל מרג",
                    Address = "רחוב:בטן אל הווא  עיר: ירושלים",
                    Latitude = 31.766358,
                    Longtitude = 35.240417
                },
                new Stations
                {
                    Code = 84,
                    Name = "מלכי ישראל/הטורים",
                    Address = " רחוב:מלכי ישראל 77 עיר: ירושלים ",
                    Latitude = 31.790758,
                    Longtitude = 35.209791
                },
                new Stations
                {
                    Code = 85,
                    Name = "בית ספר לבנים/אלמדארס",
                    Address = "רחוב:אלמדארס  עיר: ירושלים",
                    Latitude = 31.768643,
                    Longtitude = 35.238509
                },
                new Stations
                {
                    Code = 86,
                    Name = "מגרש כדורגל/אלמדארס",
                    Address = "רחוב:אלמדארס  עיר: ירושלים",
                    Latitude = 31.769899,
                    Longtitude = 35.23973
                },
                new Stations
                {
                    Code = 88,
                    Name = "בית ספר לבנות/בטן אלהוא",
                    Address = " רחוב:בטן אל הווא  עיר: ירושלים",
                    Latitude = 31.767064,
                    Longtitude = 35.238443
                },
                new Stations
                {
                    Code = 89,
                    Name = "דרך בית לחם הישה/ואדי קדום",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים ",
                    Latitude = 31.765863,
                    Longtitude = 35.247198
                },
                new Stations
                {
                    Code = 90,
                    Name = "גולדה/הרטום",
                    Address = "רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    Latitude = 31.799804,
                    Longtitude = 35.213021
                },
                new Stations
                {
                    Code = 91,
                    Name = "דרך בית לחם הישה/ואדי קדום",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים ",
                    Latitude = 31.765717,
                    Longtitude = 35.247102
                },
                new Stations
                {
                    Code = 93,
                    Name = "חוש סלימה 1",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    Latitude = 31.767265,
                    Longtitude = 35.246594
                },
                new Stations
                {
                    Code = 94,
                    Name = "דרך בית לחם הישנה ב",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    Latitude = 31.767084,
                    Longtitude = 35.246655
                },
                new Stations
                {
                    Code = 95,
                    Name = "דרך בית לחם הישנה א",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    Latitude = 31.768759,
                    Longtitude = 31.768759
                },
                new Stations
                {
                    Code = 97,
                    Name = "שכונת בזבז 2",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    Latitude = 31.77002,
                    Longtitude = 35.24348
                },
                new Stations
                {
                    Code = 102,
                    Name = "גולדה/שלמה הלוי",
                    Address = " רחוב:שדרות גולדה מאיר  עיר: ירושלים",
                    Latitude = 31.8003,
                    Longtitude = 35.208257
                },
                new Stations
                {
                    Code = 103,
                    Name = "גולדה/הרטום",
                    Address = " רחוב:שדרות גולדה מאיר  עיר: ירושלים",
                    Latitude = 31.8,
                    Longtitude = 35.214106
                },
                new Stations
                {
                    Code = 105,
                    Name = "גבעת משה",
                    Address = " רחוב:גבעת משה 2 עיר: ירושלים",
                    Latitude = 31.797708,
                    Longtitude = 35.217133
                },
                new Stations
                {
                    Code = 106,
                    Name = "גבעת משה",
                    Address = " רחוב:גבעת משה 3 עיר: ירושלים",
                    Latitude = 31.797535,
                    Longtitude = 35.217057
                },
                //20
                new Stations
                {
                    Code = 108,
                    Name = "עזרת תורה/עלי הכהן",
                    Address = "  רחוב:עזרת תורה 25 עיר: ירושלים",
                    Latitude = 31.797535,
                    Longtitude = 35.213728
                },
                new Stations
                {
                    Code = 109,
                    Name = "עזרת תורה/דורש טוב",
                    Address = "  רחוב:עזרת תורה 21 עיר: ירושלים ",
                    Latitude = 31.796818,
                    Longtitude = 35.212936
                },
                new Stations
                {
                    Code = 110,
                    Name = "עזרת תורה/דורש טוב",
                    Address = " רחוב:עזרת תורה 12 עיר: ירושלים",
                    Latitude = 31.796129,
                    Longtitude = 35.212698
                },
                new Stations
                {
                    Code = 111,
                    Name = "יעקובזון/עזרת תורה",
                    Address = "  רחוב:יעקובזון 1 עיר: ירושלים",
                    Latitude = 31.794631,
                    Longtitude = 35.21161
                },
                new Stations
                {
                    Code = 112,
                    Name = "יעקובזון/עזרת תורה",
                    Address = " רחוב:יעקובזון  עיר: ירושלים",
                    Latitude = 31.79508,
                    Longtitude = 35.211684
                },
                //25
                new Stations
                {
                    Code = 113,
                    Name = "זית רענן/אוהל יהושע",
                    Address = "  רחוב:זית רענן 1 עיר: ירושלים",
                    Latitude = 31.796255,
                    Longtitude = 35.211065
                },
                new Stations
                {
                    Code = 115,
                    Name = "זית רענן/תורת חסד",
                    Address = " רחוב:זית רענן  עיר: ירושלים",
                    Latitude = 31.798423,
                    Longtitude = 35.209575
                },
                new Stations
                {
                    Code = 116,
                    Name = "זית רענן/תורת חסד",
                    Address = "  רחוב:הרב סורוצקין 48 עיר: ירושלים ",
                    Latitude = 31.798689,
                    Longtitude = 35.208878
                },
                new Stations
                {
                    Code = 117,
                    Name = "קרית הילד/סורוצקין",
                    Address = "  רחוב:הרב סורוצקין  עיר: ירושלים",
                    Latitude = 31.799165,
                    Longtitude = 35.206918
                },
                new Stations
                {
                    Code = 119,
                    Name = "סורוצקין/שנירר",
                    Address = "  רחוב:הרב סורוצקין 31 עיר: ירושלים",
                    Latitude = 31.797829,
                    Longtitude = 35.205601
                },

                //#endregion //30
                new Stations
                {
                    Code = 1485,
                    Name = "שדרות נווה יעקוב/הרב פרדס ",
                    Address = "רחוב: שדרות נווה יעקוב  עיר:ירושלים ",
                    Latitude = 31.840063,
                    Longtitude = 35.240062

                },
                new Stations
                {
                    Code = 1486,
                    Name = "מרכז קהילתי /שדרות נווה יעקוב",
                    Address = "רחוב:שדרות נווה יעקוב ירושלים עיר:ירושלים ",
                    Latitude = 31.838481,
                    Longtitude = 35.23972
                },


                new Stations
                {
                    Code = 1487,
                    Name = " מסוף 700 /שדרות נווה יעקוב ",
            Address = "חוב:שדרות נווה יעקב 7 עיר: ירושלים  ",
                    Latitude = 31.837748,
                    Longtitude = 35.231598
                },
                new Stations
                {
                    Code = 1488,
                    Name = " הרב פרדס/אסטורהב ",
                    Address = "רחוב:מעגלות הרב פרדס  עיר: ירושלים רציף  ",
                    Latitude = 31.840279,
                    Longtitude = 35.246272
                },
                new Stations
                {
                    Code = 1490,
                    Name = "הרב פרדס/צוקרמן ",
                    Address = "רחוב:מעגלות הרב פרדס 24 עיר: ירושלים   ",
                    Latitude = 31.843598,
                    Longtitude = 35.243639
                },
                new Stations
                {
                    Code = 1491,
                    Name = "ברזיל ",
                    Address = "רחוב:ברזיל 14 עיר: ירושלים",
                    Latitude = 31.766256,
                    Longtitude = 35.173
                },
                new Stations
                {
                    Code = 1492,
                    Name = "בית וגן/הרב שאג ",
                    Address = "רחוב:בית וגן 61 עיר: ירושלים ",
                    Latitude = 31.76736,
                    Longtitude = 35.184771
                },
                new Stations
                {
                    Code = 1493,
                    Name = "בית וגן/עוזיאל ",
                    Address = "רחוב:בית וגן 21 עיר: ירושלים    ",
                    Latitude = 31.770543,
                    Longtitude = 35.183999
                },
                new Stations
                {
                    Code = 1494,
                    Name = " קרית יובל/שמריהו לוין ",
                    Address = "רחוב:ארתור הנטקה  עיר: ירושלים    ",
                    Latitude = 31.768465,
                    Longtitude = 35.178701
                },
                new Stations
                {
                    Code = 1510,
                    Name = " קורצ'אק / רינגלבלום ",
                    Address = "רחוב:יאנוש קורצ'אק 7 עיר: ירושלים",
                    Latitude = 31.759534,
                    Longtitude = 35.173688
                },
                new Stations
                {
                    Code = 1511,
                    Name = " טהון/גולומב ",
                    Address = "רחוב:יעקב טהון  עיר: ירושלים     ",
                    Latitude = 31.761447,
                    Longtitude = 35.175929
                },
                new Stations
                {
                    Code = 1512,
                    Name = "הרב הרצוג/שח''ל ",
                    Address = "רחוב:הרב הרצוג  עיר: ירושלים רציף",
                    Latitude = 31.761447,
                    Longtitude = 35.199936
                },
                new Stations
                {
                    Code = 1514,
                    Name = "פרץ ברנשטיין/נזר דוד ",
                    Address = "רחוב:הרב הרצוג  עיר: ירושלים רציף",
                    Latitude = 31.759186,
                    Longtitude = 35.189336
                },


             new Stations
            {
            Code = 1518,
            Name = "פרץ ברנשטיין/נזר דוד",
            Address = " רחוב:פרץ ברנשטיין 56 עיר: ירושלים ",
            Latitude = 31.759121,
            Longtitude = 35.189178
        },
              new Stations
              {
            Code = 1522,
            Name = "מוזיאון ישראל/רופין",
            Address = "  רחוב:דרך רופין  עיר: ירושלים ",
            Latitude = 31.774484,
            Longtitude = 35.204882
                },

             new Stations
                  {
             Code = 1523,
            Name = "הרצוג/טשרניחובסקי",
            Address = "   רחוב:הרב הרצוג  עיר: ירושלים  ",
            Latitude = 31.769652,
            Longtitude = 35.208248
                },
              new Stations
                {
              Code = 1524,
            Name = "רופין/שד' הזז",
            Address = "    רחוב:הרב הרצוג  עיר: ירושלים   ",
            Latitude = 31.769652,
            Longtitude = 35.208248,
                 },
                new Stations
                {
                    Code = 121,
                    Name = "מרכז סולם/סורוצקין ",
                    Address = " רחוב:הרב סורוצקין 13 עיר: ירושלים",
                    Latitude = 31.796033,
                    Longtitude =35.206094
                },
                new Stations
                {
                    Code = 123,
                    Name = "אוהל דוד/סורוצקין ",
                    Address = "  רחוב:הרב סורוצקין 9 עיר: ירושלים",
                    Latitude = 31.794958,
                    Longtitude =35.205216
                },
                new Stations
                {
                    Code = 122,
                    Name = "מרכז סולם/סורוצקין ",
                    Address = "  רחוב:הרב סורוצקין 28 עיר: ירושלים",
                    Latitude = 31.79617,
                    Longtitude =35.206158
                }


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
                LineExsis=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=10,
                FirstStationCode=85,
                LastStationCode=97,
                Area=DO.AREA.GENERAL,
                LineExsis=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=5,
                FirstStationCode=122,
                LastStationCode=1511,
                Area=DO.AREA.JERUSALEM,
                LineExsis=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=6,
                FirstStationCode=121,
                LastStationCode=1491,
                Area=DO.AREA.NORTH,
                LineExsis=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=33,
                FirstStationCode=119,
                LastStationCode=1491,
                Area=DO.AREA.SOUTH,
                LineExsis=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=67,
                FirstStationCode=110,
                LastStationCode=1486,
                Area=DO.AREA.YOSH,
                LineExsis=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=24,
                FirstStationCode=97,
                LastStationCode=111,
                Area=DO.AREA.JERUSALEM,
                LineExsis=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=20,
                FirstStationCode=102,
                LastStationCode=116,
                Area=DO.AREA.JERUSALEM,
                LineExsis=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=27,
                FirstStationCode=85,
                LastStationCode=102,
                Area=DO.AREA.JERUSALEM,
                LineExsis=true,
                },
                new Line
                {
                IdNumber = ++DS.Config.idLineCounter,
                NumberLine=21,
                FirstStationCode=111,
                LastStationCode=1488,
                Area=DO.AREA.JERUSALEM,
                LineExsis=true,
                }                                                             

                #endregion
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
                    LineStationExsis=true,
                    PrevStation=0,
                    NextStation=76,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=76,
                    LineStationIndex=2,
                    LineStationExsis=true,
                    PrevStation=73,
                    NextStation=77,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=77,
                    LineStationIndex=3,
                    LineStationExsis=true,
                    PrevStation=76,
                    NextStation=78,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=78,
                    LineStationIndex=4,
                    LineStationExsis=true,
                    PrevStation=77,
                    NextStation=83,
                },    
                new LineStation
                {
                    LineId=1,
                    StationCode=83,
                    LineStationIndex=5,
                    LineStationExsis=true,
                    PrevStation=78,
                    NextStation=84,
                },   
                new LineStation
                {
                    LineId=1,
                    StationCode=84,
                    LineStationIndex=6,
                    LineStationExsis=true,
                    PrevStation=83,
                    NextStation=85,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=85,
                    LineStationIndex=7,
                    LineStationExsis=true,
                    PrevStation=84,
                    NextStation=86,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=86,
                    LineStationIndex=8,
                    LineStationExsis=true,
                    PrevStation=85,
                    NextStation=88,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=88,
                    LineStationIndex=9,
                    LineStationExsis=true,
                    PrevStation=86,
                    NextStation=89,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=89,
                    LineStationIndex=10,
                    LineStationExsis=true,
                    PrevStation=88,
                    NextStation=0,
                },

             //line 10
                new LineStation
                {
                    LineId=2,
                    StationCode=85,
                    LineStationIndex=1,
                    LineStationExsis=true,
                    PrevStation=0,
                    NextStation=86,
                },  
                new LineStation
                {
                    LineId=2,
                    StationCode=86,
                    LineStationIndex=2,
                    LineStationExsis=true,
                    PrevStation=85,
                    NextStation=88,
                },  
                new LineStation
                {
                    LineId=2,
                    StationCode=88,
                    LineStationIndex=3,
                    LineStationExsis=true,
                    PrevStation=86,
                    NextStation=89,
                }, 
                new LineStation
                {
                    LineId=2,
                    StationCode=89,
                    LineStationIndex=4,
                    LineStationExsis=true,
                    PrevStation=88,
                    NextStation=90,
                }, 
                new LineStation
                {
                    LineId=2,
                    StationCode=90,
                    LineStationIndex=5,
                    LineStationExsis=true,
                    PrevStation=89,
                    NextStation=91,
                }, 
                new LineStation
                {
                    LineId=2,
                    StationCode=91,
                    LineStationIndex=6,
                    LineStationExsis=true,
                    PrevStation=90,
                    NextStation=93,
                },  
                new LineStation
                {
                    LineId=2,
                    StationCode=93,
                    LineStationIndex=7,
                    LineStationExsis=true,
                    PrevStation=91,
                    NextStation=94,
                },  
                new LineStation
                {
                    LineId=2,
                    StationCode=94,
                    LineStationIndex=8,
                    LineStationExsis=true,
                    PrevStation=93,
                    NextStation=95,
                },  
                new LineStation
                {
                    LineId=2,
                    StationCode=95,
                    LineStationIndex=9,
                    LineStationExsis=true,
                    PrevStation=94,
                    NextStation=97,
                },
                new LineStation
                {
                    LineId=2,
                    StationCode=97,
                    LineStationIndex=10,
                    LineStationExsis=true,
                    PrevStation=95,
                    NextStation=0,
                }, 
             //line 5         
                new LineStation
                {
                    LineId=3,
                    StationCode=122,
                    LineStationIndex=1,
                    LineStationExsis=true,
                    PrevStation=0,
                    NextStation=123,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=123,
                    LineStationIndex=2,
                    LineStationExsis=true,
                    PrevStation=122,
                    NextStation=121,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=121,
                    LineStationIndex=3,
                    LineStationExsis=true,
                    PrevStation=123,
                    NextStation=1524,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=1524,
                    LineStationIndex=4,
                    LineStationExsis=true,
                    PrevStation=121,
                    NextStation=1523,
                }, 
                new LineStation
                {
                    LineId=3,
                    StationCode=1523,
                    LineStationIndex=5,
                    LineStationExsis=true,
                    PrevStation=1524,
                    NextStation=1522,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=1522,
                    LineStationIndex=6,
                    LineStationExsis=true,
                    PrevStation=1523,
                    NextStation=1518,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=1518,
                    LineStationIndex=7,
                    LineStationExsis=true,
                    PrevStation=1522,
                    NextStation=1514,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=1514,
                    LineStationIndex=8,
                    LineStationExsis=true,
                    PrevStation=1518,
                    NextStation=1512,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=1512,
                    LineStationIndex=9,
                    LineStationExsis=true,
                    PrevStation=1514,
                    NextStation=1511,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=1511,
                    LineStationIndex=10,
                    LineStationExsis=true,
                    PrevStation=1512,
                    NextStation=0,
                },

                //line=6
                new LineStation
                {
                    LineId=4,
                    StationCode=121,
                    LineStationIndex=1,
                    LineStationExsis=true,
                    PrevStation=0,
                    NextStation=123,
                },   
                new LineStation
                {
                    LineId=4,
                    StationCode=123,
                    LineStationIndex=2,
                    LineStationExsis=true,
                    PrevStation=121,
                    NextStation=122,
                },  
                new LineStation
                {
                    LineId=4,
                    StationCode=122,
                    LineStationIndex=3,
                    LineStationExsis=true,
                    PrevStation=123,
                    NextStation=1524,
                }, 
                new LineStation
                {
                    LineId=4,
                    StationCode=1524,
                    LineStationIndex=4,
                    LineStationExsis=true,
                    PrevStation=122,
                    NextStation=1523,
                },  
                new LineStation
                {
                    LineId=4,
                    StationCode=1523,
                    LineStationIndex=5,
                    LineStationExsis=true,
                    PrevStation=1524,
                    NextStation=1522,
                },   
                new LineStation
                {
                    LineId=4,
                    StationCode=1522,
                    LineStationIndex=6,
                    LineStationExsis=true,
                    PrevStation=1523,
                    NextStation=1518,
                },  
                new LineStation
                {
                    LineId=4,
                    StationCode=1518,
                    LineStationIndex=7,
                    LineStationExsis=true,
                    PrevStation=1522,
                    NextStation=1514,
                },  
                new LineStation
                {
                    LineId=4,
                    StationCode=1514,
                    LineStationIndex=8,
                    LineStationExsis=true,
                    PrevStation=1518,
                    NextStation=1512,
                },    
                new LineStation
                {
                    LineId=4,
                    StationCode=1512,
                    LineStationIndex=9,
                    LineStationExsis=true,
                    PrevStation=1514,
                    NextStation=1491,
                },  
                new LineStation
                {
                    LineId=4,
                    StationCode=1491,
                    LineStationIndex=10,
                    LineStationExsis=true,
                    PrevStation=1512,
                    NextStation=0,
                }, 

                //line=33
                new LineStation
                {
                    LineId=5,
                    StationCode=119,
                    LineStationIndex=1,
                    LineStationExsis=true,
                    PrevStation=0,
                    NextStation=1485,
                }, 
                new LineStation
                {
                    LineId=5,
                    StationCode=1485,
                    LineStationIndex=2,
                    LineStationExsis=true,
                    PrevStation=119,
                    NextStation=1486,
                }, 
                new LineStation
                {
                    LineId=5,
                    StationCode=1486,
                    LineStationIndex=3,
                    LineStationExsis=true,
                    PrevStation=1485,
                    NextStation=1487,
                }, 
                new LineStation
                {
                    LineId=5,
                    StationCode=1487,
                    LineStationIndex=4,
                    LineStationExsis=true,
                    PrevStation=1486,
                    NextStation=1488,
                }, 
                new LineStation
                {
                    LineId=5,
                    StationCode=1488,
                    LineStationIndex=5,
                    LineStationExsis=true,
                    PrevStation=1487,
                    NextStation=1490,
                }, 
                new LineStation
                {
                    LineId=5,
                    StationCode=1490,
                    LineStationIndex=6,
                    LineStationExsis=true,
                    PrevStation=1488,
                    NextStation=1494,
                }, 
                new LineStation
                {
                    LineId=5,
                    StationCode=1494,
                    LineStationIndex=7,
                    LineStationExsis=true,
                    PrevStation=1490,
                    NextStation=1492,
                }, 
                new LineStation
                {
                    LineId=5,
                    StationCode=1492,
                    LineStationIndex=8,
                    LineStationExsis=true,
                    PrevStation=1494,
                    NextStation=1493,
                }, 
                new LineStation
                {
                    LineId=5,
                    StationCode=1493,
                    LineStationIndex=9,
                    LineStationExsis=true,
                    PrevStation=1492,
                    NextStation=1491,
                }, 
                new LineStation
                {
                    LineId=5,
                    StationCode=1491,
                    LineStationIndex=10,
                    LineStationExsis=true,
                    PrevStation=1493,
                    NextStation=0,
                },

                //line=67,
                new LineStation
                {
                    LineId=6,
                    StationCode=110,
                    LineStationIndex=1,
                    LineStationExsis=true,
                    PrevStation=0,
                    NextStation=111,
                }, 
                new LineStation
                {
                    LineId=6,
                    StationCode=111,
                    LineStationIndex=2,
                    LineStationExsis=true,
                    PrevStation=110,
                    NextStation=112,
                },
                new LineStation
                {
                    LineId=6,
                    StationCode=112,
                    LineStationIndex=3,
                    LineStationExsis=true,
                    PrevStation=111,
                    NextStation=113,
                },  
                new LineStation
                {
                    LineId=6,
                    StationCode=113,
                    LineStationIndex=4,
                    LineStationExsis=true,
                    PrevStation=112,
                    NextStation=115,
                }, 
                new LineStation
                {
                    LineId=6,
                    StationCode=115,
                    LineStationIndex=5,
                    LineStationExsis=true,
                    PrevStation=113,
                    NextStation=116,
                },   
                new LineStation
                {
                    LineId=6,
                    StationCode=116,
                    LineStationIndex=6,
                    LineStationExsis=true,
                    PrevStation=115,
                    NextStation=117,
                },  
                new LineStation
                {
                    LineId=6,
                    StationCode=117,
                    LineStationIndex=7,
                    LineStationExsis=true,
                    PrevStation=116,
                    NextStation=119,
                },
                new LineStation
                {
                    LineId=6,
                    StationCode=119,
                    LineStationIndex=8,
                    LineStationExsis=true,
                    PrevStation=117,
                    NextStation=1485,
                },  
                new LineStation
                {
                    LineId=6,
                    StationCode=1485,
                    LineStationIndex=9,
                    LineStationExsis=true,
                    PrevStation=119,
                    NextStation=1486,
                },   
                new LineStation
                {
                    LineId=6,
                    StationCode=1486,
                    LineStationIndex=10,
                    LineStationExsis=true,
                    PrevStation=1485,
                    NextStation=0,
                },

                //line=24,
                new LineStation
                {
                    LineId=7,
                    StationCode=97,
                    LineStationIndex=1,
                    LineStationExsis=true,
                    PrevStation=0,
                    NextStation=102,
                },
                new LineStation
                {
                    LineId=7,
                    StationCode=102,
                    LineStationIndex=2,
                    LineStationExsis=true,
                    PrevStation=97,
                    NextStation=103,
                },  
                new LineStation
                {
                    LineId=7,
                    StationCode=103,
                    LineStationIndex=3,
                    LineStationExsis=true,
                    PrevStation=102,
                    NextStation=105,
                },  
                new LineStation
                {
                    LineId=7,
                    StationCode=105,
                    LineStationIndex=4,
                    LineStationExsis=true,
                    PrevStation=103,
                    NextStation=106,
                }, 
                new LineStation
                {
                    LineId=7,
                    StationCode=106,
                    LineStationIndex=5,
                    LineStationExsis=true,
                    PrevStation=105,
                    NextStation=108,
                },  
                new LineStation
                {
                    LineId=7,
                    StationCode=108,
                    LineStationIndex=6,
                    LineStationExsis=true,
                    PrevStation=106,
                    NextStation=109,
                },  
                new LineStation
                {
                    LineId=7,
                    StationCode=109,
                    LineStationIndex=7,
                    LineStationExsis=true,
                    PrevStation=108,
                    NextStation=110,
                },  
                new LineStation
                {
                    LineId=7,
                    StationCode=110,
                    LineStationIndex=8,
                    LineStationExsis=true,
                    PrevStation=109,
                    NextStation=112,
                },  
                new LineStation
                {
                    LineId=7,
                    StationCode=112,
                    LineStationIndex=9,
                    LineStationExsis=true,
                    PrevStation=110,
                    NextStation=111,
                },   
                new LineStation
                {
                    LineId=7,
                    StationCode=111,
                    LineStationIndex=10,
                    LineStationExsis=true,
                    PrevStation=112,
                    NextStation=0,
                },
                
                //  NumberLine=20
                new LineStation
                {
                    LineId=8,
                    StationCode=102,
                    LineStationIndex=1,
                    LineStationExsis=true,
                    PrevStation=0,
                    NextStation=103,
                },  
                new LineStation
                {
                    LineId=8,
                    StationCode=103,
                    LineStationIndex=2,
                    LineStationExsis=true,
                    PrevStation=102,
                    NextStation=105,
                }, 
                new LineStation
                {
                    LineId=8,
                    StationCode=105,
                    LineStationIndex=3,
                    LineStationExsis=true,
                    PrevStation=103,
                    NextStation=106,
                }, 
                new LineStation
                {
                    LineId=8,
                    StationCode=106,
                    LineStationIndex=4,
                    LineStationExsis=true,
                    PrevStation=105,
                    NextStation=108,
                },
                new LineStation
                {
                    LineId=8,
                    StationCode=108,
                    LineStationIndex=5,
                    LineStationExsis=true,
                    PrevStation=106,
                    NextStation=109,
                },
                new LineStation
                {
                    LineId=8,
                    StationCode=109,
                    LineStationIndex=6,
                    LineStationExsis=true,
                    PrevStation=108,
                    NextStation=110,
                }, 
                new LineStation
                {
                    LineId=8,
                    StationCode=110,
                    LineStationIndex=7,
                    LineStationExsis=true,
                    PrevStation=109,
                    NextStation=111,
                }, 
                new LineStation
                {
                    LineId=8,
                    StationCode=111,
                    LineStationIndex=8,
                    LineStationExsis=true,
                    PrevStation=110,
                    NextStation=112,
                },  
                new LineStation
                {
                    LineId=8,
                    StationCode=112,
                    LineStationIndex=9,
                    LineStationExsis=true,
                    PrevStation=111,
                    NextStation=116,
                },  
                new LineStation
                {
                    LineId=8,
                    StationCode=116,
                    LineStationIndex=10,
                    LineStationExsis=true,
                    PrevStation=112,
                    NextStation=0,
                },

                //line=27
                new LineStation
                {
                    LineId=9,
                    StationCode=85,
                    LineStationIndex=1,
                    LineStationExsis=true,
                    PrevStation=0,
                    NextStation=86,
                },   
                new LineStation
                {
                    LineId=9,
                    StationCode=86,
                    LineStationIndex=2,
                    LineStationExsis=true,
                    PrevStation=85,
                    NextStation=88,
                },   
                new LineStation
                {
                    LineId=9,
                    StationCode=88,
                    LineStationIndex=3,
                    LineStationExsis=true,
                    PrevStation=86,
                    NextStation=89,
                },      
                new LineStation
                {
                    LineId=9,
                    StationCode=89,
                    LineStationIndex=4,
                    LineStationExsis=true,
                    PrevStation=88,
                    NextStation=90,
                },    
                new LineStation
                {
                    LineId=9,
                    StationCode=90,
                    LineStationIndex=5,
                    LineStationExsis=true,
                    PrevStation=89,
                    NextStation=91,
                },   
                new LineStation
                {
                    LineId=9,
                    StationCode=91,
                    LineStationIndex=6,
                    LineStationExsis=true,
                    PrevStation=90,
                    NextStation=93,
                },   
                new LineStation
                {
                    LineId=9,
                    StationCode=93,
                    LineStationIndex=7,
                    LineStationExsis=true,
                    PrevStation=91,
                    NextStation=94,
                },  
                new LineStation
                {
                    LineId=9,
                    StationCode=94,
                    LineStationIndex=8,
                    LineStationExsis=true,
                    PrevStation=93,
                    NextStation=95,
                },   
                new LineStation
                {
                    LineId=9,
                    StationCode=95,
                    LineStationIndex=9,
                    LineStationExsis=true,
                    PrevStation=94,
                    NextStation=102,
                }, 
                new LineStation
                {
                    LineId=9,
                    StationCode=102,
                    LineStationIndex=10,
                    LineStationExsis=true,
                    PrevStation=95,
                    NextStation=0,
                },

                //line=21,
                new LineStation
                {
                    LineId=10,
                    StationCode=111,
                    LineStationIndex=1,
                    LineStationExsis=true,
                    PrevStation=0,
                    NextStation=112,
                }, 
                new LineStation
                {
                    LineId=10,
                    StationCode=112,
                    LineStationIndex=2,
                    LineStationExsis=true,
                    PrevStation=111,
                    NextStation=113,
                }, 
                new LineStation
                {
                    LineId=10,
                    StationCode=113,
                    LineStationIndex=3,
                    LineStationExsis=true,
                    PrevStation=112,
                    NextStation=115,
                },
                new LineStation
                {
                    LineId=10,
                    StationCode=115,
                    LineStationIndex=4,
                    LineStationExsis=true,
                    PrevStation=113,
                    NextStation=116,
                },
                new LineStation
                {
                    LineId=10,
                    StationCode=116,
                    LineStationIndex=5,
                    LineStationExsis=true,
                    PrevStation=115,
                    NextStation=117,
                },
                new LineStation
                {
                    LineId=10,
                    StationCode=117,
                    LineStationIndex=6,
                    LineStationExsis=true,
                    PrevStation=116,
                    NextStation=119,
                }, 
                new LineStation
                {
                    LineId=10,
                    StationCode=119,
                    LineStationIndex=7,
                    LineStationExsis=true,
                    PrevStation=117,
                    NextStation=1485,
                }, 
                new LineStation
                {
                    LineId=10,
                    StationCode=1485,
                    LineStationIndex=8,
                    LineStationExsis=true,
                    PrevStation=119,
                    NextStation=1486,
                }, 
                new LineStation
                {
                    LineId=10,
                    StationCode=1486,
                    LineStationIndex=9,
                    LineStationExsis=true,
                    PrevStation=1485,
                    NextStation=1488,
                },  
                new LineStation
                {
                    LineId=10,
                    StationCode=1488,
                    LineStationIndex=10,
                    LineStationExsis=true,
                    PrevStation=1486,
                    NextStation=0,
                },

               #endregion

            };
            ListBus = new List<Bus>
            {
                #region initialization buses
                new Bus
                {
                    Licence=5267008,
                    StartingDate= new DateTime(2013, 02, 05),
                    Kilometrz=22000,
                    KilometrFromLastTreat=2000,
                    FuellAmount=200,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },


                new Bus
                {
                    Licence=2784562,
                    StartingDate= new DateTime(2014, 03, 05),
                    Kilometrz=22000,
                    KilometrFromLastTreat=2000,
                    FuellAmount=700,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
                new Bus
                {
                    Licence=12345678,
                    StartingDate= new DateTime(2019, 02, 05),
                    Kilometrz=10000,
                    KilometrFromLastTreat=10000,
                    FuellAmount=340,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
                new Bus
                {
                    Licence=5267008,
                    StartingDate= new DateTime(2013, 02, 05),
                    Kilometrz=22000,
                    KilometrFromLastTreat=2000,
                    FuellAmount=200,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
                new Bus
                {
                    Licence=1234567,
                    StartingDate= new DateTime(2013, 09, 21),
                    Kilometrz=70000,
                    KilometrFromLastTreat=1500.34,
                    FuellAmount=643.98,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
                new Bus
                {
                    Licence=7654321,
                    StartingDate= new DateTime(2013, 02, 05),
                    Kilometrz=22000,
                    KilometrFromLastTreat=2000,
                    FuellAmount=200,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
                new Bus
                {
                    Licence=5463728,
                    StartingDate= new DateTime(2013, 06, 20),
                    Kilometrz=22000,
                    KilometrFromLastTreat=78500,
                    FuellAmount=350,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
                new Bus
                {
                    Licence=8216542,
                    StartingDate= new DateTime(2010, 04, 15),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
                new Bus
                {
                    Licence=34509814,
                    StartingDate= new DateTime(2019, 02, 20),
                    Kilometrz=10500,
                    KilometrFromLastTreat=1400,
                    FuellAmount=300,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
                new Bus
                {
                    Licence=10926574,
                    StartingDate= new DateTime(2020, 04, 15),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
               new Bus
                {
                    Licence=1192657,
                    StartingDate= new DateTime(2010, 12, 15),
                    Kilometrz=28970,
                    KilometrFromLastTreat=8970,
                    FuellAmount=1000,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
             new Bus
                {
                    Licence=1265473,
                    StartingDate= new DateTime(2009, 07, 18),
                    Kilometrz=20000,
                    KilometrFromLastTreat=100,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                      BusExsis=true
                },
                new Bus
                {
                    Licence=89712365,
                    StartingDate= new DateTime(2020, 03, 15),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
                new Bus
                {
                    Licence=1778328,
                    StartingDate= new DateTime(2010, 02, 15),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
              new Bus
                {
                    Licence=5059589,
                    StartingDate= new DateTime(1999, 04, 15),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
                                                                                                                          new Bus
                {
                    Licence=12845999,
                    StartingDate= new DateTime(2020, 01, 15),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
                                                                                                                          new Bus
                {
                    Licence=2000000,
                    StartingDate= new DateTime(2012, 07, 25),
                    Kilometrz=109283,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
                                                                                                                      new Bus
                {
                    Licence=11119999,
                    StartingDate= new DateTime(2020, 08, 15),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },
                                                                                                                            new Bus
                {
                    Licence=8576669,
                    StartingDate= new DateTime(2017, 04, 15),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                     BusExsis=true
                },
                                                                                                                          new Bus
                {
                   Licence=10928300,
                    StartingDate= new DateTime(2020, 09, 22),
                    Kilometrz=100000,
                    KilometrFromLastTreat=15000,
                    FuellAmount=900,
                    StatusBus=DO.STUTUS.READT_TO_TRAVEL,
                    BusExsis=true
                },

                #endregion
            };



        }
    }
}
