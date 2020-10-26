using System;
using Aikixd.FunctionalExtensions.DiscriminatedUnions;

namespace Aikixd.FunctionalExtensions
{

  
    public abstract class Union<T1> : IEquatable<Union<T1>>
    {
        
        private readonly object value;
        
        public Union(T1 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        
        public bool Equals(Union<T1> other)
        {
            if (other is null)
                return false;
            return this.value.Equals(other.value);
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other is Union<T1> o)
                return this.Equals(o);

            return false;
        }

        public static bool operator == (Union<T1> left, Union<T1> right)
        {
            return left.Equals(right);
        }

        public static bool operator != (Union<T1> left, Union<T1> right)
        {
            return !left.Equals(right);
        }

        public bool When(Action<T1> action)
        {
            if (this.value is T1 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T1> action, Action fallback)
        {
            if (this.value is T1 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T1, TResult> fn, TResult @default)
        {
            if (this.value is T1 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T1, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T1 x)
                return fn(x);

            return fallback();
        }

        public TResult Match<TResult>(Func<T1, TResult> fn1)
        {  
            switch (this.value)
            {
		        case T1 x: return fn1(x);
         
            }
            
            throw new InvalidUnionStateException();
        }

        public void Match<TResult>(Action<T1> action1)
        {  
            switch (this.value)
            {
		        case T1 x: action1(x); break;
         
            }
            
            throw new InvalidUnionStateException();
        }

        
        public static implicit operator Union<T1>(T1 obj)
        {
            return TypeUtils<Union<T1>, T1>.Instance.CastFn(obj);
        }
         
    }
  
    public abstract class Union<T1, T2> : IEquatable<Union<T1, T2>>
    {
        
        private readonly object value;
        
        public Union(T1 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T2 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        
        public bool Equals(Union<T1, T2> other)
        {
            if (other is null)
                return false;
            return this.value.Equals(other.value);
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other is Union<T1, T2> o)
                return this.Equals(o);

            return false;
        }

        public static bool operator == (Union<T1, T2> left, Union<T1, T2> right)
        {
            return left.Equals(right);
        }

        public static bool operator != (Union<T1, T2> left, Union<T1, T2> right)
        {
            return !left.Equals(right);
        }

        public bool When(Action<T1> action)
        {
            if (this.value is T1 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T1> action, Action fallback)
        {
            if (this.value is T1 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T1, TResult> fn, TResult @default)
        {
            if (this.value is T1 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T1, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T1 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T2> action)
        {
            if (this.value is T2 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T2> action, Action fallback)
        {
            if (this.value is T2 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T2, TResult> fn, TResult @default)
        {
            if (this.value is T2 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T2, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T2 x)
                return fn(x);

            return fallback();
        }

        public TResult Match<TResult>(Func<T1, TResult> fn1, Func<T2, TResult> fn2)
        {  
            switch (this.value)
            {
		        case T1 x: return fn1(x);
                case T2 x: return fn2(x);
         
            }
            
            throw new InvalidUnionStateException();
        }

        public void Match<TResult>(Action<T1> action1, Action<T2> action2)
        {  
            switch (this.value)
            {
		        case T1 x: action1(x); break;
                case T2 x: action2(x); break;
         
            }
            
            throw new InvalidUnionStateException();
        }

        
        public static implicit operator Union<T1, T2>(T1 obj)
        {
            return TypeUtils<Union<T1, T2>, T1>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2>(T2 obj)
        {
            return TypeUtils<Union<T1, T2>, T2>.Instance.CastFn(obj);
        }
         
    }
  
    public abstract class Union<T1, T2, T3> : IEquatable<Union<T1, T2, T3>>
    {
        
        private readonly object value;
        
        public Union(T1 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T2 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T3 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        
        public bool Equals(Union<T1, T2, T3> other)
        {
            if (other is null)
                return false;
            return this.value.Equals(other.value);
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other is Union<T1, T2, T3> o)
                return this.Equals(o);

            return false;
        }

        public static bool operator == (Union<T1, T2, T3> left, Union<T1, T2, T3> right)
        {
            return left.Equals(right);
        }

        public static bool operator != (Union<T1, T2, T3> left, Union<T1, T2, T3> right)
        {
            return !left.Equals(right);
        }

        public bool When(Action<T1> action)
        {
            if (this.value is T1 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T1> action, Action fallback)
        {
            if (this.value is T1 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T1, TResult> fn, TResult @default)
        {
            if (this.value is T1 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T1, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T1 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T2> action)
        {
            if (this.value is T2 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T2> action, Action fallback)
        {
            if (this.value is T2 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T2, TResult> fn, TResult @default)
        {
            if (this.value is T2 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T2, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T2 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T3> action)
        {
            if (this.value is T3 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T3> action, Action fallback)
        {
            if (this.value is T3 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T3, TResult> fn, TResult @default)
        {
            if (this.value is T3 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T3, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T3 x)
                return fn(x);

            return fallback();
        }

        public TResult Match<TResult>(Func<T1, TResult> fn1, Func<T2, TResult> fn2, Func<T3, TResult> fn3)
        {  
            switch (this.value)
            {
		        case T1 x: return fn1(x);
                case T2 x: return fn2(x);
                case T3 x: return fn3(x);
         
            }
            
            throw new InvalidUnionStateException();
        }

        public void Match<TResult>(Action<T1> action1, Action<T2> action2, Action<T3> action3)
        {  
            switch (this.value)
            {
		        case T1 x: action1(x); break;
                case T2 x: action2(x); break;
                case T3 x: action3(x); break;
         
            }
            
            throw new InvalidUnionStateException();
        }

        
        public static implicit operator Union<T1, T2, T3>(T1 obj)
        {
            return TypeUtils<Union<T1, T2, T3>, T1>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3>(T2 obj)
        {
            return TypeUtils<Union<T1, T2, T3>, T2>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3>(T3 obj)
        {
            return TypeUtils<Union<T1, T2, T3>, T3>.Instance.CastFn(obj);
        }
         
    }
  
    public abstract class Union<T1, T2, T3, T4> : IEquatable<Union<T1, T2, T3, T4>>
    {
        
        private readonly object value;
        
        public Union(T1 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T2 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T3 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T4 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        
        public bool Equals(Union<T1, T2, T3, T4> other)
        {
            if (other is null)
                return false;
            return this.value.Equals(other.value);
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other is Union<T1, T2, T3, T4> o)
                return this.Equals(o);

            return false;
        }

        public static bool operator == (Union<T1, T2, T3, T4> left, Union<T1, T2, T3, T4> right)
        {
            return left.Equals(right);
        }

        public static bool operator != (Union<T1, T2, T3, T4> left, Union<T1, T2, T3, T4> right)
        {
            return !left.Equals(right);
        }

        public bool When(Action<T1> action)
        {
            if (this.value is T1 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T1> action, Action fallback)
        {
            if (this.value is T1 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T1, TResult> fn, TResult @default)
        {
            if (this.value is T1 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T1, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T1 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T2> action)
        {
            if (this.value is T2 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T2> action, Action fallback)
        {
            if (this.value is T2 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T2, TResult> fn, TResult @default)
        {
            if (this.value is T2 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T2, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T2 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T3> action)
        {
            if (this.value is T3 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T3> action, Action fallback)
        {
            if (this.value is T3 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T3, TResult> fn, TResult @default)
        {
            if (this.value is T3 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T3, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T3 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T4> action)
        {
            if (this.value is T4 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T4> action, Action fallback)
        {
            if (this.value is T4 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T4, TResult> fn, TResult @default)
        {
            if (this.value is T4 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T4, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T4 x)
                return fn(x);

            return fallback();
        }

        public TResult Match<TResult>(Func<T1, TResult> fn1, Func<T2, TResult> fn2, Func<T3, TResult> fn3, Func<T4, TResult> fn4)
        {  
            switch (this.value)
            {
		        case T1 x: return fn1(x);
                case T2 x: return fn2(x);
                case T3 x: return fn3(x);
                case T4 x: return fn4(x);
         
            }
            
            throw new InvalidUnionStateException();
        }

        public void Match<TResult>(Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4)
        {  
            switch (this.value)
            {
		        case T1 x: action1(x); break;
                case T2 x: action2(x); break;
                case T3 x: action3(x); break;
                case T4 x: action4(x); break;
         
            }
            
            throw new InvalidUnionStateException();
        }

        
        public static implicit operator Union<T1, T2, T3, T4>(T1 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4>, T1>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4>(T2 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4>, T2>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4>(T3 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4>, T3>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4>(T4 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4>, T4>.Instance.CastFn(obj);
        }
         
    }
  
    public abstract class Union<T1, T2, T3, T4, T5> : IEquatable<Union<T1, T2, T3, T4, T5>>
    {
        
        private readonly object value;
        
        public Union(T1 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T2 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T3 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T4 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T5 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        
        public bool Equals(Union<T1, T2, T3, T4, T5> other)
        {
            if (other is null)
                return false;
            return this.value.Equals(other.value);
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other is Union<T1, T2, T3, T4, T5> o)
                return this.Equals(o);

            return false;
        }

        public static bool operator == (Union<T1, T2, T3, T4, T5> left, Union<T1, T2, T3, T4, T5> right)
        {
            return left.Equals(right);
        }

        public static bool operator != (Union<T1, T2, T3, T4, T5> left, Union<T1, T2, T3, T4, T5> right)
        {
            return !left.Equals(right);
        }

        public bool When(Action<T1> action)
        {
            if (this.value is T1 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T1> action, Action fallback)
        {
            if (this.value is T1 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T1, TResult> fn, TResult @default)
        {
            if (this.value is T1 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T1, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T1 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T2> action)
        {
            if (this.value is T2 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T2> action, Action fallback)
        {
            if (this.value is T2 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T2, TResult> fn, TResult @default)
        {
            if (this.value is T2 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T2, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T2 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T3> action)
        {
            if (this.value is T3 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T3> action, Action fallback)
        {
            if (this.value is T3 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T3, TResult> fn, TResult @default)
        {
            if (this.value is T3 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T3, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T3 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T4> action)
        {
            if (this.value is T4 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T4> action, Action fallback)
        {
            if (this.value is T4 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T4, TResult> fn, TResult @default)
        {
            if (this.value is T4 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T4, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T4 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T5> action)
        {
            if (this.value is T5 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T5> action, Action fallback)
        {
            if (this.value is T5 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T5, TResult> fn, TResult @default)
        {
            if (this.value is T5 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T5, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T5 x)
                return fn(x);

            return fallback();
        }

        public TResult Match<TResult>(Func<T1, TResult> fn1, Func<T2, TResult> fn2, Func<T3, TResult> fn3, Func<T4, TResult> fn4, Func<T5, TResult> fn5)
        {  
            switch (this.value)
            {
		        case T1 x: return fn1(x);
                case T2 x: return fn2(x);
                case T3 x: return fn3(x);
                case T4 x: return fn4(x);
                case T5 x: return fn5(x);
         
            }
            
            throw new InvalidUnionStateException();
        }

        public void Match<TResult>(Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5)
        {  
            switch (this.value)
            {
		        case T1 x: action1(x); break;
                case T2 x: action2(x); break;
                case T3 x: action3(x); break;
                case T4 x: action4(x); break;
                case T5 x: action5(x); break;
         
            }
            
            throw new InvalidUnionStateException();
        }

        
        public static implicit operator Union<T1, T2, T3, T4, T5>(T1 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5>, T1>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4, T5>(T2 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5>, T2>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4, T5>(T3 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5>, T3>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4, T5>(T4 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5>, T4>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4, T5>(T5 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5>, T5>.Instance.CastFn(obj);
        }
         
    }
  
    public abstract class Union<T1, T2, T3, T4, T5, T6> : IEquatable<Union<T1, T2, T3, T4, T5, T6>>
    {
        
        private readonly object value;
        
        public Union(T1 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T2 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T3 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T4 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T5 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T6 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        
        public bool Equals(Union<T1, T2, T3, T4, T5, T6> other)
        {
            if (other is null)
                return false;
            return this.value.Equals(other.value);
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other is Union<T1, T2, T3, T4, T5, T6> o)
                return this.Equals(o);

            return false;
        }

        public static bool operator == (Union<T1, T2, T3, T4, T5, T6> left, Union<T1, T2, T3, T4, T5, T6> right)
        {
            return left.Equals(right);
        }

        public static bool operator != (Union<T1, T2, T3, T4, T5, T6> left, Union<T1, T2, T3, T4, T5, T6> right)
        {
            return !left.Equals(right);
        }

        public bool When(Action<T1> action)
        {
            if (this.value is T1 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T1> action, Action fallback)
        {
            if (this.value is T1 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T1, TResult> fn, TResult @default)
        {
            if (this.value is T1 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T1, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T1 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T2> action)
        {
            if (this.value is T2 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T2> action, Action fallback)
        {
            if (this.value is T2 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T2, TResult> fn, TResult @default)
        {
            if (this.value is T2 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T2, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T2 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T3> action)
        {
            if (this.value is T3 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T3> action, Action fallback)
        {
            if (this.value is T3 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T3, TResult> fn, TResult @default)
        {
            if (this.value is T3 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T3, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T3 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T4> action)
        {
            if (this.value is T4 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T4> action, Action fallback)
        {
            if (this.value is T4 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T4, TResult> fn, TResult @default)
        {
            if (this.value is T4 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T4, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T4 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T5> action)
        {
            if (this.value is T5 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T5> action, Action fallback)
        {
            if (this.value is T5 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T5, TResult> fn, TResult @default)
        {
            if (this.value is T5 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T5, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T5 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T6> action)
        {
            if (this.value is T6 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T6> action, Action fallback)
        {
            if (this.value is T6 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T6, TResult> fn, TResult @default)
        {
            if (this.value is T6 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T6, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T6 x)
                return fn(x);

            return fallback();
        }

        public TResult Match<TResult>(Func<T1, TResult> fn1, Func<T2, TResult> fn2, Func<T3, TResult> fn3, Func<T4, TResult> fn4, Func<T5, TResult> fn5, Func<T6, TResult> fn6)
        {  
            switch (this.value)
            {
		        case T1 x: return fn1(x);
                case T2 x: return fn2(x);
                case T3 x: return fn3(x);
                case T4 x: return fn4(x);
                case T5 x: return fn5(x);
                case T6 x: return fn6(x);
         
            }
            
            throw new InvalidUnionStateException();
        }

        public void Match<TResult>(Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5, Action<T6> action6)
        {  
            switch (this.value)
            {
		        case T1 x: action1(x); break;
                case T2 x: action2(x); break;
                case T3 x: action3(x); break;
                case T4 x: action4(x); break;
                case T5 x: action5(x); break;
                case T6 x: action6(x); break;
         
            }
            
            throw new InvalidUnionStateException();
        }

        
        public static implicit operator Union<T1, T2, T3, T4, T5, T6>(T1 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5, T6>, T1>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4, T5, T6>(T2 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5, T6>, T2>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4, T5, T6>(T3 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5, T6>, T3>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4, T5, T6>(T4 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5, T6>, T4>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4, T5, T6>(T5 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5, T6>, T5>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4, T5, T6>(T6 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5, T6>, T6>.Instance.CastFn(obj);
        }
         
    }
  
    public abstract class Union<T1, T2, T3, T4, T5, T6, T7> : IEquatable<Union<T1, T2, T3, T4, T5, T6, T7>>
    {
        
        private readonly object value;
        
        public Union(T1 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T2 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T3 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T4 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T5 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T6 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        public Union(T7 value)
        {
            this.value = value ?? throw new UnionCaseNullException();
        }
        
        public bool Equals(Union<T1, T2, T3, T4, T5, T6, T7> other)
        {
            if (other is null)
                return false;
            return this.value.Equals(other.value);
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other is Union<T1, T2, T3, T4, T5, T6, T7> o)
                return this.Equals(o);

            return false;
        }

        public static bool operator == (Union<T1, T2, T3, T4, T5, T6, T7> left, Union<T1, T2, T3, T4, T5, T6, T7> right)
        {
            return left.Equals(right);
        }

        public static bool operator != (Union<T1, T2, T3, T4, T5, T6, T7> left, Union<T1, T2, T3, T4, T5, T6, T7> right)
        {
            return !left.Equals(right);
        }

        public bool When(Action<T1> action)
        {
            if (this.value is T1 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T1> action, Action fallback)
        {
            if (this.value is T1 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T1, TResult> fn, TResult @default)
        {
            if (this.value is T1 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T1, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T1 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T2> action)
        {
            if (this.value is T2 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T2> action, Action fallback)
        {
            if (this.value is T2 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T2, TResult> fn, TResult @default)
        {
            if (this.value is T2 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T2, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T2 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T3> action)
        {
            if (this.value is T3 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T3> action, Action fallback)
        {
            if (this.value is T3 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T3, TResult> fn, TResult @default)
        {
            if (this.value is T3 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T3, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T3 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T4> action)
        {
            if (this.value is T4 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T4> action, Action fallback)
        {
            if (this.value is T4 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T4, TResult> fn, TResult @default)
        {
            if (this.value is T4 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T4, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T4 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T5> action)
        {
            if (this.value is T5 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T5> action, Action fallback)
        {
            if (this.value is T5 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T5, TResult> fn, TResult @default)
        {
            if (this.value is T5 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T5, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T5 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T6> action)
        {
            if (this.value is T6 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T6> action, Action fallback)
        {
            if (this.value is T6 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T6, TResult> fn, TResult @default)
        {
            if (this.value is T6 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T6, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T6 x)
                return fn(x);

            return fallback();
        }
        public bool When(Action<T7> action)
        {
            if (this.value is T7 x)
            {
                action(x);
                return true;
            }

            return false;
        }

        public bool When(Action<T7> action, Action fallback)
        {
            if (this.value is T7 x)
            {
                action(x);
                return true;
            }

            fallback();
            return false;
        }

        public TResult When<TResult>(Func<T7, TResult> fn, TResult @default)
        {
            if (this.value is T7 x)
                return fn(x);

            return @default;
        }

        public TResult When<TResult>(Func<T7, TResult> fn, Func<TResult> fallback)
        {
            if (this.value is T7 x)
                return fn(x);

            return fallback();
        }

        public TResult Match<TResult>(Func<T1, TResult> fn1, Func<T2, TResult> fn2, Func<T3, TResult> fn3, Func<T4, TResult> fn4, Func<T5, TResult> fn5, Func<T6, TResult> fn6, Func<T7, TResult> fn7)
        {  
            switch (this.value)
            {
		        case T1 x: return fn1(x);
                case T2 x: return fn2(x);
                case T3 x: return fn3(x);
                case T4 x: return fn4(x);
                case T5 x: return fn5(x);
                case T6 x: return fn6(x);
                case T7 x: return fn7(x);
         
            }
            
            throw new InvalidUnionStateException();
        }

        public void Match<TResult>(Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5, Action<T6> action6, Action<T7> action7)
        {  
            switch (this.value)
            {
		        case T1 x: action1(x); break;
                case T2 x: action2(x); break;
                case T3 x: action3(x); break;
                case T4 x: action4(x); break;
                case T5 x: action5(x); break;
                case T6 x: action6(x); break;
                case T7 x: action7(x); break;
         
            }
            
            throw new InvalidUnionStateException();
        }

        
        public static implicit operator Union<T1, T2, T3, T4, T5, T6, T7>(T1 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5, T6, T7>, T1>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4, T5, T6, T7>(T2 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5, T6, T7>, T2>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4, T5, T6, T7>(T3 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5, T6, T7>, T3>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4, T5, T6, T7>(T4 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5, T6, T7>, T4>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4, T5, T6, T7>(T5 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5, T6, T7>, T5>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4, T5, T6, T7>(T6 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5, T6, T7>, T6>.Instance.CastFn(obj);
        }
        
        public static implicit operator Union<T1, T2, T3, T4, T5, T6, T7>(T7 obj)
        {
            return TypeUtils<Union<T1, T2, T3, T4, T5, T6, T7>, T7>.Instance.CastFn(obj);
        }
         
    }


}

