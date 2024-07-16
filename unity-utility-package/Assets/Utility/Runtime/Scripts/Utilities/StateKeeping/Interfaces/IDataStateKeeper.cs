namespace MrWatts.Internal.Utilities
{
    public interface IDataStateKeeper<T>
    {
        T State { get; set; }
    }
}
