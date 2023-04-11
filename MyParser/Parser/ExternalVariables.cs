using System;
using System.Collections.Generic;
using MyParser.Expression;

namespace MyParser
{
    public class ExternalVariables
    {
        private Dictionary<string, VauleExpression> variables;

        public ExternalVariables()
        {
            variables = new Dictionary<string, VauleExpression>();
        }

        public void AddOrUpdateVariable(string name, bool value)
        {
            addVariable(name, new VauleExpression.BooleanVauleExpression(value));
        }

        public void AddOrUpdateVariable(string name, float value)
        {
            addVariable(name, new VauleExpression.FloatVauleExpression(value));
        }

        public void AddOrUpdateVariable(string name, int value)
        {
            addVariable(name, new VauleExpression.IntVauleExpression(value));
        }

        public void AddOrUpdateVariable(string name, string value)
        {
            addVariable(name, new VauleExpression.StringVauleExpression(value));
        }

        public void Clear()
        {
            variables.Clear();
        }

        public VauleExpression Get(string name)
        {
            VauleExpression value;
            if (variables.TryGetValue(name, out value))
                return value;

            return new VauleExpression.IntVauleExpression(0);
        }

        private void addVariable(string name, VauleExpression value)
        {
            try
            {
                variables.Add(name, value);
            }
            catch (ArgumentException)
            {
                variables[name] = value;
            }
        }
    }
}
