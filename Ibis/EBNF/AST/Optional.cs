namespace Ibis.EBNF.AST
{
    internal class Optional : RuleStatement
    {
        public RuleBody RuleBody { get; }

        public Optional(RuleBody ruleBody)
        {
            RuleBody = ruleBody;
        }
    }
}
