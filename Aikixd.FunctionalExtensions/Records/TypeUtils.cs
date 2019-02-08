using Aikixd.FunctionalExtensions.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Aikixd.FunctionalExtensions.Records
{
    internal class TypeUtils<T>
        where T : class
    {
        internal static TypeUtils<T> Instance { get; }

        static TypeUtils()
        {
            Instance = new TypeUtils<T>(
                IL.GenerateRecordProxy<T>(),
                IL.GenerateFieldsCompare<T>(),
                IL.GenerateGetHashCode<T>(),
                IL.GenerateToString<T>(),
                IL.GenerateRecordCopy<T>(),
                IL.GenerateRecordFieldsSetMap<T>()
                );
        }       

        private TypeUtils(
            TypeInfo proxy,
            Func<T, T, bool> equalsFn,
            Func<T, int> hashCodeFn,
            Func<T, int, string> tosStringFn,
            Func<T, T> copyFn,
            IReadOnlyDictionary<MemberInfo, Action<T, object>> setMemberFnMap
            )
        {
            this.EqualsFn = equalsFn;
            this.GetHashCodeFn = hashCodeFn;
            this.ToStringFn = tosStringFn;
            this.CopyFn = copyFn;
            this.SetMemberFnMap = setMemberFnMap;
        }

        public Func<T, T, bool> EqualsFn { get; }
        public Func<T, int> GetHashCodeFn { get; }
        public Func<T, int, string> ToStringFn { get; }
        public Func<T, T> CopyFn { get; }
        public IReadOnlyDictionary<MemberInfo, Action<T, object>> SetMemberFnMap { get; }
    }
}
