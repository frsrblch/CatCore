using OptionType;
using ResultType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat
{
    public struct DiagramPosition : IEquatable<DiagramPosition>, IComparable<DiagramPosition>
    {
        public int Number { get; }
        public Option<char> Subposition { get; }

        public DiagramPosition(int number, Option<char> subposition = default)
        {
            Number = number;
            Subposition = subposition.Map(c => char.ToUpper(c));
        }

        public static Result<DiagramPosition, Error> TryFrom(string position)
        {
            position = position.Trim();

            if (position == "R")
            {
                return Reman;
            }


            if (int.TryParse(position, out var number))
            {
                return new DiagramPosition(number);
            }

            if (position.Length > 0)
            {
                string maybeNumber = position.Substring(0, position.Length - 1);
                char subposition = position[position.Length - 1];
                if (int.TryParse(maybeNumber, out int number1) && char.IsLetter(subposition))
                {
                    return new DiagramPosition(number1, subposition);
                }
            }

            return default;
        }

        public int CompareTo(DiagramPosition other)
        {
            if (Equals(other)) return 0;

            if (Number.Equals(other.Number))
            {
                if (Subposition.IsNone)
                {
                    return -1;
                }
                else if (other.Subposition.IsNone)
                {
                    return 1;
                }
                else
                {
                    return Subposition.ValueOrThrow().CompareTo(other.Subposition.ValueOrThrow());
                }
            }
            else
            {
                return Number.CompareTo(other.Number);
            }
        }

        public bool Equals(DiagramPosition other)
        {
            return Number.Equals(other.Number) && Subposition.Equals(other.Subposition);
        }

        public override bool Equals(object obj)
        {
            return obj is DiagramPosition other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 41;
                hash = (hash * 599) ^ Number.GetHashCode();
                return (hash * 599) ^ Subposition.GetHashCode();
            }
        }

        public override string ToString()
        {
            var num = Number;
            return Subposition.Match(
                ch => $"{num}{ch}",
                () => num.ToString());
        }

        public DiagramPosition GetNextSubposition()
        {
            var newSubposition = Subposition.Match(
                subPos => (char)(subPos + 1),
                () => 'A');

            return new DiagramPosition(Number, newSubposition);
        }

        public static readonly DiagramPosition Reman = new DiagramPosition(0, 'R');

        public bool IsParentPosition()
        {
            return Subposition.IsNone && Number > 0;
        }
    }
}
