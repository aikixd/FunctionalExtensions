using System;
using System.Collections.Generic;
using System.Text;

namespace SharpToolkit.FunctionalExtensions
{
    public class Option<T> : Union<Option<T>.Some, Option<T>.None>
    {
        public Option(Some @case) : base(@case)
        {
        }

        public Option(None @case) : base(@case)
        {
        }

        public class Some : Case<Option<T>, T>
        {
            public Some(T value) : base(value)
            {
            }
        }

        public class None : Case<Option<T>> { }
    }
}
