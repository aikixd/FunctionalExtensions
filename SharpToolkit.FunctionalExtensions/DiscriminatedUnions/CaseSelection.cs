using System;
using System.Collections.Generic;
using System.Text;

namespace SharpToolkit.FunctionalExtensions.DiscriminatedUnions
{
    abstract class CaseSelection<TCase>
    {
        internal abstract void Do(Action<TCase> action);
    }

    class SelectedCase<TCase> : CaseSelection<TCase>
    {
        TCase @case;

        internal SelectedCase(TCase @case)
        {
            this.@case = @case;
        }

        internal override void Do(Action<TCase> action)
        {
            action(this.@case);
        }
    }

    class UnselectedCase<TCase> : CaseSelection<TCase>
    {
        internal UnselectedCase() { }

        internal override void Do(Action<TCase> action)
        {

        }
    }
}
