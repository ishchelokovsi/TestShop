using Domains.Core;
using System;
using UnityEngine;

namespace Domains.Location
{
    [Serializable]
    public class LocationReward : IReward
    {
        #region Fields

        [SerializeField] private string _location;

        #endregion

        #region Properties

        public string Location => _location;

        #endregion

        #region IReward

        public void ApplyReward()
        {
            LocationManager.Instance.Set(Location);
        }

        public override string ToString()
        {
            return string.Format("{0} move", Location);
        }

        #endregion
    }
}