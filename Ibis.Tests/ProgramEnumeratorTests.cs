using Ibis.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibis.Tests
{
    [TestClass]
    public class ProgramEnumeratorTests
    {
        [TestMethod]
        public void Matching()
        {
            var program = "word otherWord";
            var enumerator = new ProgramEnumerator(program);

            Assert.AreEqual(true, enumerator.Matches("word"));
            enumerator.Advance(4);
            Assert.AreEqual(true, enumerator.Matches(" "));
            enumerator.Advance(1);
            Assert.AreEqual(true, enumerator.Matches("otherWord"));
            enumerator.Advance(9);
            Assert.AreEqual(true, enumerator.IsFinished);
        }

        [TestMethod]
        public void MatchingOutOfBounds()
        {
            var program = "abc";
            var enumerator = new ProgramEnumerator(program);

            Assert.AreEqual(false, enumerator.Matches("abcd"));
            enumerator.Advance(4);
            Assert.AreEqual(true, enumerator.IsFinished);
        }

        [TestMethod]
        public void Backtracking()
        {
            var program = "func FuncName = 3 * 4";
            var enumerator = new ProgramEnumerator(program);

            Assert.AreEqual(true, enumerator.Matches("func"));
            enumerator.Advance(4);
            Assert.AreEqual(true, enumerator.Matches(" "));
            enumerator.Advance(1);
            Assert.AreEqual(true, enumerator.Matches("FuncName"));
            enumerator.Advance(8);
            Assert.AreEqual(true, enumerator.Matches(" "));
            enumerator.Advance(1);
            enumerator.SetBacktrackPoint();
            Assert.AreEqual(false, enumerator.Matches("'"));
            enumerator.Advance(1);
            enumerator.Backtrack();
            Assert.AreEqual(true, enumerator.Matches("="));
            enumerator.Advance(1);
            Assert.AreEqual(true, enumerator.Matches(" "));
            enumerator.Advance(1);
            Assert.AreEqual(true, enumerator.Matches("3"));
            enumerator.Advance(1);
            Assert.AreEqual(true, enumerator.Matches(" "));
            enumerator.Advance(1);
            Assert.AreEqual(true, enumerator.Matches("*"));
            enumerator.Advance(1);
            Assert.AreEqual(true, enumerator.Matches(" "));
            enumerator.Advance(1);
            Assert.AreEqual(true, enumerator.Matches("4"));
            enumerator.Advance(1);
            Assert.AreEqual(true, enumerator.IsFinished);
        }

        [TestMethod]
        public void NestedBacktracking()
        {
            var program = "x = '4' | '5'";
            var enumerator = new ProgramEnumerator(program);

            Assert.AreEqual(true, enumerator.Matches("x"));
            enumerator.Advance(1);
            Assert.AreEqual(true, enumerator.Matches(" = "));
            enumerator.Advance(3);
            enumerator.SetBacktrackPoint();
            Assert.AreEqual(true, enumerator.Matches("'4'"));
            enumerator.Advance(3);
            enumerator.SetBacktrackPoint();
            Assert.AreEqual(false, enumerator.Matches(" / "));
            enumerator.Advance(3);
            enumerator.Backtrack();
            Assert.AreEqual(true, enumerator.Matches(" | "));
            enumerator.Advance(3);
            Assert.AreEqual(false, enumerator.Matches(")"));
            enumerator.Advance(1);
            enumerator.Backtrack();
            Assert.AreEqual(true, enumerator.Matches("'4' | '5'"));
            enumerator.Advance(9);
            Assert.AreEqual(true, enumerator.IsFinished);
        }
    }
}
