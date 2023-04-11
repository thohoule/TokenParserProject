using System;

namespace MyParser.Expression
{
    internal sealed class MessageExpression : VauleExpression
    {
        public string Message { get { return (string)internalObject; } }
        public override Type GetValueType { get { return typeof(string); } }

        public MessageExpression(string message)
        {
            internalObject = message;
        }
    }
}
