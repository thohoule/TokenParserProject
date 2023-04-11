using System;

namespace MyParser.Expression
{
    internal sealed class IgnoredExpression : IExpression
    {
        public ExpressionType Type { get { return ExpressionType.Invailed; } }
    }
}
