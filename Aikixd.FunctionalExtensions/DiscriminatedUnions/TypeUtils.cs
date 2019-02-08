using Aikixd.FunctionalExtensions.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Aikixd.FunctionalExtensions.DiscriminatedUnions
{
    internal class TypeUtils<TUnion, TCase>
        where TCase : Case<TUnion>
    {
        private static ReaderWriterLockSlim typeUtilsLock;
        private static Dictionary<Type, TypeUtils<TUnion, TCase>> typeUtilsCache;

        static TypeUtils()
        {
            typeUtilsLock = new ReaderWriterLockSlim();
            typeUtilsCache = new Dictionary<Type, TypeUtils<TUnion, TCase>>();
        }

        public static TypeUtils<TUnion, TCase> GetUtils(Type caseType)
        {
            typeUtilsLock.EnterReadLock();

            typeUtilsCache.TryGetValue(caseType, out var utils);

            typeUtilsLock.ExitReadLock();

            if (utils != null)
                return utils;

            typeUtilsLock.EnterWriteLock();

            utils = new TypeUtils<TUnion, TCase>(
                IL.GenerateCaseCast<TUnion, TCase>(caseType)
            );
            typeUtilsCache.Add(caseType, utils);

            typeUtilsLock.ExitWriteLock();

            return utils;
        }

        internal TypeUtils(
            Func<TCase, TUnion> castFn)
        {
            this.CastFn = castFn;
        }

        public Func<TCase, TUnion> CastFn { get; }
    }
}
