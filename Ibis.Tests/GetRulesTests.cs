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
            var grammar = "A = 'A'; ";

            var rules = new Parser().GetRules(grammar);

            Assert.AreEqual(1, rules.Count);
            Assert.AreEqual("A ", rules[0].name);
            Assert.AreEqual(" 'A'", rules[0].body);
        }

        [TestMethod]
        public void Multiple()
        {
            var grammar = @"binary_digit = '0' | '1';
                            binary_number = binary_digit{binary_digit};";

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
            var grammar = @"optional = '[' rule_body ']';

                            repetition = '{' rule_body '}';

                            grouping = '(' rule_body ')';

                            literal = ''' (character | number) {character | number} ''';";

            var rules = new Parser().GetRules(grammar);

            Assert.AreEqual(4, rules.Count);
            Assert.AreEqual("optional ", rules[0].name);
            Assert.AreEqual(" '[' rule_body ']'", rules[0].body);
            Assert.AreEqual("repetition ", rules[1].name);
            Assert.AreEqual(" '{' rule_body '}'", rules[1].body);
            Assert.AreEqual("grouping ", rules[2].name);
            Assert.AreEqual(" '(' rule_body ')'", rules[2].body);
            Assert.AreEqual("literal ", rules[3].name);
            Assert.AreEqual(" ''' (character | number) {character | number} '''", rules[3].body);
        }
    }
}
