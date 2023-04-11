
namespace MyParser.Expression
{
    public sealed class AddExpression : LeftRightExpression
    {
        protected override bool process(IExpressionWalker walker, VauleExpression left, VauleExpression right)
        {
            var leftNumber = left as VauleExpression.NumericVauleExpression;

            if (leftNumber == null)
            {
                walker.Fail("Internal parsing error: Expected value on the left to be a number.");
                return false;
            }

            var rightNumber = right as VauleExpression.NumericVauleExpression;

            if (rightNumber == null)
            {
                walker.Fail("Internal parsing error: Expected value on the right to be a number.");
                return false;
            }

            result = new VauleExpression.FloatVauleExpression(leftNumber.Value + rightNumber.Value);
            walker.ValueStack.Push(result);
            return true;
        }

        #region Old
        //public override bool Process(IExpressionWalker walker)
        //{
        //    if (walker.ValueStack.Count == 0)
        //    {
        //        walker.Fail("Internal parsing error: Expected value on the left.");
        //        return false;
        //    }

        //    var leftNumber = walker.ValueStack.Pop() as VauleExpression.NumericVauleExpression;

        //    if (leftNumber == null)
        //    {
        //        walker.Fail("Internal parsing error: Expected value on the left to be a number.");
        //        return false;
        //    }

        //    if (!walker.Step() || walker.ValueStack.Count == 0)
        //    {
        //        if (walker.HasFailed)
        //            return false;

        //        walker.Fail("Internal parsing error: Expected value on the right.");
        //        return false;
        //    }

        //    var rightNumber = walker.ValueStack.Pop() as VauleExpression.NumericVauleExpression;

        //    if (rightNumber == null)
        //    {
        //        walker.Fail("Internal parsing error: Expected value on the right to be a number.");
        //        return false;
        //    }

        //    result = new VauleExpression.FloatVauleExpression(leftNumber.Value + rightNumber.Value);
        //    walker.ValueStack.Push(result);
        //    return true;
        //}
        #endregion
    }
}
