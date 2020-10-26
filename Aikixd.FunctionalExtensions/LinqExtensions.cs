using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aikixd.FunctionalExtensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<IEnumerable<T>>
            GetPermutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static (IEnumerable<T> trues, IEnumerable<T> falses)
            Partition<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            return (collection.Where(predicate), collection.Where(x => predicate(x) == false));
        }
    }

    public static class ObjectExtensions
    {
        public static U To<T, U>(this T obj, Func<T, U> fn)
        {
            return fn(obj);
        }

        public static void To<T>(this T obj, Action<T> action)
        {
            action(obj);
        }

        public static IEnumerable<T> Yield<T>(this T obj)
        {
            yield return obj;
        }
    }
}
