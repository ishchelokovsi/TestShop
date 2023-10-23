using Domains.Core;
using System;
using UnityEngine;

namespace Domains.Health
{
    [Serializable]
    public class HealthReward : IReward
    {
        #region Fields

        [SerializeField] private int _value;

        #endregion

        #region Properties

        public int RewardValue => _value;

        #endregion

        #region IReward

        public void ApplyReward()
        {
            HealthManager.Instance.Add(RewardValue);
        }

        public override string ToString()
        {
            return string.Format("{0} health", RewardValue);
        }

        #endregion
    }
}