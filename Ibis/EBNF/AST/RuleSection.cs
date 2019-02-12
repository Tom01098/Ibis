using System.Collections.Generic;

namespace Ibis.EBNF.AST
{
    internal class RuleSection
    {
        public List<RuleStatement> RuleStatements { get; }

        public RuleSection(List<RuleStatement> ruleStatements)
        {
            RuleStatements = ruleStatements;
        }
    }
}
