using System;

namespace Domains.Location
{
    public class LocationManager
    {
        #region Fields

        public event Action<string> OnStateChanged;

        private static LocationManager _instance;

        #endregion

        #region Properties

        //for simplicity
        public string Value { get; private set; } = "default";

        public static LocationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LocationManager();
                }

                return _instance;
            }
        }

        #endregion

        #region Class lifecycle

        private LocationManager() { }

        #endregion

        #region Methods

        public void Set(string value)
        {
            Value = value;
            OnStateChanged?.Invoke(value);
        }

        #endregion
    }
}