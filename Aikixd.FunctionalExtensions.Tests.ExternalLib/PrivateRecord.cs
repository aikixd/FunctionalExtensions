using System;

using Aikixd.FunctionalExtensions;

namespace Aikixd.FunctionalExtensions.Tests.ExternalLib
{
    class PrivateRecord : Record<PrivateRecord>
    {
        public string Value { get; }

        public PrivateRecord(string value)
        {
            this.Value = value;
        }
    }
}
