using Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cat
{
    public class MachineSerialPrefix : IEquatable<MachineSerialPrefix>
    {
        private readonly string _value;

        internal MachineSerialPrefix(string value)
        {
            _value = value.Trim().ToUpperInvariant();
        }

        public static Result<MachineSerialPrefix, Error> Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new Error();
            }

            value = value.Trim().ToUpperInvariant();

            if (value.Length == 3 && value.All(char.IsLetterOrDigit))
            {
                return new MachineSerialPrefix(value);
            }

            return new Error();
        }

        public static implicit operator string(MachineSerialPrefix machineSerialPrefix) => machineSerialPrefix.ToString();

        public bool Equals(MachineSerialPrefix other)
        {
            if (other is null)
            {
                return false;
            }

            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MachineSerialPrefix);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return 1123 * _value.GetHashCode();
            }
        }

        public override string ToString() => _value;
    }
}
