using Domains.Core;
using System;
using UnityEngine;

namespace Domains.Gold
{
    [Serializable]
    public class GoldRewardable : IReward
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
            GoldManager.Instance.Add(RewardValue);
        }

        public override string ToString()
        {
            return string.Format("{0} gold", RewardValue);
        }

        #endregion
    }
}