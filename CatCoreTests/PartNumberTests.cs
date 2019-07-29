using System;
using Xunit;
using Result;
using CatCore;

namespace CatCoreTests
{
    public class PartNumberTests
    {
        [Fact]
        public void Parse_GivenNull_ReturnsError()
        {
            var result = PartNumber.Parse(null);

            Assert.True(result.ContainsError(PartNumberParseError.InvalidInput));
        }
    }
}
