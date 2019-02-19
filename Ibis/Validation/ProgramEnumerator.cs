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

        public char Current => program.Span[indexes.Peek()];

        public bool MoveNext()
        {
            indexes.Push(indexes.Pop() + 1);
            return indexes.Peek() < program.Length;
        }

        public void SetBacktrackPoint() => indexes.Push(indexes.Peek());

        public void Backtrack() => indexes.Pop();
    }
}
