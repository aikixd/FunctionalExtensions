using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Aikixd.FunctionalExtensions.Records
{
    public class RecordMemberCopy<TRecord> : IEnumerable<(MemberInfo member, Func<TRecord, object> fn)>
    {
        private List<(MemberInfo, Func<TRecord, object?>)> alterations;

        internal RecordMemberCopy()
        {
            // TODO: initialize with half members count
            alterations = new List<(MemberInfo, Func<TRecord, object?>)>();
        }

        public IEnumerator<(MemberInfo member, Func<TRecord, object> fn)> GetEnumerator()
        {
            return this.alterations.GetEnumerator();
        }

        public RecordMemberCopy<TRecord> With<TMember>(
            Expression<Func<TRecord, TMember>> memberAccess,
            TMember newVal)
        {
            alterations.Add((((MemberExpression)memberAccess.Body).Member, (x) => newVal));

            return this;
        }

        public RecordMemberCopy<TRecord> With<TMember>(
            Expression<Func<TRecord, TMember>> memberAccess,
            Func<TRecord, TMember> newValFn)
        {
            alterations.Add((((MemberExpression)memberAccess.Body).Member, (x) => newValFn(x)));

            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.alterations.GetEnumerator();
        }
    }
}
