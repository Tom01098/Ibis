using System;

namespace Ibis.EBNF
{
    internal abstract class Rule
    {
        public abstract bool IsValid(ReadOnlySpan<char> span);
    }
}
