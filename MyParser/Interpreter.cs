using System;
using System.Collections.Generic;

namespace MyParser
{
    public class Interpreter
    {
        private ExpressionParser parser;
        //private Dictionary<string, VauleExpression> externalVariables;

        public ExternalVariables ParserVariables { get; private set; }

        public Interpreter(ExpressionParser parser)
        {
            this.parser = parser;
            ParserVariables = new ExternalVariables();
            //externalVariables = new Dictionary<string, VauleExpression>();
        }

        public T Translate<T>(string expression)
        {
            T result;
            TryTranslate<T>(expression, out result);

            return result;
        }

        public bool TryTranslate<T>(string expression, out T result)
        {
            FunctionExpression functionResult;
            FunctionExpressionWalker walker;

            if (parser.TryParse(expression, out functionResult))
            {
                walker = new FunctionExpressionWalker(ParserVariables, functionResult);

                if (walker.StepThrough() && walker.Results.Count != 0)
                {
                    result = (T)walker.Results.Dequeue().Value;
                    return true;
                }
            }

            result = default(T);
            return false;
        }

        public string RunExpression(string expression)
        {
            string result;
            if (TryTranslate<string>(expression, out result))
                return result;

            return "Error";
        }
    }
}
