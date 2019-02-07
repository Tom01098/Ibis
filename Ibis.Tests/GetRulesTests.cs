using Ibis.EBNF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibis.Tests
{
    [TestClass]
    public class GetRulesTests
    {
        [TestMethod]
        public void Single()
        {
            var grammar = "binary_digit = '0' | '1'";

            var rules = new Parser().GetRules(grammar);

            Assert.AreEqual(1, rules.Count);
            Assert.AreEqual("binary_digit ", rules[0].name);
            Assert.AreEqual(" '0' | '1'", rules[0].body);
        }

        [TestMethod]
        public void Multiple()
        {
            var grammar = @"binary_digit = '0' | '1'
                            binary_number = binary_digit{binary_digit}";

            var rules = new Parser().GetRules(grammar);

            Assert.AreEqual(2, rules.Count);
            Assert.AreEqual("binary_digit ", rules[0].name);
            Assert.AreEqual(" '0' | '1'", rules[0].body);
            Assert.AreEqual("binary_number ", rules[1].name);
            Assert.AreEqual(" binary_digit{binary_digit}", rules[1].body);
        }

        [TestMethod]
        public void Multiple2()
        {
            var grammar = @"optional = '[' rule_body ']'

                            repetition = '{' rule_body '}'

                            grouping = '(' rule_body ')'

                            literal = ''' (character | number) {character | number} '''";

            var rules = new Parser().GetRules(grammar);

            Assert.AreEqual(4, rules.Count);
            Assert.AreEqual("optional ", rules[0].name);
            Assert.AreEqual(" '[' rule_body ']'", rules[0].body);
            Assert.AreEqual("repetition ", rules[1].name);
            Assert.AreEqual(" '(' rule_body ')'", rules[1].body);
            Assert.AreEqual("grouping ", rules[2].name);
            Assert.AreEqual(" '{' rule_body '}'", rules[2].body);
            Assert.AreEqual("literal ", rules[3].name);
            Assert.AreEqual(" ''' (character | number) {character | number} '''", rules[4].body);
        }

        [TestMethod]
        public void Multiple3()
        {
            var grammar = @"whitespace = ? whitespace characters ? ;

character = ? a-z and A-Z ? ;

number = ? 0-9 ? ;



rule = name [whitespace] '=' rule_body [whitespace] ';' ;

name = character {character | number} ;

rule_body = [whitespace] rule_statement {whitespace '|' whitespace rule_statement} [whitespace] ;

rule_statement = optional | repetition | grouping | literal | name ;



optional = '[' rule_body ']' ;

repetition = '{' rule_body '}' ;

grouping = '(' rule_body ')' ;

literal = ''' (character | number) {character | number} ''' ;";

            var rules = new Parser().GetRules(grammar);

            Assert.AreEqual(11, rules.Count);
            Assert.AreEqual("whitespace ", rules[0].name);
            Assert.AreEqual(" ? whitespace characters ?", rules[0].body);
            Assert.AreEqual("character ", rules[1].name);
            Assert.AreEqual(" ? a-z and A-Z ?", rules[1].body);
            Assert.AreEqual("number ", rules[2].name);
            Assert.AreEqual(" ? 0-9 ?", rules[2].body);
            Assert.AreEqual("rule ", rules[3].name);
            Assert.AreEqual(" name [whitespace] '=' rule_body [whitespace] ';'", rules[3].body);
            Assert.AreEqual("name ", rules[4].name);
            Assert.AreEqual(" character {character | number}", rules[4].body);
            Assert.AreEqual("rule_body ", rules[5].name);
            Assert.AreEqual(" [whitespace] rule_statement {whitespace '|' whitespace rule_statement}", rules[5].body);
            Assert.AreEqual("rule_statement ", rules[6].name);
            Assert.AreEqual(" optional | repetition | grouping | literal | name", rules[6].body);
            Assert.AreEqual("optional ", rules[7].name);
            Assert.AreEqual(" '[' rule_body ']'", rules[7].body);
            Assert.AreEqual("repetition ", rules[8].name);
            Assert.AreEqual(" '{' rule_body '}'", rules[8].body);
            Assert.AreEqual("grouping ", rules[9].name);
            Assert.AreEqual(" '(' rule_body ')'", rules[9].body);
            Assert.AreEqual("literal ", rules[10].name);
            Assert.AreEqual(" ''' (character | number) {character | number} '''", rules[10].body);
        }
    }
}
