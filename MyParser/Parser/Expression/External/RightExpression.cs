using System;

namespace MyParser.Expression
{
    public abstract class RightExpression : IOperatorExpression
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
            if (!walker.Step() || walker.ValueStack.Count == 0)
            {
                if (walker.HasFailed)
                    return false;

                walker.Fail("Internal parsing error: Expected value on the right.");
                return false;
            }

            var right = walker.ValueStack.Pop();

            return process(walker, right);
        }

        protected abstract bool process(IExpressionWalker walker, VauleExpression right);
    }
}
