using System;
using System.Threading.Tasks;
using UnityEngine.Events;

// Don't complain about async void since this class is all about wrapping it.
#pragma warning disable EPC17

namespace MrWatts.Internal.Utilities
{
    /// <summary>
    /// Factory that creates <see langword="async"/> event handlers that wrap others so they don't have to return
    /// <see langword="async"/> void.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Event handlers usually return void, and when they become asynchronous, that tends to turn into `async void`,
    ///         which results in exceptions of inner tasks not being observed correctly without special care, which in turn
    ///         is either forgotten or try-catch is sprinkled everywhere. This wrapper does this for you.
    ///     </para>
    ///     <para>
    ///         Unobserved exceptions are logged through the injected logger.
    ///     </para>
    /// </remarks>
    public sealed class AsyncVoidMethodFactory
    {
        private readonly IExceptionLogger logger;

        public AsyncVoidMethodFactory(IExceptionLogger logger)
        {
            this.logger = logger;
        }

#region Event Handlers
        public EventHandler<TEventArgs> Create<TEventArgs>(Func<object, TEventArgs, Task> method)
        {
            return async (object sender, TEventArgs args) =>
#pragma warning restore EPC17
            {
                try
                {
                    await method(sender, args);
                }
                catch (Exception e)
                {
                    logger.Log(e, e.Message);
                }
            };
        }

        public EventHandler Create(Func<object, EventArgs, Task> method)
        {
            return async (object sender, EventArgs args) =>
            {
                try
                {
                    await method(sender, args);
                }
                catch (Exception e)
                {
                    logger.Log(e, e.Message);
                }
            };
        }
#endregion Event Handlers

#region Actions
        public Action<T1> CreateAction<T1>(Func<T1, Task> method)
        {
            return async (T1 args) =>
            {
                try
                {
                    await method(args);
                }
                catch (Exception e)
                {
                    logger.Log(e, e.Message);
                }
            };
        }

        public Action<T1, T2> CreateAction<T1, T2>(Func<T1, T2, Task> method)
        {
            return async (T1 args1, T2 args2) =>
            {
                try
                {
                    await method(args1, args2);
                }
                catch (Exception e)
                {
                    logger.Log(e, e.Message);
                }
            };
        }

        public Action<T1, T2, T3> CreateAction<T1, T2, T3>(Func<T1, T2, T3, Task> method)
        {
            return async (T1 args1, T2 args2, T3 args3) =>
            {
                try
                {
                    await method(args1, args2, args3);
                }
                catch (Exception e)
                {
                    logger.Log(e, e.Message);
                }
            };
        }

        public Action<T1, T2, T3, T4> CreateAction<T1, T2, T3, T4>(Func<T1, T2, T3, T4, Task> method)
        {
            return async (T1 args1, T2 args2, T3 args3, T4 args4) =>
            {
                try
                {
                    await method(args1, args2, args3, args4);
                }
                catch (Exception e)
                {
                    logger.Log(e, e.Message);
                }
            };
        }
#endregion Actions

#region Unity Actions
        public UnityAction<T1> CreateUnityAction<T1>(Func<T1, Task> method)
        {
            return async (T1 args) =>
            {
                try
                {
                    await method(args);
                }
                catch (Exception e)
                {
                    logger.Log(e, e.Message);
                }
            };
        }

        public UnityAction<T1, T2> CreateUnityAction<T1, T2>(Func<T1, T2, Task> method)
        {
            return async (T1 args1, T2 args2) =>
            {
                try
                {
                    await method(args1, args2);
                }
                catch (Exception e)
                {
                    logger.Log(e, e.Message);
                }
            };
        }

        public UnityAction<T1, T2, T3> CreateUnityAction<T1, T2, T3>(Func<T1, T2, T3, Task> method)
        {
            return async (T1 args1, T2 args2, T3 args3) =>
            {
                try
                {
                    await method(args1, args2, args3);
                }
                catch (Exception e)
                {
                    logger.Log(e, e.Message);
                }
            };
        }

        public UnityAction<T1, T2, T3, T4> CreateUnityAction<T1, T2, T3, T4>(Func<T1, T2, T3, T4, Task> method)
        {
            return async (T1 args1, T2 args2, T3 args3, T4 args4) =>
            {
                try
                {
                    await method(args1, args2, args3, args4);
                }
                catch (Exception e)
                {
                    logger.Log(e, e.Message);
                }
            };
        }
#endregion Unity Actions
    }
}