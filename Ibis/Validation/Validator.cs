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

            var result = Validate(rules["main"]);

            if (!enumerator.IsFinished)
            {
                return false;
            }

            return result;
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
                //enumerator.SetBacktrackPoint();

                foreach (var ruleStatement in ruleSection.RuleStatements)
                {
                    if (!Validate(ruleStatement))
                    {
                        //enumerator.Backtrack();
                        return false;
                    }
                }

                return true;
            }
            // Repetition handling
            else if (obj is Repetition repetition)
            {
                while (Validate(repetition.RuleBody))
                {

                }

                return true;
            }
            // Optional handling
            else if (obj is Optional optional)
            {
                //enumerator.SetBacktrackPoint();

                if (!Validate(optional.RuleBody))
                {
                    //enumerator.Backtrack();
                }

                return true;
            }
            // Grouping handling
            else if (obj is Grouping grouping)
            {
                //enumerator.SetBacktrackPoint();

                var result = Validate(grouping.RuleBody);

                if (!result)
                {
                    //enumerator.Backtrack();
                }

                return result;
            }
            // Name handling
            else if (obj is Name name)
            {
                //enumerator.SetBacktrackPoint();

                var result = Validate(rules[name.Value]);

                if (!result)
                {
                    //enumerator.Backtrack();
                }

                return result;
            }
            // Literal handling
            else if (obj is Literal literal)
            {
                //enumerator.SetBacktrackPoint();

                if (enumerator.Matches(literal.Value))
                {
                    enumerator.Advance(literal.Value.Length);
                    return true;
                }

                //enumerator.Backtrack();
                return false;
            }

            throw new ArgumentException("Could not validate supplied object");
        }
    }
}
