using Aikixd.FunctionalExtensions.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

#nullable disable

namespace Aikixd.FunctionalExtensions.DiscriminatedUnions
{
    internal class TypeUtils<TUnion, TCase>
    {
        internal static TypeUtils<TUnion, TCase> Instance { get; }

        static TypeUtils()
        {
            try
            {
                var castFn = IL.GenerateCaseCast<TUnion, TCase>();

                Instance = new TypeUtils<TUnion, TCase>(castFn);
            }
            catch (Exception e)
            {
                Debugger.Break();
            }

            Debug.Assert(Instance != null);
        }


        internal TypeUtils(
            Func<TCase, TUnion> castFn)
        {
            this.CastFn = castFn;
        }

        public Func<TCase, TUnion> CastFn { get; }
    }
}

#nullable restore