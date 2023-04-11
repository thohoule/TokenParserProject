
namespace MyParser.LexerAnalysis
{
    public class LexerResult
    {
        public Token Token { get; private set; }

        /// <summary>
        /// Token Content
        /// </summary>
        public string Content { get; private set; }

        public LexerResult(Token token, string content)
        {
            Token = token;
            Content = content;
        }
    }
}
