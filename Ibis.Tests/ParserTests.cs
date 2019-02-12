using Ibis.EBNF;
using Ibis.EBNF.AST;
using Ibis.EBNF.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ibis.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Literal()
        {
            var tokens = new List<Token>
            {
                new IdentifierToken("zero"),
                new SymbolToken(SymbolType.Equals),
                new SymbolToken(SymbolType.Quotation),
                new IdentifierToken("0"),
                new SymbolToken(SymbolType.Quotation),
                new SymbolToken(SymbolType.Semicolon),
                new EOFToken()
            };

            var rules = new Parser().Parse(tokens);

            Assert.AreEqual(1, rules.Count);
            Assert.AreEqual("zero", rules[0].Name.Value);
            Assert.AreEqual("0", 
                ((Literal)rules[0].RuleBody.RuleSections[0].RuleStatements[0]).Value);
        }

        [TestMethod]
        public void Name()
        {
            var tokens = new List<Token>
            {
                new IdentifierToken("x"),
                new SymbolToken(SymbolType.Equals),
                new IdentifierToken("y"),
                new SymbolToken(SymbolType.Semicolon),
                new EOFToken()
            };

            var rules = new Parser().Parse(tokens);

            Assert.AreEqual(1, rules.Count);
            Assert.AreEqual("x", rules[0].Name.Value);
            Assert.AreEqual("y", 
                ((Name)rules[0].RuleBody.RuleSections[0].RuleStatements[0]).Value);
        }

        [TestMethod]
        public void Grouping()
        {
            var tokens = new List<Token>
            {
                new IdentifierToken("x"),
                new SymbolToken(SymbolType.Equals),
                new SymbolToken(SymbolType.OpenParenthesis),
                new IdentifierToken("y"),
                new SymbolToken(SymbolType.CloseParenthesis),
                new SymbolToken(SymbolType.Semicolon),
                new EOFToken()
            };

            var rules = new Parser().Parse(tokens);

            Assert.AreEqual(1, rules.Count);
            Assert.AreEqual("x", rules[0].Name.Value);
            Assert.AreEqual("y", 
                ((Name)((Grouping)rules[0].RuleBody.RuleSections[0].RuleStatements[0])
                .RuleBody.RuleSections[0].RuleStatements[0]).Value);
        }

        [TestMethod]
        public void Repetition()
        {
            var tokens = new List<Token>
            {
                new IdentifierToken("x"),
                new SymbolToken(SymbolType.Equals),
                new SymbolToken(SymbolType.OpenCurlyParenthesis),
                new IdentifierToken("y"),
                new SymbolToken(SymbolType.CloseCurlyParenthesis),
                new SymbolToken(SymbolType.Semicolon),
                new EOFToken()
            };

            var rules = new Parser().Parse(tokens);

            Assert.AreEqual(1, rules.Count);
            Assert.AreEqual("x", rules[0].Name.Value);
            Assert.AreEqual("y",
                ((Name)((Repetition)rules[0].RuleBody.RuleSections[0].RuleStatements[0])
                .RuleBody.RuleSections[0].RuleStatements[0]).Value);
        }

        [TestMethod]
        public void Optional()
        {
            var tokens = new List<Token>
            {
                new IdentifierToken("x"),
                new SymbolToken(SymbolType.Equals),
                new SymbolToken(SymbolType.OpenSquareParenthesis),
                new IdentifierToken("y"),
                new SymbolToken(SymbolType.CloseSquareParenthesis),
                new SymbolToken(SymbolType.Semicolon),
                new EOFToken()
            };

            var rules = new Parser().Parse(tokens);

            Assert.AreEqual(1, rules.Count);
            Assert.AreEqual("x", rules[0].Name.Value);
            Assert.AreEqual("y",
                ((Name)((Optional)rules[0].RuleBody.RuleSections[0].RuleStatements[0])
                .RuleBody.RuleSections[0].RuleStatements[0]).Value);
        }
        
        [TestMethod]
        public void ChoiceInRepetition()
        {
            var tokens = new List<Token>
            {
                new IdentifierToken("x"),
                new SymbolToken(SymbolType.Equals),
                new SymbolToken(SymbolType.OpenCurlyParenthesis),
                new IdentifierToken("a"),
                new SymbolToken(SymbolType.Pipe),
                new IdentifierToken("b"),
                new SymbolToken(SymbolType.CloseCurlyParenthesis),
                new SymbolToken(SymbolType.Semicolon),
                new EOFToken()
            };

            var rules = new Parser().Parse(tokens);
            
            Assert.AreEqual(1, rules.Count);
            Assert.AreEqual("x", rules[0].Name.Value);
            Assert.AreEqual("a", 
                ((Name)((Repetition)rules[0].RuleBody.RuleSections[0].RuleStatements[1]).RuleBody.RuleSections[0].RuleStatements[0]).Value);
            Assert.AreEqual("b",
                ((Name)((Repetition)rules[0].RuleBody.RuleSections[0].RuleStatements[1]).RuleBody.RuleSections[1].RuleStatements[0]).Value);
        }

        [TestMethod]
        public void Multiple()
        {
            var tokens = new List<Token>
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
                new SymbolToken(SymbolType.Quotation),
                new IdentifierToken("0"),
                new SymbolToken(SymbolType.Quotation),
                new SymbolToken(SymbolType.Pipe),
                new SymbolToken(SymbolType.Quotation),
                new IdentifierToken("1"),
                new SymbolToken(SymbolType.Quotation),
                new SymbolToken(SymbolType.Semicolon),
                new EOFToken()
            };

            var rules = new Parser().Parse(tokens);

            Assert.AreEqual(2, rules.Count);
            Assert.AreEqual("number", rules[0].Name.Value);
            Assert.AreEqual("digit", 
                ((Name)rules[0].RuleBody.RuleSections[0].RuleStatements[0]).Value);
            Assert.AreEqual("digit", 
                ((Name)((Repetition)rules[0].RuleBody.RuleSections[0].RuleStatements[1]).RuleBody.RuleSections[0].RuleStatements[0]).Value);
            Assert.AreEqual("digit", rules[1].Name.Value);
            Assert.AreEqual("0", 
                ((Literal)rules[1].RuleBody.RuleSections[0].RuleStatements[0]).Value);
            Assert.AreEqual("1", 
                ((Literal)rules[1].RuleBody.RuleSections[1].RuleStatements[0]).Value);
        }
    }
}
