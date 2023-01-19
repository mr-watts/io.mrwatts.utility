using UnityEngine;

namespace MrWatts.Internal.Utilities
{
    public static class TransformExtensions
    {
        public static void ClearChildren(this Transform t)
        {
            foreach (Transform child in t)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}