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
        public void ResultUnion_CreateCaseAsUnion()
        {
            var result = new Result<int>.Ok(1).ToUnion;

            var result2 =
                result.Match(
                    ok => new Result.Ok().ToUnion,
                    err => new Result.Error(err.Value).ToUnion);

            Assert.AreEqual(new Result.Ok(), result2.When<Result.Ok>(x => x));
        }
    }
}
