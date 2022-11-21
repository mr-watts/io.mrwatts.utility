namespace MrWatts.Internal.Utilities
{
    public static class FloatExtensions
    {
        public static float Normalized(this float f, float minValue, float maxValue)
        {
            return (f - minValue) / (maxValue - minValue);
        }
        public static float Denormalized(this float f, float minValue, float maxValue)
        {
            return (f * (maxValue - minValue)) + minValue;
        }
    }
}