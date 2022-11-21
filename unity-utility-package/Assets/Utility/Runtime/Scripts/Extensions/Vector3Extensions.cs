using System;
using UnityEngine;

namespace MrWatts.Internal.Utilities
{
    public static class Vector3Extensions
    {
        public static float GetBiggestValue(this Vector3 vector3)
        {
            return Math.Max(Math.Max(vector3.x, vector3.y), vector3.z);
        }

        public static float GetSmallestValue(this Vector3 vector3)
        {
            return Math.Min(Math.Min(vector3.x, vector3.y), vector3.z);
        }
    }
}