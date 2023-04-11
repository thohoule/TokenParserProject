using System;

namespace MyParser.Expression
{
    internal sealed class ParenthesesExpression : IExpression
    {
        public bool IsOpen { get; private set; }
        public ExpressionType Type { get { return IsOpen ? ExpressionType.OpenParentheses : ExpressionType.CloseParentheses; } }

        public ParenthesesExpression(bool isOpen)
        {
            IsOpen = isOpen;
        }
    }
}
