using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyParser.LexerAnalysis;
using MyParser.Expression;

namespace MyParser
{
    public class ExpressionParser
    {
        private ExpressionDictionary operatorLookup;
        private Lexer lexer;

        //private static Lexer _lexer;
        //private static Lexer lexer { get { return _lexer ?? (_lexer = Lexer.StandardLexar); } }

        public ExpressionParser() : this(ExpressionDictionary.Standard) { }

        public ExpressionParser(ExpressionDictionary dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException();

            operatorLookup = dictionary;
            lexer = new Lexer();
        }

        public FunctionExpression Parse(string input)
        {
            FunctionExpression results;
            TryParse(input, out results);

            return results;
        }

        public bool TryParse(string input, out FunctionExpression results)
        {
            results = new FunctionExpression();
            if (input == "")
                return false;

            lexer.ReadFrom(new StringReader(input));
            //ExpressionWalker walker = new ExpressionWalker();

            while (lexer.Next())
            {
                if (!buildExpression(results))
                    break;
            }

            lexer.Dispose();

            //walker.StepThrough();
            //results = walker.Results.ToList();

            return results.IsValid;
        }

        private bool buildExpression(FunctionExpression function)
        {
            switch ((StandardTokenType)lexer.LastResult.Token)
            {
                case StandardTokenType.Quotes:
                    function.Enqueue(new VauleExpression.StringVauleExpression(lexer.LastResult.Content));
                    return true;
                case StandardTokenType.Float:
                    float floatNumber;
                    if (float.TryParse(lexer.LastResult.Content, out floatNumber))
                    {
                        function.Enqueue(new VauleExpression.FloatVauleExpression(floatNumber));
                        return true;
                    }
                    else
                        return function.Invalidate("Internal parsing error: Was expecting a float.");
                case StandardTokenType.Int:
                    int IntNumber;
                    if (int.TryParse(lexer.LastResult.Content, out IntNumber))
                    {
                        function.Enqueue(new VauleExpression.IntVauleExpression(IntNumber));
                        return true;
                    }
                    else
                        return function.Invalidate("Internal parsing error: Was expecting a int.");
                case StandardTokenType.True:
                    function.Enqueue(new VauleExpression.BooleanVauleExpression(true));
                    return true;
                case StandardTokenType.False:
                    function.Enqueue(new VauleExpression.BooleanVauleExpression(false));
                    return true;
                case StandardTokenType.Symbol:
                    return buildOperationalExpression(lexer.LastResult.Content, function);
                case StandardTokenType.Dot:
                    return true;
                case StandardTokenType.OpenParentheses:
                    function.Enqueue(new ParenthesesExpression(true));
                    return true;
                case StandardTokenType.CloseParentheses:
                    function.Enqueue(new ParenthesesExpression(false));
                    return true;
                case StandardTokenType.OpenCurlyBraces:
                    return buildVariableExpression(function);
                    //walker.ProcessStack.Enqueue(new CurlyBracesExpression(true));
                    //return true;
                case StandardTokenType.CloseCurlyBraces:
                    //walker.ProcessStack.Enqueue(new CurlyBracesExpression(false));
                    //return true;
                    return function.Invalidate("Internal parsing error: Was expecting a '{'.");
                case StandardTokenType.Space:
                    return true;
                default:
                    return function.Invalidate("Internal parsing error: Unexpected token.");

            }
        }

        private bool buildOperationalExpression(string symbol, FunctionExpression function)
        {
            Func<IOperatorExpression> operationExpression;
            if (operatorLookup.TryGetValue(symbol, out operationExpression))
                function.Enqueue(operationExpression.Invoke());
            else
                function.Enqueue(new VauleExpression.StringVauleExpression(symbol));

            return true;

            //walker.Fail(string.Format("Internal parsing error: Invaild operational expression \"{0}\".", symbol));
            //return false;
        }

        private bool buildVariableExpression(FunctionExpression function)
        {
            if (!lexer.Next() || lexer.LastResult.Token != StandardTokenType.Symbol)
                return function.Invalidate("Internal parsing error: Was expecting a variable name.");

            function.Enqueue(new VariableExpression(lexer.LastResult.Content));

            if (!lexer.Next() || lexer.LastResult.Token != StandardTokenType.CloseCurlyBraces)
                return function.Invalidate("Internal parsing error: Was expecting a '}'.");

            return true;
        }
    }
}
