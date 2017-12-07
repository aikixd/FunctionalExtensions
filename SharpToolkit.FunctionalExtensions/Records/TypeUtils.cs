using SharpToolkit.FunctionalExtensions.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpToolkit.FunctionalExtensions.Records
{
    internal class TypeUtils<T>
    {
        internal static TypeUtils<T> Instance { get; }

        static TypeUtils()
        {
            Instance = new TypeUtils<T>(
                IL.GenerateFieldsCompare<T>(),
                IL.GenerateGetHashCode<T>(),
                IL.GenerateToString<T>()
                );
        }       

        private TypeUtils(
            Func<T, T, bool> equalsFn,
            Func<T, int> hashCodeFn,
            Func<T, int, string> tosStringFn
            )
        {
            this.EqualsFn = equalsFn;
            this.GetHashCodeFn = hashCodeFn;
            this.ToStringFn = tosStringFn;
        }

        public Func<T, T, bool> EqualsFn { get; }
        public Func<T, int> GetHashCodeFn { get; }
        public Func<T, int, string> ToStringFn { get; }
    }
}
