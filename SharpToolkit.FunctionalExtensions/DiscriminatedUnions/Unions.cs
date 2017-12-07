using System;

namespace SharpToolkit.FunctionalExtensions.DiscriminatedUnions
{

  
    public abstract class Union <T1>
        where T1 : Case
    {
        CaseSelection<T1> case1;
        
        public Union(T1 @case)
        {
            set(@case);
        }
        
        private void set(T1 @case)
        {
            this.case1 = new SelectedCase<T1>(@case);
                    
        }

        public void When(Action<T1> action)
        {
            this.case1.Do(action);
        }

        
        public void Switch(Action<T1> action1)
        {
            this.case1.Do(action1);
        }
    }
  
    public abstract class Union <T1, T2>
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
            this.case1.Do(action);
        }
        public void When(Action<T2> action)
        {
            this.case2.Do(action);
        }

        
        public void Switch(Action<T1> action1, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
        }
        
        public void Switch(Action<T2> action2, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
        }
    }
  
    public abstract class Union <T1, T2, T3>
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
            this.case1.Do(action);
        }
        public void When(Action<T2> action)
        {
            this.case2.Do(action);
        }
        public void When(Action<T3> action)
        {
            this.case3.Do(action);
        }

        
        public void Switch(Action<T1> action1, Action<T2> action2, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
        }
        
        public void Switch(Action<T1> action1, Action<T3> action3, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
        }
        
        public void Switch(Action<T2> action2, Action<T1> action1, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
        }
        
        public void Switch(Action<T2> action2, Action<T3> action3, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
        }
        
        public void Switch(Action<T3> action3, Action<T1> action1, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
        }
        
        public void Switch(Action<T3> action3, Action<T2> action2, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
        }
    }
  
    public abstract class Union <T1, T2, T3, T4>
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
            this.case1.Do(action);
        }
        public void When(Action<T2> action)
        {
            this.case2.Do(action);
        }
        public void When(Action<T3> action)
        {
            this.case3.Do(action);
        }
        public void When(Action<T4> action)
        {
            this.case4.Do(action);
        }

        
        public void Switch(Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T1> action1, Action<T2> action2, Action<T4> action4, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T1> action1, Action<T3> action3, Action<T2> action2, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T1> action1, Action<T3> action3, Action<T4> action4, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T1> action1, Action<T4> action4, Action<T2> action2, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T1> action1, Action<T4> action4, Action<T3> action3, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T2> action2, Action<T1> action1, Action<T3> action3, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T2> action2, Action<T1> action1, Action<T4> action4, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T2> action2, Action<T3> action3, Action<T1> action1, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T2> action2, Action<T4> action4, Action<T1> action1, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T2> action2, Action<T4> action4, Action<T3> action3, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T3> action3, Action<T1> action1, Action<T2> action2, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T3> action3, Action<T1> action1, Action<T4> action4, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T3> action3, Action<T2> action2, Action<T1> action1, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T3> action3, Action<T2> action2, Action<T4> action4, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T3> action3, Action<T4> action4, Action<T1> action1, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T3> action3, Action<T4> action4, Action<T2> action2, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T4> action4, Action<T1> action1, Action<T2> action2, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T4> action4, Action<T1> action1, Action<T3> action3, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T4> action4, Action<T2> action2, Action<T1> action1, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T4> action4, Action<T2> action2, Action<T3> action3, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T4> action4, Action<T3> action3, Action<T1> action1, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
        
        public void Switch(Action<T4> action4, Action<T3> action3, Action<T2> action2, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
        }
    }
  
    public abstract class Union <T1, T2, T3, T4, T5>
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
            this.case1.Do(action);
        }
        public void When(Action<T2> action)
        {
            this.case2.Do(action);
        }
        public void When(Action<T3> action)
        {
            this.case3.Do(action);
        }
        public void When(Action<T4> action)
        {
            this.case4.Do(action);
        }
        public void When(Action<T5> action)
        {
            this.case5.Do(action);
        }

        
        public void Switch(Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T5> action5, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T2> action2, Action<T4> action4, Action<T3> action3, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T2> action2, Action<T4> action4, Action<T5> action5, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T2> action2, Action<T5> action5, Action<T3> action3, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T2> action2, Action<T5> action5, Action<T4> action4, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T3> action3, Action<T2> action2, Action<T4> action4, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T3> action3, Action<T2> action2, Action<T5> action5, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T3> action3, Action<T4> action4, Action<T2> action2, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T3> action3, Action<T4> action4, Action<T5> action5, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T3> action3, Action<T5> action5, Action<T2> action2, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T3> action3, Action<T5> action5, Action<T4> action4, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T4> action4, Action<T2> action2, Action<T3> action3, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T4> action4, Action<T2> action2, Action<T5> action5, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T4> action4, Action<T3> action3, Action<T2> action2, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T4> action4, Action<T3> action3, Action<T5> action5, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T4> action4, Action<T5> action5, Action<T2> action2, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T4> action4, Action<T5> action5, Action<T3> action3, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T5> action5, Action<T2> action2, Action<T3> action3, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T5> action5, Action<T2> action2, Action<T4> action4, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T5> action5, Action<T3> action3, Action<T2> action2, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T5> action5, Action<T3> action3, Action<T4> action4, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T5> action5, Action<T4> action4, Action<T2> action2, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T1> action1, Action<T5> action5, Action<T4> action4, Action<T3> action3, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T1> action1, Action<T3> action3, Action<T4> action4, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T1> action1, Action<T3> action3, Action<T5> action5, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T1> action1, Action<T4> action4, Action<T3> action3, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T1> action1, Action<T4> action4, Action<T5> action5, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T1> action1, Action<T5> action5, Action<T3> action3, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T1> action1, Action<T5> action5, Action<T4> action4, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T3> action3, Action<T1> action1, Action<T4> action4, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T3> action3, Action<T1> action1, Action<T5> action5, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T1> action1, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T3> action3, Action<T5> action5, Action<T1> action1, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T3> action3, Action<T5> action5, Action<T4> action4, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T4> action4, Action<T1> action1, Action<T3> action3, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T4> action4, Action<T1> action1, Action<T5> action5, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T4> action4, Action<T3> action3, Action<T1> action1, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T4> action4, Action<T3> action3, Action<T5> action5, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T4> action4, Action<T5> action5, Action<T1> action1, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T4> action4, Action<T5> action5, Action<T3> action3, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T5> action5, Action<T1> action1, Action<T3> action3, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T5> action5, Action<T1> action1, Action<T4> action4, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T5> action5, Action<T3> action3, Action<T1> action1, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T5> action5, Action<T3> action3, Action<T4> action4, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T5> action5, Action<T4> action4, Action<T1> action1, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T2> action2, Action<T5> action5, Action<T4> action4, Action<T3> action3, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T1> action1, Action<T2> action2, Action<T4> action4, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T1> action1, Action<T2> action2, Action<T5> action5, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T1> action1, Action<T4> action4, Action<T2> action2, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T1> action1, Action<T4> action4, Action<T5> action5, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T1> action1, Action<T5> action5, Action<T2> action2, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T1> action1, Action<T5> action5, Action<T4> action4, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T2> action2, Action<T1> action1, Action<T4> action4, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T2> action2, Action<T1> action1, Action<T5> action5, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T2> action2, Action<T4> action4, Action<T1> action1, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T2> action2, Action<T4> action4, Action<T5> action5, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T2> action2, Action<T5> action5, Action<T1> action1, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T2> action2, Action<T5> action5, Action<T4> action4, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T4> action4, Action<T1> action1, Action<T2> action2, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T4> action4, Action<T1> action1, Action<T5> action5, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T4> action4, Action<T2> action2, Action<T1> action1, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T4> action4, Action<T2> action2, Action<T5> action5, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T4> action4, Action<T5> action5, Action<T1> action1, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T4> action4, Action<T5> action5, Action<T2> action2, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T5> action5, Action<T1> action1, Action<T2> action2, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T5> action5, Action<T1> action1, Action<T4> action4, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T5> action5, Action<T2> action2, Action<T1> action1, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T5> action5, Action<T2> action2, Action<T4> action4, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T5> action5, Action<T4> action4, Action<T1> action1, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T3> action3, Action<T5> action5, Action<T4> action4, Action<T2> action2, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T1> action1, Action<T2> action2, Action<T5> action5, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T1> action1, Action<T3> action3, Action<T2> action2, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T1> action1, Action<T3> action3, Action<T5> action5, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T1> action1, Action<T5> action5, Action<T2> action2, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T1> action1, Action<T5> action5, Action<T3> action3, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T2> action2, Action<T1> action1, Action<T3> action3, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T2> action2, Action<T1> action1, Action<T5> action5, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T2> action2, Action<T3> action3, Action<T1> action1, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T2> action2, Action<T3> action3, Action<T5> action5, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T2> action2, Action<T5> action5, Action<T1> action1, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T2> action2, Action<T5> action5, Action<T3> action3, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T3> action3, Action<T1> action1, Action<T2> action2, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T3> action3, Action<T1> action1, Action<T5> action5, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T3> action3, Action<T2> action2, Action<T1> action1, Action<T5> action5)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T3> action3, Action<T2> action2, Action<T5> action5, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T3> action3, Action<T5> action5, Action<T1> action1, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T3> action3, Action<T5> action5, Action<T2> action2, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T5> action5, Action<T1> action1, Action<T2> action2, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T5> action5, Action<T1> action1, Action<T3> action3, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T5> action5, Action<T2> action2, Action<T1> action1, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T5> action5, Action<T2> action2, Action<T3> action3, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T5> action5, Action<T3> action3, Action<T1> action1, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T4> action4, Action<T5> action5, Action<T3> action3, Action<T2> action2, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T1> action1, Action<T2> action2, Action<T4> action4, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T1> action1, Action<T3> action3, Action<T2> action2, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T1> action1, Action<T3> action3, Action<T4> action4, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T1> action1, Action<T4> action4, Action<T2> action2, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T1> action1, Action<T4> action4, Action<T3> action3, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T2> action2, Action<T1> action1, Action<T3> action3, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T2> action2, Action<T1> action1, Action<T4> action4, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T2> action2, Action<T3> action3, Action<T1> action1, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T2> action2, Action<T4> action4, Action<T1> action1, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T2> action2, Action<T4> action4, Action<T3> action3, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T3> action3, Action<T1> action1, Action<T2> action2, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T3> action3, Action<T1> action1, Action<T4> action4, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T3> action3, Action<T2> action2, Action<T1> action1, Action<T4> action4)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T3> action3, Action<T2> action2, Action<T4> action4, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T3> action3, Action<T4> action4, Action<T1> action1, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T3> action3, Action<T4> action4, Action<T2> action2, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T4> action4, Action<T1> action1, Action<T2> action2, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T4> action4, Action<T1> action1, Action<T3> action3, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T4> action4, Action<T2> action2, Action<T1> action1, Action<T3> action3)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T4> action4, Action<T2> action2, Action<T3> action3, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T4> action4, Action<T3> action3, Action<T1> action1, Action<T2> action2)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
        
        public void Switch(Action<T5> action5, Action<T4> action4, Action<T3> action3, Action<T2> action2, Action<T1> action1)
        {
            this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
        }
    }
  
    public abstract class Union <T1, T2, T3, T4, T5, T6>
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
            this.case1.Do(action);
        }
        public void When(Action<T2> action)
        {
            this.case2.Do(action);
        }
        public void When(Action<T3> action)
        {
            this.case3.Do(action);
        }
        public void When(Action<T4> action)
        {
            this.case4.Do(action);
        }
        public void When(Action<T5> action)
        {
            this.case5.Do(action);
        }
        public void When(Action<T6> action)
        {
            this.case6.Do(action);
        }

        public void Switch(Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5, Action<T6> action6)
        { 
		    this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
            this.case6.Do(action6);
        }
    }
  
    public abstract class Union <T1, T2, T3, T4, T5, T6, T7>
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
            this.case1.Do(action);
        }
        public void When(Action<T2> action)
        {
            this.case2.Do(action);
        }
        public void When(Action<T3> action)
        {
            this.case3.Do(action);
        }
        public void When(Action<T4> action)
        {
            this.case4.Do(action);
        }
        public void When(Action<T5> action)
        {
            this.case5.Do(action);
        }
        public void When(Action<T6> action)
        {
            this.case6.Do(action);
        }
        public void When(Action<T7> action)
        {
            this.case7.Do(action);
        }

        public void Switch(Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5, Action<T6> action6, Action<T7> action7)
        { 
		    this.case1.Do(action1);
            this.case2.Do(action2);
            this.case3.Do(action3);
            this.case4.Do(action4);
            this.case5.Do(action5);
            this.case6.Do(action6);
            this.case7.Do(action7);
        }
    }
}

