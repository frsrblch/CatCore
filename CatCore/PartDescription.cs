using Result;
using System;

namespace Cat
{
    public class PartDescription : IEquatable<PartDescription>
    {
        public const int MaxLength = 40;

        private readonly string _value;

        public PartDescription(string value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"The description given is not a valid part description: '{value}'");
            }

            _value = TrimToUpper(value);
        }

        public static Result<PartDescription, Error> Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new Error();
            }

            return new PartDescription(value);
        }

        private static string TrimToUpper(string value)
        {
            value = value
                .ToUpperInvariant()
                .Trim();

            if (value.Length > MaxLength)
            {
                return value.Substring(0, MaxLength);
            }

            return value;
        }

        public static implicit operator string(PartDescription description) => description.ToString();

        public static bool operator ==(PartDescription d1, PartDescription d2) => d1.Equals(d2);

        public static bool operator !=(PartDescription d1, PartDescription d2) => !d1.Equals(d2);

        public override string ToString() => _value;

        public bool Equals(PartDescription other)
        {
            if (other is null)
            {
                return false;
            }

            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PartDescription);
        }

        public override int GetHashCode()
        {
            return -1584136870 + _value.GetHashCode();
        }

        public bool Contains(string value) => _value.Contains(value.ToUpper());
    }
}
