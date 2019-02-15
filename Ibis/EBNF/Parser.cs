using Ibis.EBNF.AST;
using Ibis.EBNF.Tokens;
using System;
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
                var rule = ParseWith(Rule);

                if (rule is null)
                {
                    throw new ArgumentException(
                        $"Unexpected token {tokens.Current.ToString()}");
                }

                rules.Add(rule);
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

        private T ParseWith<T>(params Func<T>[] methods)
            where T : class
        {
            foreach (var method in methods)
            {
                tokens.SetBacktrackPoint();

                T result = method();

                if (!(result is null))
                {
                    return result;
                }

                tokens.Backtrack();
            }

            return null;
        }

        // rule = name '=' rule_body ';';
        private Rule Rule()
        {
            var name = AcceptIdentifier();

            if (name is null)
            {
                return null;
            }

            if (AcceptSymbol(SymbolType.Equals) is null)
            {
                return null;
            }

            var ruleBody = ParseWith(RuleBody);

            if (ruleBody is null)
            {
                return null;
            }

            if (AcceptSymbol(SymbolType.Semicolon) is null)
            {
                return null;
            }

            return new Rule(new Name(name.Identifier), ruleBody);
        }

        // name = (character | number) {character | number};
        private Name Name()
        {
            var identifier = AcceptIdentifier();

            if (identifier is null)
            {
                return null;
            }

            return new Name(identifier.Identifier);
        }

        // rule_body = rule_section {'|' rule_section};
        private RuleBody RuleBody()
        {
            var ruleSection = ParseWith(RuleSection);

            if (ruleSection is null)
            {
                return null;
            }

            var sections = new List<RuleSection> { ruleSection };

            while (!(AcceptSymbol(SymbolType.Pipe) is null))
            {
                ruleSection = ParseWith(RuleSection);

                if (ruleSection is null)
                {
                    return null;
                }

                sections.Add(ruleSection);
            }

            return new RuleBody(sections);
        }

        // rule_section = rule_statement {rule_statement};
        private RuleSection RuleSection()
        {
            var statement = ParseWith(RuleStatement);

            if (statement is null)
            {
                return null;
            }

            var statements = new List<RuleStatement>();

            while (!(statement is null))
            {
                statements.Add(statement);
                statement = ParseWith(RuleStatement);
            }

            return new RuleSection(statements);
        }

        // rule_statement = optional | repetition | grouping | literal | name;
        private RuleStatement RuleStatement()
        {
            return ParseWith<RuleStatement>(Optional, Repetition, Grouping, Literal, Name);
        }

        // optional = '[' rule_body ']';
        private Optional Optional()
        {
            if (AcceptSymbol(SymbolType.OpenSquareParenthesis) is null)
            {
                return null;
            }

            var ruleBody = ParseWith(RuleBody);

            if (AcceptSymbol(SymbolType.CloseSquareParenthesis) is null)
            {
                return null;
            }

            return new Optional(ruleBody);
        }

        // repetition = '{' rule_body '}';
        private Repetition Repetition()
        {
            if (AcceptSymbol(SymbolType.OpenCurlyParenthesis) is null)
            {
                return null;
            }

            var ruleBody = ParseWith(RuleBody);

            if (AcceptSymbol(SymbolType.CloseCurlyParenthesis) is null)
            {
                return null;
            }

            return new Repetition(ruleBody);
        }

        // grouping = '(' rule_body ')';
        private Grouping Grouping()
        {
            if (AcceptSymbol(SymbolType.OpenParenthesis) is null)
            {
                return null;
            }

            var ruleBody = ParseWith(RuleBody);

            if (AcceptSymbol(SymbolType.CloseParenthesis) is null)
            {
                return null;
            }

            return new Grouping(ruleBody);
        }

        // literal = ''' (character | number) {character | number} ''';
        private Literal Literal()
        {
            if (AcceptSymbol(SymbolType.Quotation) is null)
            {
                return null;
            }

            var identifier = AcceptIdentifier();

            if (identifier is null)
            {
                return null;
            }

            if (AcceptSymbol(SymbolType.Quotation) is null)
            {
                return null;
            }

            return new Literal(identifier.Identifier);
        }
    }
}
