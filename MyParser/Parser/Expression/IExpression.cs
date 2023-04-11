using System;

namespace MyParser.Expression
{
    public interface IExpression
    {
        ExpressionType Type { get; }
    }

    public enum ExpressionType
    {
        Invailed,
        Operator,
        Command, //like operator, but everything to the right gets processed first
        Value,
        OpenParentheses,
        CloseParentheses,
        OpenCurlyBraces,
        CloseCurlyBraces
    }
}
