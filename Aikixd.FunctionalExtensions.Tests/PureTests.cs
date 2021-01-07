using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aikixd.FunctionalExtensions.Tests
{
    [TestClass]
    public class PureTests
    {
        interface IFreeDivision
        {
            IO<int> Divide(int a, int b);
        }

        class FreeDiv : IFreeDivision
        {
            public IO<int> Divide(int a, int b)
            {
                if (b == 0)
                    return new Error<string>($"{a}/{b}");

                return new Ok<int>(a / b);
            }
        }

        private static async Pure<int> SingleBound(int a, int b, IFreeDivision freeIface)
        {
            var result = await freeIface.Divide(a, b);

            return result;
        }

        private static async Pure<int> DoubleBound(int a, int b, IFreeDivision freeIface)
        {
            var result = await freeIface.Divide(a, b);

            result = await freeIface.Divide(a, result);

            return result;
        }

        private static async Pure<int> PureSync(int a, int b)
        {
            return a + b;
        }


        [TestMethod]
        public async Task Bound_Success()
        {
            var a = 10;
            var b = 2;

            var result = await SingleBound(a, b, new FreeDiv());

            Assert.IsTrue(
                result.Match(
                    Ok => true,
                    error => false));
        }

        [TestMethod]
        public async Task Bound_2_Success()
        {
            var a = 12;
            var b = 2;

            var result = await DoubleBound(a, b, new FreeDiv());

            Assert.AreEqual(
                2,
                result.Match(
                    Ok => Ok.Value,
                    error => -1));
        }

        [TestMethod]
        public async Task Bound_2_Fail_1()
        {
            var a = 12;
            var b = 0;

            var result = await DoubleBound(a, b, new FreeDiv());

            Assert.AreEqual(
                "12/0",
                result.Match(
                    Ok => "",
                    error => error.Value));
        }

        [TestMethod]
        public async Task Bound_2_Fail_2()
        {
            var a = 1;
            var b = 2;

            var result = await DoubleBound(a, b, new FreeDiv());

            Assert.AreEqual(
                "1/0",
                result.Match(
                    Ok => "",
                    error => error.Value));
        }

        [TestMethod]
        public async Task Bound_Fail()
        {
            var a = 10;
            var b = 0;

            var result = await SingleBound(a, b, new FreeDiv());

            Assert.IsTrue(
                result.Match(
                    Ok => true,
                    error => false));
        }

        [TestMethod]
        public async Task NotBound()
        {
            var a = 10;
            var b = 2;

            var result = await PureTests.PureSync(a, b);

            Assert.IsTrue(
                result.Match(
                    Ok => false,
                    error => true));
        }
    }
}
