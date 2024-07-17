using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using MrWatts.Internal.FuelInject;
using UnityEngine;

namespace MrWatts.Internal.Utilities
{
    public sealed class MainThreadDispatcher : ITickable, IDisposable
    {
        private ConcurrentQueue<Func<Task>> scheduledTaskFactories = new();
        private ConcurrentDictionary<Task, bool> pendingTasks = new();

        public bool HasQueuedTasks => scheduledTaskFactories.Count > 0;
        public bool HasPendingTasks => pendingTasks.Count > 0;

        public void Tick()
        {
            while (scheduledTaskFactories.TryDequeue(out Func<Task> taskFactory))
            {
                Task task = taskFactory.Invoke();
                pendingTasks.TryAdd(task, true);
                task.ContinueWith(_ => pendingTasks.TryRemove(task, out bool _));
            }
        }

        public void Dispose()
        {
            if (HasQueuedTasks || HasPendingTasks)
            {
                Debug.LogWarning(
                    $"MainThreadDispatcher was destroyed whilst {pendingTasks.Count} jobs were still running and " +
                    $"{scheduledTaskFactories.Count} were still scheduled and not yet started. The former might " +
                    "finish running in the future with no one awaiting their results, and the latter will never " +
                    "be executed and are dropped."
                );
            }
        }

        /// <summary>
        /// Runs the specified action on the main thread asynchronously.
        /// </summary>
        /// <param name="action"></param>
        /// <returns>A task that completes once the action is completed (or has faulted or was cancelled) on the main thread.</returns>
        public Task RunAsync(Action action)
        {
            return RunAsync(() =>
            {
                action();
                return Task.CompletedTask;
            });
        }

        /// <summary>
        /// Runs the specified asynchronous action on the main thread asynchronously.
        /// </summary>
        /// <param name="taskFactory"></param>
        /// <returns>A task that completes once the action is completed (or has faulted or was cancelled) on the main thread.</returns>
        public Task RunAsync(Func<Task> taskFactory)
        {
            return RunAsync(async () =>
            {
                await taskFactory();
                return true;
            });
        }

        /// <summary>
        /// Runs the specified asynchronous function on the main thread asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="taskFactory"></param>
        /// <returns>A task that completes with the result of the function once the action is completed (or has faulted
        /// or was cancelled) on the main thread.</returns>
        public Task<T> RunAsync<T>(Func<Task<T>> taskFactory)
        {
            var taskCompletionSource = new TaskCompletionSource<T>();

            scheduledTaskFactories.Enqueue(async () =>
            {
                T result;

                try
                {
                    result = await taskFactory();
                }
                catch (Exception e)
                {
                    taskCompletionSource.TrySetException(e);
                    throw;
                }

                taskCompletionSource.TrySetResult(result);
            });

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Enqueues the specified action to be run on the main thread asynchronously. This call will not block until
        /// the action is completed, so you will not be able to catch exceptions (use <see cref="RunAsync"/>
        /// instead if that's what you want or if you want to wait for the acction to be executed).
        /// </summary>
        /// <param name="action"></param>
        public void Enqueue(Action action)
        {
            _ = RunAsync(action);
        }
    }
}