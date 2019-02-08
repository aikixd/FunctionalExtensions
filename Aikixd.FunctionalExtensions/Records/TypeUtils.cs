using Aikixd.FunctionalExtensions.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {

            var recordProxy = IL.GenerateRecordProxy<T>();
            var fieldCompare = IL.GenerateFieldsCompare<T>();
            var getHashCode = IL.GenerateGetHashCode<T>();
            var toString = IL.GenerateToString<T>();
            var recordCopy = IL.GenerateRecordCopy<T>();
            var recordFieldSetMap = IL.GenerateRecordFieldsSetMap<T>();

            Instance = new TypeUtils<T>(
                recordProxy,
                fieldCompare,
                getHashCode,
                toString,
                recordCopy,
                recordFieldSetMap);
            }

            catch (Exception e)
            {
                Debugger.Break();
            }

            Debug.Assert(Instance != null);
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
