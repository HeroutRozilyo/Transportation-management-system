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

            // public int Licence { get; set; }
            //public DateTime StartingDate { get; set; }
            //public double Kilometrz { get; set; }
            //public double KilometrFromLastTreat { get; set; }
            //public double FuellAmount { get; set; }
            //public STUTUS StatusBus { get; set; }
            //public bool BusExsis { get; set; }


            ListBus = new List<Bus>
            {
                #region initialization buses
                new Bus
                {
                    Licence=5267008,
                    StartingDate= new DateTime(2013, 02, 05),
            //Kilometrz
            //KilometrFromLastTreat
            //FuellAmount
            //StatusBus
            //BusExsis
        },
                new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                                              new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                                                    new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                                                          new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                                                                new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                                                                      new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                                                                            new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                                                                                  new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                                                                                        new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                                                                                              new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                                                                                                    new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                                                                                                          new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                                                                                                                new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                                                                                                                      new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                                                                                                                            new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },
                                                                                                                                  new Bus
                {
                    //Licence=
                    //StartingDate
                    //Kilometrz
                    //KilometrFromLastTreat
                    //FuellAmount
                    //StatusBus
                    //BusExsis
                },




                #endregion
            };





        }
    }
}
