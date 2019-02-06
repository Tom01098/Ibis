using System;
using System.Collections.Generic;

namespace Ibis.EBNF
{
    internal class Parser
    {
        private Dictionary<string, Rule> rules = new Dictionary<string, Rule>();

        public Dictionary<string, Rule> Parse(string grammar)
        {
            var span = grammar.AsSpan();

            return rules;
        }
    }
}
