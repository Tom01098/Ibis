using Ibis.EBNF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ibis.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Optional()
        {
            var str = "x = ['x'];";
            var rules = new Parser().Parse(str);

            Assert.AreEqual(1, rules.Count);

            var x = rules["x"];
            var body = ((Rule.Optional)x.sections[0].rules[0]).body;
            Assert.AreEqual("x", ((Rule.Literal)body.sections[0].rules[0]).literal);
        }

        [TestMethod]
        public void Repetition()
        {
            var str = "x = {'x'};";
            var rules = new Parser().Parse(str);

            Assert.AreEqual(1, rules.Count);

            var x = rules["x"];
            var body = ((Rule.Repetition)x.sections[0].rules[0]).body;
            Assert.AreEqual("x", ((Rule.Literal)body.sections[0].rules[0]).literal);
        }

        [TestMethod]
        public void Grouping()
        {
            var str = "x = ('x');";
            var rules = new Parser().Parse(str);

            Assert.AreEqual(1, rules.Count);

            var x = rules["x"];
            var body = ((Rule.Grouping)x.sections[0].rules[0]).body;
            Assert.AreEqual("x", ((Rule.Literal)body.sections[0].rules[0]).literal);
        }

        [TestMethod]
        public void Literal()
        {
            var str = "x = 'x';";
            var rules = new Parser().Parse(str);

            Assert.AreEqual(1, rules.Count);

            var x = rules["x"];
            Assert.AreEqual("x", ((Rule.Literal)x.sections[0].rules[0]).literal);
        }

        [TestMethod]
        public void Name()
        {
            var str = "x = y;";
            var rules = new Parser().Parse(str);

            Assert.AreEqual(1, rules.Count);

            var x = rules["x"];
            Assert.AreEqual("y", ((Rule.Name)x.sections[0].rules[0]).name);
        }
    }
}
