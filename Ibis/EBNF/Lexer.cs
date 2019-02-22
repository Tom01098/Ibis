using Ibis.EBNF.Tokens;
using System;
using System.Collections.Generic;
using static System.Char;

namespace Ibis.EBNF
{
    internal partial class Parser
    {
        /// <summary>
        /// Convert an input string - in the form of an EBNF grammar -
        /// into a list of tokens representing it
        /// </summary>
        /// 
        /// <param name="grammar">
        /// The EBNF grammar as a string
        /// </param>
        /// 
        /// <returns>
        /// The EBNF grammar as a list of tokens
        /// </returns>
        public List<Token> Lex(string grammar)
        {
            var tokens = new List<Token>();

            var span = grammar.AsSpan();
            var index = 0;

            while (index < span.Length)
            {
                // Identifier
                if (IsLetterOrDigit(span[index]) || span[index] == '_')
                {
                    var start = index;

                    do
                    {
                        index++;
                    }
                    while (index < span.Length && (IsLetterOrDigit(span[index]) || span[index] == '_'));

                    tokens.Add(new IdentifierToken(span.Slice(start, index - start).ToString()));

                    continue;
                }
                // Symbols
                else if (span[index] == '(')
                {
                    tokens.Add(new SymbolToken(SymbolType.OpenParenthesis));
                }
                else if (span[index] == ')')
                {
                    tokens.Add(new SymbolToken(SymbolType.CloseParenthesis));
                }
                else if (span[index] == '[')
                {
                    tokens.Add(new SymbolToken(SymbolType.OpenSquareParenthesis));
                }
                else if (span[index] == ']')
                {
                    tokens.Add(new SymbolToken(SymbolType.CloseSquareParenthesis));
                }
                else if (span[index] == '{')
                {
                    tokens.Add(new SymbolToken(SymbolType.OpenCurlyParenthesis));
                }
                else if (span[index] == '}')
                {
                    tokens.Add(new SymbolToken(SymbolType.CloseCurlyParenthesis));
                }
                else if (span[index] == '=')
                {
                    tokens.Add(new SymbolToken(SymbolType.Equals));
                }
                else if (span[index] == '|')
                {
                    tokens.Add(new SymbolToken(SymbolType.Pipe));
                }
                else if (span[index] == '\'')
                {
                    tokens.Add(new SymbolToken(SymbolType.Quotation));

                    index++;
                    var start = index;
                    
                    while (index < span.Length && span[index] != '\'')
                    {
                        index++;
                    }

                    if (index != start)
                    {
                        tokens.Add(new IdentifierToken(span.Slice(start, index - start).ToString()));
                    }

                    if (index < span.Length)
                    {
                        tokens.Add(new SymbolToken(SymbolType.Quotation));
                    }
                }
                else if (span[index] == ';')
                {
                    tokens.Add(new SymbolToken(SymbolType.Semicolon));
                }
                else if (!IsWhiteSpace(span[index]))
                {
                    throw new ArgumentException($"Unrecognised character '{span[index]}'");
                }

                index++;
            }

            tokens.Add(new EOFToken());

            return tokens;
        }
    }
}
