//using System;
//using System.Collections.Generic;

//namespace doNet5781_03B_4789_9647
//{
//    public static class BusCompany
//    {
//        static private List<Bus> egged = new List<Bus>();
//        static Random r = new Random(DateTime.Now.Millisecond);

//        static public Egged(){ get; set; }
       

//        private static void construct(List<Bus> egged)    //List<Bus> egged)                                             //private void initBuses(ObservableCollection<Bus> egged)
//        {
//            int license1;
//            DateTime date1;


//            for (int i = 0; i < 10; i++) ///restart 10 buses 
//            {
//                date1 = new DateTime(r.Next(1990, DateTime.Today.Year + 1), r.Next(1, 13), r.Next(1, 29));//,r.Next(1,25),r.Next(0,60),r.Next(0,60));
//                                                                                                          //int a = date1.Year;
//                do
//                {
//                    if ((date1.Year < 2018))
//                    {
//                        license1 = r.Next(1000000, 10000000); //to random number with 7 digite
//                    }
//                    else
//                    {
//                        license1 = r.Next(10000000, 100000000); //to random number with 8 digite
//                    }

//                } while (findBuse(egged, license1)); //check if the license exsis

//                try
//                {
//                    Bus temp = new Bus(license1, date1);
//                    egged.Add(temp);
//                }
//                catch (Exception)
//                {
//                    i--;
//                }




//            }
//        }

//        private static bool findBuse(List<Bus> buses, int num)
//        //private static bool findBuse(ObservableCollection<Bus> buses, int num)
//        {
//            int temp1;


//            foreach (Bus item in buses) //move on the list buses
//            {
//                string temp = item.License;

//                temp = temp.Replace("-", string.Empty); //To remove the hyphens from our license number
//                int.TryParse(temp, out temp1);

//                if (temp1 == num) //check if the licenes equal
//                {
//                    return true;
//                }

//            }
//            return false;

//        }

//    }
//}