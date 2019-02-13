using System.Collections.Generic;

namespace Ibis.EBNF.Tokens
{
    internal class TokenEnumerator
    {
        private readonly Token[] tokens;
        private readonly Stack<int> indexes;

        public TokenEnumerator(Token[] tokens)
        {
            this.tokens = tokens;
            indexes = new Stack<int>();
            indexes.Push(0);
        }

        public Token Current
        {
            get
            {
                if (indexes.Peek() >= tokens.Length)
                {
                    return null;
                }

                return tokens[indexes.Peek()];
            }
        }

        public bool MoveNext()
        {
            indexes.Push(indexes.Pop() + 1);
            return indexes.Peek() < tokens.Length;
        }

        public void SetBacktrackPoint()
        {
            indexes.Push(indexes.Peek());
        }

        public void Backtrack()
        {
            indexes.Pop();
        }
    }
}
