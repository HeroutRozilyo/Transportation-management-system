namespace DS
{
    public static class Config
    {
        //for Line
        public static int idLineCounter;
        //for trip uses
        public static int tripUser;
        //for bus trip
        public static int tripBus;


        static Config()
        {
            idLineCounter = 0;
            tripUser = 0;
            tripBus = 0;
        }
    }
}
