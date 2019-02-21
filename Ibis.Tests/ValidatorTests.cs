using Ibis.EBNF.AST;
using Ibis.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ibis.Tests
{
    [TestClass]
    public class ValidatorTests
    {
        [TestMethod]
        public void SingleLiteral()
        {
            var rules = new List<Rule>
            {
                new Rule
                (
                    new Name("main"),
                    new RuleBody
                    (
                        new List<RuleSection>
                        {
                            new RuleSection
                            (
                                new List<RuleStatement>
                                {
                                    new Literal("0")
                                }
                            )
                        }
                    )
                )
            };

            var valid = "0";
            var invalid = "1";

            var validResult = new Validator().Validate(rules, valid);
            var invalidResult = new Validator().Validate(rules, invalid);

            Assert.AreEqual(true, validResult);
            Assert.AreEqual(false, invalidResult);
        }

        [TestMethod]
        public void DoubleLiteral()
        {
            var rules = new List<Rule>
            {
                new Rule
                (
                    new Name("main"),
                    new RuleBody
                    (
                        new List<RuleSection>
                        {
                            new RuleSection
                            (
                                new List<RuleStatement>
                                {
                                    new Literal("0"),
                                    new Literal("1")
                                }
                            )
                        }
                    )
                )
            };

            var valid = "01";
            var invalid = "00";

            var validResult = new Validator().Validate(rules, valid);
            var invalidResult = new Validator().Validate(rules, invalid);

            Assert.AreEqual(true, validResult);
            Assert.AreEqual(false, invalidResult);
        }

        [TestMethod]
        public void NameLiteral()
        {
            var rules = new List<Rule>
            {
                new Rule
                (
                    new Name("main"),
                    new RuleBody
                    (
                        new List<RuleSection>
                        {
                            new RuleSection
                            (
                                new List<RuleStatement>
                                {
                                    new Name("digit")
                                }
                            )
                        }
                    )
                ),
                new Rule
                (
                    new Name("digit"),
                    new RuleBody
                    (
                        new List<RuleSection>
                        {
                            new RuleSection
                            (
                                new List<RuleStatement>
                                {
                                    new Literal("0")
                                }
                            )
                        }
                    )
                )
            };

            var valid = "0";
            var invalid = "1";

            var validResult = new Validator().Validate(rules, valid);
            var invalidResult = new Validator().Validate(rules, invalid);

            Assert.AreEqual(true, validResult);
            Assert.AreEqual(false, invalidResult);
        }

        [TestMethod]
        public void RepetitionLiteral()
        {
            var rules = new List<Rule>
            {
                new Rule
                (
                    new Name("main"),
                    new RuleBody
                    (
                        new List<RuleSection>
                        {
                            new RuleSection
                            (
                                new List<RuleStatement>
                                {
                                    new Repetition
                                    (
                                        new RuleBody
                                        (
                                            new List<RuleSection>
                                            {
                                                new RuleSection
                                                (
                                                    new List<RuleStatement>
                                                    {
                                                        new Literal("0")
                                                    }
                                                )
                                            }
                                        )
                                    )
                                }
                            )
                        }
                    )
                )
            };

            var valid = "0000000000000";
            var valid2 = "";
            var invalid = "0000000100000";

            var validResult = new Validator().Validate(rules, valid);
            var valid2Result = new Validator().Validate(rules, valid2);
            var invalidResult = new Validator().Validate(rules, invalid);

            Assert.AreEqual(true, validResult);
            Assert.AreEqual(true, valid2Result);
            Assert.AreEqual(false, invalidResult);
        }

        [TestMethod]
        public void OptionalLiteral()
        {
            var rules = new List<Rule>
            {
                new Rule
                (
                    new Name("main"),
                    new RuleBody
                    (
                        new List<RuleSection>
                        {
                            new RuleSection
                            (
                                new List<RuleStatement>
                                {
                                    new Optional
                                    (
                                        new RuleBody
                                        (
                                            new List<RuleSection>
                                            {
                                                new RuleSection
                                                (
                                                    new List<RuleStatement>
                                                    {
                                                        new Literal("0")
                                                    }
                                                )
                                            }
                                        )
                                    )
                                }
                            )
                        }
                    )
                )
            };

            var valid = "0";
            var valid2 = "";
            var invalid = "1";

            var validResult = new Validator().Validate(rules, valid);
            var valid2Result = new Validator().Validate(rules, valid2);
            var invalidResult = new Validator().Validate(rules, invalid);

            Assert.AreEqual(true, validResult);
            Assert.AreEqual(true, valid2Result);
            Assert.AreEqual(false, invalidResult);
        }

        [TestMethod]
        public void GroupedLiteral()
        {
            var rules = new List<Rule>
            {
                new Rule
                (
                    new Name("main"),
                    new RuleBody
                    (
                        new List<RuleSection>
                        {
                            new RuleSection
                            (
                                new List<RuleStatement>
                                {
                                    new Grouping
                                    (
                                        new RuleBody
                                        (
                                            new List<RuleSection>
                                            {
                                                new RuleSection
                                                (
                                                    new List<RuleStatement>
                                                    {
                                                        new Literal("0")
                                                    }
                                                )
                                            }
                                        )
                                    )
                                }
                            )
                        }
                    )
                )
            };

            var valid = "0";
            var invalid = "1";

            var validResult = new Validator().Validate(rules, valid);
            var invalidResult = new Validator().Validate(rules, invalid);

            Assert.AreEqual(true, validResult);
            Assert.AreEqual(false, invalidResult);
        }

        [TestMethod]
        public void ChoiceLiteral()
        {
            var rules = new List<Rule>
            {
                new Rule
                (
                    new Name("main"),
                    new RuleBody
                    (
                        new List<RuleSection>
                        {
                            new RuleSection
                            (
                                new List<RuleStatement>
                                {
                                    new Literal("0")
                                }
                            ),
                            new RuleSection
                            (
                                new List<RuleStatement>
                                {
                                    new Literal("1")
                                }
                            )
                        }
                    )
                )
            };

            var valid = "0";
            var valid2 = "1";
            var invalid = "2";

            var validResult = new Validator().Validate(rules, valid);
            var valid2Result = new Validator().Validate(rules, valid2);
            var invalidResult = new Validator().Validate(rules, invalid);

            Assert.AreEqual(true, validResult);
            Assert.AreEqual(true, valid2Result);
            Assert.AreEqual(false, invalidResult);
        }

        [TestMethod]
        public void RepetitionWithChoice()
        {
            var rules = new List<Rule>
            {
                new Rule
                (
                    new Name("main"),
                    new RuleBody
                    (
                        new List<RuleSection>
                        {
                            new RuleSection
                            (
                                new List<RuleStatement>
                                {
                                    new Repetition
                                    (
                                        new RuleBody
                                        (
                                            new List<RuleSection>
                                            {
                                                new RuleSection
                                                (
                                                    new List<RuleStatement>
                                                    {
                                                        new Literal("0")
                                                    }
                                                ),
                                                new RuleSection
                                                (
                                                    new List<RuleStatement>
                                                    {
                                                        new Literal("1")
                                                    }
                                                )
                                            }
                                        )
                                    )
                                }
                            )
                        }
                    )
                )
            };

            var valid = "";
            var valid2 = "00000";
            var valid3 = "11111";
            var valid4 = "11001101100";
            var invalid = "110120110";

            var validResult = new Validator().Validate(rules, valid);
            var valid2Result = new Validator().Validate(rules, valid2);
            var valid3Result = new Validator().Validate(rules, valid3);
            var valid4Result = new Validator().Validate(rules, valid4);
            var invalidResult = new Validator().Validate(rules, invalid);

            Assert.AreEqual(true, validResult);
            Assert.AreEqual(true, valid2Result);
            Assert.AreEqual(true, valid3Result);
            Assert.AreEqual(true, valid4Result);
            Assert.AreEqual(false, invalidResult);
        }

        [TestMethod]
        public void MultipleOptionals()
        {
            var rules = new List<Rule>
            {
                new Rule
                (
                    new Name("main"),
                    new RuleBody
                    (
                        new List<RuleSection>
                        {
                            new RuleSection
                            (
                                new List<RuleStatement>
                                {
                                    new Optional
                                    (
                                        new RuleBody
                                        (
                                            new List<RuleSection>
                                            {
                                                new RuleSection
                                                (
                                                    new List<RuleStatement>
                                                    {
                                                        new Literal("A")
                                                    }
                                                )
                                            }
                                        )
                                    ),
                                    new Optional
                                    (
                                        new RuleBody
                                        (
                                            new List<RuleSection>
                                            {
                                                new RuleSection
                                                (
                                                    new List<RuleStatement>
                                                    {
                                                        new Literal("B")
                                                    }
                                                )
                                            }
                                        )
                                    ),
                                    new Optional
                                    (
                                        new RuleBody
                                        (
                                            new List<RuleSection>
                                            {
                                                new RuleSection
                                                (
                                                    new List<RuleStatement>
                                                    {
                                                        new Literal("C")
                                                    }
                                                )
                                            }
                                        )
                                    )
                                }
                            )
                        }
                    )
                )
            };

            var valid = "";
            var valid2 = "ABC";
            var valid3 = "AB";
            var valid4 = "BC";
            var valid5 = "AC";
            var invalid = "BAC";

            var validResult = new Validator().Validate(rules, valid);
            var valid2Result = new Validator().Validate(rules, valid2);
            var valid3Result = new Validator().Validate(rules, valid3);
            var valid4Result = new Validator().Validate(rules, valid4);
            var valid5Result = new Validator().Validate(rules, valid5);
            var invalidResult = new Validator().Validate(rules, invalid);

            Assert.AreEqual(true, validResult);
            Assert.AreEqual(true, valid2Result);
            Assert.AreEqual(true, valid3Result);
            Assert.AreEqual(true, valid4Result);
            Assert.AreEqual(true, valid5Result);
            Assert.AreEqual(false, invalidResult);
        }

        [TestMethod]
        public void NestedRepetition()
        {
            var rules = new List<Rule>
            {
                new Rule
                (
                    new Name("main"),
                    new RuleBody
                    (
                        new List<RuleSection>
                        {
                            new RuleSection
                            (
                                new List<RuleStatement>
                                {
                                    new Repetition
                                    (
                                        new RuleBody
                                        (
                                            new List<RuleSection>
                                            {
                                                new RuleSection
                                                (
                                                    new List<RuleStatement>
                                                    {
                                                        new Literal("A"),
                                                        new Repetition
                                                        (
                                                            new RuleBody
                                                            (
                                                                new List<RuleSection>
                                                                {
                                                                    new RuleSection
                                                                    (
                                                                        new List<RuleStatement>
                                                                        {
                                                                            new Literal("X")
                                                                        }
                                                                    )
                                                                }
                                                            )
                                                        )
                                                    }
                                                )
                                            }
                                        )
                                    )
                                }
                            )
                        }
                    )
                )
            };

            var valid = "";
            var valid2 = "A";
            var valid3 = "AXXXXXXX";
            var valid4 = "AXAXAX";
            var valid5 = "AXXAXXXA";
            var invalid = "XAXXX";

            var validResult = new Validator().Validate(rules, valid);
            var valid2Result = new Validator().Validate(rules, valid2);
            var valid3Result = new Validator().Validate(rules, valid3);
            var valid4Result = new Validator().Validate(rules, valid4);
            var valid5Result = new Validator().Validate(rules, valid5);
            var invalidResult = new Validator().Validate(rules, invalid);

            Assert.AreEqual(true, validResult);
            Assert.AreEqual(true, valid2Result);
            Assert.AreEqual(true, valid3Result);
            Assert.AreEqual(true, valid4Result);
            Assert.AreEqual(true, valid5Result);
            Assert.AreEqual(false, invalidResult);
        }
    }
}
