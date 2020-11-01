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

        [TestMethod]
        public void ResultUnion_Bind()
        {
            var ok = new Result<int>(new Ok());
            var error = new Result<int>(new Error<int>(1));

            var resultOk =
                ok.Bind(() => new Result<int>(new Ok()));

            var resultError =
                error.Bind(() => new Result<int>(new Error<int>(2)));

            var resultError2 =
                ok.Bind(() => new Result<int>(new Error<int>(2)));

            Assert.AreEqual(new Result<int>(new Ok()), resultOk);
            Assert.AreEqual(new Result<int>(new Error<int>(1)), resultError);
            Assert.AreEqual(new Result<int>(new Error<int>(2)), resultError2);
        }

        [TestMethod]
        public void ResultUnion_BindConvert()
        {
            var error = new Result<int>(new Error<int>(1));

            var resultError =
                error.Bind(() => new Result<string>(new Error<string>("one")), x => x.ToString());

            Assert.AreEqual(new Result<string>(new Error<string>("1")), resultError);
        }

        [TestMethod]
        public void ResultUnion_Generic_Bind()
        {
            var ok = new Result<int, int>(new Ok<int>(123));
            var error = new Result<string, int>(new Error<int>(1));

            var resultOk =
                ok.Bind(x => new Result<string, int>(new Ok<string>(x.ToString())));

            var resultError =
                error.Bind(x => new Result<string, int>(new Error<int>(2)));

            var resultError2 =
                ok.Bind(x => new Result<string, int>(new Error<int>(2)));

            Assert.AreEqual(new Result<string, int>(new Ok<string>("123")), resultOk);
            Assert.AreEqual(new Result<string, int>(new Error<int>(1)), resultError);
            Assert.AreEqual(new Result<string, int>(new Error<int>(2)), resultError2);
        }

        [TestMethod]
        public void ResultUnion_Generic_Bind_Leaf()
        {
            var ok = new Result<int, int>(new Ok<int>(123));

            var resultOk =
                ok.Bind(x => new Result<int>(new Ok()));
                
            Assert.AreEqual(new Result<int>(new Ok()), resultOk);
        }

        [TestMethod]
        public void ResultUnion_Generic_BindConvert()
        {
            var error = new Result<string,int>(new Error<int>(1));

            var resultError =
                error.Bind(x => new Result<string, string>(new Error<string>("one")), x => x.ToString());

            Assert.AreEqual(new Result<string, string>(new Error<string>("1")), resultError);
        }

        [TestMethod]
        public void ResulUnion_Select()
        {
            var ok = new Result<int>(new Ok());
            var error = new Result<int>(new Error<int>(1));

            var resultOk = ok.Select(x => x.ToString());
            var resultError = error.Select(x => x.ToString());

            Assert.AreEqual(new Result<string>(new Ok()), resultOk);
            Assert.AreEqual(new Result<string>(new Error<string>("1")), resultError);
        }

        [TestMethod]
        public void ResulUnion_Generic_Select()
        {
            var ok = new Result<int, int>(new Ok<int>(1));
            var error = new Result<int, int>(new Error<int>(1));

            var resultOk = ok.Select(x => x.ToString(), x => (x + x).ToString());
            var resultError = error.Select(x => x.ToString(), x => (x + x).ToString());

            Assert.AreEqual(new Result<string, string>(new Ok<string>("1")), resultOk);
            Assert.AreEqual(new Result<string, string>(new Error<string>("2")), resultError);
        }
    }
}
