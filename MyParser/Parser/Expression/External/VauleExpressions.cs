using System;

namespace MyParser.Expression
{
    public abstract class VauleExpression : IExpression
    {
        protected object internalObject;

        public object RawValue { get { return internalObject; } }
        public object Value { get { return internalObject; } }
        public abstract Type GetValueType { get; }

        public ExpressionType Type { get { return ExpressionType.Value; } }

        public abstract class NumericVauleExpression : VauleExpression
        {
            public new float Value { get { return Convert.ToSingle(internalObject); } }
        }
        
        public sealed class FloatVauleExpression : NumericVauleExpression
        {
            //public new float Value { get; set; }
            public override Type GetValueType { get { return typeof(float); } }

            public FloatVauleExpression(float value)
            {
                internalObject = value;
            }
        }

        public sealed class IntVauleExpression : NumericVauleExpression
        {
            public new int Value { get { return (int)internalObject; } }
            public override Type GetValueType { get { return typeof(int); } }

            public IntVauleExpression(int value)
            {
                internalObject = value;
            }
        }

        public sealed class StringVauleExpression : VauleExpression
        {
            public new string Value { get { return (string)internalObject; } }
            public override Type GetValueType { get { return typeof(string); } }

            public StringVauleExpression(string value)
            {
                internalObject = value;
            }
        }

        public sealed class BooleanVauleExpression : VauleExpression
        {
            public new bool Value { get { return (bool)internalObject; } }
            public override Type GetValueType { get { return typeof(bool); } }

            public BooleanVauleExpression(bool value)
            {
                internalObject = value;
            }
        }
    }
}
