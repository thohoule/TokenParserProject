using System;
using System.IO;

namespace MyParser.LexerAnalysis
{
    public sealed class Lexer : IDisposable
    {
        //private TokenDefinition[] tokenDefinitions;
        private TokenDictionary tokenDictionary;
        //private Dictionary<string, TokenType> tokenDefinitions;
        private TextReader reader;

        private string lineRemaining;

        public LexerResult LastResult { get; private set; }

        //public string TokenContents { get; private set; }

        //public StandardTokenType Token { get; private set; }

        public int LineNumber { get; private set; }

        public int Position { get; private set; }

        public Lexer() : this(TokenDictionary.Standard) { }

        public Lexer(TokenDictionary tokenDictionary)
        {
            this.tokenDictionary = tokenDictionary;
        }

        public Lexer(TextReader reader) : this(TokenDictionary.Standard, reader) { }

        public Lexer(TokenDictionary tokenDictionary, TextReader reader) : this(tokenDictionary)
        {
            ReadFrom(reader);
        }

        public void ReadFrom(TextReader reader)
        {
            this.reader = reader;
            nextLine();
        }

        public bool Next(out LexerResult result)
        {
            var boolResult = Next();
            result = LastResult;
            return boolResult;
        }

        public bool Next()
        {
            if (lineRemaining == null)
                return false;

            Token token;
            var matched = tokenDictionary.Match(lineRemaining, out token);

            if (matched > 0)
            {
                Position += matched;

                if (token == StandardTokenType.Quotes)
                    LastResult = new LexerResult(token, lineRemaining.Substring(1, matched - 2));
                else
                    LastResult = new LexerResult(token, lineRemaining.Substring(0, matched));

                lineRemaining = lineRemaining.Substring(matched);
                if (lineRemaining.Length == 0)
                    nextLine();

                return true;
            }

            throw new Exception(string.Format("Unable to match against any tokens at line {0} position {1} \"{2}\"",
                                              LineNumber, Position, lineRemaining));
        }

        #region Old
        //public bool Next()
        //{
        //    if (lineRemaining == null)
        //        return false;

        //    foreach (var def in tokenDefinitions)
        //    {
        //        //var matched = def.Matcher.Match(lineRemaining);
        //        var matched = def.CompareTo(lineRemaining);
        //        if (matched > 0)
        //        {
        //            Position += matched;
        //            Token = def.Token;
        //            if (def.Token == StandardTokenType.Quotes)
        //                TokenContents = lineRemaining.Substring(1, matched - 2);
        //            else
        //                TokenContents = lineRemaining.Substring(0, matched);
        //            lineRemaining = lineRemaining.Substring(matched);
        //            if (lineRemaining.Length == 0)
        //                nextLine();

        //            return true;
        //        }
        //    }
        //    throw new Exception(string.Format("Unable to match against any tokens at line {0} position {1} \"{2}\"",
        //                                      LineNumber, Position, lineRemaining));
        //}
        #endregion

        private void nextLine()
        {
            do
            {
                lineRemaining = reader.ReadLine();
                ++LineNumber;
                Position = 0;
            } while (lineRemaining != null && lineRemaining.Length == 0);
        }

        public void Dispose()
        {
            reader.Dispose();
        }
    }
}
