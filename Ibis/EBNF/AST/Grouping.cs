namespace Ibis.EBNF.AST
{
    internal class Grouping : RuleStatement
    {
        public RuleBody RuleBody { get; }

        public Grouping(RuleBody ruleBody)
        {
            RuleBody = ruleBody;
        }
    }
}
