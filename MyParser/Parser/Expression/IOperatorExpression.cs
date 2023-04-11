using System;

namespace MyParser.Expression
{
    public interface IOperatorExpression : IExpression
    {
        VauleExpression GetValue { get; }
        bool Process(IExpressionWalker walker);
    }
}
