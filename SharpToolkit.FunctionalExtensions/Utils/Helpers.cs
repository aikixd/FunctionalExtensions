using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SharpToolkit.FunctionalExtensions.Utils
{
    class IlHelpers
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string getRecordLineIndent(int i)
        {
            var r = "";

            for (; i >= 0; i -= 1)
                r += "  ";

            return r;
        }
    }
}
