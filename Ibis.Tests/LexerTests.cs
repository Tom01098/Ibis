using Ibis.EBNF;
using Ibis.EBNF.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ibis.Tests
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void Identifier()
        {
            var str = "a";
            var tokens = new Parser().Lex(str);

            var expected = new List<Token>
            {
                new IdentifierToken("a"),
                new EOFToken()
            };

            CollectionAssert.AreEqual(expected, tokens);
        }

        [TestMethod]
        public void Identifier2()
        {
            var str = "ab51";
            var tokens = new Parser().Lex(str);

            var expected = new List<Token>
            {
                new IdentifierToken("ab51"),
                new EOFToken()
            };

            CollectionAssert.AreEqual(expected, tokens);
        }

        [TestMethod]
        public void OpenSquareParenthesis()
        {
            var str = "[";
            var tokens = new Parser().Lex(str);

            var expected = new List<Token>
            {
                new SymbolToken(SymbolType.OpenSquareParenthesis),
                new EOFToken()
            };

            CollectionAssert.AreEqual(expected, tokens);
        }

        [TestMethod]
        public void CloseSquareParenthesis()
        {
            var str = "]";
            var tokens = new Parser().Lex(str);

            var expected = new List<Token>
            {
                new SymbolToken(SymbolType.CloseSquareParenthesis),
                new EOFToken()
            };

            CollectionAssert.AreEqual(expected, tokens);
        }

        [TestMethod]
        public void OpenParenthesis()
        {
            var str = "(";
            var tokens = new Parser().Lex(str);

            var expected = new List<Token>
            {
                new SymbolToken(SymbolType.OpenParenthesis),
                new EOFToken()
            };

            CollectionAssert.AreEqual(expected, tokens);
        }

        [TestMethod]
        public void CloseParenthesis()
        {
            var str = ")";
            var tokens = new Parser().Lex(str);

            var expected = new List<Token>
            {
                new SymbolToken(SymbolType.CloseParenthesis),
                new EOFToken()
            };

            CollectionAssert.AreEqual(expected, tokens);
        }

        [TestMethod]
        public void OpenCurlyParenthesis()
        {
            var str = "{";
            var tokens = new Parser().Lex(str);

            var expected = new List<Token>
            {
                new SymbolToken(SymbolType.OpenCurlyParenthesis),
                new EOFToken()
            };

            CollectionAssert.AreEqual(expected, tokens);
        }

        [TestMethod]
        public void CloseCurlyParenthesis()
        {
            var str = "}";
            var tokens = new Parser().Lex(str);

            var expected = new List<Token>
            {
                new SymbolToken(SymbolType.CloseCurlyParenthesis),
                new EOFToken()
            };

            CollectionAssert.AreEqual(expected, tokens);
        }

        [TestMethod]
        public void Semicolon()
        {
            var str = ";";
            var tokens = new Parser().Lex(str);

            var expected = new List<Token>
            {
                new SymbolToken(SymbolType.Semicolon),
                new EOFToken()
            };

            CollectionAssert.AreEqual(expected, tokens);
        }

        [TestMethod]
        public void Equals()
        {
            var str = "=";
            var tokens = new Parser().Lex(str);

            var expected = new List<Token>
            {
                new SymbolToken(SymbolType.Equals),
                new EOFToken()
            };

            CollectionAssert.AreEqual(expected, tokens);
        }

        [TestMethod]
        public void Pipe()
        {
            var str = "|";
            var tokens = new Parser().Lex(str);

            var expected = new List<Token>
            {
                new SymbolToken(SymbolType.Pipe),
                new EOFToken()
            };

            CollectionAssert.AreEqual(expected, tokens);
        }

        [TestMethod]
        public void Quotation()
        {
            var str = "'";
            var tokens = new Parser().Lex(str);

            var expected = new List<Token>
            {
                new SymbolToken(SymbolType.Quotation),
                new EOFToken()
            };

            CollectionAssert.AreEqual(expected, tokens);
        }

        [TestMethod]
        public void Rule()
        {
            var str = "x = 'x'";
            var tokens = new Parser().Lex(str);

            var expected = new List<Token>
            {
                new IdentifierToken("x"),
                new SymbolToken(SymbolType.Equals),
                new SymbolToken(SymbolType.Quotation),
                new IdentifierToken("x"),
                new SymbolToken(SymbolType.Quotation),
                new EOFToken()
            };

            CollectionAssert.AreEqual(expected, tokens);
        }

        [TestMethod]
        public void Rule2()
        {
            var str = "number = digit{digit};";
            var tokens = new Parser().Lex(str);

            var expected = new List<Token>
            {
                new IdentifierToken("number"),
                new SymbolToken(SymbolType.Equals),
                new IdentifierToken("digit"),
                new SymbolToken(SymbolType.OpenCurlyParenthesis),
                new IdentifierToken("digit"),
                new SymbolToken(SymbolType.CloseCurlyParenthesis),
                new SymbolToken(SymbolType.Semicolon),
                new EOFToken()
            };

            CollectionAssert.AreEqual(expected, tokens);
        }

        [TestMethod]
        public void Rules()
        {
            var str = @"number = digit{digit};
                        digit = 0 | 1;";
            var tokens = new Parser().Lex(str);

            var expected = new List<Token>
            {
                new IdentifierToken("number"),
                new SymbolToken(SymbolType.Equals),
                new IdentifierToken("digit"),
                new SymbolToken(SymbolType.OpenCurlyParenthesis),
                new IdentifierToken("digit"),
                new SymbolToken(SymbolType.CloseCurlyParenthesis),
                new SymbolToken(SymbolType.Semicolon),
                new IdentifierToken("digit"),
                new SymbolToken(SymbolType.Equals),
                new IdentifierToken("0"),
                new SymbolToken(SymbolType.Pipe),
                new IdentifierToken("1"),
                new SymbolToken(SymbolType.Semicolon),
                new EOFToken()
            };

            CollectionAssert.AreEqual(expected, tokens);
        }
    }
}
