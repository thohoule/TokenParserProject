using System;

namespace MyParser.LexerAnalysis
{
    public struct Token : IEquatable<int>, IEquatable<StandardTokenType>
    {
        public int Value { get; private set; }

        public Token(StandardTokenType type) : this((int)type) { }

        public Token(int value)
        {
            Value = value;
        }

        public bool Equals(int other)
        {
            return Value == other;
        }

        public bool Equals(StandardTokenType other)
        {
            return Value == (int)other;
        }

        public static implicit operator int(Token token)
        {
            return token.Value;
        }

        public static implicit operator Token(int value)
        {
            return new Token(value);
        }

        public static implicit operator StandardTokenType(Token token)
        {
            return (StandardTokenType)token.Value;
        }

        public static implicit operator Token(StandardTokenType value)
        {
            return new Token(value);
        }

        public static bool operator ==(Token token, int value)
        {
            return token.Value == value;
        }

        public static bool operator !=(Token token, int value)
        {
            return token.Value != value;
        }

        public static bool operator ==(Token token, StandardTokenType value)
        {
            return token.Value == (int)value;
        }

        public static bool operator !=(Token token, StandardTokenType value)
        {
            return token.Value != (int)value;
        }
    }
}
