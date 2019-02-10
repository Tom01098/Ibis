﻿using System;
using System.Collections.Generic;

namespace Ibis.EBNF
{
    internal class Parser
    {
        private GrammarEnumerator enumerator;

        public Dictionary<string, Rule> Parse(string grammar)
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
    }
}
