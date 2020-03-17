using Cat;
using OptionType;
using ResultType;
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
            List<SisLine> newLines = new List<SisLine>();

            Option<SisLine> previousParentLine() => newLines.LastOrDefault(ln => ln.Position.IsParentPosition());

            foreach (var line in lines)
            {
                var previousParent = previousParentLine();

                // fold duplicate lines into each other (e.g., sensors 14, 14A, 14B should all be one line)
                if (previousParent.IsSome)
                {
                    var previous = previousParent.ValueOrThrow();
                    if (line.Number.Equals(previous.Number) && line.Position.Number == previous.Position.Number)
                    {
                        previous.Quantity += line.Quantity;
                        continue;
                    }
                }

                if (line.Position.IsParentPosition())
                {
                    // when qty is zero, check if the previous line is a similar part and copy qty
                    previousParentLine().Match(
                        previous =>
                        {
                            if (line.Quantity == 0 && previous.Description.FirstWord() == line.Description.FirstWord())
                            {
                                line.Quantity = previous.Quantity;
                            }
                        }, 
                        () => { });
                }
                else
                {
                    // skip child parts for purchasable sensors, switches, and harnesses                    
                    if (previousParent.IsSome)
                    {
                        var previous = previousParent.ValueOrThrow();
                        var prevDesc = previous.Description;
                        var isRelevantPart = prevDesc.IsHarnessAs() || prevDesc.IsSensor() || prevDesc.IsSwitch();
                        if (isRelevantPart && previous.Purchasable)
                        {
                            continue;
                        }
                    }
                }

                newLines.Add(line);
            }

            return newLines;
        }

        public static IEnumerable<Result<SisGroup, Error>> ReadFromDirectory(string directeory)
        {
            return System.IO.Directory.GetFiles(directeory)
                .Where(f => f.EndsWith(".sis", StringComparison.OrdinalIgnoreCase))
                .Select(f => System.IO.File.ReadAllText(f))
                .Select(contents => SisGroupExtensions.DeserializeSisGroup(contents))
                .ToList();
        }
    }
}
