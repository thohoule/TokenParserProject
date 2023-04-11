//using System;

//namespace MyParser.LexerAnalysis
//{
//    public sealed partial class Lexer : IDisposable
//    {
//        public static Lexer standardLexarInstance;

//        public static Lexer StandardLexar
//        {
//            get
//            {
//                if (standardLexarInstance == null)
//                    standardLexarInstance = new Lexer(new TokenDefinition[]
//                    {
//                        new TokenDefinition(@"([""'])(?:\\\1|.)*?\1", StandardTokenType.Quotes),
//                        // Thanks to http://www.regular-expressions.info/floatingpoint.html
//                        new TokenDefinition(@"[-+]?\d*\.\d+([eE][-+]?\d+)?", StandardTokenType.Float),
//                        new TokenDefinition(@"[-+]?\d+", StandardTokenType.Int),
//                        new TokenDefinition(@"#t", StandardTokenType.True),
//                        new TokenDefinition(@"#f", StandardTokenType.False),
//                        new TokenDefinition(@"[*<>\?\-+/A-Za-z->!]+", StandardTokenType.Symbol),
//                        new TokenDefinition(@"\.", StandardTokenType.Dot),
//                        new TokenDefinition(@"\(", StandardTokenType.OpenParentheses),
//                        new TokenDefinition(@"\)", StandardTokenType.CloseParentheses),
//                        new TokenDefinition(@"\{", StandardTokenType.OpenCurlyBraces),
//                        new TokenDefinition(@"\}", StandardTokenType.CloseCurlyBraces),
//                        new TokenDefinition(@"\s", StandardTokenType.Space)
//                    });

//                return standardLexarInstance;
//            }
//        }
//    }
//}
