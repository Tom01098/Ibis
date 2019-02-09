namespace Ibis.EBNF
{
    internal abstract class Rule
    {
        internal class Body : Rule
        {
            private readonly Rule[] rules;

            public Body(Rule[] rules)
            {
                this.rules = rules;
            }
        }

        internal class Optional : Rule
        {
            private readonly Body body;

            public Optional(Body body)
            {
                this.body = body;
            }
        }

        internal class Repetition : Rule
        {
            private readonly Body body;

            public Repetition(Body body)
            {
                this.body = body;
            }
        }

        internal class Grouping : Rule
        {
            private readonly Body body;

            public Grouping(Body body)
            {
                this.body = body;
            }
        }

        internal class Literal : Rule
        {
            private readonly string literal;

            public Literal(string literal)
            {
                this.literal = literal;
            }
        }
    }
}
