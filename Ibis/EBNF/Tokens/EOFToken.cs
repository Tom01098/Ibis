namespace Ibis.EBNF.Tokens
{
    internal class EOFToken : Token
    {
        public override string ToString() => "EOF";

        public override bool Equals(object obj)
        {
            return Equals(obj as EOFToken);
        }

        public bool Equals(EOFToken other)
        {
            return other != null;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
