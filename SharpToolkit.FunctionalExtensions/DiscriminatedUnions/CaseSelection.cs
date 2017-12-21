using System;
using System.Collections.Generic;
using System.Text;

namespace SharpToolkit.FunctionalExtensions.DiscriminatedUnions
{
    abstract class CaseSelection<TCase> : IEquatable<CaseSelection<TCase>>
        where TCase : Case
    {
        public abstract bool Equals(CaseSelection<TCase> other);
        internal abstract bool Do<TResult>(Func<TCase, TResult> fn, out TResult result);
    }

    class SelectedCase<TCase> : CaseSelection<TCase>
        where TCase : Case
    {
        TCase @case;

        internal SelectedCase(TCase @case)
        {
            this.@case = @case;
        }

        public override bool Equals(CaseSelection<TCase> other)
        {
            if (other is SelectedCase<TCase> o)
                return this.@case.Equals(o.@case);

            return false;
        }

        internal override bool Do<TResult>(Func<TCase, TResult> fn, out TResult result)
        {
            result = fn(this.@case);

            return true;
        }
    }

    class UnselectedCase<TCase> : CaseSelection<TCase>
        where TCase : Case
    {
        internal UnselectedCase() { }

        public override bool Equals(CaseSelection<TCase> other)
        {
            if (other is UnselectedCase<TCase>)
                return true;

            return false;
        }

        internal override bool Do<TResult>(Func<TCase, TResult> fn, out TResult result)
        {
            result = default;

            return false;
        }
    }
}
