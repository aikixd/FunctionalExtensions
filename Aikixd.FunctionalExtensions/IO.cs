using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Diagnostics;


namespace Aikixd.FunctionalExtensions
{
//    class IoBuilder<T>
//    {
//        private IAsyncStateMachine? stateMachine;
//#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
//        private T result;
//#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

//        public static IoBuilder<T> Create()
//        {
//            return new IoBuilder<T>();
//        }

//        public void Start<TStateMachine>(ref TStateMachine stateMachine)
//            where TStateMachine : IAsyncStateMachine
//        {
//            stateMachine.MoveNext();
//        }

//        public void SetStateMachine(IAsyncStateMachine stateMachine)
//        {
//            this.stateMachine = stateMachine;
//        }

//        public void SetException(Exception exception)
//        {
//            Debugger.Break();
//        }

//        public void SetResult(T result)
//        {
//            this.result = result;
//        }

//        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
//            ref TAwaiter awaiter, ref TStateMachine stateMachine)
//            where TAwaiter : INotifyCompletion
//            where TStateMachine : IAsyncStateMachine
//        {
//            awaiter.OnCompleted(() => this.stateMachine?.MoveNext());
//        }
//        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
//            ref TAwaiter awaiter, ref TStateMachine stateMachine)
//            where TAwaiter : ICriticalNotifyCompletion
//            where TStateMachine : IAsyncStateMachine
//        {
//            awaiter.UnsafeOnCompleted(() => this.stateMachine?.MoveNext());
//        }

//       // public IO<T> Task { get; }
//    }

    public class PureBuilder<T>
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private IAsyncStateMachine? stateMachine;
        private Pure<T> result;
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public static PureBuilder<T> Create()
        {
            return new PureBuilder<T>();
        }

        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            this.stateMachine = stateMachine;

            stateMachine.MoveNext();
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void SetException(Exception exception)
        {
            Debugger.Break();
        }

        public void SetResult(T result)
        {
            this.result = new Pure<T>(new Ok<T>(result));
        }

        private bool HandleIOAwaiter(object awaiter)
        {
            if (awaiter is IoAwaiter<T> x
                && x.Result.Is<Error<string>>())
            {
                this.result = new Pure<T>(x.Result);
                return true;
            }

            return false;
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, 
            ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (HandleIOAwaiter(awaiter))
                return;

            awaiter.OnCompleted(() => this.stateMachine?.MoveNext());
        }
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, 
            ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (HandleIOAwaiter(awaiter))
                return;

            awaiter.UnsafeOnCompleted(() => this.stateMachine?.MoveNext());
        }

        public Pure<T> Task => this.result;
    }

    public class IoAwaiter<T> : INotifyCompletion
    {
        public Result<T, string> Result { get; }

        public IoAwaiter(Result<T, string> result)
        {
            this.Result = result;

            this.Result.When((Ok<T> _) => this.IsCompleted = true);
        }

        public void OnCompleted(Action continuation)
        {
            if (this.IsCompleted)
            {
                continuation();
            }
        }

        public bool IsCompleted { get; set; }

        public T GetResult()
        {
            return this.Result.When((Ok<T> x) => x.Value, () => throw new Exception(""));
        }
    }

    //[AsyncMethodBuilder(typeof(IoBuilder<>))]
    public class IO<T>
    {
        public Result<T, string> Result { get; }

        public IO(Result<T, string> result)
        {
            this.Result = result;
        }

        public IoAwaiter<T> GetAwaiter()
        {
            return new IoAwaiter<T>(this.Result);
        }

        public static implicit operator IO<T>(Ok<T> other)
            => new IO<T>(other);

        public static implicit operator IO<T>(Error<string> other)
            => new IO<T>(other);

        public static implicit operator IO<T>(Result<T, string> other)
            => new IO<T>(other);
    }

    [AsyncMethodBuilder(typeof(PureBuilder<>))]
    public class Pure<T>
    {
        public Result<T, string> Result { get; }
        
        public Pure(Result<T, string> result)
        {
            this.Result = result;
        }

        public PureAwaiter<T> GetAwaiter()
        {
            return new PureAwaiter<T>(this);
        }
    }

    public class PureAwaiter<T> : INotifyCompletion
    {
        private Action? continuation;
        private Pure<T> Pure { get; }

        public PureAwaiter(Pure<T> pure)
        {
            this.Pure = pure;
            this.IsCompleted = true;
        }

        public void OnCompleted(Action continuation)
        {
            this.continuation = continuation;
            if (this.IsCompleted)
            {
                continuation();
            }
        }

        public bool IsCompleted { get; set; }

        public Result<T, string> GetResult()
        {
            return this.Pure.Result;
        }
    }

}
