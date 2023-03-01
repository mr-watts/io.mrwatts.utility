using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UnityEngine;

namespace MrWatts.Internal.Utilities
{
    public sealed class WorkQueueHandler : MonoBehaviour
    {
        private static WorkQueueHandler _instance;
        public static WorkQueueHandler Instance => _instance ?? (_instance = FindObjectOfType<WorkQueueHandler>());

        private ConcurrentQueue<Action> queue = new ConcurrentQueue<Action>();

        private void Awake()
        {
            if (Instance != this)
            {
                Destroy(this);
            }
        }

        private void Update()
        {
            while (queue.TryDequeue(out Action action))
            {
                action?.Invoke();
            }
        }

        public void Enqueue(Action action)
        {
            _ = EnqueueAsync(action);
        }

        public Task EnqueueAsync(Action action)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

            queue.Enqueue(() =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    taskCompletionSource.TrySetException(e);
                    throw;
                }

                taskCompletionSource.TrySetResult(true);
            });

            return taskCompletionSource.Task;
        }
    }
}