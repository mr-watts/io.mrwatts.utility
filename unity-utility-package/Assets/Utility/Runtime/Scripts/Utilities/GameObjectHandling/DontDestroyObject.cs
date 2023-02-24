using UnityEngine;

namespace MrWatts.Internal.Utilities
{
    public sealed class DontDestroyObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}