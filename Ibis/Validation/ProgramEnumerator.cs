using System;
using System.Collections.Generic;

namespace Ibis.Validation
{
    internal class ProgramEnumerator
    {
        private ReadOnlyMemory<char> program;
        private Stack<int> indexes;

        public ProgramEnumerator(string program)
        {
            this.program = program.AsMemory();
            indexes = new Stack<int>();
            indexes.Push(0);
        }

        public bool IsFinished => indexes.Peek() >= program.Length;

        public void SetBacktrackPoint() => indexes.Push(indexes.Peek());

        public void Backtrack() => indexes.Pop();

        public void Advance(int count) => indexes.Push(indexes.Pop() + count);

        public bool Matches(string str)
        {
            var match = str.AsSpan();
            var span = program.Span;

            if (match.Length + indexes.Peek() > span.Length)
            {
                return false;
            }

            for (int i = 0; i < match.Length; i++)
            {
                if (match[i] != span[indexes.Peek() + i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
