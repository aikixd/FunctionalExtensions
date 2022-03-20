using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aikixd.FunctionalExtensions.DiscriminatedUnions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Aikixd.FunctionalExtensions.Tests
{
    [TestClass]
    public class UnionTests
    {
        public class Email { }
        public class Phone { }

        public class ContactInfo : Union<Email, Phone>
        {
            public ContactInfo(Email @case) : base(@case)
            {
            }

            public ContactInfo(Phone @case) : base(@case)
            {
            }

        }

        public class A { }
        public class B { }
        public class C { }

        public class Synthetic : Union<A, B, C>
        {
            public Synthetic(A value) : base(value) { }

            public Synthetic(B value) : base(value) { }

            public Synthetic(C value) : base(value) { }
        }

        [TestMethod]
        public void Union_Create()
        {
            var some = new Option<int>.Some(5);

            Option<int> a = some;

            Option<int> b = new Option<int>.None();

            Assert.AreNotSame(a, b);
        }

        [TestMethod]
        public void Union_Return()
        {
            Option<int> someNum = new Option<int>.Some(5);

            var someNumResult = someNum.Match(
                some => true,
                none => false);

            ContactInfo ci = new ContactInfo(new Email());

            var contactInfoResult = ci.Match(email => (object)email, phone => phone);

            Assert.AreEqual(someNumResult, true);
            Assert.IsInstanceOfType(contactInfoResult, typeof(Email));
        }

        [TestMethod]
        public void Union_CaseCompare()
        {
            var none1 = new Option<int>.None();
            var none2 = new Option<int>.None();

            var noneOther = new Option<string>.None();

            var some1 = new Option<int>.Some(1);
            var some2 = new Option<int>.Some(1);
            var some3 = new Option<int>.Some(2);

            var someOther = new Option<string>.Some("str");

            Assert.IsTrue(none1.Equals(none2));
            Assert.IsTrue(none1.Equals((object)none2));
            Assert.IsTrue(none1 == none2);

            Assert.IsFalse(none1.Equals(noneOther));

            Assert.IsTrue(some1.Equals(some2));
            Assert.IsTrue(some1 == some2);
            Assert.IsTrue(some1.Equals((object)some2));

            Assert.IsFalse(some1.Equals(some3));
            Assert.IsFalse(some1 == some3);
            Assert.IsFalse(some1.Equals((object)some3));

            Assert.IsFalse(some1.Equals(someOther));

            Assert.IsFalse(none1.Equals(some1));
            Assert.IsFalse(some1.Equals(none1));
        }

        [TestMethod]
        public void Union_Compare()
        {
            var email = new Email();
            var ci1 = (ContactInfo) new ContactInfo(email);
            var ci2 = (ContactInfo) new ContactInfo(email);
            var ci3 = (ContactInfo)new ContactInfo(new Email());

            var none1 = (Option<int>) new Option<int>.None();
            var none2 = (Option<int>)new Option<int>.None();

            var noneOther = (Option<string>) new Option<string>.None();

            var some1 = (Option<int>)new Option<int>.Some(1);
            var some2 = (Option<int>)new Option<int>.Some(1);
            var some3 = (Option<int>)new Option<int>.Some(2);

            var someOther = (Option<string>)new Option<string>.Some("str");

            Assert.IsTrue(ci1.Equals(ci2));
            Assert.IsTrue(ci1 == ci2);
            Assert.IsTrue(ci1.Equals((object)ci2));
            Assert.IsFalse(ci1.Equals(ci3));
            Assert.IsFalse(ci1 == ci3);
            Assert.IsFalse(ci1.Equals((object)ci3));

            // Option struct asserts
            Assert.IsTrue(none1.Equals(none2));
            Assert.IsTrue(none1 == none2);
            Assert.IsTrue(none1.Equals((object)none2));

            Assert.IsFalse(none1.Equals(noneOther));

            Assert.IsTrue(some1.Equals(some2));
            Assert.IsTrue(some1 == some2);
            Assert.IsTrue(some1.Equals((object)some2));

            Assert.IsFalse(some1.Equals(some3));
            Assert.IsFalse(some1 == some3);
            Assert.IsFalse(some1.Equals((object)some3));

            Assert.IsFalse(some1.Equals(someOther));
        }

        [TestMethod]
        public void Union_Match_Fns_Async()
        {
            var union = new Synthetic(new B());

            
        }

        [TestMethod]
        public void Union_When_Actions()
        {
            var union = new Synthetic(new B());

            var t = "";
            var f = "";

            // Wrongs
            Assert.IsFalse(union.When((A _) => { t = "1A"; }));
            Assert.AreEqual("", t);
            Assert.IsFalse(union.When((C _) => { t = "1C"; }));
            Assert.AreEqual("", t);

            // Rights
            Assert.IsTrue(union.When((B _) => { t = "1B"; }));
            Assert.AreEqual("1B", t);

            t = "";
            f = "";

            // Wrongs
            Assert.IsFalse(union.When((A _) => { t = "2A"; }, () => { f = "2A-F"; }));
            Assert.AreEqual("", t);
            Assert.AreEqual("2A-F", f);
            Assert.IsFalse(union.When((C _) => { t = "2C"; }, () => { f = "2C-F"; }));
            Assert.AreEqual("", t);
            Assert.AreEqual("2C-F", f);

            // Rights
            f = "";
            Assert.IsTrue(union.When((B _) => { t = "2B"; }));
            Assert.AreEqual("2B", t);
            Assert.AreEqual("", f);

        }

        [TestMethod]
        public void Union_When_Fns()
        {
            var union = new Synthetic(new B());

            // Wrongs
            Assert.AreEqual("1A-F", union.When((A _) => "1A", "1A-F"));
            Assert.AreEqual("1C-F", union.When((C _) => "1C", "1C-F"));

            Assert.AreEqual("1A-F", union.When((A _) => "1A", () => "1A-F"));
            Assert.AreEqual("1C-F", union.When((C _) => "1C", () => "1C-F"));

            // Rights
            Assert.AreEqual("1B", union.When((B _) => "1B", "1B-F"));
            Assert.AreEqual("1B", union.When((B _) => "1B", () => "1B-F"));
        }

        [TestMethod]
        public async Task Union_When_Actions_Async()
        {
            var union = new Synthetic(new B());

            var t = "";
            var f = "";

            // Wrongs
            Assert.IsFalse(
                await union.WhenAsync(                 // Await in control context
                    async (A _) => {                   // Async 
                        var task = new Task(                
                            () => { t = "1A"; });      // The task
                        task.Start();
                        await task;                    // Await in nested context
                    })); 

            Assert.AreEqual("", t);
            Assert.IsFalse(
                await union.WhenAsync(                 // Await in control context
                    async (C _) => {                   // Async 
                        var task = new Task(                
                            () => { t = "1C"; });      // The task
                        task.Start();
                        await task;                    // Await in nested context
                    })); 
            Assert.AreEqual("", t);

            // Rights
            Assert.IsTrue(
                await union.WhenAsync(                 // Await in control context
                    async (B _) => {                   // Async 
                        var task = new Task(                
                            () => { t = "1B"; });      // The task
                        task.Start(); 
                        await task; }));               // Await in nested context
            Assert.AreEqual("1B", t);

            t = "";
            f = "";

            // Wrongs
            Assert.IsFalse(
                await union.WhenAsync(                 // Await in control context
                    async (A _) => {                   // Async 
                        var task = new Task(                
                            () => { t = "2A"; });      // The task
                        task.Start(); 
                        await task; },                 // Await in nested context
                    async () => {
                        var task = new Task(
                            () => { f = "2A-F"; });
                        task.Start();
                        await task;
                    }));
            Assert.AreEqual("", t);
            Assert.AreEqual("2A-F", f);
            Assert.IsFalse(
                await union.WhenAsync(                 // Await in control context
                    async (A _) => {                   // Async 
                        var task = new Task(                
                            () => { t = "2C"; });      // The task
                        task.Start(); 
                        await task; },                 // Await in nested context
                    async () => {
                        var task = new Task(
                            () => { f = "2C-F"; }); 
                        task.Start(); 
                        await task; }));
            Assert.AreEqual("", t);
            Assert.AreEqual("2C-F", f);

            // Rights
            f = "";
            Assert.IsTrue(
                await union.WhenAsync(                 // Await in control context
                    async (B _) => {                   // Async 
                        var task = new Task(                
                            () => { t = "2B"; });      // The task
                        task.Start(); 
                        await task; },                 // Await in nested context
                    async () => {
                        var task = new Task(
                            () => { f = "2B-F"; }); 
                        task.Start(); 
                        await task; }));
            Assert.AreEqual("2B", t);
            Assert.AreEqual("", f);

        }

        [TestMethod]
        public async Task Union_When_Fns_Async()
        {
            var union = new Synthetic(new B());

            // Wrongs
            Assert.AreEqual("1A-F", await union.When(async (A _) => "1A", async () => "1A-F"));
            Assert.AreEqual("1C-F", await union.When(async (C _) => "1C", async () => "1C-F"));

            Assert.AreEqual("1A-F", await union.When((A _) => Task.FromResult("1A"), Task.FromResult("1A-F")));
            Assert.AreEqual("1C-F", await union.When((C _) => Task.FromResult("1C"), Task.FromResult("1C-F")));

            Assert.AreEqual("1A-F", await union.When(async (A _) => await Task.FromResult("1A"), async () => await Task.FromResult("1A-F")));
            Assert.AreEqual("1C-F", await union.When(async (C _) => await Task.FromResult("1C"), async () => await Task.FromResult("1C-F")));

            // Rights
            Assert.AreEqual("1B", await union.When(async (B _) => "1B", async () => "1B-F"));
            Assert.AreEqual("1B", await union.When((B _) => Task.FromResult("1B"), Task.FromResult("1B-F")));
            Assert.AreEqual("1B", await union.When(async (B _) => await Task.FromResult("1B"), async () => await Task.FromResult("1B-F")));
        }
    }
}
