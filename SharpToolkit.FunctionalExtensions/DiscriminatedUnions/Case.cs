using SharpToolkit.FunctionalExtensions.DiscriminatedUnions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpToolkit.FunctionalExtensions
{
    public abstract class Case { }

    public abstract class Case<TUnion> : Case, IEquatable<Case<TUnion>>
    {
        internal readonly TypeUtils<TUnion, Case<TUnion>> TypeUtils;

        private Type type;

        public Case()
        {
            TypeUtils = TypeUtils<TUnion, Case<TUnion>>.GetUtils(this.GetType());

            this.type = this.GetType();
        }

        public override bool Equals(object obj)
        {
            if (obj is Case<TUnion> o)
                return this.type == o.type;

            return false;
        }

        public bool Equals(Case<TUnion> other)
        {
            // If case has no value, all cases of same type are equal.
            return this.type == other.type;
        }

        public override int GetHashCode()
        {
            return this.type.GetHashCode();
        }

        public TUnion ToUnion =>
            this.TypeUtils.CastFn(this);

        public static implicit operator TUnion(Case<TUnion> @case)
        {
            return @case.TypeUtils.CastFn(@case);
        }

        public static bool operator == (Case<TUnion> left, Case<TUnion> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Case<TUnion> left, Case<TUnion> right)
        {
            return !left.Equals(right);
        }
    }

    public abstract class Case<TUnion, TVal> : Case<TUnion>, IEquatable<Case<TUnion, TVal>>
    {
        public TVal Value { get; }

        public Case(TVal value)
        {
            this.Value = value;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Case<TUnion, TVal> o)
                return this.Equals(o);

            return false;
        }

        public bool Equals(Case<TUnion, TVal> other)
        {
            if (base.Equals(other))
                return this.EqualsImpl(other);

            return false;
        }

        private bool EqualsImpl(Case<TUnion, TVal> other)
        {
            if (this.Value is IEquatable<TVal> v)
                return v.Equals((IEquatable<TVal>)other.Value);

            return this.Value.Equals(other.Value);
        }

        public static bool operator ==(Case<TUnion, TVal> left, Case<TUnion, TVal> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Case<TUnion, TVal> left, Case<TUnion, TVal> right)
        {
            return left.Equals(right);
        }
    }
}
