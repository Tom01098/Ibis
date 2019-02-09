using Ibis.EBNF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibis.Tests
{
    [TestClass]
    public class GrammarEnumeratorTests
    {
        [TestMethod]
        public void Enumerate()
        {
            var str = "a b";
            var enumerator = new GrammarEnumerator(str);

            Assert.AreEqual('a', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual(' ', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual('b', enumerator.Current);
            Assert.AreEqual(false, enumerator.MoveNext());
        }

        [TestMethod]
        public void Enumerate2()
        {
            var str = "a bc";
            var enumerator = new GrammarEnumerator(str);

            Assert.AreEqual('a', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual(' ', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual('b', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual('c', enumerator.Current);
            Assert.AreEqual(false, enumerator.MoveNext());
        }

        [TestMethod]
        public void Enumerate3()
        {
            var str = @"x
y";
            var enumerator = new GrammarEnumerator(str);

            Assert.AreEqual('x', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual('\r', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual('\n', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual('y', enumerator.Current);
            Assert.AreEqual(false, enumerator.MoveNext());
        }
    }
}
