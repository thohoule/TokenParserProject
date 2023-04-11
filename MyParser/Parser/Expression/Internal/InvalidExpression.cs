using System;

namespace MyParser.Expression
{
    internal sealed class InvalidExpression : VauleExpression
    {
        public string Message { get { return (string)internalObject; } }
        public override Type GetValueType { get { return typeof(string); } }

        public InvalidExpression(string message)
        {
            internalObject = message;
        }
    }
}
