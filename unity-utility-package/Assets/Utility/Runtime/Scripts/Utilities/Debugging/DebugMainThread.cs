using System;
using System.Threading.Tasks;

#if UNITY_5_3_OR_NEWER && ENABLE_WINMD_SUPPORT
using UnityEngine.WSA;
#endif

namespace MrWatts.Internal.Utilities
{
    /// <summary>
    /// <para>Various functions from Unity's <see cref="UnityEngine.Debug">Debug</see>, executed on the main thread.</para>
    /// <para>
    /// Logging debug messages works fine from other threads, but they might only show up in Unity's log, and not in places
    /// such as UI elements listening to new logs being generated. For this to work, logging must happen in the main thread.
    /// This class provides wrappers to achieve this.
    /// </para>
    /// </summary>
    public static class DebugMainThread
    {
#pragma warning disable RCS1046
        public static Task Log(object message)
        {
            var promise = new TaskCompletionSource<bool>();

#if UNITY_5_3_OR_NEWER && ENABLE_WINMD_SUPPORT
            Application.InvokeOnAppThread(
                () =>
                {
                    UnityEngine.Debug.Log(message);
                    promise.TrySetResult(true);
                },
                waitUntilDone: false
            );
#elif UNITY_5_3_OR_NEWER
            UnityEngine.Debug.Log(message);
            promise.TrySetResult(true);
#else
            Console.WriteLine(message);
            promise.TrySetResult(true);
#endif

            return promise.Task;
        }

        public static Task LogWarning(object message)
        {
            var promise = new TaskCompletionSource<bool>();

#if UNITY_5_3_OR_NEWER && ENABLE_WINMD_SUPPORT
            Application.InvokeOnAppThread(
                () =>
                {
                    UnityEngine.Debug.LogWarning(message);
                    promise.TrySetResult(true);
                },
                waitUntilDone: false
            );
#elif UNITY_5_3_OR_NEWER
            UnityEngine.Debug.LogWarning(message);
            promise.TrySetResult(true);
#else
            Console.WriteLine("WARNING: " + message);
            promise.TrySetResult(true);
#endif

            return promise.Task;
        }

        public static Task LogAssertion(object message)
        {
            var promise = new TaskCompletionSource<bool>();

#if UNITY_5_3_OR_NEWER && ENABLE_WINMD_SUPPORT
            Application.InvokeOnAppThread(
                () =>
                {
                    UnityEngine.Debug.LogAssertion(message);
                    promise.TrySetResult(true);
                },
                waitUntilDone: false
            );
#elif UNITY_5_3_OR_NEWER
            UnityEngine.Debug.LogAssertion(message);
            promise.TrySetResult(true);
#else
            Console.WriteLine("ASSERTION: " + message);
            promise.TrySetResult(true);
#endif

            return promise.Task;
        }

        public static Task LogError(object message)
        {
            var promise = new TaskCompletionSource<bool>();

#if UNITY_5_3_OR_NEWER && ENABLE_WINMD_SUPPORT
            Application.InvokeOnAppThread(
                () =>
                {
                    UnityEngine.Debug.LogError(message);
                    promise.TrySetResult(true);
                },
                waitUntilDone: false
            );
#elif UNITY_5_3_OR_NEWER
            UnityEngine.Debug.LogError(message);
            promise.TrySetResult(true);
#else
            Console.Error.WriteLine("ERROR: " + message);
            promise.TrySetResult(true);
#endif

            return promise.Task;
        }

        public static Task LogException(Exception exception)
        {
            var promise = new TaskCompletionSource<bool>();

#if UNITY_5_3_OR_NEWER && ENABLE_WINMD_SUPPORT
            Application.InvokeOnAppThread(
                () =>
                {
                    UnityEngine.Debug.LogException(exception);
                    promise.TrySetResult(true);
                },
                waitUntilDone: false
            );
#elif UNITY_5_3_OR_NEWER
            UnityEngine.Debug.LogException(exception);
            promise.TrySetResult(true);
#else
            Console.Error.WriteLine("EXCEPTION: " + exception.ToString());
            promise.TrySetResult(true);
#endif

            return promise.Task;
        }
#pragma warning disable RCS1046
    }
}