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
        public class Email { }
        public class Phone { }

        public class ContactInfo : Union<ContactInfo.Email, ContactInfo.Phone>
        {
            public ContactInfo(Email @case) : base(@case)
            {
            }

            public ContactInfo(Phone @case) : base(@case)
            {
            }

            public class Email : Case<ContactInfo, UnionTests.Email>
            {
                public Email(UnionTests.Email value) : base(value)
                {
                }
            }

            public class Phone : Case<ContactInfo, UnionTests.Phone>
            {
                public Phone(UnionTests.Phone value) : base(value)
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

        [TestMethod]
        public void Union_Return()
        {
            Option<int> someNum = new Option<int>.Some(5);

            var r = someNum.Match(
                some => true,
                none => false);

            ContactInfo ci = new ContactInfo.Email(new Email());

            var o = ci.Match(email => (object)email.Value, phone => phone.Value);

            Assert.IsInstanceOfType(o, typeof(Email));
        }
    }
}
