using System;
using System.Collections.Generic;
using static System.Char;

namespace Ibis.EBNF
{
    internal class Parser
    {
        public Dictionary<string, Rule> Parse(string grammar)
        {
            var rules = GetRules(grammar);

            return null;
        }

        // Get the rules from a given grammar
        public List<(string name, string body)> GetRules(string grammar)
        {
            var rules = new List<(string name, string body)>();
            var span = grammar.AsSpan();

            int index = 0;
            int current = 0;

            // Iterate through the grammar
            while (index < span.Length)
            {
                // Ignore starting whitespace
                while (IsWhiteSpace(span[index]))
                {
                    index++;
                    current++;

                    // Skip to the end
                    if (index == span.Length)
                    {
                        // Lovely use of a goto
                        goto end;
                    }
                }

                // Find the '='
                while (span[index] != '=')
                {
                    index++;
                }

                // Split to get rule name
                var name = span.Slice(current, index - current).ToString();

                index++;
                current = index;
                
                // Find the ';'
                while (span[index] != ';')
                {
                    index++;
                }

                // Split to get rule body
                var body = span.Slice(current, index - current).ToString();

                index++;
                current = index;

                // Add rule name and body
                rules.Add((name, body));
            }

            // Label for goto
            end:

            return rules;
        }
    }
}
