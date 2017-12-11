using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SharpToolkit.FunctionalExtensions
{
    public class ErrorResult
    {
        public string Message { get; }
        public IEnumerable<KeyValuePair<string, string>> Data { get; }

        public ErrorResult(string message, IEnumerable<KeyValuePair<string, string>> data)
        {
            this.Message = message;
            this.Data = data;
        }

        public ErrorResult(string message, IEnumerable<(string key, string value)> data)
            : this(message, data.Select(x => new KeyValuePair<string, string>(x.key, x.value)))
        {

        }

        public ErrorResult(string message, params (string key, string value)[] data)
            : this(message, data.Select(x => new KeyValuePair<string, string>(x.key, x.value)))
        {

        }
    }

    public class Result : Union<Result.Ok, Result.Error>
    {
        public Result(Ok @case) : base(@case)
        {
        }

        public Result(Error @case) : base(@case)
        {
        }

        public class Ok : Case<Result> { }
        public class Error : Case<Result, ErrorResult>
        {
            public Error(ErrorResult value) : base(value)
            {
            }
        }
    }

    public class Result<T> : Union<Result<T>.Ok, Result<T>.Error>
    {
        public Result(Ok @case) : base(@case)
        {
        }

        public Result(Error @case) : base(@case)
        {
        }

        public class Ok : Case<Result<T>, T>
        {
            public Ok(T value) : base(value)
            {
            }
        }

        public class Error : Case<Result<T>, ErrorResult>
        {
            public Error(ErrorResult value) : base(value)
            {
            }
        }
    }
}
