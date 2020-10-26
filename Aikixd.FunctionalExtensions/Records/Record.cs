using Aikixd.FunctionalExtensions.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Aikixd.FunctionalExtensions
{
    /// <summary>
    /// Marker interface
    /// </summary>
    public interface IRecord
    {

    }

    public abstract class Record<T> : IRecord, IEquatable<T>
        where T : Record<T>
    {
        private readonly TypeUtils<T> utils;

        protected Record()
        {
            utils = TypeUtils<T>.Instance;
        }

        public T Copy(Func<RecordMemberCopy<T>, RecordMemberCopy<T>> alterationsFn)
        {
            var r = utils.CopyFn((T)this);

            var alterations = alterationsFn(new RecordMemberCopy<T>());

            foreach (var (member, val) in alterations)
                this.utils.SetMemberFnMap[member](r, val(r));
            
            return r;
        }

        public override bool Equals(object obj)
        {
            if (obj is T o)
                return this.Equals(o);

            return false;
        }

        public bool Equals(T other)
        {
            return utils.EqualsFn((T)this, other);
        }

        public override int GetHashCode()
        {
            return this.utils.GetHashCodeFn((T)this);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }

        public static bool operator ==(Record<T> x, Record<T> y) =>
            x.Equals((T)y);

        public static bool operator !=(Record<T> x, Record<T> y) =>
            !x.Equals((T)y);

        public override string ToString()
        {
            return this.ToString(0);
        }

        public string ToString(int indent)
        {
            return this.utils.ToStringFn((T)this, indent);
        }


#pragma warning disable IDE1006 // For VS debugging
        private string DebugView
#pragma warning restore IDE1006
        {
            get
            {
                return this.ToString();
            }
        }
    }
}
