namespace Domains.Core
{
    public class BaseSingleton<TConcrete> where TConcrete : BaseSingleton<TConcrete>, new()
    {
        #region Fields

        private static TConcrete _instance;

        #endregion

        #region Properties

        //not really a singleton as we can create TConcrete as TConcrete constructor is not private but for simplicity reasons
        public static TConcrete Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TConcrete();
                }

                return _instance;
            }
        }

        #endregion
    }
}