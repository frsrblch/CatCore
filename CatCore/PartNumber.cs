using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Result;

namespace CatCore
{
    public enum PartNumberParseError
    {
        InvalidInput
    }

    public class PartNumber
    {
        public const int MinLength = 1;
        public const int MaxLength = 30;

        private readonly string _value;

        private PartNumber(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static Result<PartNumber, PartNumberParseError> Parse(string value)
        {
            return PartNumberParseError.InvalidInput;
        }
    }
}
