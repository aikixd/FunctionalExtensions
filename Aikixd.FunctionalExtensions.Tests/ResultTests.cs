using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

namespace Aikixd.FunctionalExtensions.Tests
{
    [TestClass]
    public class ResultTests
    {
        record AppError(string Message);

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
                ok.Then(() => new Result<int>(new Ok()));

            var resultError =
                error.Then(() => new Result<int>(new Error<int>(2)));

            var resultError2 =
                ok.Then(() => new Result<int>(new Error<int>(2)));

            Assert.AreEqual(new Result<int>(new Ok()), resultOk);
            Assert.AreEqual(new Result<int>(new Error<int>(1)), resultError);
            Assert.AreEqual(new Result<int>(new Error<int>(2)), resultError2);
        }

        [TestMethod]
        public async Task ResultUnion_Bind_Async()
        {
            var ok = new Result<int>(new Ok());
            var error = new Result<int>(new Error<int>(1));

            var resultOk =
                await ok.ThenAsync(async () => {
                    await Task.Delay(50);
                    return new Result<int>(new Ok()); 
                });

            var resultError =
                await error.ThenAsync(async () =>
                {
                    await Task.Delay(50);
                    return new Result<int>(new Error<int>(2));
                });

            Assert.AreEqual(new Result<int>(new Ok()), resultOk);
            Assert.AreEqual(new Result<int>(new Error<int>(1)), resultError);
        }

        [TestMethod]
        public async Task ResultUnion_BindAsync_Ok_Sync_Async_Async()
        {
            Result<string, AppError> result = Ok.New("A");

            var r1 =
                await result
                    .ThenAsync((ok) => Task.FromResult(new Result<string, AppError>(ok + "B")))
                    .ThenAsync(async (ok) => {
                        await Task.Delay(1);
                        return new Result<string, AppError>(Ok.New(ok + "C"));
                    });

            Assert.AreEqual(new Result<string, AppError>(Ok.New("ABC")), r1);
        }

        [TestMethod]
        public async Task ResultUnion_BindAsync_Ok_Sync_Async_Sync()
        {
            Result<string, AppError> result = Ok.New("A");

            var r1 =
                await result
                    .ThenAsync((ok) => Task.FromResult(new Result<string, AppError>(ok + "B")))
                    .Then((ok) => new Result<string, AppError>(Ok.New(ok + "C")));

            Assert.AreEqual(new Result<string, AppError>(Ok.New("ABC")), r1);
        }

        [TestMethod]
        public async Task ResultUnion_BindAsync_Error_Sync_Async_Async()
        {
            Result<string, string> result = Error.New("A");

            var r1 =
                await result
                    .ThenAsync((ok) => Task.FromResult(new Result<string, string>(Ok.New(ok + "B"))))
                    .ThenAsync(async (ok) => {
                        await Task.Delay(1);
                        return new Result<string, string>(Ok.New(ok + "C"));
                    });

            Assert.AreEqual(new Result<string, string>(Error.New("A")), r1);
        }

        [TestMethod]
        public async Task ResultUnion_BindAsync_Error_Sync_Async_Sync()
        {
            Result<string, string> result = Error.New("A");

            var r1 =
                await result
                    .ThenAsync((ok) => Task.FromResult(new Result<string, string>(Ok.New(ok + "B"))))
                    .Then((ok) => new Result<string, string>(Ok.New(ok + "C")));

            Assert.AreEqual(new Result<string, string>(Error.New("A")), r1);
        }

        [TestMethod]
        public async Task ResultUnion_BindAsync_Error_Sync_Async_Async_Intermediate()
        {
            Result<string, string> result = Ok.New("A");

            var r1 =
                await result
                    .ThenAsync((ok) => Task.FromResult(new Result<string, string>(Error.New(ok + "B"))))
                    .ThenAsync(async (ok) => {
                        await Task.Delay(1);
                        return new Result<string, string>(Ok.New(ok + "C"));
                    });

            Assert.AreEqual(new Result<string, string>(Error.New("AB")), r1);
        }

        [TestMethod]
        public async Task ResultUnion_BindAsync_Error_Sync_Async_Sync_Intermediate()
        {
            Result<string, string> result = Ok.New("A");

            var r1 =
                await result
                    .ThenAsync((ok) => Task.FromResult(new Result<string, string>(Error.New(ok + "B"))))
                    .Then((ok) => new Result<string, string>(Ok.New(ok + "C")));

            Assert.AreEqual(new Result<string, string>(Error.New("AB")), r1);
        }

        [TestMethod]
        public void ResultUnion_Generic_Bind()
        {
            var ok = new Result<int, int>(new Ok<int>(123));
            var error = new Result<string, int>(new Error<int>(1));

            var resultOk =
                ok.Then(x => new Result<string, int>(new Ok<string>(x.ToString())));

            var resultError =
                error.Then(x => new Result<string, int>(new Error<int>(2)));

            var resultError2 =
                ok.Then(x => new Result<string, int>(new Error<int>(2)));

            Assert.AreEqual(new Result<string, int>(new Ok<string>("123")), resultOk);
            Assert.AreEqual(new Result<string, int>(new Error<int>(1)), resultError);
            Assert.AreEqual(new Result<string, int>(new Error<int>(2)), resultError2);
        }

        [TestMethod]
        public void ResultUnion_Generic_Bind_Leaf()
        {
            var ok = new Result<int, int>(new Ok<int>(123));

            var resultOk =
                ok.Then(x => new Result<int>(new Ok()));
                
            Assert.AreEqual(new Result<int>(new Ok()), resultOk);
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
