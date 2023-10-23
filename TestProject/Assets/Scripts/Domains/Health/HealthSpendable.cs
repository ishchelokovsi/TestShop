using Domains.Core;
using System;
using UnityEngine;

namespace Domains.Health
{
    [Serializable]
    public class HealthSpendable : ISpendable
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
            HealthManager.Instance.OnStateChanged -= OnStateChanged;
        }

        private void OnStateChanged(int value)
        {
            OnAvailabilityChanged?.Invoke(CanSpend());
        }

        #endregion

        #region ISpendable

        public void Init()
        {
            HealthManager.Instance.OnStateChanged += OnStateChanged;
        }

        public bool TrySpend()
        {
            var available = CanSpend();
            if (!available)
            {
                return false;
            }

            HealthManager.Instance.Spend(Value);
            return true;
        }

        public bool CanSpend()
        {
            return HealthManager.Instance.IsEnough(Value);
        }

        public event Action<bool> OnAvailabilityChanged;

        #endregion
    }
}