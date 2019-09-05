using Cat;
using ResultType;
using System;
using System.Collections.Generic;
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

            return $"{pos}\t{num}\t{desc}\t{qty}\t{purch}{gp}";
        }

        public static Result<SisLine, Error> DeserializeSisLine(string line)
        {
            var cells = line.Split('\t');

            if (cells.Length < 5)
            {
                return new Error();
            }

            var pos = DiagramPosition.TryFrom(cells[0]);

            var pn = PartNumber.Parse(cells[1]);

            var desc = PartDescription.Parse(cells[2]);

            if (!int.TryParse(cells[3], out int qty))
            {
                return new Error();
            }

            bool purch = cells[4].Contains("P");
            bool gp = cells[4].Contains("G");

            return pn
                .CombineWith(desc)
                .CombineWith(pos)
                .Match(
                    ok => new SisLine(ok.Item1, ok.Item2, ok.Item3, qty, purch, gp),
                    err => Result.Error<SisLine, Error>(err));
        }

        public static string Serialize(this SisGroup group)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"GROUP {group.Number} {group.Description}");
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
            var firstLine = lines[0].Substring(6);
            var endOfPn = firstLine.IndexOf(' ');

            if (endOfPn < 1) return new Error();

            var groupNumber = PartNumber.Parse(firstLine.Substring(0, endOfPn));
            var groupDesc = PartDescription.Parse(firstLine.Substring(endOfPn));

            var lineResults = lines
                .Skip(1)
                .Select(txt => DeserializeSisLine(txt))
                .ToList();

            // fail if any lines 
            if (lineResults.Any(ln => ln.IsError)) return new Error();

            var sisLines = lineResults
                .Select(ln => ln.ValueOrThrow())    // all results verified
                .ToList();

            return groupNumber
                .CombineWith(groupDesc)
                .Map(t => new SisGroup(t.Item1, t.Item2, sisLines));
        }
    }
}
