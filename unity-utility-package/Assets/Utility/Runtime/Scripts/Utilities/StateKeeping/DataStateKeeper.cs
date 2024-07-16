namespace MrWatts.Internal.Utilities
{
    public sealed class DataStateKeeper<T> : IDataStateKeeper<T>
    {
        public T State { get; set; }
    }
}
