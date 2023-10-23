using Domains.Core;
using System;
using UnityEngine;

namespace Domains.Gold
{
    [Serializable]
    public class GoldSpendable : ISpendable
    {
        #region Fields

        [SerializeField] private int _value;

        #endregion

        #region Properties

        public int Value => _value;

        #endregion

        #region Methods

        public void Dispose()
        {
            GoldManager.Instance.OnStateChanged -= OnGoldStateChanged;
        }

        private void OnGoldStateChanged(int value)
        {
            OnAvailabilityChanged?.Invoke(CanSpend());
        }

        #endregion

        #region ISpendable

        public void Init()
        {
            GoldManager.Instance.OnStateChanged += OnGoldStateChanged;
        }

        public bool TrySpend()
        {
            var available = CanSpend();
            if (!available)
            {
                return false;
            }

            GoldManager.Instance.Spend(Value);
            return true;
        }

        public bool CanSpend()
        {
            return GoldManager.Instance.IsEnough(Value);
        }

        public event Action<bool> OnAvailabilityChanged;

        #endregion
    }
}