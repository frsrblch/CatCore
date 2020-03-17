using Cat;
using ResultType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat
{
    public static class SisGroupExtensions
    {
        public static string Serialize(this SisLine line)
        {
            var pos = line.Position.ToString();

            var num = line.Number;
            var desc = line.Description;
            var qty = line.Quantity;
            var purch = line.Purchasable ? "P" : string.Empty;
            var gp = line.IsChildGroup ? "G" : string.Empty;
            var ccr = line.IsCcrPart ? "C" : string.Empty;

            return $"{pos}\t{qty}\t{purch}{gp}{ccr}\t{num}\t{desc}";
        }

        public static Result<SisLine, Error> DeserializeSisLine(string line)
        {
            var cells = line.Split('\t');

            if (cells.Length < 5)
            {
                return new Error($"Invalid tab count, cannot parse as SisLine: {line}");
            }

            var pos = DiagramPosition.TryFrom(cells[0]);

            if (!int.TryParse(cells[1], out int qty))
            {
                return new Error($"Could not parse line quantity: {line}");
            }

            bool purch = cells[2].Contains("P");
            bool gp = cells[2].Contains("G");
            bool ccr = cells[2].Contains("C");

            var pn = PartNumber.Parse(cells[3]);

            var desc = PartDescription.Parse(cells[4]);

            return pn
                .CombineWith(desc)
                .CombineWith(pos)
                .Match(
                    ok => new SisLine(ok.Item1, ok.Item2, ok.Item3, qty, purch, gp, ccr),
                    err => Result.Error<SisLine, Error>(err));
        }

        public static string Serialize(this SisGroup group)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"GROUP\t{group.Number}\t{group.Description}");
            foreach (var line in group.Lines)
            {
                sb.AppendLine(line.Serialize());
            }
            return sb.ToString();
        }

        public static Result<SisGroup, Error> DeserializeSisGroup(string text)
        {
            var lines = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length < 1) return new Error();
            var firstLine = lines[0].Split('\t');

            if (firstLine.Length < 3) return new Error("Could not separate group description from group number.");

            var groupNumber = PartNumber.Parse(firstLine[1]);
            var groupDesc = PartDescription.Parse(firstLine[2]);

            var lineResults = lines
                .Skip(1)    // skip header line with group information
                .Select(txt => DeserializeSisLine(txt))
                .ToList();

            // fail if any lines could not be deserialized
            if (lineResults.Any(ln => ln.IsError))
            {
                return lineResults.First(ln => ln.IsError).ErrorOrThrow();
            }

            var sisLines = lineResults
                .Select(ln => ln.ValueOrThrow())    // all results verified
                .ToList();

            return groupNumber
                .CombineWith(groupDesc)
                .Map(t => new SisGroup(t.Item1, t.Item2, sisLines));
        }

        public static Result<Okay, Error> WriteToDirectory(this SisGroup group, string directory)
        {
            if (!Directory.Exists(directory))
            {
                return new Error($"Invalid directory: {directory}");
            }

            string filename = $"{group.Number}.sis";
            string path = Path.Combine(directory, filename);
            string contents = group.Serialize();

            try
            {
                File.WriteAllText(path, contents);
                return new Okay();
            }
            catch
            {
                return new Error($"Error writing serialized file: {path}");
            }
        }

        public static Result<Okay, Error> WriteToDirectory(this IEnumerable<SisGroup> groups, string directory)
        {
            var results = groups
                .Select(gp => WriteToDirectory(gp, directory))
                .ToList();

            if (results.Any(r => r.IsError))
            {
                return results.First(r => r.IsError).ErrorOrThrow();
            }

            return new Okay();
        }
    }
}
