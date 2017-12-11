using SharpToolkit.FunctionalExtensions.DiscriminatedUnions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpToolkit.FunctionalExtensions
{
    public abstract class Case { }

    public abstract class Case<TUnion> : Case
    {
        internal readonly TypeUtils<TUnion, Case<TUnion>> TypeUtils;

        public Case()
        {
            TypeUtils = TypeUtils<TUnion, Case<TUnion>>.GetUtils(this.GetType());
        }

        public static implicit operator TUnion(Case<TUnion> @case)
        {
            return @case.TypeUtils.CastFn(@case);
        }
    }

    public abstract class Case<TUnion, TVal> : Case<TUnion>
    {
        public TVal Value { get; }

        public Case(TVal value)
        {
            this.Value = value;
        }
    }
}
