namespace Ibis.EBNF
{
    internal class GrammarEnumerator
    {
        private int index = 0;

        private readonly string grammar;

        public GrammarEnumerator(string grammar)
        {
            this.grammar = grammar;
        }

        public char Current => grammar[index];

        public bool MoveNext() => index++ < grammar.Length - 1;
    }
}
