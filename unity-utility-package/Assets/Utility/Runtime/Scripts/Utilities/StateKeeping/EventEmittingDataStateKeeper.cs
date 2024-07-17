using System;

namespace MrWatts.Internal.Utilities
{
    public sealed class EventEmittingDataStateKeeper<T> : IDataStateKeeper<T>
    {
        public T State
        {
            get
            {
                return delegatee.State;
            }
            set
            {
                T oldValue = delegatee.State;
                delegatee.State = value;
                OnValueChanged?.Invoke(this, new DataStateKeeperValueChangedEventArgs<T>(oldValue, value));
            }
        }

        public event EventHandler<DataStateKeeperValueChangedEventArgs<T>>? OnValueChanged;

        private readonly IDataStateKeeper<T> delegatee;

        public EventEmittingDataStateKeeper(IDataStateKeeper<T> delegatee)
        {
            this.delegatee = delegatee;
        }
    }
}
