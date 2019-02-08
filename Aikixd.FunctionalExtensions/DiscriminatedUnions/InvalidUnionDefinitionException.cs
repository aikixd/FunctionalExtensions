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
}
