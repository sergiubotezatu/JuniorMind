using System;
using Xunit;
using RangeClass;
using Range = RangeClass.Range;

namespace RangeFacts
{
    public class RangeFacts
    {
        [Fact]
        public void ReturnsFalseIfStringIsnull()
        {
            string test = null;
            Range upperCase = new Range('A', 'B');
            Assert.False(upperCase.Match(test));
        }

        [Fact]
        public void ReturnsFalseIfStringIsEmpty()
        {
            string test = "";
            Range upperCase = new Range('A', 'B');
            Assert.False(upperCase.Match(test));
        }

        [Fact]
        public void ReturnsFalseIfOnlyOneCharOutOfRange()
        {
            string test = "D";
            Range upperCase = new Range('A', 'C');
            Assert.False(upperCase.Match(test));
        }

        [Fact]
        public void ReturnsTrueIfOnlyOneCharInRange()
        {
            string test = "C";
            Range upperCase = new Range('A', 'D');
            Assert.True(upperCase.Match(test));
        }

        [Fact]
        public void ReturnsTrueIfAllCharsInRange()
        {
            string test = "AGHD";
            Range upperCase = new Range('A', 'I');
            Assert.True(upperCase.Match(test));
        }

        [Fact]
        public void ReturnsFalseIfAllCharsInRangeAndOneOutOfRange()
        {
            string test = "AGHZD";
            Range upperCase = new Range('A', 'I');
            Assert.False(upperCase.Match(test));
        }

        [Theory]
        [InlineData('0', '9', "34896021", true)]
        [InlineData('1', '9', "34896021", false)]
        [InlineData('a', 'z', "bcefghijklmn", true)]
        [InlineData('a', 'z', "AZ", false)]
        [InlineData('0', 'z', "1234A56", true)]
        [InlineData('0', 'Z', "1234a56", false)]
        public void ValidatesCorrectltyInRangeChars(char start, char end, string test, bool expected)
        {
            var rangeTest = new Range(start, end);
            bool result = rangeTest.Match(test);
            Assert.True(result == expected);
        }
    }
}
