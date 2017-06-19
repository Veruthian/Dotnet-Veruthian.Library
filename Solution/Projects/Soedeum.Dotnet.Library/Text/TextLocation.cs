using System;
using Soedeum.Dotnet.Library.Utility;

namespace Soedeum.Dotnet.Library.Text
{
    public struct TextLocation : IEquatable<TextLocation>
    {
        readonly int position;

        readonly int line;

        readonly int column;


        public TextLocation(int position, int line, int column)
        {
            this.position = position;
            this.line = line;
            this.column = column;
        }


        public int Position { get => position; }

        public int Line { get => line; }

        public int Column { get => column; }


        public TextLocation IncrementLine(int lengthToEnd = 1)
        {
            return new TextLocation(this.position + lengthToEnd, this.line + 1, 0);
        }

        public TextLocation MoveToNext(char current, char next, bool acceptNulls = true)
        {
            switch (current)
            {
                case '\0':
                    return (acceptNulls) ? this + 1 : this;

                case '\n':
                    return this.IncrementLine();

                case '\r':
                    return (next == '\n') ? this + 1 : this.IncrementLine();

                default:
                    return this + 1;
            }
        }

        public TextLocation MoveThrough(char current, string following, bool acceptNulls = true)
        {
            TextLocation result = this;

            foreach (char next in following)
            {
                result = result.MoveToNext(current, next, acceptNulls);

                current = next;
            }

            return result;
        }


        public bool Equals(TextLocation position)
        {
            return (this.position == position.position) &&
                   (this.line == position.line) &&
                   (this.column == position.column);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var position = (TextLocation)obj;

            return this.Equals(position);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return HashCodeCombiner.Combiner.Combine(Position, Line, Column);
        }

        public static bool operator ==(TextLocation left, TextLocation right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TextLocation left, TextLocation right)
        {
            return !left.Equals(right);
        }


        public static TextLocation operator +(TextLocation position, int amount)
        {
            return new TextLocation(position.position + amount, position.line, position.column + amount);
        }

        public static TextLocation operator ++(TextLocation position)
        {
            return new TextLocation(position.position + 1, position.line, position.column + 1);
        }


        public override string ToString()
        {
            return string.Format("Position: {0}; Line: {1}; Column: {2}", Position, Line, Column);
        }        
    }
}