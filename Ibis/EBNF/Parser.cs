using System;
using System.Collections.Generic;
using static System.Char;

namespace Ibis.EBNF
{
    internal class Parser
    {
        private GrammarEnumerator enumerator;

        public Dictionary<string, Rule.Body> Parse(string grammar)
        {
            enumerator = new GrammarEnumerator(grammar);

            return null;
        }

        private T ParseWith<T>(params Func<T>[] methods)
            where T : class
        {
            int index = enumerator.Index;

            foreach (var method in methods)
            {
                T result = method();

                if (result != null)
                {
                    return result;
                }

                enumerator.ChangeIndex(index);
            }

            return null;
        }

        // rule_body = [whitespace] rule_statement {whitespace '|' whitespace rule_statement} [whitespace];
        private Rule.Body RuleBody()
        {
            Whitespace();

            var statement = ParseWith<Rule>(Optional, Repetition, Grouping, Literal, Name);

            return null;
        }

        // rule_section = rule_statement {whitespace rule_statement};
        private Rule.Section RuleSection()
        {
            return null;
        }

        // optional = '[' rule_body ']';
        private Rule.Optional Optional()
        {
            return null;
        }

        // repetition = '{' rule_body '}';
        private Rule.Repetition Repetition()
        {
            return null;
        }

        // grouping = '(' rule_body ')';
        private Rule.Grouping Grouping()
        {
            return null;
        }

        // literal = ''' (character | number) {character | number} ''';
        private Rule.Literal Literal()
        {
            return null;
        }

        // name = character {character | number};
        private Rule.Name Name()
        {
            return null;
        }

        private bool Whitespace()
        {
            if (!IsWhiteSpace(enumerator.Current))
            {
                return false;
            }

            while (IsWhiteSpace(enumerator.Current))
            {
                enumerator.MoveNext();
            }

            return true;
        }
    }
}
