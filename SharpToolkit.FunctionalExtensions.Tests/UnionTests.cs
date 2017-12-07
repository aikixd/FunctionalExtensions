using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpToolkit.FunctionalExtensions.DiscriminatedUnions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SharpToolkit.FunctionalExtensions.Tests
{
    [TestClass]
    public class UnionTests
    {
        class Option<T> : Union<Option<T>.Some, Option<T>.None>
        {
            public Option(Some @case) : base(@case)
            {
            }

            public Option(None @case) : base(@case)
            {
            }

            public class None : Case<Option<T>> { }
            public class Some : Case<Option<T>, T>
            {
                public Some(T value) : base(value)
                {
                }
            }
        }

        [TestMethod]
        public void Union_Create()
        {
            var some = new Option<int>.Some(5);

            Option<int> a = some;

            Option<int> b = new Option<int>.None();

            Assert.AreNotSame(a, b);

            Debugger.Break();
        }
    }
}
