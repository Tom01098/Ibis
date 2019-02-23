using Ibis.EBNF;
using Ibis.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibis.Tests
{
    [TestClass]
    public class CombinedTests
    {
        [TestMethod]
        public void BinaryNumber()
        {
            var grammar = "main = digit{digit}; digit = '0' | '1';";

            var parser = new Parser();
            var rules = parser.Parse(parser.Lex(grammar));

            Assert.AreEqual(true, new Validator().Validate(rules, "0"));
            Assert.AreEqual(true, new Validator().Validate(rules, "1"));
            Assert.AreEqual(true, new Validator().Validate(rules, "1001011"));
            Assert.AreEqual(false, new Validator().Validate(rules, ""));
            Assert.AreEqual(false, new Validator().Validate(rules, "11110111x01110"));
        }

        [TestMethod]
        public void PrefixedHexNumber()
        {
            var grammar = @"main = '0x' digit {digit}; 
                            digit = '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9' | 'A' | 'B' | 'C' | 'D' | 'E' | 'F';";

            var parser = new Parser();
            var rules = parser.Parse(parser.Lex(grammar));

            Assert.AreEqual(true, new Validator().Validate(rules, "0x0"));
            Assert.AreEqual(true, new Validator().Validate(rules, "0x5"));
            Assert.AreEqual(true, new Validator().Validate(rules, "0xA"));
            Assert.AreEqual(true, new Validator().Validate(rules, "0x524213AE"));
            Assert.AreEqual(true, new Validator().Validate(rules, "0xAEF899"));
            Assert.AreEqual(false, new Validator().Validate(rules, "AEF899"));
            Assert.AreEqual(false, new Validator().Validate(rules, ""));
            Assert.AreEqual(false, new Validator().Validate(rules, "0"));
            Assert.AreEqual(false, new Validator().Validate(rules, "0x"));
        }

        [TestMethod]
        public void EscapedQuotations()
        {
            var grammar = @"main = '\'' digit {digit} '\''; 
                            digit = '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9' | 'A' | 'B' | 'C' | 'D' | 'E' | 'F';";

            var parser = new Parser();
            var rules = parser.Parse(parser.Lex(grammar));

            Assert.AreEqual(true, new Validator().Validate(rules, "'2312'"));
            Assert.AreEqual(true, new Validator().Validate(rules, "'23'"));
            Assert.AreEqual(true, new Validator().Validate(rules, "'1'"));
            Assert.AreEqual(true, new Validator().Validate(rules, "'949899042'"));
            Assert.AreEqual(true, new Validator().Validate(rules, "'23219489245'"));
            Assert.AreEqual(false, new Validator().Validate(rules, "''"));
            Assert.AreEqual(false, new Validator().Validate(rules, "'"));
            Assert.AreEqual(false, new Validator().Validate(rules, "23123'"));
            Assert.AreEqual(false, new Validator().Validate(rules, "' 2312 '"));
        }
    }
}
