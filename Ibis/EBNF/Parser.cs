using Ibis.EBNF.AST;
using Ibis.EBNF.Tokens;
using System.Collections.Generic;

namespace Ibis.EBNF
{
    internal partial class Parser
    {
        private TokenEnumerator tokens;

        public List<Rule> Parse(List<Token> tokenList)
        {
            tokens = new TokenEnumerator(tokenList.ToArray());

            var rules = new List<Rule>();

            while (!(tokens.Current is EOFToken))
            {
                rules.Add(Rule());
            }

            return rules;
        }

        private IdentifierToken AcceptIdentifier()
        {
            var identifier = tokens.Current as IdentifierToken;

            if (!(identifier is null))
            {
                tokens.MoveNext();
            }

            return identifier;
        }

        private SymbolToken AcceptSymbol(SymbolType type)
        {
            var symbol = tokens.Current as SymbolToken;

            if (!(symbol is null) && symbol.Type == type)
            {
                tokens.MoveNext();
                return symbol;
            }

            return null;
        }

        // rule = name '=' rule_body ';';
        private Rule Rule()
        {
            return null;
        }

        // name = (character | number) {character | number};
        private Name Name()
        {
            return null;
        }

        // rule_body = rule_section {'|' rule_section};
        private RuleBody RuleBody()
        {
            return null;
        }

        // rule_section = rule_statement {rule_statement};
        private RuleSection RuleSection()
        {
            return null;
        }

        // rule_statement = optional | repetition | grouping | literal | name;
        private RuleStatement RuleStatement()
        {
            return null;
        }

        // optional = '[' rule_body ']';
        private Optional Optional()
        {
            return null;
        }

        // repetition = '{' rule_body '}';
        private Repetition Repetition()
        {
            return null;
        }

        // grouping = '(' rule_body ')';
        private Grouping Grouping()
        {
            return null;
        }

        // literal = ''' (character | number) {character | number} ''';
        private Literal Literal()
        {
            return null;
        }
    }
}
