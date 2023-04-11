using System;

namespace MyParser.Expression
{
    public class PrintExpression : CommandExpression
    {
        public override bool Process(IExpressionWalker walker)
        {
            if (walker.ValueStack.Count == 0)
                walker.Message("");
            else
                walker.Message(walker.ValueStack.Pop().Value.ToString());

            return true;
        }

        //protected override bool process(IExpressionWalker walker, VauleExpression right)
        //{
        //    var stringValue = right as VauleExpression.StringVauleExpression;

        //    if (stringValue == null)
        //        return walker.Fail("Internal parsing error: Expected value for run command to be a string.");

        //    walker.Message(string.Format("Run: {0}", stringValue.Value));
        //    return true;
        //}
    }
}
