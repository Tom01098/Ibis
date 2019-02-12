using System.Collections.Generic;

namespace Ibis.EBNF.AST
{
    internal class RuleBody : Node
    {
        public List<RuleSection> RuleSections { get; }

        public RuleBody(List<RuleSection> ruleSections)
        {
            RuleSections = ruleSections;
        }
    }
}
