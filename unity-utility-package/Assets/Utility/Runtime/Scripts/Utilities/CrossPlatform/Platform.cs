using System;

#pragma warning disable RCS1135
namespace MrWatts.CommScope.EnclosureVisualizer
{
    [Flags]
    public enum Platform
    {
        Unknown = 0,
        Editor = 1,
        HoloLens2 = 1 << 1,
        Android = 1 << 2,
        IOS = 1 << 3,
        WindowsPhone = 1 << 4,
        WindowsDesktop = 1 << 5,
        WindowsIoT = 1 << 6,
        WindowsXbox = 1 << 7,
        WindowsSurfaceHub = 1 << 8
    }
}