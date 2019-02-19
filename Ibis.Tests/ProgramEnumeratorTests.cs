using Ibis.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibis.Tests
{
    [TestClass]
    public class ProgramEnumeratorTests
    {
        [TestMethod]
        public void Enumerate()
        {
            var program = "xyz";
            var enumerator = new ProgramEnumerator(program);

            Assert.AreEqual('x', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual('y', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual('z', enumerator.Current);
            Assert.AreEqual(false, enumerator.MoveNext());
        }

        [TestMethod]
        public void Backtracking()
        {
            var program = "abcd";
            var enumerator = new ProgramEnumerator(program);

            Assert.AreEqual('a', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual('b', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            enumerator.SetBacktrackPoint();
            Assert.AreEqual('c', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            enumerator.Backtrack();
            Assert.AreEqual('c', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual('d', enumerator.Current);
            Assert.AreEqual(false, enumerator.MoveNext());
        }

        [TestMethod]
        public void NestedBacktracking()
        {
            var program = "abcd";
            var enumerator = new ProgramEnumerator(program);

            Assert.AreEqual('a', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            enumerator.SetBacktrackPoint();
            Assert.AreEqual('b', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            enumerator.SetBacktrackPoint();
            Assert.AreEqual('c', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            enumerator.Backtrack();
            Assert.AreEqual('c', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            enumerator.Backtrack();
            Assert.AreEqual('b', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual('c', enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual('d', enumerator.Current);
            Assert.AreEqual(false, enumerator.MoveNext());
        }
    }
}
