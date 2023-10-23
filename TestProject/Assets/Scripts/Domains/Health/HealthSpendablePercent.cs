using Domains.Core;
using System;
using UnityEngine;

namespace Domains.Health
{
    [Serializable]
    public class HealthSpendablePercent : ISpendable
    {
        #region Fields

        [SerializeField] [Range(0f, 1f)] private float _value;

        #endregion

        #region Properties

        public float Value => _value;

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
            var value = Mathf.CeilToInt(HealthManager.Instance.Value * Value);
            var available = CanSpend();
            if (!available)
            {
                return false;
            }

            HealthManager.Instance.Spend(value);
            return true;
        }

        public bool CanSpend()
        {
            var value = Mathf.CeilToInt(HealthManager.Instance.Value * Value);
            return HealthManager.Instance.IsEnough(value);
        }

        public event Action<bool> OnAvailabilityChanged;

        #endregion
    }
}