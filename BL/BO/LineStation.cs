namespace BO
{
    public class LineStation
    {
        public int LineId { get; set; }
        public int StationCode { get; set; }
        public int LineStationIndex { get; set; }
        public bool LineStationExist { get; set; }
        public int PrevStation { get; set; } //opsionaly
        public int NextStation { get; set; } //opsionaly

        public double DistanceFromNext { get; set; }
        public double TimeAverageFromNext { get; set; }



    }
}
