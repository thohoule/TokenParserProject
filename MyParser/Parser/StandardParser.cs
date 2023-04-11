//using System;
//using System.Collections.Generic;

//namespace MyParser
//{
//    public partial class ExpressionParser
//    {
//        public static ExpressionParser StandardParser
//        {
//            get
//            {
//                return new ExpressionParser(new Dictionary<string, Func<IOperatorExpression>>()
//                {
//                    { "+", () => new AddExpression() },
//                    { "-", () => new SubtractExpression() },
//                    { "*", () => new MultiplyExpression() },
//                    { "/", () => new DivideExpression() },
//                });

//            }
//        }
//    }
//}
