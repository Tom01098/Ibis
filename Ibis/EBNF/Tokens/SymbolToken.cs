using System;

namespace Ibis.EBNF.Tokens
{
    internal class SymbolToken : Token, IEquatable<SymbolToken>
    {
        public SymbolType Type { get; }

        public SymbolToken(SymbolType type) =>
            Type = type;

        public override string ToString() =>
            $"Symbol ({Type})";

        public override bool Equals(object obj)
        {
            return Equals(obj as SymbolToken);
        }

        public bool Equals(SymbolToken other)
        {
            return other != null &&
                   Type == other.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type);
        }
    }
}
