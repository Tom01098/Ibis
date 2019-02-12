namespace Ibis.EBNF.AST
{
    internal class Repetition : RuleStatement
    {
        public RuleBody RuleBody { get; }

        public Repetition(RuleBody ruleBody)
        {
            RuleBody = ruleBody;
        }
    }
}
