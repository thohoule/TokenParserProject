using System.Collections.Generic;
using MyParser.Expression;

namespace MyParser
{
    public sealed class FunctionExpression
    {
        private Queue<IExpression> processStack;

        public Queue<IExpression> InstructionsInstance { get { return IsValid ? new Queue<IExpression>(processStack) : null; } }
        public bool IsValid { get; private set; }
        public string FailMessage { get; private set; }

        public FunctionExpression()
        {
            processStack = new Queue<IExpression>();
            IsValid = true;
        }

        public void Enqueue(IExpression item)
        {
            processStack.Enqueue(item);
        }

        public bool Invalidate(string message)
        {
            IsValid = false;
            FailMessage = message;
            Clear();
            return false;
        }

        public void Clear()
        {
            processStack.Clear();
        }
    }
}
