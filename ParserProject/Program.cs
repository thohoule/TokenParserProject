using System;
using System.Collections.Generic;
using MyParser;

namespace ParserProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //NormalTest();
            //VariableTest();
            PrintTest();

            Console.ReadKey();
        }

        static void NormalTest()
        {
            string expression = "2 + 5";
            //string expression = "2 + (5 + 3)";
            //string expression = "2 + ((5 + 3) + (7 + 9))";
            //string expression = "2 + (4 * 5)";
            //string expression = "2 + {name}";

            //List<VauleExpression> results;
            var interpreter = new Interpreter(new ExpressionParser());

            Console.WriteLine(interpreter.Translate<float>(expression));
        }

        static void VariableTest()
        {
            string expression = "2 + {name}";

            var interpreter = new Interpreter(new ExpressionParser());
            interpreter.ParserVariables.AddOrUpdateVariable("name", 8);

            Console.WriteLine(interpreter.Translate<float>(expression));
        }

        static void PrintTest()
        {
            string expression = "print \"hello {name}!\""; //add fomat command

            var interpreter = new Interpreter(new ExpressionParser());
            interpreter.ParserVariables.AddOrUpdateVariable("name", "Tyy");

            Console.WriteLine(interpreter.RunExpression(expression));
        }
    }
}
