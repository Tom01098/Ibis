namespace Ibis.EBNF.AST
{
    internal class Literal : RuleStatement
    {
        public string Value { get; }

        public Literal(string value)
        {
            Value = value;
        }
    }
}
