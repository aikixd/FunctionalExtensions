using System;
using System.Collections.Generic;
using System.Text;

namespace SharpToolkit.FunctionalExtensions
{
    public static class FunctionalExtensions
    {
        public static Func<int> AsFunc(this Action action)
        {
            return new Func<int>(() => { action(); return 0; });
        }

        public static Func<T, int> AsFunc<T>(this Action<T> action)
        {
            return new Func<T, int>(x => { action(x); return 0; });
        }

        public static Func<T1, T2, int> AsFunc<T1, T2>(this Action<T1, T2> action)
        {
            return new Func<T1, T2, int>((a, b) => { action(a, b); return 0; });
        }

        public static Func<T1, T2, T3, int> AsFunc<T1, T2, T3>(this Action<T1, T2, T3> action)
        {
            return new Func<T1, T2, T3, int>((a, b, c) => { action(a, b, c); return 0; });
        }

        public static Func<T1, T2, T3, T4, int> AsFunc<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action)
        {
            return new Func<T1, T2, T3, T4, int>((a, b, c, d) => { action(a, b, c, d); return 0; });
        }

        public static Func<T1, T2, T3, T4, T5, int> AsFunc<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action)
        {
            return new Func<T1, T2, T3, T4, T5, int>((a, b, c, d, e) => { action(a, b, c, d, e); return 0; });
        }

        public static Func<T1, T2, T3, T4, T5, T6, int> AsFunc<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> action)
        {
            return new Func<T1, T2, T3, T4, T5, T6, int>((a, b, c, d, e, f) => { action(a, b, c, d, e, f); return 0; });
        }

        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }
    }
}
