namespace Ibis.EBNF
{
    internal class GrammarEnumerator
    {
        private readonly string grammar;

        public GrammarEnumerator(string grammar)
        {
            this.grammar = grammar;
        }

        public char Current => grammar[Index];

        public bool MoveNext() => Index++ < grammar.Length - 1;

        public int Index { get; private set; } = 0;

        public void ChangeIndex(int index) => this.Index = index;
    }
}
