using Aikixd.FunctionalExtensions.DiscriminatedUnions;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Aikixd.FunctionalExtensions
{
    public class Option<T> : Union<Option<T>.Some, Option<T>.None>
    {
        public Option(Option<T>.Some value) : base(value) { }

        public Option(Option<T>.None value) : base(value) { }

        public class Some : Record<Some>
        {
            public T Value { get; }

            public Some(T value)
            {
                this.Value = value ?? throw new NullReferenceException(nameof(value));
            }

            public static implicit operator Option<T>(Some some)
            {
                return new Option<T>(some);
            }
        }

        public class None : Record<None>
        {
            public static implicit operator Option<T>(None none)
            {
                return new Option<T>(none);
            }
        }

    }
}
