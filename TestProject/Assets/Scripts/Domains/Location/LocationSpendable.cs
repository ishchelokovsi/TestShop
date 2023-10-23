using Domains.Core;
using System;
using UnityEngine;

namespace Domains.Location
{
    [Serializable]
    public class LocationSpendable : ISpendable
    {
        #region Fields

        [SerializeField] private string _location;

        #endregion

        #region Properties

        public string Location => _location;

        #endregion

        #region Methods

        public void Dispose()
        {
            LocationManager.Instance.OnStateChanged -= OnStateChanged;
        }

        private void OnStateChanged(string value)
        {
            OnAvailabilityChanged?.Invoke(CanSpend());
        }

        #endregion

        #region ISpendable

        public void Init()
        {
            LocationManager.Instance.OnStateChanged += OnStateChanged;
        }

        public bool TrySpend()
        {
            LocationManager.Instance.Set(Location);
            return true;
        }

        public bool CanSpend()
        {
            return LocationManager.Instance.Value != Location;
        }

        public event Action<bool> OnAvailabilityChanged;

        #endregion
    }
}