using System;
using System.Collections.Generic;
using System.Text;

namespace SharpToolkit.FunctionalExtensions.DiscriminatedUnions
{
    abstract class CaseSelection<TCase>
    {
        internal abstract bool Do<TResult>(Func<TCase, TResult> fn, out TResult result);
    }

    class SelectedCase<TCase> : CaseSelection<TCase>
    {
        TCase @case;

        internal SelectedCase(TCase @case)
        {
            this.@case = @case;
        }

        internal override bool Do<TResult>(Func<TCase, TResult> fn, out TResult result)
        {
            result = fn(this.@case);

            return true;
        }
    }

    class UnselectedCase<TCase> : CaseSelection<TCase>
    {
        internal UnselectedCase() { }

        internal override bool Do<TResult>(Func<TCase, TResult> fn, out TResult result)
        {
            result = default;

            return false;
        }
    }
}
