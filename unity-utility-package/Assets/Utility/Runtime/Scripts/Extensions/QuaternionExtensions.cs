using System.Collections.Generic;
using UnityEngine;

namespace MrWatts.Internal.Utilities
{
    public static class QuaternionExtensions
    {
        public static Quaternion Average(this IEnumerable<Quaternion> quaternions)
        {
            float x = 0f;
            float y = 0f;
            float z = 0f;
            float w = 0f;

            foreach (Quaternion q in quaternions)
            {
                x += q.x;
                y += q.y;
                z += q.z;
                w += q.w;
            }

            float k = 1.0f / Mathf.Sqrt((x * x) + (y * y) + (z * z) + (w * w));
            return new Quaternion(x * k, y * k, z * k, w * k);
        }
    }
}