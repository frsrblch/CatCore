using System;
using Cat;
using Xunit;

namespace CatCoreTests
{
    public class DiagramPositionTests
    {
        [Fact]
        public void CaseInvariance()
        {
            var left = new DiagramPosition(5, 'a');
            var right = new DiagramPosition(5, 'A');

            Assert.Equal(left, right);
        }

        [Fact]
        public void From_GivenNumber_ReturnsPosition()
        {
            var result = DiagramPosition.TryFrom("502");

            Assert.True(result.Contains(new DiagramPosition(502)));
        }

        [Fact]
        public void From_GivenNumberAndSubposition_ReturnsPosition()
        {
            var result = DiagramPosition.TryFrom("301a");

            Assert.True(result.Contains(new DiagramPosition(301, 'A')));
        }

        [Fact]
        public void From_GivenInvalid_ReturnsNone()
        {
            var result = DiagramPosition.TryFrom("a");

            Assert.True(result.IsError);
        }

        [Fact]
        public void From_GivenEmpty_ReturnsNone()
        {
            var result = DiagramPosition.TryFrom("");

            Assert.True(result.IsError);
        }

        [Fact]
        public void ToString_Number()
        {
            var pos = new DiagramPosition(3);

            Assert.Equal("3", pos.ToString());
        }

        [Fact]
        public void ToString_NumberWithSubposition()
        {
            var pos = new DiagramPosition(3, 'b');

            Assert.Equal("3B", pos.ToString());
        }

        [Fact]
        public void CompareTo_Equal_ReturnsZero()
        {
            var pos = new DiagramPosition(1);
            var same = new DiagramPosition(1);

            Assert.Equal(0, pos.CompareTo(same));
        }

        [Fact]
        public void CompareTo_Subposition_ReturnsLessThan()
        {
            var pos = new DiagramPosition(1);
            var same = new DiagramPosition(1, 'a');

            Assert.Equal(-1, pos.CompareTo(same));
        }

        [Fact]
        public void CompareTo_ParentPosition_ReturnsGreaterThan()
        {
            var pos = new DiagramPosition(1, 'a');
            var same = new DiagramPosition(1);

            Assert.Equal(1, pos.CompareTo(same));
        }

        [Fact]
        public void GetHashCode_GivenSame_ReturnsSame()
        {
            var pos = new DiagramPosition(1, 'a');
            var same = new DiagramPosition(1, 'a');

            Assert.Equal(pos.GetHashCode(), same.GetHashCode());
        }

        [Fact]
        public void GetHashCode_GivenDifferentNumber_ReturnsDifferent()
        {
            var pos = new DiagramPosition(1, 'a');
            var diff = new DiagramPosition(2, 'a');

            Assert.NotEqual(pos.GetHashCode(), diff.GetHashCode());
        }

        [Fact]
        public void GetHashCode_GivenNoSubpos_ReturnsDifferent()
        {
            var pos = new DiagramPosition(1, 'a');
            var diff = new DiagramPosition(1);

            Assert.NotEqual(pos.GetHashCode(), diff.GetHashCode());
        }

        [Fact]
        public void GetHashCode_GivenDifferentSubpos_ReturnsDifferent()
        {
            var pos = new DiagramPosition(1, 'a');
            var diff = new DiagramPosition(1, 'b');

            Assert.NotEqual(pos.GetHashCode(), diff.GetHashCode());
        }

        [Fact]
        public void GetNextSubposition_Given1_Returns1A()
        {
            var pos = new DiagramPosition(1);
            var next = new DiagramPosition(1, 'A');

            Assert.Equal(next, pos.GetNextSubposition());
        }

        [Fact]
        public void GetNextSubposition_Given1A_Returns1B()
        {

            var pos = new DiagramPosition(1, 'A');
            var next = new DiagramPosition(1, 'B');

            Assert.Equal(next, pos.GetNextSubposition());
        }

        [Fact]
        public void TryFrom_GivenR_Returns0R()
        {
            var reman = DiagramPosition.TryFrom("R").ValueOrThrow();

            Assert.Equal(new DiagramPosition(0, 'R'), reman);
        }

        [Fact]
        public void IsParentPart()
        {
            Assert.False(new DiagramPosition(0).IsParentPosition());
            Assert.False(new DiagramPosition(1, 'a').IsParentPosition());
            Assert.True(new DiagramPosition(1).IsParentPosition());
        }
    }
}
