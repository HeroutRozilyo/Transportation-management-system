//using DALAPI;
//using DL;
//using DO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace checkDO
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            IDAL dl = DalFactory.GetDL();
//            STUTUS stusus = DO.STUTUS.READT_TO_TRAVEL;
//            DO.Bus bus = new Bus
//            {
//                Licence = "11111111",
//                StartingDate = new DateTime(2013, 02, 05),
//                Kilometrz = 22000,
//                KilometrFromLastTreat = 2000,
//                FuellAmount = 200,
//                StatusBus = DO.STUTUS.READT_TO_TRAVEL,
//                BusExsis = true
//            };
//            string a = "5267008";
//            string licence = "5267008";
//            //buscondition
//            DO.Bus b = dl.GetBus(a); //return the bus exsis according to the licence

//            IEnumerable<DO.Bus> ba = dl.GetAllBuses(); //return all the buses that we have
//            //IEnumerable<DO.Bus> bc= dl.GetAllBusesBy( buscondition);
//            IEnumerable<DO.Bus> bs = dl.GetAllBusesStusus(stusus);
//            int bw = dl.AddBus(bus);
//            bool be = dl.DeleteBus(licence);
//            bool br = dl.UpdateBus(bus);


//        }
//    }
//}
