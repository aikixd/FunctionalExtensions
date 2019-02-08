using System;
using System.Collections.Generic;
using System.Text;

namespace Aikixd.FunctionalExtensions
{
    public class Valid<T> : Case<Validated<T>, T>
        where T : IRecord
    {
        internal Valid(T value) : base(value)
        {
        }
    }

    public class Validated<T> : Union<Valid<T>, Validated<T>.Invalid>
        where T : IRecord
    {
        

        public Validated(Invalid @case) : base(@case)
        {
        }

        public Validated(Valid<T> @case) : base(@case)
        {
        }

        public class Invalid : Case<Validated<T>, RecordValidationError<T>>
        {
            public Invalid(RecordValidationError<T> value) : base(value)
            {
            }
        }
    }

    public class RecordValidationError<T> : Record<RecordValidationError<T>>
    {
        public T Record { get; }
        public ErrorResult Error { get; }

        public RecordValidationError(T record, ErrorResult error)
        {
            this.Record = record;
            this.Error = error;
        }
    }
}
