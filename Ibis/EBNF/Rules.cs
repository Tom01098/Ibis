namespace Ibis.EBNF
{
    internal abstract class Rule
    {
        internal class Body : Rule
        {
            internal readonly Section[] sections;

            public Body(Section[] sections)
            {
                this.sections = sections;
            }
        }

        internal class Section : Rule
        {
            internal readonly Rule[] rules;

            public Section(Rule[] rules)
            {
                this.rules = rules;
            }
        }

        internal class Optional : Rule
        {
            internal readonly Body body;

            public Optional(Body body)
            {
                this.body = body;
            }
        }

        internal class Repetition : Rule
        {
            internal readonly Body body;

            public Repetition(Body body)
            {
                this.body = body;
            }
        }

        internal class Grouping : Rule
        {
            internal readonly Body body;

            public Grouping(Body body)
            {
                this.body = body;
            }
        }

        internal class Literal : Rule
        {
            internal readonly string literal;

            public Literal(string literal)
            {
                this.literal = literal;
            }
        }

        internal class Name : Rule
        {
            internal readonly string name;

            public Name(string name)
            {
                this.name = name;
            }
        }
    }
}
