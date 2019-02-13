using Ibis.EBNF.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ibis.Tests
{
    [TestClass]
    public class TokenEnumeratorTests
    {
        [TestMethod]
        public void Enumerate()
        {
            var tokens = new List<Token>
            {
                new IdentifierToken("x"),
                new SymbolToken(SymbolType.Equals),
                new IdentifierToken("3")
            };

            var enumerator = new TokenEnumerator(tokens.ToArray());

            Assert.AreEqual(new IdentifierToken("x"), enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual(new SymbolToken(SymbolType.Equals), enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual(new IdentifierToken("3"), enumerator.Current);
            Assert.AreEqual(false, enumerator.MoveNext());
            Assert.AreEqual(null, enumerator.Current);
        }

        [TestMethod]
        public void Backtracking()
        {
            var tokens = new List<Token>
            {
                new IdentifierToken("x"),
                new SymbolToken(SymbolType.Equals),
                new IdentifierToken("3")
            };

            var enumerator = new TokenEnumerator(tokens.ToArray());

            Assert.AreEqual(new IdentifierToken("x"), enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            enumerator.SetBacktrackPoint();
            Assert.AreEqual(new SymbolToken(SymbolType.Equals), enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            enumerator.Backtrack();
            Assert.AreEqual(new SymbolToken(SymbolType.Equals), enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual(new IdentifierToken("3"), enumerator.Current);
            Assert.AreEqual(false, enumerator.MoveNext());
            Assert.AreEqual(null, enumerator.Current);
        }

        [TestMethod]
        public void NestedBacktracking()
        {
            var tokens = new List<Token>
            {
                new IdentifierToken("x"),
                new SymbolToken(SymbolType.Equals),
                new IdentifierToken("3")
            };

            var enumerator = new TokenEnumerator(tokens.ToArray());

            Assert.AreEqual(new IdentifierToken("x"), enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            enumerator.SetBacktrackPoint();
            Assert.AreEqual(new SymbolToken(SymbolType.Equals), enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            enumerator.SetBacktrackPoint();
            Assert.AreEqual(new IdentifierToken("3"), enumerator.Current);
            Assert.AreEqual(false, enumerator.MoveNext());
            enumerator.Backtrack();
            Assert.AreEqual(new IdentifierToken("3"), enumerator.Current);
            Assert.AreEqual(false, enumerator.MoveNext());
            enumerator.Backtrack();
            Assert.AreEqual(new SymbolToken(SymbolType.Equals), enumerator.Current);
            Assert.AreEqual(true, enumerator.MoveNext());
            Assert.AreEqual(new IdentifierToken("3"), enumerator.Current);
            Assert.AreEqual(false, enumerator.MoveNext());
            Assert.AreEqual(null, enumerator.Current);
        }
    }
}
