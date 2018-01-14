 

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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


    /**********************************/
    /********** Result union **********/
    /**********************************/
    public struct Result : IEquatable<Result>
    {
        #region Struct backing union

        private ResultBacking union;
        private ResultBacking unionSafe =>
            union ?? MakeUnion();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ResultBacking MakeUnion()
        {
            if (this.union == null)
                union = new ErrorBacking(new Error(new ErrorResult("Result created implicitly.")));

            return union;
        }

        #endregion
        
        /*****************************************/
        /********** Union backing class **********/
        /*****************************************/
        internal class ResultBacking : Union<OkBacking, ErrorBacking>
        {
            public ResultBacking(OkBacking @case) : base(@case)
            {
            }

            public ResultBacking(ErrorBacking @case) : base(@case)
            {
            } 
        }
                    
        /*****************************/
        /********** Ok case **********/
        /*****************************/

        /********** Union wrapper constructor **********/
        public Result(Ok @case)
        {
            this.union = new OkBacking(@case);
        }        
        
        /********** Case wrapper **********/
        public class Ok : IEquatable<Ok>
        {

            public Ok()
            {
                
            }
            
            /*********** Equals(object) ***********/
            public override bool Equals(object obj)
            {
                return obj is Ok;
            }

            /*********** Equals(T) ***********/
            public bool Equals(Ok other)
            {
                // Type has no value
                return true;
            }

            public override int GetHashCode()
            {
                // Since the type has no value each instance is the same.
                return 1;
            }

            public Result ToUnion =>            
                new Result(this);
            
            
            public static implicit operator Result(Ok @case) =>
                new Result(@case);

            public static bool operator == (Ok left, Ok right) =>
                left.Equals(right);            

            public static bool operator != (Ok left, Ok right) =>
                !left.Equals(right);
        }

        /*********** Real case **********/
        internal class OkBacking : Case<ResultBacking, Ok>
        {
            public OkBacking(Ok value) : base(value)
            {
                
            }
        }

        /********** Union case methods **********/

        public void When(Action<Ok> action)
        {
            this.unionSafe.When(x => action(x.Value));
        }

        public TResult When<TResult>(Func<Ok, TResult> fn)
        {
            return this.unionSafe.When(x => fn(x.Value));
        }
            
        /********************************/
        /********** Error case **********/
        /********************************/

        /********** Union wrapper constructor **********/
        public Result(Error @case)
        {
            this.union = new ErrorBacking(@case);
        }        
        
        /********** Case wrapper **********/
        public class Error : IEquatable<Error>
        {
            public ErrorResult Value { get; }

            public Error(ErrorResult value)
            {
                this.Value = value;
            }

            
            /*********** Equals(object) ***********/
            public override bool Equals(object obj)
            {
                if (obj is Error o)
                    return this.Equals(o);

                return false;
            }

            /*********** Equals(T) ***********/
            public bool Equals(Error other)
            {
                return this.Value.Equals(other.Value);
            }

            public override int GetHashCode()
            {
            
                return this.Value.GetHashCode();
            }

            public Result ToUnion =>            
                new Result(this);
            
            
            public static implicit operator Result(Error @case) =>
                new Result(@case);

            public static bool operator == (Error left, Error right) =>
                left.Equals(right);            

            public static bool operator != (Error left, Error right) =>
                !left.Equals(right);
        }

        /*********** Real case **********/
        internal class ErrorBacking : Case<ResultBacking, Error>
        {
            public ErrorBacking(Error value) : base(value)
            {
                
            }
        }

        /********** Union case methods **********/

        public void When(Action<Error> action)
        {
            this.unionSafe.When(x => action(x.Value));
        }

        public TResult When<TResult>(Func<Error, TResult> fn)
        {
            return this.unionSafe.When(x => fn(x.Value));
        }

        /********** Match methods **********/
        public void Match(Action<Ok> a0, Action<Error> a1)
        {
            this.unionSafe.Match(
                x => a0(x.Value),
                x => a1(x.Value)
            );
        }

        public TResult Match<TResult>(Func<Ok, TResult> fn0, Func<Error, TResult> fn1)
        {
            return this.unionSafe.Match(
                x => fn0(x.Value),
                x => fn1(x.Value)
            );
        }

        /********** Union case IEquitable **********/

        public bool Equals(Result other)
        {
            return this.unionSafe == other.unionSafe;
        }
    }

    /*************************************/
    /********** Result<T> union **********/
    /*************************************/
    public struct Result<T> : IEquatable<Result<T>>
    {
        #region Struct backing union

        private ResultBacking union;
        private ResultBacking unionSafe =>
            union ?? MakeUnion();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ResultBacking MakeUnion()
        {
            if (this.union == null)
                union = new ErrorBacking(new Error(new ErrorResult("Result created implicitly.")));

            return union;
        }

        #endregion
        
        /*****************************************/
        /********** Union backing class **********/
        /*****************************************/
        internal class ResultBacking : Union<OkBacking, ErrorBacking>
        {
            public ResultBacking(OkBacking @case) : base(@case)
            {
            }

            public ResultBacking(ErrorBacking @case) : base(@case)
            {
            } 
        }
                    
        /*****************************/
        /********** Ok case **********/
        /*****************************/

        /********** Union wrapper constructor **********/
        public Result(Ok @case)
        {
            this.union = new OkBacking(@case);
        }        
        
        /********** Case wrapper **********/
        public class Ok : IEquatable<Ok>
        {
            public T Value { get; }

            public Ok(T value)
            {
                this.Value = value;
            }

            
            /*********** Equals(object) ***********/
            public override bool Equals(object obj)
            {
                if (obj is Ok o)
                    return this.Equals(o);

                return false;
            }

            /*********** Equals(T) ***********/
            public bool Equals(Ok other)
            {
                return this.Value.Equals(other.Value);
            }

            public override int GetHashCode()
            {
            
                return this.Value.GetHashCode();
            }

            public Result<T> ToUnion =>            
                new Result<T>(this);
            
            
            public static implicit operator Result<T>(Ok @case) =>
                new Result<T>(@case);

            public static bool operator == (Ok left, Ok right) =>
                left.Equals(right);            

            public static bool operator != (Ok left, Ok right) =>
                !left.Equals(right);
        }

        /*********** Real case **********/
        internal class OkBacking : Case<ResultBacking, Ok>
        {
            public OkBacking(Ok value) : base(value)
            {
                
            }
        }

        /********** Union case methods **********/

        public void When(Action<Ok> action)
        {
            this.unionSafe.When(x => action(x.Value));
        }

        public TResult When<TResult>(Func<Ok, TResult> fn)
        {
            return this.unionSafe.When(x => fn(x.Value));
        }
            
        /********************************/
        /********** Error case **********/
        /********************************/

        /********** Union wrapper constructor **********/
        public Result(Error @case)
        {
            this.union = new ErrorBacking(@case);
        }        
        
        /********** Case wrapper **********/
        public class Error : IEquatable<Error>
        {
            public ErrorResult Value { get; }

            public Error(ErrorResult value)
            {
                this.Value = value;
            }

            
            /*********** Equals(object) ***********/
            public override bool Equals(object obj)
            {
                if (obj is Error o)
                    return this.Equals(o);

                return false;
            }

            /*********** Equals(T) ***********/
            public bool Equals(Error other)
            {
                return this.Value.Equals(other.Value);
            }

            public override int GetHashCode()
            {
            
                return this.Value.GetHashCode();
            }

            public Result<T> ToUnion =>            
                new Result<T>(this);
            
            
            public static implicit operator Result<T>(Error @case) =>
                new Result<T>(@case);

            public static bool operator == (Error left, Error right) =>
                left.Equals(right);            

            public static bool operator != (Error left, Error right) =>
                !left.Equals(right);
        }

        /*********** Real case **********/
        internal class ErrorBacking : Case<ResultBacking, Error>
        {
            public ErrorBacking(Error value) : base(value)
            {
                
            }
        }

        /********** Union case methods **********/

        public void When(Action<Error> action)
        {
            this.unionSafe.When(x => action(x.Value));
        }

        public TResult When<TResult>(Func<Error, TResult> fn)
        {
            return this.unionSafe.When(x => fn(x.Value));
        }

        /********** Match methods **********/
        public void Match(Action<Ok> a0, Action<Error> a1)
        {
            this.unionSafe.Match(
                x => a0(x.Value),
                x => a1(x.Value)
            );
        }

        public TResult Match<TResult>(Func<Ok, TResult> fn0, Func<Error, TResult> fn1)
        {
            return this.unionSafe.Match(
                x => fn0(x.Value),
                x => fn1(x.Value)
            );
        }

        /********** Union case IEquitable **********/

        public bool Equals(Result<T> other)
        {
            return this.unionSafe == other.unionSafe;
        }
    }


} 
