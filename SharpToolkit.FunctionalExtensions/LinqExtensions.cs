using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpToolkit.FunctionalExtensions.DiscriminatedUnions
{
    public static class LinqExtensions
    {
        static IEnumerable<IEnumerable<T>>
            GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
