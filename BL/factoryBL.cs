using BL;

namespace BlAPI
{
    public static class factoryBL
    {
        public static IBL GetBl()
        {
            return BlImp.Instance;

        }

    }
}
