using Cat;
using OptionType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat
{
    public class SisGroup
    {
        public PartNumber Number { get; }
        public PartDescription Description { get; }
        public IReadOnlyList<SisLine> Lines { get; }

        public SisGroup(PartNumber groupNumber, PartDescription groupDescription, IReadOnlyList<SisLine> lines)
        {
            Number = groupNumber ?? throw new ArgumentNullException(nameof(groupNumber));
            Description = groupDescription ?? throw new ArgumentNullException(nameof(groupDescription));
            Lines = CompressMatchingLines(lines ?? throw new ArgumentNullException(nameof(lines)));
        }

        private static IReadOnlyList<SisLine> CompressMatchingLines(IReadOnlyList<SisLine> lines)
        {
            Option<SisLine> previousLine = default;

            List<SisLine> newLines = new List<SisLine>();

            foreach (var line in lines)
            {
                if (previousLine.IsNone)
                {
                    previousLine = line;
                    newLines.Add(line);
                }
                else
                {
                    // if the next line is a repeat of the previous one,
                    // merge the second into the first rather than keeping the duplicate line
                    var previous = previousLine.ValueOrThrow();
                    if (line.Number == previous.Number
                        && line.Position.Number == previous.Position.Number)
                    {
                        previous.Quantity += line.Quantity;
                    }
                    else
                    {
                        previousLine = line;
                        newLines.Add(line);
                    }
                }
            }

            return newLines;
        }
    }
}
