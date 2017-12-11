using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace SharpToolkit.FunctionalExtensions.Tests
{
    [TestClass]
    public class RecordTests
    {
        public class EqualityClass : IEqualityComparer<EqualityClass>
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

        public class EvenNumber : Record<EvenNumber>
        {
            public int Value { get; }

            public EvenNumber(int i)
            {
                this.Value = i;
            }

            public override Result Validate()
            {
                if (this.Value % 2 == 0)
                    return new Result.Ok();

                return new Result.Error(new ErrorResult("Validation failed.", new[] { ("Reason", $"{this.Value} is not even.") }));
            }
        }

        public class InnerRecord : Record<InnerRecord>
        {
            public readonly int One;
            public readonly int Two;

            public InnerRecord(int one, int two)
            {
                One = one;
                Two = two;
            }
        }

        public class SomeRecord : Record<SomeRecord>
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
                this.SomeInt = someInt;
                this.SomeString = SomeString;
                this.privateInt = privateInt;
                this.AnotherInt = anotherInt;
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
                    i += this.privateInt.GetHashCode();
                    i += this.AnotherInt.GetHashCode();
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
            var eqClass = new EqualityClass(1);

            var r1 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, eqClass);
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, eqClass);

            Assert.IsTrue(r1.Equals(r2));
            Assert.IsTrue(r1 == r2);
        }

        [TestMethod]
        public void Records_AreNotEqual_PrivateProp()
        {
            var list = new List<int>();
            var eqClass = new EqualityClass(1);

            var r1 = new SomeRecord(1, "1", 2, 2, 4, new InnerRecord(1, 2), list, eqClass);
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, eqClass);

            Assert.IsFalse(r1.Equals(r2));
            Assert.IsTrue(r1 != r2);
        }

        [TestMethod]
        public void Records_AreNotEqual_PublicProp()
        {
            var list = new List<int>();
            var eqClass = new EqualityClass(1);

            var r1 = new SomeRecord(1, "4", 2, 3, 4, new InnerRecord(1, 2), list, eqClass);
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, eqClass);

            Assert.IsFalse(r1.Equals(r2));
            Assert.IsTrue(r1 != r2);
        }

        [TestMethod]
        public void Records_AreNotEqual_PublicField()
        {
            var list = new List<int>();
            var eqClass = new EqualityClass(1);

            var r1 = new SomeRecord(1, "4", 4, 3, 4, new InnerRecord(1, 2), list, eqClass);
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, eqClass);

            Assert.IsFalse(r1.Equals(r2));
            Assert.IsTrue(r1 != r2);
        }

        [TestMethod]
        public void Records_AreNotEqual_PrivateField()
        {
            var list = new List<int>();
            var eqClass = new EqualityClass(1);

            var r1 = new SomeRecord(1, "4", 2, 3, 5, new InnerRecord(1, 2), list, eqClass);
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, eqClass);

            Assert.IsFalse(r1.Equals(r2));
            Assert.IsTrue(r1 != r2);
        }

        [TestMethod]
        public void Records_AreNotEqual_InnerRecord()
        {
            var list = new List<int>();
            var eqClass = new EqualityClass(1);

            var r1 = new SomeRecord(1, "4", 2, 3, 4, new InnerRecord(1, 4), list, eqClass);
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, eqClass);

            Assert.IsFalse(r1.Equals(r2));
            Assert.IsTrue(r1 != r2);
        }

        [TestMethod]
        public void Records_AreNotEqual_DotNetClass()
        {
            var list = new List<int>();
            var eqClass = new EqualityClass(1);

            var r1 = new SomeRecord(1, "4", 2, 3, 4, new InnerRecord(1, 2), list, eqClass);
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), new List<int>(), eqClass);

            Assert.IsFalse(r1.Equals(r2));
            Assert.IsTrue(r1 != r2);
        }

        [TestMethod]
        public void Records_AreNotEqual_EqualityComparer()
        {
            var list = new List<int>();

            var r1 = new SomeRecord(1, "4", 2, 3, 4, new InnerRecord(1, 2), list, new EqualityClass(1));
            var r2 = new SomeRecord(1, "1", 2, 3, 4, new InnerRecord(1, 2), list, new EqualityClass(2));

            Assert.IsFalse(r1.Equals(r2));
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

        [TestMethod]
        public void Record_Copy()
        {
            var list = new List<int>();

            var r1 = new SomeRecord(1, "4", 2, 3, 5, new InnerRecord(1, 2), list, new EqualityClass(1));

            var r2 = r1.Copy(x => x);

            Assert.AreEqual(r1, r2);
            Assert.AreNotSame(r1, r2);
        }

        [TestMethod]
        public void Record_With()
        {
            var list = new List<int>();

            var r1 = new SomeRecord(1, "4", 2, 3, 5, new InnerRecord(1, 2), list, new EqualityClass(1));

            var r2 = r1.Copy(x =>
                x
                .With(y => y.SomeInt, 5)
                .With(y => y.SomeString, "Another string"));

            Debugger.Break();

            Assert.AreNotEqual(r1, r2);

            Assert.AreEqual(r2.SomeInt, 5);
            Assert.AreEqual(r2.SomeString, "Another string");
        }

        [TestMethod]
        public void Record_With_InnerRecord()
        {
            var list = new List<int>();

            var r1 = new SomeRecord(1, "4", 2, 3, 5, new InnerRecord(1, 2), list, new EqualityClass(1));

            var r2 = r1.Copy(x =>
                x.With(
                    y => y.Rec,
                    y => y.Rec.Copy(x_ =>
                        x_.With(
                            y_ => y_.One,
                            10))));

            Debugger.Break();

            Assert.AreNotEqual(r1.Rec, r2.Rec);

            Assert.AreEqual(10, r2.Rec.One);
        }

        [TestMethod]
        public void Record_Validation()
        {
            var v1 = EvenNumber.Valid(new EvenNumber(4));
            var v2 = EvenNumber.Valid(new EvenNumber(3));

            v1.Match(
                ok => { },
                err => { Assert.Fail(); });

            v2.Match(
                ok => { Assert.Fail(); },
                err => { });
        }
    }
}
