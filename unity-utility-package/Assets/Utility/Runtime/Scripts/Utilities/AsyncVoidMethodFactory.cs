using System;
using System.Threading.Tasks;

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

        public EventHandler<TEventArgs> Create<TEventArgs>(Func<object, TEventArgs, Task> method)
        {
#pragma warning disable EPC17
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
    }
}