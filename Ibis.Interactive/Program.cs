using Ibis.EBNF;
using Ibis.Validation;
using System;
using System.Diagnostics;
using System.IO;

namespace Ibis.Interactive
{
    class Program
    {
        static void Main(string[] args)
        {
            var grammar = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, args[0]));
            var program = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, args[1]));

            var parser = new Parser();
            var validator = new Validator();
            var stopwatch = new Stopwatch();

            try
            {
                // Lexing
                stopwatch.Start();
                var tokens = parser.Lex(grammar);
                stopwatch.Stop();

                WriteLine($"Lexing ({stopwatch.ElapsedMilliseconds}ms): {tokens.Count} tokens");

                // Parsing
                stopwatch.Restart();
                var rules = parser.Parse(tokens);
                stopwatch.Stop();

                WriteLine($"Parsing ({stopwatch.ElapsedMilliseconds}ms): {rules.Count} rules");

                // Verifying
                stopwatch.Restart();
                var result = validator.Validate(rules, program);
                stopwatch.Stop();

                WriteLine($"Validating ({stopwatch.ElapsedMilliseconds}ms)");

                // Final result
                WriteLine($"Program {(result ? "DOES" : "DOES NOT")} satisfy the EBNF grammar");
            }
            catch (Exception e)
            {
                WriteLine($"Error: {e.Message}", ConsoleColor.Red);
            }

            Console.ReadKey();
        }

        static void WriteLine(string str, ConsoleColor colour = ConsoleColor.White)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(str);
        }
    }
}
