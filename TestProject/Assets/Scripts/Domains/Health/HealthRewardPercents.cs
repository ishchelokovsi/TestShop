using Domains.Core;
using System;
using UnityEngine;

namespace Domains.Health
{
    [Serializable]
    public class HealthRewardPercents : IReward
    {
        #region Fields

        [SerializeField] private float _value;

        #endregion

        #region Properties

        public float RewardValue => _value;

        #endregion

        #region IReward

        public void ApplyReward()
        {
            var value = Mathf.CeilToInt(HealthManager.Instance.Value * RewardValue);
            HealthManager.Instance.Add(value);
        }

        public override string ToString()
        {
            return string.Format("{0} percents of health", RewardValue);
        }

        #endregion
    }
}