﻿using Ibis.EBNF.AST;
using System;
using System.Collections.Generic;

namespace Ibis.Validation
{
    internal class Validator
    {
        private Dictionary<string, RuleBody> rules = new Dictionary<string, RuleBody>();
        private ProgramEnumerator enumerator;

        public bool Validate(List<Rule> rulesList, string program)
        {
            enumerator = new ProgramEnumerator(program);

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

            return Validate(rules["main"]);
        }

        private bool Validate(object obj)
        {
            // RuleBody handling
            if (obj is RuleBody ruleBody)
            {
                foreach (var ruleSection in ruleBody.RuleSections)
                {
                    if (Validate(ruleSection))
                    {
                        return true;
                    }
                }

                return false;
            }
            // RuleSection handling
            else if (obj is RuleSection ruleSection)
            {
                foreach (var ruleStatement in ruleSection.RuleStatements)
                {
                    if (!Validate(ruleStatement))
                    {
                        return false;
                    }
                }

                return true;
            }
            // Repetition handling
            else if (obj is Repetition repetition)
            {
                throw new NotImplementedException();
            }
            // Optional handling
            else if (obj is Optional optional)
            {
                throw new NotImplementedException();
            }
            // Grouping handling
            else if (obj is Grouping grouping)
            {
                throw new NotImplementedException();
            }
            // Name handling
            else if (obj is Name name)
            {
                throw new NotImplementedException();
            }
            // Literal handling
            else if (obj is Literal literal)
            {
                throw new NotImplementedException();
            }

            throw new ArgumentException("Could not validate supplied object");
        }
    }
}
