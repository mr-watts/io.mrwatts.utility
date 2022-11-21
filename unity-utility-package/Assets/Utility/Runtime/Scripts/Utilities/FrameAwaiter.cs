using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace MrWatts.Internal.Utilities
{
    public sealed class FrameAwaiter
    {
        private bool isDone;
        private Coroutine? coroutine;

        private MonoBehaviour targetComponent;

        public FrameAwaiter(MonoBehaviour targetComponent)
        {
            this.targetComponent = targetComponent;
        }

        public async Task OneFrameAsync()
        {
            isDone = false;

            if (coroutine != null)
            {
                targetComponent.StopCoroutine(coroutine);
            }

            coroutine = targetComponent.StartCoroutine(WaitForFrame());

            while (!isDone)
            {
                await Task.Delay(1);
            }
        }

        private IEnumerator WaitForFrame()
        {
            yield return new WaitForEndOfFrame();
            isDone = true;
        }
    }
}