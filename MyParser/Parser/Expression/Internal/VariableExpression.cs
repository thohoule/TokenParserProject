using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyParser.Expression
{
    internal class VariableExpression : IOperatorExpression
    {
        private VauleExpression result;

        public VauleExpression GetValue
        {
            get
            {
                return result ?? new InvalidExpression("Expression has not been processed yet.");
            }
        }
        public ExpressionType Type { get { return ExpressionType.Operator; } }
        public string VariableName { get; private set; }

        public VariableExpression(string variableName)
        {
            VariableName = variableName;
        }

        public bool Process(IExpressionWalker walker)
        {
            //result = ExpressionParser.ExternalLookup(VariableName);
            result = walker.ParserVariables.Get(VariableName);
            walker.ValueStack.Push(result);
            return true;
        }
    }
}
