using Domains.Core;
using System;

namespace Domains.Location
{
    public class LocationManager : BaseSingleton<LocationManager>
    {
        #region Fields

        public event Action<string> OnStateChanged;

        #endregion

        #region Properties

        //for simplicity
        public string Value { get; private set; } = "default";

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