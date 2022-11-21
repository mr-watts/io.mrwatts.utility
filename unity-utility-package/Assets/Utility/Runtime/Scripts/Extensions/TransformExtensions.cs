using UnityEngine;

namespace MrWatts.Internal.Utilities
{
    internal static class TransformExtensions
    {
        internal static void ClearChildren(this Transform t)
        {
            foreach (Transform child in t)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}