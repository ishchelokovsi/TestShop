using System;

namespace Domains.Core
{
    public interface ISpendable
    {
        #region Fields

        event Action<bool> OnAvailabilityChanged;

        #endregion

        #region Methods

        void Init();

        bool TrySpend();

        bool CanSpend();

        #endregion
    }
}