using System;
using System.Collections.Generic;
using System.Linq;

namespace SilverlightRun.Util
{
    /// <summary>
    /// Additional operators for the IEnumerable of T monad.
    /// </summary>
    public static class EnumerableExtensions
    {
        public static IEnumerable<U> MZip<T, U>(this IEnumerable<IEnumerable<T>> lists, Func<IEnumerable<T>, U> selector)
        {
            int maxLenght = lists.Max((t) => t.Count());
            int vectorSize = lists.Count();
            for (int i = 0; i < maxLenght; i++)
            {
                var aggregate = new T[vectorSize];
                for (int j = 0; j < vectorSize; j++) aggregate[j] = lists.ElementAt(j).ElementAtOrDefault(i);
                yield return selector(aggregate);
            }
        }

        public static List<T> FillUp<T>(this IEnumerable<T> list, Func<T, T> creator)
        {
            var onenotnull = list.First((t) => t != null);
            return list.Select((t) => t == null ? creator(onenotnull) : t).ToList();
        }
    }
}
