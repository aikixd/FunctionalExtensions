using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aikixd.FunctionalExtensions.DiscriminatedUnions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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
    }
}
