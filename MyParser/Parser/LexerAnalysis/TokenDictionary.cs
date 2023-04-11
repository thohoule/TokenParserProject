using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyParser.LexerAnalysis
{
    public sealed class TokenDictionary
    {
        private TokenDefinition[] tokenDefinitions;

        public TokenDictionary(TokenDefinition[] tokenDefinitions)
        {
            this.tokenDefinitions = tokenDefinitions;
        }

        public int Match(string line, out Token token)
        {
            foreach (var def in tokenDefinitions)
            {
                var matched = def.CompareTo(line);
                if (matched > 0)
                {
                    token = def.Token;
                    return matched;
                }
            }

            token = -1;
            return -1;
        }

        public static TokenDictionary Standard
        {
            get
            {
                return new TokenDictionary(new TokenDefinition[]
                    {
                        new TokenDefinition(@"([""'])(?:\\\1|.)*?\1", StandardTokenType.Quotes),
                        // Thanks to http://www.regular-expressions.info/floatingpoint.html
                        new TokenDefinition(@"[-+]?\d*\.\d+([eE][-+]?\d+)?", StandardTokenType.Float),
                        new TokenDefinition(@"[-+]?\d+", StandardTokenType.Int),
                        new TokenDefinition(@"#t", StandardTokenType.True),
                        new TokenDefinition(@"#f", StandardTokenType.False),
                        new TokenDefinition(@"[*<>\?\-+/A-Za-z->!]+", StandardTokenType.Symbol),
                        new TokenDefinition(@"\.", StandardTokenType.Dot),
                        new TokenDefinition(@"\(", StandardTokenType.OpenParentheses),
                        new TokenDefinition(@"\)", StandardTokenType.CloseParentheses),
                        new TokenDefinition(@"\{", StandardTokenType.OpenCurlyBraces),
                        new TokenDefinition(@"\}", StandardTokenType.CloseCurlyBraces),
                        new TokenDefinition(@"\s", StandardTokenType.Space)
                    });
            }
        }
    }
}
