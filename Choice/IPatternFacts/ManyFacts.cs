using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ChoiceLibrary;
using InterFace;
using Range = InterFace.Range;

namespace ChoiceFacts
{
    public class ManyFacts
    {
        [Fact]
        public void ReturnsNullWhenInputIsNull()
        {
            var a = new Many(new Character('a'));

            Assert.True(a.Match(null).Success());
            Assert.True(a.Match(null).RemainingText() == null);
        }

        [Fact]
        public void ReturnsEmptyStringWhenInputIsEmpty()
        {
            var a = new Many(new Character('a'));

            Assert.True(a.Match("").Success());
            Assert.True(a.Match("").RemainingText() == "");
        }

        [Fact]
        public void ReturnsInputIfPatternIsNotLocatedAtTheStart()
        {
            var a = new Many(new Character('a'));

            Assert.True(a.Match("ba").Success());
            Assert.True(a.Match("ba").RemainingText() == "ba");
        }

        [Fact]
        public void ReturnsEmptyStringIfInputAndPatternAreTheSame()
        {
            var a = new Many(new Character('a'));

            Assert.True(a.Match("a").Success());
            Assert.True(a.Match("a").RemainingText() == "");
        }

        [Fact]
        public void ReturnsInputExceptIdentifiedPattern()
        {
            var a = new Many(new Character('a'));

            Assert.True(a.Match("abc").Success());
            Assert.True(a.Match("abc").RemainingText() == "bc");
        }

        [Fact]
        public void ReturnsInputExceptIdentifiedRepeatedPattern()
        {
            var a = new Many(new Character('a'));

            Assert.True(a.Match("aaaaabc").Success());
            Assert.True(a.Match("aaaaabc").RemainingText() == "bc");
        }

        [Theory]
        [InlineData(null, true, null)]
        [InlineData("", true, "")]
        [InlineData("ab", true, "ab")]
        [InlineData("a1b", true, "a1b")]
        [InlineData("1234", true, "")]
        [InlineData("1546ab1", true, "ab1")]
        public void WorksWithRangePatterns(string text, bool True, string expected)
        {
            var digits = new Many(new Range('0', '9'));
            bool actual = digits.Match(text).Success();
            string result = digits.Match(text).RemainingText();
            Assert.True(actual == True);
            Assert.True(result == expected);
        }
    }
}
