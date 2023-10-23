using System;

namespace Domains.Core
{
    public abstract class BaseResourceManager<TData, TConcrete>
        where TData : IComparable<TData>, IEquatable<TData>, IConvertible
        where TConcrete : BaseResourceManager<TData, TConcrete>, new()
    {
        #region Fields

        public event Action<TData> OnStateChanged;

        private static BaseResourceManager<TData, TConcrete> _instance;

        #endregion

        #region Properties

        public static BaseResourceManager<TData, TConcrete> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TConcrete();
                }

                return _instance;
            }
        }

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