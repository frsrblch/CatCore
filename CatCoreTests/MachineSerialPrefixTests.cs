using Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CatCoreTests
{
    public class MachineSerialPrefixTests
    {
        [Fact]
        public void Parse_GivenNull_ReturnsError()
        {
            var result = MachineSerialPrefix.Parse(null);

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenEmpty_ReturnsError()
        {
            var result = MachineSerialPrefix.Parse(string.Empty);

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenTooShort_ReturnsError()
        {
            var result = MachineSerialPrefix.Parse("12");

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenTooLong_ReturnsError()
        {
            var result = MachineSerialPrefix.Parse("1234");

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenCorrectLength_ReturnsOkay()
        {
            var result = MachineSerialPrefix.Parse("123");

            Assert.True(result.IsOkay);
        }

        [Fact]
        public void Parse_GivenInvalidCharacters_ReturnsError()
        {
            var result = MachineSerialPrefix.Parse("AB.");

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_CaseInvariant_AreEqual()
        {
            var lower = MachineSerialPrefix.Parse("abc").ValueOrThrow();
            var upper = MachineSerialPrefix.Parse("ABC").ValueOrThrow();

            Assert.Equal(lower, upper);
        }

        [Fact]
        public void Parse_WhiteSpaceInvariant_AreEqual()
        {
            var spaced = MachineSerialPrefix.Parse("  ABC  ").ValueOrThrow();
            var unspaced = MachineSerialPrefix.Parse("ABC").ValueOrThrow();

            Assert.Equal(spaced, unspaced);
        }

        [Fact]
        public void Equals_GivenEqual_AreEqual()
        {
            var serial = MachineSerialPrefix.Parse("LAJ").ValueOrThrow();
            var same = MachineSerialPrefix.Parse("LAJ").ValueOrThrow();

            Assert.Equal(serial, same);
        }

        [Fact]
        public void Equals_GivenNotEqual_AreNotEqual()
        {
            var serial = MachineSerialPrefix.Parse("LAJ").ValueOrThrow();
            var different = MachineSerialPrefix.Parse("SSP").ValueOrThrow();

            Assert.NotEqual(serial, different);
        }
    }
}
