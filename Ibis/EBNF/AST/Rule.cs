namespace Ibis.EBNF.AST
{
    internal class Rule
    {
        public Name Name { get; }
        public RuleBody RuleBody { get; }

        public Rule(Name name, RuleBody ruleBody)
        {
            Name = name;
            RuleBody = ruleBody;
        }
    }
}
