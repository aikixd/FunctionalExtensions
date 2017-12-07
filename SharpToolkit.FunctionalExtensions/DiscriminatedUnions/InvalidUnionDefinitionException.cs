using System;
using System.Collections.Generic;
using System.Text;

namespace SharpToolkit.FunctionalExtensions.DiscriminatedUnions
{
    public class InvalidUnionDefinitionException : Exception
    {
        public InvalidUnionDefinitionException(string message) : base(message)
        {
        }
    }
}
