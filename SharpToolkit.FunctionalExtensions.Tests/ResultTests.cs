using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpToolkit.FunctionalExtensions.Tests
{
    [TestClass]
    public class ResultTests
    {
        [TestMethod]
        public void ResultUnion_Casts()
        {
            Result r1 = new Result.Error(new ErrorResult(""));
            Result<int> r2 = new Result.Error(new ErrorResult(""));
        }

        [TestMethod]
        public void ResultUnion_Compare()
        {
            Assert.AreEqual(new Result.Ok(), new Result.Ok());
            Assert.AreEqual(new Result.Ok().ToUnion(), new Result.Ok().ToUnion());

            // Two identical errors are not same
            Assert.AreNotEqual(new Result.Error(new ErrorResult("1")), new Result.Error(new ErrorResult("1")));

            Assert.AreEqual(new Result<int>.Ok(1), new Result<int>.Ok(1));
            Assert.AreEqual(new Result<int>.Ok(1).ToUnion(), new Result<int>.Ok(1).ToUnion());

            Assert.AreNotEqual(new Result<int>.Ok(2), new Result<int>.Ok(1));
            Assert.AreNotEqual(new Result<int>.Ok(2).ToUnion(), new Result<int>.Ok(1).ToUnion());
        }

        [TestMethod]
        public void ResultUnion_CreateCaseAsUnion()
        {
            var result = new Result<int>.Ok(1).ToUnion();

            var result2 =
                result.Match(
                    ok => new Result.Ok().ToUnion(),
                    err => new Result.Error(err.Value));

            Assert.AreEqual(new Result.Ok(), result2.When<Result.Ok>(x => x));
        }
    }
}
