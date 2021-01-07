using System;

#nullable disable

namespace Aikixd.FunctionalExtensions.Records
{
    [Serializable]
    public class RecordVisibilityException : Exception
    {
        public Type RecordType { get; }
        public RecordVisibilityException(Type recordType, Exception internalException) : base(
            "Record types must be defined public, as C# doesn't allow inspecting private types in external assemblies.",
            internalException) 
        {
            this.RecordType = recordType;
        }
    }
}

#nullable restore