using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat
{
    public class Part
    {
        public PartNumber Number { get; }
        public PartDescription Description { get; }

        public Part(PartNumber number, PartDescription description)
        {
            Number = number ?? throw new ArgumentNullException(nameof(number));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public override bool Equals(object obj)
        {
            return obj is Part other
                && Number.Equals(other.Number);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 1913049801;
                hashCode = hashCode * -1521134295 + Number.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{Number} {Description}";
        }
    }
}
