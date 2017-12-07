using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SharpToolkit.FunctionalExtensions.Tests
{
    [TestClass]
    public class RecordTests
    {
        class EqualityClass : IEqualityComparer<EqualityClass>
        {
            public int I { get; set; }

            public bool Equals(EqualityClass x, EqualityClass y)
            {
                return x.I == y.I;
            }

            public int GetHashCode(EqualityClass obj)
            {
                return I.GetHashCode();
            }

            public EqualityClass(int i)
            {
                this.I = i;
            }
        }

        class InnerRecord : Record<InnerRecord>
        {
            public readonly int One;
            public readonly int Two;

            public InnerRecord(int one, int two)
            {
                One = one;
                Two = two;
            }
        }

        class SomeRecord : Record<SomeRecord>
        {
            private int privateInt { get; }

            public int SomeInt { get; }
            public string SomeString { get; }


            public readonly int AnotherInt;
            private readonly int privateFieldInt;

            public InnerRecord Rec { get; }
            public List<int> List { get; }
            public EqualityClass equality { get; }


            public SomeRecord(
                int someInt, 
                string SomeString, 
                int anotherInt, 
                int privateInt, 
                int privateFieldInt,
                InnerRecord rec,
                List<int> list,
                EqualityClass equality)
            {
                this.SomeInt    = someInt;
                this.SomeString = SomeString;
                this.AnotherInt = anotherInt;
                this.privateInt = privateInt;
                this.privateFieldInt = privateFieldInt;
                this.Rec = rec;
                this.List = list;
                this.equality = equality;
            }

            public int GetHashCodeTest()
            {
                var i = 0;

                unchecked
                {
                    i += this.SomeInt.GetHashCode();
                    i += this.SomeString.GetHashCode();
                    i += this.AnotherInt.GetHashCode();
                    i += this.privateInt.GetHashCode();
                    i += this.privateFieldInt.GetHashCode();
                    i += this.Rec.GetHashCode();
                    i += this.List.GetHashCode();
                    i += this.equality.GetHashCode();
                }

                return i;
            }
        }

        [TestMethod]
        public void Records_AreEqual()
        {
            var list = new List<int>();

            var r1 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, new EqualityClass(1));
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, new EqualityClass(1));

            Assert.IsTrue(r1.Equals(r1, r2));
            Assert.IsTrue(r1 == r2);
        }

        [TestMethod]
        public void Records_AreNotEqual_PrivateProp()
        {
            var list = new List<int>();

            var r1 = new SomeRecord(1, "1", 2, 2, 4, new InnerRecord(1, 2), list, new EqualityClass(1));
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, new EqualityClass(1));

            Assert.IsFalse(r1.Equals(r1, r2));
            Assert.IsTrue(r1 != r2);
        }

        [TestMethod]
        public void Records_AreNotEqual_PublicProp()
        {
            var list = new List<int>();

            var r1 = new SomeRecord(1, "4", 2, 3, 4, new InnerRecord(1, 2), list, new EqualityClass(1));
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, new EqualityClass(1));

            Assert.IsFalse(r1.Equals(r1, r2));
            Assert.IsTrue(r1 != r2);
        }

        [TestMethod]
        public void Records_AreNotEqual_PublicField()
        {
            var list = new List<int>();

            var r1 = new SomeRecord(1, "4", 4, 3, 4, new InnerRecord(1, 2), list, new EqualityClass(1));
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, new EqualityClass(1));

            Assert.IsFalse(r1.Equals(r1, r2));
            Assert.IsTrue(r1 != r2);
        }

        [TestMethod]
        public void Records_AreNotEqual_PrivateField()
        {
            var list = new List<int>();

            var r1 = new SomeRecord(1, "4", 2, 3, 5, new InnerRecord(1, 2), list, new EqualityClass(1));
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, new EqualityClass(1));

            Assert.IsFalse(r1.Equals(r1, r2));
            Assert.IsTrue(r1 != r2);
        }

        [TestMethod]
        public void Records_AreNotEqual_InnerRecord()
        {
            var list = new List<int>();

            var r1 = new SomeRecord(1, "4", 2, 3, 4, new InnerRecord(1, 4), list, new EqualityClass(1));
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, new EqualityClass(1));

            Assert.IsFalse(r1.Equals(r1, r2));
            Assert.IsTrue(r1 != r2);
        }

        [TestMethod]
        public void Records_AreNotEqual_DotNetClass()
        {
            var list = new List<int>();

            var r1 = new SomeRecord(1, "4", 2, 3, 4, new InnerRecord(1, 2), list, new EqualityClass(1));
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), new List<int>(), new EqualityClass(1));

            Assert.IsFalse(r1.Equals(r1, r2));
            Assert.IsTrue(r1 != r2);
        }

        [TestMethod]
        public void Records_AreNotEqual_EqualityComparer()
        {
            var list = new List<int>();

            var r1 = new SomeRecord(1, "4", 2, 3, 4, new InnerRecord(1, 2), list, new EqualityClass(1));
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, new EqualityClass(2));

            Assert.IsFalse(r1.Equals(r1, r2));
            Assert.IsTrue(r1 != r2);
        }

        [TestMethod]
        public void Records_GetHashCode()
        {
            var list = new List<int>();

            var r = new SomeRecord(1, "4", 2, 3, 5, new InnerRecord(1, 2), list, new EqualityClass(1));

            Assert.AreEqual(r.GetHashCode(), r.GetHashCodeTest());
        }

        [TestMethod]
        public void Records_ToString()
        {
            var list = new List<int>();

            var r = new SomeRecord(1, "4", 2, 3, 5, new InnerRecord(1, 2), list, new EqualityClass(1));

            Assert.AreEqual(
                "{privateInt:3,SomeInt:1,SomeString:4,AnotherInt:2,privateFieldInt:5,Rec:{One:1,Two:2},List:System.Collections.Generic.List`1[System.Int32],equality:SharpToolkit.FunctionalExtensions.Tests.RecordTests+EqualityClass}",
                r.ToString().Replace(" ", "").Replace("\n", ""));
        }
    }
}
