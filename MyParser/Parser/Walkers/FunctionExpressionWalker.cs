using System;
using System.Collections.Generic;
using MyParser.Expression;

namespace MyParser
{
    public class FunctionExpressionWalker : IExpressionWalker
    {
        //private Queue<IExpression> processStack;
        private int depth;
        private bool curlyDepth;

        private Queue<IExpression> processStack;

        public ExternalVariables ParserVariables { get; private set; }
        public Stack<VauleExpression> ValueStack { get; private set; }
        public Queue<VauleExpression> Results { get; private set; }
        public bool HasFailed { get; private set; }

        public FunctionExpressionWalker(ExternalVariables parserVariables, FunctionExpression function)
        {
            if (function == null)
                throw new ArgumentNullException();

            if (!function.IsValid)
                throw new ArgumentException("Function must be valid.");

            processStack = function.InstructionsInstance;
            ParserVariables = parserVariables;
            ValueStack = new Stack<VauleExpression>();
            Results = new Queue<VauleExpression>();
        }

        public bool StepThrough()
        {
            if (processStack.Count == 0)
                return false;

            while (Step())
            {

            }

            while (ValueStack.Count > 0)
            {
                Results.Enqueue(ValueStack.Pop());
            }

            //Results = ValueStack.ToList();

            return !HasFailed;
        }

        public bool Step()
        {
            IExpression expression;
            return Step(out expression);
        }

        public bool Step(out IExpression result)
        {
            result = null;
            if (processStack.Count <= 0)
                return false;

            result = processStack.Dequeue();

            switch (result.Type)
            {
                case ExpressionType.Value:
                    ValueStack.Push(result as VauleExpression);
                    return true;
                case ExpressionType.Operator:
                    return (result as IOperatorExpression).Process(this);
                case ExpressionType.Command:
                    if (StepThrough())
                        return (result as IOperatorExpression).Process(this);
                    else
                        return false;
                case ExpressionType.OpenParentheses:
                    return stepTillClose();
                case ExpressionType.CloseParentheses:
                    if (depth > 0)
                        return false;
                    else
                        return Fail("Internal parsing error: Expected opening parentheses.");
                //case ExpressionType.OpenCurlyBraces:
                //curlyDepth = true;
                //return (result as CurlyBracesExpression).Process(this);
                //case ExpressionType.CloseCurlyBraces:
                //if (curlyDepth)
                //{
                //    curlyDepth = false;
                //    return true;
                //}
                //return Fail("Internal parsing error: Was expecting a '{'.");
                default:
                    return false;
            }
        }

        private bool stepTillClose()
        {
            depth++;

            IExpression expression;
            while (Step(out expression))
            {
            }

            //if (ProcessStack.Count > 0)
            //{
            var closePar = expression as ParenthesesExpression;

            if (closePar == null)
                Fail("Internal parsing error: Expected closing parentheses.");
            else
            {
                depth--;
                return true;
            }
            //}
            //else
            //    Fail("Internal parsing error: Expected closing parentheses.");

            return false;
        }

        public bool Fail(string message)
        {
            Dispose();
            HasFailed = true;
            ValueStack.Push(new InvalidExpression(message));
            return false;
            //Results.Add(new InvalidExpression(message));
        }

        public void Message(string message)
        {
            ValueStack.Push(new MessageExpression(message));
        }

        public void Dispose()
        {
            processStack.Clear();
            ValueStack.Clear();
            Results.Clear();
        }
    }
}
