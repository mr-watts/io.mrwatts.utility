using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrWatts.Internal.Utilities
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> action)
        {
            foreach (var item in enumerable)
            {
                await action(item);
            }
        }

        internal static Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, Task<TResult>> selector)
        {
            return source.SelectAsync((x, _) => selector(x));
        }

        internal static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, Task<TResult>> selector)
        {
            int i = 0;
            ConcurrentBag<TResult> resultList = new();

            foreach (TSource item in source)
            {
                resultList.Add(await selector(item, i++));
            }

            return resultList;
        }
    }
}
