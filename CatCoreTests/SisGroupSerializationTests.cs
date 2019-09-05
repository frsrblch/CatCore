using System;
using System.Collections.Generic;
using Cat;
using Xunit;

namespace CatCoreTests
{
    public class SisGroupSerializationTests
    {
        private SisGroup TestGroup => new SisGroup(new PartNumber("1234567"), new PartDescription("TEST GP"), TestLines);

        private IReadOnlyList<SisLine> TestLines => new List<SisLine>()
        {
            new SisLine(new PartNumber("2345678"), new PartDescription("Test Part 1"), new DiagramPosition(1), 2),
            new SisLine(new PartNumber("3456789"), new PartDescription("Test Part 2"), new DiagramPosition(2), 3),
        };

        [Fact]
        public void SerializeDeserialize()
        {
            var group = TestGroup;

            var serialized = group.Serialize();
            var deserialized = SisGroupExtensions.DeserializeSisGroup(serialized).ValueOrThrow();

            Assert.Equal(group.Number, deserialized.Number);
            Assert.Equal(group.Description, deserialized.Description);
            Assert.Equal(group.Lines.Count, deserialized.Lines.Count);
        }
    }
}
