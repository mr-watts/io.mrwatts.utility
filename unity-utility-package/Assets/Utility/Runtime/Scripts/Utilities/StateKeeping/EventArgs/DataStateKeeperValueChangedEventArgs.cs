using System;

namespace MrWatts.Internal.Utilities
{
    public sealed class DataStateKeeperValueChangedEventArgs<T> : EventArgs
    {
        public T? OldValue;
        public T? NewValue;

        public DataStateKeeperValueChangedEventArgs(T? oldValue, T? newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}