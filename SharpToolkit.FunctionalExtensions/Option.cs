using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SharpToolkit.FunctionalExtensions
{
    public struct Option<T> : IEquatable<Option<T>>
    {
        public class OptionBacking : Union<Some, None>
        {
            public OptionBacking(Some @case) : base(@case)
            {
            }

            public OptionBacking(None @case) : base(@case)
            {
            }
        }

        public class Some : Case<OptionBacking, T>
        {
            public Some(T value) : base(value)
            {
            }

            public static implicit operator Option<T>(Some @case) =>
                new Option<T>(@case);
        }

        public class None : Case<OptionBacking>
        {
            public static implicit operator Option<T>(None @case) =>
                new Option<T>(@case);
        }

        private OptionBacking union;

        private OptionBacking unionSafe =>
            union ?? makeUnion();

        public Option(Some @case)
        {
            this.union = @case;
        }

        public Option(None @case)
        {
            this.union = @case;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private OptionBacking makeUnion()
        {
            if (this.union == null)
                this.union = new None();

            return this.union;
        }

        public void When(Action<Some> action)
        {
            this.unionSafe.When(action);
        }

        public void When(Action<None> action)
        {
            this.unionSafe.When(action);
        }

        public TResult When<TResult>(Func<Some, TResult> fn)
        {
            return this.unionSafe.When(fn);
        }

        public TResult When<TResult>(Func<None, TResult> fn)
        {
            return this.unionSafe.When(fn);
        }

        public void Match(Action<Some> someAction, Action<None> noneAction)
        {
            this.unionSafe.Match(someAction, noneAction);
        }

        public TResult Match<TResult>(Func<Some, TResult> someFn, Func<None, TResult> noneFn)
        {
            return this.unionSafe.Match(someFn, noneFn);
        }

        public bool Equals(Option<T> other)
        {
            return this.unionSafe.Equals(other.unionSafe);
        }

        public static bool operator ==(Option<T> left, Option<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Option<T> left, Option<T> right)
        {
            return !left.Equals(right);
        }
    }
}
