using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrWatts.Internal.Utilities
{
    /// <summary>
    /// Runs tasks in a queue, one by one, waiting for the previous queued ones to finish before starting the next.
    /// </summary>
    public sealed class QueuedTaskRunner
    {
        private HashSet<Task> tasks = new();

        /// <summary>
        /// Runs the task returned by the factory once all previously queued tasks have finished executing.
        /// </summary>
        /// <returns>A task that resolves or fails (only) when the task returned by the factory does so.</returns>
        public async Task<T> RunAsync<T>(Func<Task<T>> taskFactory)
        {
            Task[] tasksToWaitFor = tasks.ToArray();

            TaskCompletionSource<bool> taskCompletionSource = new();

            tasks.Add(taskCompletionSource.Task);

            if (tasksToWaitFor.Length > 0)
            {
                try
                {
                    await Task.WhenAll(tasksToWaitFor);
                }
#pragma warning disable RCS1075,ERP022 // Avoid empty catch clause that catches System.Exception
                catch (Exception)
                {
                    /*
                        We don't care about the exception, as the original call to this method will already fail with it.
                        This method should only resolve a faulty task if the main async job itself fails, as the only
                        intent here is to wait for completion of the previous ones (failed or not).
                    */
                }
#pragma warning restore RCS1075,ERP022 // Avoid empty catch clause that catches System.Exception
            }

            try
            {
                T value = await taskFactory();

                return value;
            }
            finally
            {
                taskCompletionSource.SetResult(true);

                tasks.Remove(taskCompletionSource.Task);
            }
        }

        /// <summary>
        /// Runs the task returned by the factory once all previously queued tasks have finished executing.
        /// </summary>
        /// <returns>A task that resolves or fails (only) when the task returned by the factory does so.</returns>
        public async Task RunAsync(Func<Task> taskFactory)
        {
            await RunAsync(async () =>
            {
                await taskFactory();
                return true;
            });
        }
    }
}