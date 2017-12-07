using SharpToolkit.FunctionalExtensions.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpToolkit.FunctionalExtensions
{
    /// <summary>
    /// Marker interface
    /// </summary>
    internal interface IRecord
    {

    }

    public abstract class Record<T> : IRecord, IEqualityComparer<T>
        where T : Record<T>
    {
        private readonly TypeUtils<T> utils;

        protected Record()
        {
            utils = TypeUtils<T>.Instance;
        }

        public virtual bool Equals(T x, T y)
        {
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
                return false;

            return utils.EqualsFn(x, y);
        }

        public override bool Equals(object obj)
        {
            if (obj is T o)
                return this.Equals((T)this, o);

            return false;
        }

        public int GetHashCode(T obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return this.utils.GetHashCodeFn((T)this);
        }

        public static bool operator ==(Record<T> x, Record<T> y) =>
            x.Equals((T)x, (T)y);

        public static bool operator !=(Record<T> x, Record<T> y) =>
            !x.Equals((T)x, (T)y);

        public override string ToString()
        {
            return this.ToString(0);
        }

        public string ToString(int indent)
        {
            return this.utils.ToStringFn((T)this, indent);
        }

        private string DebugView
        {
            get
            {
                return this.ToString();
            }
        }
    }
}
