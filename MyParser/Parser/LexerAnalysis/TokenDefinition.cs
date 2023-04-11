using System;
using System.Text.RegularExpressions;

namespace MyParser.LexerAnalysis
{
    public struct TokenDefinition : IComparable<string>
    {
        //public readonly IMatcher Matcher;
        private readonly Regex regex;
        //public readonly StandardTokenType Token;
        public readonly Token Token;

        public TokenDefinition(string regex, StandardTokenType token)
        {
            //this.Matcher = new RegexMatcher(regex);
            this.regex = new Regex(string.Format("^{0}", regex));
            this.Token = token;
        }

        public TokenDefinition(string regex, Token token)
        {
            this.regex = new Regex(string.Format("^{0}", regex));
            Token = token;
        }

        public int CompareTo(string other)
        {
            var m = regex.Match(other);
            return m.Success ? m.Length : 0;
        }

        public override string ToString()
        {
            return regex.ToString();
        }
    }

    public enum StandardTokenType
    {
        Quotes,
        Float,
        Int,
        True,
        False,
        Symbol,
        Dot,
        OpenParentheses,
        CloseParentheses,
        OpenCurlyBraces,
        CloseCurlyBraces,
        Space
    }
}
