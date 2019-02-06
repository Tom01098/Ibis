using System.Collections.Generic;

namespace Ibis.EBNF
{
    internal class Parser
    {
        public Dictionary<string, Rule> Parse(string grammar)
        {
            var rules = GetRules(grammar);

            return null;
        }

        internal List<(string name, string body)> GetRules(string grammar)
        {
            var rules = new List<(string name, string body)>();



            return rules;
        }
    }
}
