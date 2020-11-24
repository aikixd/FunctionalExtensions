 

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Threading.Tasks;

namespace Aikixd.FunctionalExtensions
{
    public class ErrorResult
    {
        public string Message { get; }
        public IEnumerable<KeyValuePair<string, string>> Data { get; }

        public ErrorResult(string message, IEnumerable<KeyValuePair<string, string>> data)
        {
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
            this.Data = data ?? throw new ArgumentNullException(nameof(data));
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

    public class Ok : Record<Ok> { }

    public class Error<T> : Record<Error<T>>
    {
        public T Value { get; }

        public Error(T value)
        {
            this.Value = value ?? throw new NullReferenceException();
        }
    }

    public class Ok<T> : Record<Ok<T>>
    {
        public T Value { get; }

        public Ok(T value)
        {
            this.Value = value ?? throw new NullReferenceException();
        }
    }

    public class Result<TError> : Union<Ok, Error<TError>>
    {
        public Result(Ok value) : base(value) { }

        public Result(Error<TError> value) : base(value) { }

        public Result<TError> Bind(Func<Result<TError>> fn)
        {
            return
                this.Match(
                    ok => fn(),
                    error => this);
        }

        public Result<TOtherError> Bind<TOtherError>(
            Func<Result<TOtherError>> fn,
            Func<TError, TOtherError> convert)
        {
            return
                this.Match(
                    ok => fn(),
                    error => convert(error.Value));
        }

        public Task<Result<TError>> Bind(Func<Task<Result<TError>>> fn)
        {
            return
                this.Match(
                    ok => fn(),
                    error => Task.FromResult(this));
        }

        public Task<Result<TOtherError>> Bind<TOtherError>(
            Func<Task<Result<TOtherError>>> fn,
            Func<TError, Task<TOtherError>> convert)
        {
            return
                this.Match(
                    ok => fn(),
                    async error => (Result<TOtherError>) await convert(error.Value));
        }

        public Result<TOtherError> Select<TOtherError>(
            Func<TError, TOtherError> map)
        {

            return this.Match(
                ok => new Result<TOtherError>(ok),
                error => new Result<TOtherError>(new Error<TOtherError>(map(error.Value)))
            );
        }

        public static implicit operator Result<TError>(Ok ok)
            => new Result<TError>(ok);

        public static implicit operator Result<TError>(Error<TError> error)
            => new Result<TError>(error);

        public static implicit operator Result<TError>(TError error)
            => new Result<TError>(new Error<TError>(error));
    }

    public class Result<T, TError> : Union<Ok<T>, Error<TError>>
    {

        public Result(T value) : base(new Ok<T>(value)) { }

        public Result(Ok<T> value) : base(value) { }

        public Result(TError value) : base(new Error<TError>(value)) { }

        public Result(Error<TError> value) : base(value) { }

        public Result<U, TError> Bind<U>(Func<T, Result<U, TError>> fn)
        {
            return
                this.Match(
                    ok => fn(ok.Value),
                    error => error);
        }

        public Result<TError> Bind(Func<T, Result<TError>> fn)
        {
            return
                this.Match(
                    ok => fn(ok.Value),
                    error => error);
        }

        public Result<U, UError> Bind<U, UError>(Func<T, Result<U, UError>> fn, Func<TError, UError> convert)
        {
            return
                this.Match(
                    ok => fn(ok.Value),
                    error => convert(error.Value));
        }

        public Task<Result<U, TError>> Bind<U>(Func<T, Task<Result<U, TError>>> fn)
        {
            return
                this.Match(
                    ok => fn(ok.Value),
                    async error => (Result<U, TError>) await Task.FromResult(error));
        }

        public Task<Result<TError>> Bind(Func<T, Task<Result<TError>>> fn)
        {
            return
                
                this.Match(
                    ok =>  fn(ok.Value),
                    async error => (Result<TError>) await Task.FromResult(error));
        }

        public Task<Result<U, UError>> Bind<U, UError>(
            Func<T, Task<Result<U, UError>>> fn,
            Func<TError, Task<UError>> convert)
        {
            return
                this.Match(
                    ok => fn(ok.Value),
                    async error => (Result<U, UError>) await convert(error.Value));
        }

        public Result<TOtherResult, TOtherError> Select<TOtherResult, TOtherError>(
            Func<T, TOtherResult> mapOk,
            Func<TError, TOtherError> mapErr)
        {

            return this.Match(
                ok => new Result<TOtherResult, TOtherError>(new Ok<TOtherResult>(mapOk(ok.Value))),
                error => new Result<TOtherResult, TOtherError>(new Error<TOtherError>(mapErr(error.Value)))
            );
        }

        public Result<TOtherResult, TError> Select<TOtherResult>(
            Func<T, TOtherResult> map)
        {
            return this.Select(map, x => x);
        }

        public static implicit operator Result<T, TError>(Ok<T> ok)
            => new Result<T, TError>(ok);

        public static implicit operator Result<T, TError>(T ok)
            => new Result<T, TError>(new Ok<T>(ok));

        public static implicit operator Result<T, TError>(Error<TError> error)
            => new Result<T, TError>(error);

        public static implicit operator Result<T, TError>(TError error)
            => new Result<T, TError>(new Error<TError>(error));
    }
} 
