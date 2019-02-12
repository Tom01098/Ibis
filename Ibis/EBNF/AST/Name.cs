namespace Ibis.EBNF.AST
{
    internal class Name : RuleStatement
    {
        public string Value { get; }

        public Name(string value)
        {
            Value = value;
        }
    }
}
