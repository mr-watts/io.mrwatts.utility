using System.Collections;
using UnityEngine;

namespace MrWatts.Internal.Utilities
{
    public sealed class CoroutineRunner : MonoBehaviour
    {
        public Coroutine Run(IEnumerator enumerator)
        {
            return StartCoroutine(enumerator);
        }

        public void Run(IEnumerator enumerator, ref Coroutine coroutine)
        {
            Stop(coroutine);
            coroutine = StartCoroutine(enumerator);
        }

        public void Stop(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
    }
}