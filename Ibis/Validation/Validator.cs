using Ibis.EBNF.AST;
using System;
using System.Collections.Generic;

namespace Ibis.Validation
{
    internal class Validator
    {
        public bool Validate(List<Rule> rulesList, string program)
        {
            var enumerator = new ProgramEnumerator(program);

            var rules = new Dictionary<string, RuleBody>();

            foreach (var rule in rulesList)
            {
                if (!rules.TryAdd(rule.Name.Value, rule.RuleBody))
                {
                    throw new ArgumentException(
                        $"Grammar has multiple rules called {rule.Name.Value}");
                }
            }

            if (!rules.ContainsKey("main"))
            {
                throw new ArgumentException("Grammar does not contain a 'main' rule");
            }

            throw new NotImplementedException();
        }
    }
}
