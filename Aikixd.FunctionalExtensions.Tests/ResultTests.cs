using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aikixd.FunctionalExtensions.Tests
{
    [TestClass]
    public class ResultTests
    {
        [TestMethod]
        public void ResultUnion_Casts()
        {
            Result<ErrorResult> r1 = new ErrorResult("");
            Result<int, ErrorResult> r2 = new ErrorResult("");
        }

        [TestMethod]
        public void ResultUnion_Compare()
        {
            Assert.AreEqual(new Ok(), new Ok());
            Assert.AreEqual(new Result<int>(new Ok()), new Result<int>(new Ok()));

            Assert.AreEqual(new Ok<int>(1), new Ok<int>(1));
            Assert.AreEqual(new Result<int, int>(new Ok<int>(1)), new Result<int, int>(new Ok<int>(1)));
            Assert.AreEqual(new Result<int, string>(1), (Result<int, string>)new Ok<int>(1));

            Assert.AreNotEqual(new Ok<int>(2), new Ok<int>(1));
            Assert.AreNotEqual(new Result<int, string>(2), new Result<int, string>(1));
        }

    }
}
