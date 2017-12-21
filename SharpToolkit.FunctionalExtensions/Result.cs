using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;

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

    public struct Result : IEquatable<Result>
    {
        public class ResultBacking : Union<Ok, Error>
        {
            public ResultBacking(Ok @case) : base(@case)
            {
            }

            public ResultBacking(Error @case) : base(@case)
            {
            }            
        }

        public class Ok : Case<ResultBacking>
        {
            public static implicit operator Result(Ok @case) =>
                new Result(@case);
        }
        public class Error : Case<ResultBacking, ErrorResult>
        {
            public Error(ErrorResult value) : base(value)
            {
            }

            public static implicit operator Result(Error @case) =>
                new Result(@case);
        }

        public Result(Ok @case)
        {
            this.union = @case;
        }

        public Result(Error @case)
        {
            this.union = @case;
        }

        private ResultBacking union;
        private ResultBacking unionSafe =>
            union ?? makeUnion();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ResultBacking makeUnion()
        {
            if (this.union == null)
                union = new Error(new ErrorResult("Union created implicitly."));

            return union;
        }

        public void When(Action<Ok> action)
        {
            this.unionSafe.When(action);
        }

        public void When(Action<Error> action)
        {
            this.unionSafe.When(action);
        }

        public TResult When<TResult>(Func<Ok, TResult> fn)
        {
            return this.unionSafe.When(fn);
        }

        public TResult When<TResult>(Func<Error, TResult> fn)
        {
            return this.unionSafe.When(fn);
        }

        public void Match(Action<Ok> okAction, Action<Error> errorAction)
        {
            this.unionSafe.Match(okAction, errorAction);
        }

        public TResult Match<TResult>(Func<Ok, TResult> okFn, Func<Error, TResult> errorFn)
        {
            return this.unionSafe.Match(okFn, errorFn);
        }

        public bool Equals(Result other)
        {
            return this.unionSafe.Equals(other.unionSafe);
        }
    }

    public struct Result<T> : IEquatable<Result<T>>
    {
        public class ResultBacking : Union<Ok, Error>
        {
            public ResultBacking(Ok @case) : base(@case)
            {
            }

            public ResultBacking(Error @case) : base(@case)
            {
            }
        }

        public class Ok : Case<ResultBacking, T>
        {
            public Ok(T value) : base(value)
            {
            }

            public static implicit operator Result<T>(Ok @case) =>
                new Result<T>(@case);
        }
        public class Error : Case<ResultBacking, ErrorResult>
        {
            public Error(ErrorResult value) : base(value)
            {
            }

            public static implicit operator Result<T>(Error @case) =>
                new Result<T>(@case);
        }

        public Result(Ok @case)
        {
            this.union = @case;
        }

        public Result(Error @case)
        {
            this.union = @case;
        }

        private ResultBacking union;
        private ResultBacking unionSafe =>
            union ?? makeUnion();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ResultBacking makeUnion()
        {
            if (this.union == null)
                union = new Error(new ErrorResult("Result union's case can't be created implicitly."));

            return union;
        }

        public void When(Action<Ok> action)
        {
            this.unionSafe.When(action);
        }

        public void When(Action<Error> action)
        {
            this.unionSafe.When(action);
        }

        public TResult When<TResult>(Func<Ok, TResult> fn)
        {
            return this.unionSafe.When(fn);
        }

        public TResult When<TResult>(Func<Error, TResult> fn)
        {
            return this.unionSafe.When(fn);
        }

        public void Match(Action<Ok> okAction, Action<Error> errorAction)
        {
            this.unionSafe.Match(okAction, errorAction);
        }

        public TResult Match<TResult>(Func<Ok, TResult> okFn, Func<Error, TResult> errorFn)
        {
            return this.unionSafe.Match(okFn, errorFn);
        }

        public bool Equals(Result<T> other)
        {
            return this.unionSafe.Equals(other.unionSafe);
        }
    }
}
