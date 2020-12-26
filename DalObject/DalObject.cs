using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using DALAPI;
//using DO;
using DS;


namespace DL
{
    sealed class DalObject : IDAL
    {
        #region singelton
        static readonly DalObject instance = new DalObject();
        static DalObject() { }
        DalObject() { }
        public static DalObject Instance => instance;
        #endregion

        #region Bus
        public DO.Bus GetBus(int licence) //check if the bus exsis according to the licence
        {
            DO.Bus bus = DataSource.ListBus.Find(b => b.Licence == licence);
            if (bus != null && bus.BusExsis)
            {
                return bus.Clone();
            }
            else
                throw new DO.WrongLicenceException(licence, $"Licence not valid:{licence}");
          
        }
        public IEnumerable<DO.Bus> GetAllBuses() //return all the buses that we have
        {
            return from bus in DataSource.ListBus
                   select bus.Clone();
        }

        public IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> buscondition) 
        {
            var list = from bus in DataSource.ListBus
                       where (bus.BusExsis && buscondition(bus))
                       select bus.Clone();
            return list;
        }

        public void AddBus(DO.Bus bus)
        {
            if (DataSource.ListBus.FirstOrDefault(b => b.Licence == bus.Licence) != null) //if != null its means that this licence is allready exsis
                throw new DO.WrongLicenceException(bus.Licence, "This licence already exsis");
            DataSource.ListBus.Add(bus.Clone());
        }

        ///===================================================================
        ///לבדוק איך מעדכנים נתון בתוך הרשימה שלי
        public void DeleteBus(int licence) 
        {
            DO.Bus bus= DataSource.ListBus.Find(b => b.Licence == licence);
            if (bus != null && bus.BusExsis)
            {
                // bus.BusExsis = false;
                DataSource.ListBus.Find(b => b.Licence == licence).BusExsis = false;
            }
            else
                throw new DO.WrongLicenceException(licence, "Licence not exsis");
        }
        public void UpdateBus(DO.Bus buses)
        {
            DO.Bus bus = DataSource.ListBus.Find(b => b.Licence == buses.Licence );
            if (bus != null && bus.BusExsis)
            {
                DataSource.ListBus.Remove(bus);
                DataSource.ListBus.Add(buses.Clone());
            }
            else
                throw new DO.WrongLicenceException(buses.Licence, "Licence not exsis");
        }



        //public void UpdateBus(int licence, Action<DO.Bus> update)
        //{
        //    DO.Bus bus = DataSource.ListBus.Find(b => b.Licence == licence);
        //    if (bus != null && bus.BusExsis)//
        //    {


        //    }
        //    else
        //        throw new DO.WrongLicenceException(licence, "Licence not exsis");
        //}

        #endregion Bus

        #region Line
        //public int Id { get; set; }
        //public int CodeLine { get; set; }
        //public int FirstStationCode { get; set; }
        //public int LastStationCode { get; set; }
        //public AREA Area { get; set; }
        //public bool LineExsis { get; set; }

        public DO.Line GetLine(int licence)
        {
            DO.Line line = DataSource.ListLine.Find(l => l.Licence == licence);
            if (line != null&&line.LineExsis)
                return line.Clone();
            else
                throw new DO.WrongLicenceException(licence, "The line not exsis ");

        }

        public void AddLine(DO.Line line)
        {
            if(DataSource.ListLine.FirstOrDefault(l=>l.Licence==line.Licence)!=null) //so the line is already exsis
            {
                DO.Line l = DataSource.ListLine.Find(b => b.Licence == line.Licence );

                if (l.LineExsis)
                    throw new DO.WrongLicenceException(line.Licence, "the line is already exsis");
                if(DataSource.ListBus.FirstOrDefault(b=>b.Licence==line.Licence)!=null&&!(DataSource.ListBus.Find(b => b.Licence == line.Licence).BusExsis))
                    throw new DO.WrongLicenceException(line.Licence, "the line is already exsis");
                else
                    l.LineExsis = true; ///לבדוק אם זה באמת מעדכן את השדה הזה


            }


        }
           /// כדי לא ךשכוח. צריך לחשוב אם כל פעם שמוחקים אוטובוס אז צרך לעדכן גם ברשומה של הקו אם הוא לא קיים. אחרת צריך כל פעם לעשות בדיקות בקו אוטובוס נראלי. 


        #endregion Line
        


        
    }
}
