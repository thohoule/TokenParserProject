using System;

namespace MyParser.Expression
{
    public abstract class LeftRightExpression : IOperatorExpression
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

        public bool Process(IExpressionWalker walker)
        {
            if (walker.ValueStack.Count == 0)
            {
                walker.Fail("Internal parsing error: Expected value on the left.");
                return false;
            }

            var left = walker.ValueStack.Pop();

            if (!walker.Step() || walker.ValueStack.Count == 0)
            {
                if (walker.HasFailed)
                    return false;

                walker.Fail("Internal parsing error: Expected value on the right.");
                return false;
            }

            var right = walker.ValueStack.Pop();

            return process(walker, left, right);
        }

        protected abstract bool process(IExpressionWalker walker, VauleExpression left, VauleExpression right);
    }
}
