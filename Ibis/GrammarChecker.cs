using Ibis.EBNF;
using Ibis.Validation;

namespace Ibis
{
    public static class GrammarChecker
    {
        public static bool IsProgramValid(string program, string grammar)
        {
            var rules = new Parser().Parse(grammar);

            return new Validator().IsValid(program, rules);
        }
    }
}
