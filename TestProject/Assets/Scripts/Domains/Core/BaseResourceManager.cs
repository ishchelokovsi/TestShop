using System;

namespace Domains.Core
{
    public abstract class BaseResourceManager<TData, TConcrete> : BaseSingleton<TConcrete>
        where TData : IComparable<TData>, IEquatable<TData>, IConvertible
        where TConcrete : BaseResourceManager<TData, TConcrete>, new()
    {
        #region Fields

        public event Action<TData> OnStateChanged;

        #endregion

        #region Properties

        public TData Value { get; private set; }

        #endregion

        #region Methods

        public bool IsEnough(TData value)
        {
            return (dynamic) Value > 0 && (dynamic) Value >= value;
        }

        public void Spend(TData value)
        {
            Value = (dynamic) Value - value;
            OnStateChanged?.Invoke(value);
        }

        public void Add(TData value)
        {
            Value = (dynamic) Value + value;
            OnStateChanged?.Invoke(value);
        }

        #endregion
    }
}