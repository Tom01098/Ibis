using System;

namespace Ibis.EBNF.Tokens
{
    internal class IdentifierToken : Token, IEquatable<IdentifierToken>
    {
        public string Identifier { get; }

        public IdentifierToken(string identifier) =>
            Identifier = identifier;

        public override string ToString() =>
            $"Identifier ({Identifier})";

        public override bool Equals(object obj)
        {
            return Equals(obj as IdentifierToken);
        }

        public bool Equals(IdentifierToken other)
        {
            return other != null &&
                   Identifier == other.Identifier;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Identifier);
        }
    }
}
