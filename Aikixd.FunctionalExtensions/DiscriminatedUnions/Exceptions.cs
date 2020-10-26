using System;
using System.Collections.Generic;
using System.Text;

namespace Aikixd.FunctionalExtensions.DiscriminatedUnions
{
    public class InvalidUnionDefinitionException : Exception
    {
        public InvalidUnionDefinitionException(string message) : base(message)
        {
        }
    }


    [Serializable]
    public class UnionCaseNullException : Exception
    {
        private const string MSG =
            "Union case cannot be null, as it is impossible to differentiate null values. " +
            "Use empty types to represent a lack of value, e.g. Unit.";

        public UnionCaseNullException() : base(MSG) { }
        public UnionCaseNullException(Exception inner) : base(MSG, inner) { }
        protected UnionCaseNullException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class InvalidUnionStateException : Exception
    {
        private const string MSG =
            "Union state is invalid. Either it's value is null or outside of the defined " +
            "range of allowed types.";

        public InvalidUnionStateException() : base(MSG) { }
        public InvalidUnionStateException(Exception inner) : base(MSG, inner) { }
        protected InvalidUnionStateException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
