using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Result;

namespace CatCore
{
    public enum PartNumberParseError
    {
        InvalidInput
    }

    public class PartNumber : IEquatable<PartNumber>
    {
        // Regex pattern examples:                            (  123-4567  |   1A-2345    |  20R-2345  )
        private readonly static Regex Pattern = new Regex(@"\b(\d{3}-?\d{4}|\d[A-Z]-?\d{4}|\d?0R-?\d{4})\b", RegexOptions.IgnoreCase);

        public const int MinLength = 1;
        public const int MaxLength = 30;

        private readonly string _value;

        internal PartNumber(string value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _value = value.Replace("-", string.Empty).ToUpperInvariant();
        }

        public static Result<PartNumber, Unit> Parse(string value)
        {
            if (value is null)
            {
                return new Unit();
            }

            var match = Pattern.Match(value);
            if (match.Success)
            {
                return new PartNumber(match.Value);
            }

            return new Unit();
        }

        public static IEnumerable<PartNumber> ParseAll(string value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Pattern
                .Matches(value)
                .Cast<Match>()
                .Select(match => new PartNumber(match.Value))
                .Distinct()
                .ToList();
        }

        public bool Equals(PartNumber other)
        {
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            return obj is PartNumber other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return 193 * _value.GetHashCode();
            }
        }

        public override string ToString()
        {
            return _value;
        }

        public string ToText()
        {
            var sb = new StringBuilder(8);
            int midpoint = _value.Length - 4;
            sb.Append(_value, 0, midpoint);
            sb.Append('-');
            sb.Append(_value, midpoint, 4);
            return sb.ToString();
        }

        public static bool operator ==(PartNumber p1, PartNumber p2) => p1.Equals(p2);

        public static bool operator !=(PartNumber p1, PartNumber p2) => !p1.Equals(p2);
    }
}
