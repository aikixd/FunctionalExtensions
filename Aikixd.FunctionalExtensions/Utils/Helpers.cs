using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Aikixd.FunctionalExtensions.Utils
{
    class IlHelpers
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetRecordLineIndent(int i)
        {
            var r = "";

            for (; i >= 0; i -= 1)
                r += "  ";

            return r;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForceAssign(object target, object value, FieldInfo nfo)
        {
            nfo.SetValue(target, value);
        }

        public static string StripBakingFieldName(string name)
        {
            return name.Substring(1, name.IndexOf('>') - 1);
        }
    }
}
