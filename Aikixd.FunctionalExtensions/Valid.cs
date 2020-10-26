using System;
using System.Collections.Generic;
using System.Text;

namespace Aikixd.FunctionalExtensions
{
    
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
