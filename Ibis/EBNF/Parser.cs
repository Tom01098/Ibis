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

        
    }
}
