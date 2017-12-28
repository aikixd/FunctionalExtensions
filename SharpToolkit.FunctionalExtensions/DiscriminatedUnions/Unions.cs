using System;
using SharpToolkit.FunctionalExtensions.DiscriminatedUnions;

namespace SharpToolkit.FunctionalExtensions
{

  
    public abstract class Union<T1> : IEquatable<Union<T1>>
        where T1 : Case
    {
    
        CaseSelection<T1> case1;
        
        public Union(T1 @case)
        {
            set(@case);
        }
        
        public bool Equals(Union<T1> other)
        {
            return 
                this.case1.Equals(other.case1);
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
            private void set(T1 @case)
        {
            this.case1 = new SelectedCase<T1>(@case);
                    
        }

        public void When(Action<T1> action)
        {
            this.case1.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T1, TResult> fn)
        {
            this.case1.Do(fn, out var r);

            return r;
        }


        public TResult Match<TResult>(Func<T1, TResult> fn1)
        {  
            TResult r = default;
		    if (this.case1.Do(fn1, out r)) return r;
         
              throw new InvalidOperationException("The union is empty. This is a bug, please report an issue to https://github.com/SharpToolkit/FunctionalExtensions.");
        }

        public void Match(Action<T1> action1)
        { 
            int r = default;
		    this.case1.Do(action1.AsFunc(), out r);
        }

        
    }
  
    public abstract class Union<T1, T2> : IEquatable<Union<T1, T2>>
        where T1 : Case
        where T2 : Case
    {
    
        CaseSelection<T1> case1;
        CaseSelection<T2> case2;
        
        public Union(T1 @case)
        {
            set(@case);
        }
        public Union(T2 @case)
        {
            set(@case);
        }
        
        public bool Equals(Union<T1, T2> other)
        {
            return 
                this.case1.Equals(other.case1) && 
                this.case2.Equals(other.case2);
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
            private void set(T1 @case)
        {
            this.case1 = new SelectedCase<T1>(@case);
            this.case2 = new UnselectedCase<T2>();
                    
        }
        private void set(T2 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new SelectedCase<T2>(@case);
                    
        }

        public void When(Action<T1> action)
        {
            this.case1.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T1, TResult> fn)
        {
            this.case1.Do(fn, out var r);

            return r;
        }
        public void When(Action<T2> action)
        {
            this.case2.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T2, TResult> fn)
        {
            this.case2.Do(fn, out var r);

            return r;
        }


        public TResult Match<TResult>(Func<T1, TResult> fn1, Func<T2, TResult> fn2)
        {  
            TResult r = default;
		    if (this.case1.Do(fn1, out r)) return r;
            if (this.case2.Do(fn2, out r)) return r;
         
              throw new InvalidOperationException("The union is empty. This is a bug, please report an issue to https://github.com/SharpToolkit/FunctionalExtensions.");
        }

        public void Match(Action<T1> action1, Action<T2> action2)
        { 
            int r = default;
		    this.case1.Do(action1.AsFunc(), out r);
            this.case2.Do(action2.AsFunc(), out r);
        }

        
    }
  
    public abstract class Union<T1, T2, T3> : IEquatable<Union<T1, T2, T3>>
        where T1 : Case
        where T2 : Case
        where T3 : Case
    {
    
        CaseSelection<T1> case1;
        CaseSelection<T2> case2;
        CaseSelection<T3> case3;
        
        public Union(T1 @case)
        {
            set(@case);
        }
        public Union(T2 @case)
        {
            set(@case);
        }
        public Union(T3 @case)
        {
            set(@case);
        }
        
        public bool Equals(Union<T1, T2, T3> other)
        {
            return 
                this.case1.Equals(other.case1) && 
                this.case2.Equals(other.case2) && 
                this.case3.Equals(other.case3);
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
            private void set(T1 @case)
        {
            this.case1 = new SelectedCase<T1>(@case);
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new UnselectedCase<T3>();
                    
        }
        private void set(T2 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new SelectedCase<T2>(@case);
            this.case3 = new UnselectedCase<T3>();
                    
        }
        private void set(T3 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new SelectedCase<T3>(@case);
                    
        }

        public void When(Action<T1> action)
        {
            this.case1.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T1, TResult> fn)
        {
            this.case1.Do(fn, out var r);

            return r;
        }
        public void When(Action<T2> action)
        {
            this.case2.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T2, TResult> fn)
        {
            this.case2.Do(fn, out var r);

            return r;
        }
        public void When(Action<T3> action)
        {
            this.case3.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T3, TResult> fn)
        {
            this.case3.Do(fn, out var r);

            return r;
        }


        public TResult Match<TResult>(Func<T1, TResult> fn1, Func<T2, TResult> fn2, Func<T3, TResult> fn3)
        {  
            TResult r = default;
		    if (this.case1.Do(fn1, out r)) return r;
            if (this.case2.Do(fn2, out r)) return r;
            if (this.case3.Do(fn3, out r)) return r;
         
              throw new InvalidOperationException("The union is empty. This is a bug, please report an issue to https://github.com/SharpToolkit/FunctionalExtensions.");
        }

        public void Match(Action<T1> action1, Action<T2> action2, Action<T3> action3)
        { 
            int r = default;
		    this.case1.Do(action1.AsFunc(), out r);
            this.case2.Do(action2.AsFunc(), out r);
            this.case3.Do(action3.AsFunc(), out r);
        }

        
    }
  
    public abstract class Union<T1, T2, T3, T4> : IEquatable<Union<T1, T2, T3, T4>>
        where T1 : Case
        where T2 : Case
        where T3 : Case
        where T4 : Case
    {
    
        CaseSelection<T1> case1;
        CaseSelection<T2> case2;
        CaseSelection<T3> case3;
        CaseSelection<T4> case4;
        
        public Union(T1 @case)
        {
            set(@case);
        }
        public Union(T2 @case)
        {
            set(@case);
        }
        public Union(T3 @case)
        {
            set(@case);
        }
        public Union(T4 @case)
        {
            set(@case);
        }
        
        public bool Equals(Union<T1, T2, T3, T4> other)
        {
            return 
                this.case1.Equals(other.case1) && 
                this.case2.Equals(other.case2) && 
                this.case3.Equals(other.case3) && 
                this.case4.Equals(other.case4);
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
            private void set(T1 @case)
        {
            this.case1 = new SelectedCase<T1>(@case);
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new UnselectedCase<T4>();
                    
        }
        private void set(T2 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new SelectedCase<T2>(@case);
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new UnselectedCase<T4>();
                    
        }
        private void set(T3 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new SelectedCase<T3>(@case);
            this.case4 = new UnselectedCase<T4>();
                    
        }
        private void set(T4 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new SelectedCase<T4>(@case);
                    
        }

        public void When(Action<T1> action)
        {
            this.case1.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T1, TResult> fn)
        {
            this.case1.Do(fn, out var r);

            return r;
        }
        public void When(Action<T2> action)
        {
            this.case2.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T2, TResult> fn)
        {
            this.case2.Do(fn, out var r);

            return r;
        }
        public void When(Action<T3> action)
        {
            this.case3.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T3, TResult> fn)
        {
            this.case3.Do(fn, out var r);

            return r;
        }
        public void When(Action<T4> action)
        {
            this.case4.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T4, TResult> fn)
        {
            this.case4.Do(fn, out var r);

            return r;
        }


        public TResult Match<TResult>(Func<T1, TResult> fn1, Func<T2, TResult> fn2, Func<T3, TResult> fn3, Func<T4, TResult> fn4)
        {  
            TResult r = default;
		    if (this.case1.Do(fn1, out r)) return r;
            if (this.case2.Do(fn2, out r)) return r;
            if (this.case3.Do(fn3, out r)) return r;
            if (this.case4.Do(fn4, out r)) return r;
         
              throw new InvalidOperationException("The union is empty. This is a bug, please report an issue to https://github.com/SharpToolkit/FunctionalExtensions.");
        }

        public void Match(Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4)
        { 
            int r = default;
		    this.case1.Do(action1.AsFunc(), out r);
            this.case2.Do(action2.AsFunc(), out r);
            this.case3.Do(action3.AsFunc(), out r);
            this.case4.Do(action4.AsFunc(), out r);
        }

        
    }
  
    public abstract class Union<T1, T2, T3, T4, T5> : IEquatable<Union<T1, T2, T3, T4, T5>>
        where T1 : Case
        where T2 : Case
        where T3 : Case
        where T4 : Case
        where T5 : Case
    {
    
        CaseSelection<T1> case1;
        CaseSelection<T2> case2;
        CaseSelection<T3> case3;
        CaseSelection<T4> case4;
        CaseSelection<T5> case5;
        
        public Union(T1 @case)
        {
            set(@case);
        }
        public Union(T2 @case)
        {
            set(@case);
        }
        public Union(T3 @case)
        {
            set(@case);
        }
        public Union(T4 @case)
        {
            set(@case);
        }
        public Union(T5 @case)
        {
            set(@case);
        }
        
        public bool Equals(Union<T1, T2, T3, T4, T5> other)
        {
            return 
                this.case1.Equals(other.case1) && 
                this.case2.Equals(other.case2) && 
                this.case3.Equals(other.case3) && 
                this.case4.Equals(other.case4) && 
                this.case5.Equals(other.case5);
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
            private void set(T1 @case)
        {
            this.case1 = new SelectedCase<T1>(@case);
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new UnselectedCase<T4>();
            this.case5 = new UnselectedCase<T5>();
                    
        }
        private void set(T2 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new SelectedCase<T2>(@case);
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new UnselectedCase<T4>();
            this.case5 = new UnselectedCase<T5>();
                    
        }
        private void set(T3 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new SelectedCase<T3>(@case);
            this.case4 = new UnselectedCase<T4>();
            this.case5 = new UnselectedCase<T5>();
                    
        }
        private void set(T4 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new SelectedCase<T4>(@case);
            this.case5 = new UnselectedCase<T5>();
                    
        }
        private void set(T5 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new UnselectedCase<T4>();
            this.case5 = new SelectedCase<T5>(@case);
                    
        }

        public void When(Action<T1> action)
        {
            this.case1.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T1, TResult> fn)
        {
            this.case1.Do(fn, out var r);

            return r;
        }
        public void When(Action<T2> action)
        {
            this.case2.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T2, TResult> fn)
        {
            this.case2.Do(fn, out var r);

            return r;
        }
        public void When(Action<T3> action)
        {
            this.case3.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T3, TResult> fn)
        {
            this.case3.Do(fn, out var r);

            return r;
        }
        public void When(Action<T4> action)
        {
            this.case4.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T4, TResult> fn)
        {
            this.case4.Do(fn, out var r);

            return r;
        }
        public void When(Action<T5> action)
        {
            this.case5.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T5, TResult> fn)
        {
            this.case5.Do(fn, out var r);

            return r;
        }


        public TResult Match<TResult>(Func<T1, TResult> fn1, Func<T2, TResult> fn2, Func<T3, TResult> fn3, Func<T4, TResult> fn4, Func<T5, TResult> fn5)
        {  
            TResult r = default;
		    if (this.case1.Do(fn1, out r)) return r;
            if (this.case2.Do(fn2, out r)) return r;
            if (this.case3.Do(fn3, out r)) return r;
            if (this.case4.Do(fn4, out r)) return r;
            if (this.case5.Do(fn5, out r)) return r;
         
              throw new InvalidOperationException("The union is empty. This is a bug, please report an issue to https://github.com/SharpToolkit/FunctionalExtensions.");
        }

        public void Match(Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5)
        { 
            int r = default;
		    this.case1.Do(action1.AsFunc(), out r);
            this.case2.Do(action2.AsFunc(), out r);
            this.case3.Do(action3.AsFunc(), out r);
            this.case4.Do(action4.AsFunc(), out r);
            this.case5.Do(action5.AsFunc(), out r);
        }

        
    }
  
    public abstract class Union<T1, T2, T3, T4, T5, T6> : IEquatable<Union<T1, T2, T3, T4, T5, T6>>
        where T1 : Case
        where T2 : Case
        where T3 : Case
        where T4 : Case
        where T5 : Case
        where T6 : Case
    {
    
        CaseSelection<T1> case1;
        CaseSelection<T2> case2;
        CaseSelection<T3> case3;
        CaseSelection<T4> case4;
        CaseSelection<T5> case5;
        CaseSelection<T6> case6;
        
        public Union(T1 @case)
        {
            set(@case);
        }
        public Union(T2 @case)
        {
            set(@case);
        }
        public Union(T3 @case)
        {
            set(@case);
        }
        public Union(T4 @case)
        {
            set(@case);
        }
        public Union(T5 @case)
        {
            set(@case);
        }
        public Union(T6 @case)
        {
            set(@case);
        }
        
        public bool Equals(Union<T1, T2, T3, T4, T5, T6> other)
        {
            return 
                this.case1.Equals(other.case1) && 
                this.case2.Equals(other.case2) && 
                this.case3.Equals(other.case3) && 
                this.case4.Equals(other.case4) && 
                this.case5.Equals(other.case5) && 
                this.case6.Equals(other.case6);
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
            private void set(T1 @case)
        {
            this.case1 = new SelectedCase<T1>(@case);
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new UnselectedCase<T4>();
            this.case5 = new UnselectedCase<T5>();
            this.case6 = new UnselectedCase<T6>();
                    
        }
        private void set(T2 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new SelectedCase<T2>(@case);
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new UnselectedCase<T4>();
            this.case5 = new UnselectedCase<T5>();
            this.case6 = new UnselectedCase<T6>();
                    
        }
        private void set(T3 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new SelectedCase<T3>(@case);
            this.case4 = new UnselectedCase<T4>();
            this.case5 = new UnselectedCase<T5>();
            this.case6 = new UnselectedCase<T6>();
                    
        }
        private void set(T4 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new SelectedCase<T4>(@case);
            this.case5 = new UnselectedCase<T5>();
            this.case6 = new UnselectedCase<T6>();
                    
        }
        private void set(T5 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new UnselectedCase<T4>();
            this.case5 = new SelectedCase<T5>(@case);
            this.case6 = new UnselectedCase<T6>();
                    
        }
        private void set(T6 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new UnselectedCase<T4>();
            this.case5 = new UnselectedCase<T5>();
            this.case6 = new SelectedCase<T6>(@case);
                    
        }

        public void When(Action<T1> action)
        {
            this.case1.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T1, TResult> fn)
        {
            this.case1.Do(fn, out var r);

            return r;
        }
        public void When(Action<T2> action)
        {
            this.case2.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T2, TResult> fn)
        {
            this.case2.Do(fn, out var r);

            return r;
        }
        public void When(Action<T3> action)
        {
            this.case3.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T3, TResult> fn)
        {
            this.case3.Do(fn, out var r);

            return r;
        }
        public void When(Action<T4> action)
        {
            this.case4.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T4, TResult> fn)
        {
            this.case4.Do(fn, out var r);

            return r;
        }
        public void When(Action<T5> action)
        {
            this.case5.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T5, TResult> fn)
        {
            this.case5.Do(fn, out var r);

            return r;
        }
        public void When(Action<T6> action)
        {
            this.case6.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T6, TResult> fn)
        {
            this.case6.Do(fn, out var r);

            return r;
        }


        public TResult Match<TResult>(Func<T1, TResult> fn1, Func<T2, TResult> fn2, Func<T3, TResult> fn3, Func<T4, TResult> fn4, Func<T5, TResult> fn5, Func<T6, TResult> fn6)
        {  
            TResult r = default;
		    if (this.case1.Do(fn1, out r)) return r;
            if (this.case2.Do(fn2, out r)) return r;
            if (this.case3.Do(fn3, out r)) return r;
            if (this.case4.Do(fn4, out r)) return r;
            if (this.case5.Do(fn5, out r)) return r;
            if (this.case6.Do(fn6, out r)) return r;
         
              throw new InvalidOperationException("The union is empty. This is a bug, please report an issue to https://github.com/SharpToolkit/FunctionalExtensions.");
        }

        public void Match(Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5, Action<T6> action6)
        { 
            int r = default;
		    this.case1.Do(action1.AsFunc(), out r);
            this.case2.Do(action2.AsFunc(), out r);
            this.case3.Do(action3.AsFunc(), out r);
            this.case4.Do(action4.AsFunc(), out r);
            this.case5.Do(action5.AsFunc(), out r);
            this.case6.Do(action6.AsFunc(), out r);
        }

        
    }
  
    public abstract class Union<T1, T2, T3, T4, T5, T6, T7> : IEquatable<Union<T1, T2, T3, T4, T5, T6, T7>>
        where T1 : Case
        where T2 : Case
        where T3 : Case
        where T4 : Case
        where T5 : Case
        where T6 : Case
        where T7 : Case
    {
    
        CaseSelection<T1> case1;
        CaseSelection<T2> case2;
        CaseSelection<T3> case3;
        CaseSelection<T4> case4;
        CaseSelection<T5> case5;
        CaseSelection<T6> case6;
        CaseSelection<T7> case7;
        
        public Union(T1 @case)
        {
            set(@case);
        }
        public Union(T2 @case)
        {
            set(@case);
        }
        public Union(T3 @case)
        {
            set(@case);
        }
        public Union(T4 @case)
        {
            set(@case);
        }
        public Union(T5 @case)
        {
            set(@case);
        }
        public Union(T6 @case)
        {
            set(@case);
        }
        public Union(T7 @case)
        {
            set(@case);
        }
        
        public bool Equals(Union<T1, T2, T3, T4, T5, T6, T7> other)
        {
            return 
                this.case1.Equals(other.case1) && 
                this.case2.Equals(other.case2) && 
                this.case3.Equals(other.case3) && 
                this.case4.Equals(other.case4) && 
                this.case5.Equals(other.case5) && 
                this.case6.Equals(other.case6) && 
                this.case7.Equals(other.case7);
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
            private void set(T1 @case)
        {
            this.case1 = new SelectedCase<T1>(@case);
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new UnselectedCase<T4>();
            this.case5 = new UnselectedCase<T5>();
            this.case6 = new UnselectedCase<T6>();
            this.case7 = new UnselectedCase<T7>();
                    
        }
        private void set(T2 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new SelectedCase<T2>(@case);
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new UnselectedCase<T4>();
            this.case5 = new UnselectedCase<T5>();
            this.case6 = new UnselectedCase<T6>();
            this.case7 = new UnselectedCase<T7>();
                    
        }
        private void set(T3 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new SelectedCase<T3>(@case);
            this.case4 = new UnselectedCase<T4>();
            this.case5 = new UnselectedCase<T5>();
            this.case6 = new UnselectedCase<T6>();
            this.case7 = new UnselectedCase<T7>();
                    
        }
        private void set(T4 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new SelectedCase<T4>(@case);
            this.case5 = new UnselectedCase<T5>();
            this.case6 = new UnselectedCase<T6>();
            this.case7 = new UnselectedCase<T7>();
                    
        }
        private void set(T5 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new UnselectedCase<T4>();
            this.case5 = new SelectedCase<T5>(@case);
            this.case6 = new UnselectedCase<T6>();
            this.case7 = new UnselectedCase<T7>();
                    
        }
        private void set(T6 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new UnselectedCase<T4>();
            this.case5 = new UnselectedCase<T5>();
            this.case6 = new SelectedCase<T6>(@case);
            this.case7 = new UnselectedCase<T7>();
                    
        }
        private void set(T7 @case)
        {
            this.case1 = new UnselectedCase<T1>();
            this.case2 = new UnselectedCase<T2>();
            this.case3 = new UnselectedCase<T3>();
            this.case4 = new UnselectedCase<T4>();
            this.case5 = new UnselectedCase<T5>();
            this.case6 = new UnselectedCase<T6>();
            this.case7 = new SelectedCase<T7>(@case);
                    
        }

        public void When(Action<T1> action)
        {
            this.case1.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T1, TResult> fn)
        {
            this.case1.Do(fn, out var r);

            return r;
        }
        public void When(Action<T2> action)
        {
            this.case2.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T2, TResult> fn)
        {
            this.case2.Do(fn, out var r);

            return r;
        }
        public void When(Action<T3> action)
        {
            this.case3.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T3, TResult> fn)
        {
            this.case3.Do(fn, out var r);

            return r;
        }
        public void When(Action<T4> action)
        {
            this.case4.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T4, TResult> fn)
        {
            this.case4.Do(fn, out var r);

            return r;
        }
        public void When(Action<T5> action)
        {
            this.case5.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T5, TResult> fn)
        {
            this.case5.Do(fn, out var r);

            return r;
        }
        public void When(Action<T6> action)
        {
            this.case6.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T6, TResult> fn)
        {
            this.case6.Do(fn, out var r);

            return r;
        }
        public void When(Action<T7> action)
        {
            this.case7.Do(action.AsFunc(), out var r);
        }

        public TResult When<TResult>(Func<T7, TResult> fn)
        {
            this.case7.Do(fn, out var r);

            return r;
        }


        public TResult Match<TResult>(Func<T1, TResult> fn1, Func<T2, TResult> fn2, Func<T3, TResult> fn3, Func<T4, TResult> fn4, Func<T5, TResult> fn5, Func<T6, TResult> fn6, Func<T7, TResult> fn7)
        {  
            TResult r = default;
		    if (this.case1.Do(fn1, out r)) return r;
            if (this.case2.Do(fn2, out r)) return r;
            if (this.case3.Do(fn3, out r)) return r;
            if (this.case4.Do(fn4, out r)) return r;
            if (this.case5.Do(fn5, out r)) return r;
            if (this.case6.Do(fn6, out r)) return r;
            if (this.case7.Do(fn7, out r)) return r;
         
              throw new InvalidOperationException("The union is empty. This is a bug, please report an issue to https://github.com/SharpToolkit/FunctionalExtensions.");
        }

        public void Match(Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5, Action<T6> action6, Action<T7> action7)
        { 
            int r = default;
		    this.case1.Do(action1.AsFunc(), out r);
            this.case2.Do(action2.AsFunc(), out r);
            this.case3.Do(action3.AsFunc(), out r);
            this.case4.Do(action4.AsFunc(), out r);
            this.case5.Do(action5.AsFunc(), out r);
            this.case6.Do(action6.AsFunc(), out r);
            this.case7.Do(action7.AsFunc(), out r);
        }

        
    }


}

