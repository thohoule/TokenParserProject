using System;

namespace MyParser.Expression
{
    public abstract class CommandExpression : IOperatorExpression
    {
        protected VauleExpression result;

        public ExpressionType Type { get { return ExpressionType.Operator; } }

        public virtual VauleExpression GetValue
        {
            get
            {
                return result ??
                    new InvalidExpression("Expression has not been processed yet.");
            }
        }

        public abstract bool Process(IExpressionWalker walker);
    }
}
