using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Aikixd.FunctionalExtensions.Tests
{
    [TestClass]
    public class OptionTests
    {
        [TestMethod]
        public void Option_Cast()
        {
            Option<string> r1 = new Option<string>.Some("123");
            Option<string> r2 = new Option<string>.None();
        }

        [TestMethod]
        public void Option_Compare()
        {
            Assert.AreEqual(new Option<int>.None(), new Option<int>.None());
            Assert.AreEqual(new Option<int>(new Option<int>.None()), new Option<int>.None());
            Assert.AreEqual(new Option<int>(new Option<int>.None()), new Option<int>(new Option<int>.None()));

            Assert.AreNotEqual(new Option<int>.None(), new Option<int>.Some(1));
            Assert.AreNotEqual(new Option<string>.None(), new Option<int>.Some(1));
            Assert.AreNotEqual(new Option<int>(new Option<int>.None()), new Option<int>.Some(1));
            Assert.AreNotEqual(new Option<string>.None(), new Option<string>(new Option<string>.Some("abc")));
            Assert.AreNotEqual(new Option<int>(new Option<int>.None()), new Option<int>(new Option<int>.Some(1)));

            Assert.AreEqual(new Option<int>.Some(1), new Option<int>.Some(1));
            Assert.AreNotEqual(new Option<int>.Some(1), new Option<int>.Some(2));
            Assert.AreEqual(new Option<int>(new Option<int>.Some(1)), new Option<int>.Some(1));
            Assert.AreEqual(new Option<int>(new Option<int>.Some(1)), new Option<int>(new Option<int>.Some(1)));
            Assert.AreNotEqual(new Option<int>(new Option<int>.Some(1)), new Option<int>.Some(2));
            Assert.AreNotEqual(new Option<int>(new Option<int>.Some(1)), new Option<int>(new Option<int>.Some(2)));
        }

        [TestMethod]
        public void Option_Correctness()
        {
            Option<int> oIntNone = new Option<int>.None();
            Option<int> oIntSome = new Option<int>.Some(1);

            Assert.AreEqual(oIntSome.When((Option<int>.Some x) => x.Value, () => throw new Exception()), 1);
            Assert.AreEqual(oIntNone.When((Option<int>.Some _) => throw new Exception(), () => 2), 2);
            Assert.IsTrue(oIntSome.When((Option<int>.Some _) => { }));
            Assert.IsFalse(oIntSome.When((Option<int>.None _) => { }));

            oIntSome.Match(
                _ => { },
                _ => { Assert.Fail(); });

            oIntNone.Match(
                x => { Assert.Fail(); },
                x => { });
        }
    }
}
