﻿using System;
using Xunit;
using ResultType;
using Cat;
using System.Linq;

namespace CatCoreTests
{
    public class PartNumberTests
    {
        private string LongPN => "1234567";

        private string HyphenatedLongPN => "123-4567";

        private string ShortPN => "1A2345";

        private string HyphenatedShortPN => "1A-2345";

        private string RemanPN => "20R1234";

        private string HyphenatedRemanPN => "20R-1234";


        [Fact]
        public void Parse_GivenNull_ReturnsError()
        {
            var result = PartNumber.Parse(null);

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenEmptyString_ReturnsError()
        {
            var result = PartNumber.Parse(string.Empty);

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenNotAPartNumber_ReturnsError()
        {
            var result = PartNumber.Parse("not a part");

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenPartNumber_ReturnsOkay()
        {
            var result = PartNumber.Parse(LongPN);

            Assert.True(result.IsOkay);
            Assert.True(result.Contains(new PartNumber(LongPN)));
        }

        [Fact]
        public void Parse_GivenHyphenatedPartNumber_ReturnsOkay()
        {
            var result = PartNumber.Parse(HyphenatedLongPN);

            Assert.True(result.IsOkay);
            Assert.True(result.Contains(new PartNumber(LongPN)));
        }

        [Fact]
        public void Parse_GivenWrongHyphenation1_ReturnsError()
        {
            var result = PartNumber.Parse("12-34567");

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenWrongHyphenation2_ReturnsError()
        {
            var result = PartNumber.Parse("1234-567");

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenShortPartNumber_ReturnsOkay()
        {
            var result = PartNumber.Parse(ShortPN);

            Assert.True(result.IsOkay);
            Assert.True(result.Contains(new PartNumber(ShortPN)));
        }

        [Fact]
        public void Parse_GivenHyphantedShortPartNumber_ReturnsOkay()
        {
            var result = PartNumber.Parse(HyphenatedShortPN);

            Assert.True(result.IsOkay);
            Assert.True(result.Contains(new PartNumber(ShortPN)));
        }

        [Fact]
        public void Parse_GivenWrongHyphenation3_ReturnsError()
        {
            var result = PartNumber.Parse("1-A2345");

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenWrongHyphenation4_ReturnsError()
        {
            var result = PartNumber.Parse("1A2-345");

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenUntrimmedPartNumber_ReturnsOkay()
        {
            var result = PartNumber.Parse("  1234567  ");

            Assert.True(result.IsOkay);
            Assert.True(result.Contains(new PartNumber("1234567")));
        }

        [Fact]
        public void Parse_GivenTooLongLongPartNumber_ReturnsError()
        {
            var result = PartNumber.Parse("  12345678  ");

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenTooLongShortPartNumber_ReturnsError()
        {
            var result = PartNumber.Parse("  1A23456  ");

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenRemanPartNumber_ReturnsOkay()
        {
            var result = PartNumber.Parse(RemanPN);

            Assert.True(result.Contains(new PartNumber(RemanPN)));
        }

        [Fact]
        public void Parse_GivenHyphantedRemanPartNumber_ReturnsOkay()
        {
            var result = PartNumber.Parse(HyphenatedRemanPN);

            Assert.True(result.Contains(new PartNumber(RemanPN)));
        }

        [Fact]
        public void Parse_GivenWrongHyphenation5_ReturnsError()
        {
            var result = PartNumber.Parse("20R1-234");

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenWrongHyphenation6_ReturnsError()
        {
            var result = PartNumber.Parse("20-R1234");

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenTooLongRemanPartNumber_ReturnsError()
        {
            var result = PartNumber.Parse("  20R12345  ");

            Assert.True(result.IsError);
        }

        [Fact]
        public void Parse_GivenLetterOtherThanR_ReturnsError()
        {
            var result = PartNumber.Parse("20B1234");

            Assert.True(result.IsError);
        }

        [Fact]
        public void ToText_GivenNumericPartNumber_ReturnsHyphenated()
        {
            var partNumber = new PartNumber(LongPN);

            Assert.Equal("123-4567", partNumber.ToText());
        }

        [Fact]
        public void ToText_GivenAlphanumericPartNumber_ReturnsHyphenated()
        {
            var partNumber = new PartNumber(ShortPN);

            Assert.Equal("1A-2345", partNumber.ToText());
        }

        [Fact]
        public void ToText_GivenRemanPartNumber_ReturnsHyphenated()
        {
            var partNumber = new PartNumber(RemanPN);

            Assert.Equal("20R-1234", partNumber.ToText());
        }

        [Fact]
        public void Parse_AlphanumericPartNumber_IsCaseInvariant()
        {
            var upper = PartNumber.Parse("1A2345");
            var lower = PartNumber.Parse("1a2345");

            Assert.Equal(upper, lower);
        }

        [Fact]
        public void Parse_RemanPartNumber_IsCaseInvariant()
        {
            var upper = PartNumber.Parse("20R1234");
            var lower = PartNumber.Parse("20r1234");

            Assert.Equal(upper, lower);
        }

        [Fact]
        public void Parse_GivenShortRemanPartNumber_IsOkay()
        {
            var result = PartNumber.Parse("0r1234");

            Assert.Equal("0R1234", result.ValueOrThrow());
        }

        [Fact]
        public void Parse_LongPartNumberWithPunctuation_IsOkay()
        {
            var result = PartNumber.Parse(":1234567.");

            Assert.Equal("1234567", result.ValueOrThrow());
        }

        [Fact]
        public void Parse_ShortPartNumberWithPunctuation_IsOkay()
        {
            var result = PartNumber.Parse(":1A2345.");

            Assert.Equal("1A2345", result.ValueOrThrow());
        }

        [Fact]
        public void Parse_RemanPartNumberWithPunctuation1_IsOkay()
        {
            var result = PartNumber.Parse(":20R1234.");

            Assert.Equal("20R1234", result.ValueOrThrow());
        }

        [Fact]
        public void Parse_RemanPartNumberWithPunctuation2_IsOkay()
        {
            var result = PartNumber.Parse("20R1234");

            Assert.Equal("20R1234", result.ValueOrThrow());
        }

        [Fact]
        public void ParseAll_GivenMultiplePartNumbers_ReturnsAll()
        {
            var text = " this part: 1234567, this duplicate part: 1234567, that part: 1A2345, and the other part: 20R3456 are all part numbers";
            var partNumbers = PartNumber.ParseAll(text);

            Assert.Equal(3, partNumbers.Count());
            Assert.Contains(new PartNumber("1234567"), partNumbers);
            Assert.Contains(new PartNumber("1A2345"), partNumbers);
            Assert.Contains(new PartNumber("20R3456"), partNumbers);
        }

        [Fact]
        public void Equals_GivenSameValue_ReturnsAsEqual()
        {
            var part = new PartNumber("1234567");
            var same = new PartNumber("1234567");

            Assert.Equal(part, same);
        }

        [Fact]
        public void Equals_GivenDifferentValue_ReturnsAsEqual()
        {
            var part = new PartNumber("1234567");
            var different = new PartNumber("2234567");

            Assert.NotEqual(part, different);
        }

        [Fact]
        public void GetHashCode_GivenSameValue_ReturnsAsEqual()
        {
            var part = new PartNumber("1234567").GetHashCode();
            var same = new PartNumber("1234567").GetHashCode();

            Assert.Equal(part, same);
        }

        [Fact]
        public void GetHashCode_GivenDifferentValue_ReturnsAsEqual()
        {
            var part = new PartNumber("1234567").GetHashCode();
            var different = new PartNumber("2234567").GetHashCode();

            Assert.NotEqual(part, different);
        }

        [Fact]
        public void IsRemanPart_LongPart_ReturnsFalse()
        {
            var part = new PartNumber("1234567");

            Assert.False(part.IsRemanPart());
        }

        [Fact]
        public void IsRemanPart_ShortPart_ReturnsFalse()
        {
            var part = new PartNumber("1R2345");

            Assert.False(part.IsRemanPart());
        }

        [Fact]
        public void IsRemanPart_ShortestRemanPart_ReturnsTrue()
        {
            var part = new PartNumber("0R2345");

            Assert.True(part.IsRemanPart());
        }

        [Fact]
        public void IsRemanPart_ShortRemanPart_ReturnsTrue()
        {
            var part = new PartNumber("10R2345");

            Assert.True(part.IsRemanPart());
        }

        [Fact]
        public void IsRemanPart_ShortRemanPart11_ReturnsTrue()
        {
            var part = new PartNumber("11R2345");

            Assert.True(part.IsRemanPart());
        }

        [Fact]
        public void FirstWordTest_GivenDash()
        {
            var desc = new PartDescription("BOLT-THREADED");

            Assert.Equal("BOLT", desc.FirstWord());
        }

        [Fact]
        public void FirstWordTest_GivenSpace()
        {
            var desc = new PartDescription("BOLT THREADED");

            Assert.Equal("BOLT", desc.FirstWord());
        }
    }
}
