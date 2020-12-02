using System;
using Xunit;
using InterFace;
using Range = InterFace.Range;

namespace IPatternFacts
{
    public class ChoiceTests
    {
        [Fact]
        public void InvalidatesNullValues()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9'));
            Assert.False(digit.Match(null).Success());
            Assert.True(digit.Match(null).RemainingText() == null);
        }

        [Fact]
        public void InvalidatesEmptyString()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9'));
            Assert.False(digit.Match(string.Empty).Success());
            Assert.True(digit.Match(string.Empty).RemainingText() == string.Empty);
        }

        [Fact]
        public void InValidatesCorrectlyOneCharString()
        {
            var digit = new Choice(
            new Character('0'),
            new Range('1', '9'));
            Assert.False(digit.Match("a").Success());
            Assert.True(digit.Match("a").RemainingText() == "a");
        }

        [Fact]
        public void ValidatesCorrectString()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9'));
            Assert.True(digit.Match("02").Success());
            Assert.True(digit.Match("02").RemainingText() == "2");
        }

        [Fact]
        public void ValidatesCorrectlyMultipleCharsUsingCharacter()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9'));
            Assert.True(digit.Match("052").Success());
            Assert.True(digit.Match("052").RemainingText() == "52");

        }

        [Fact]
        public void ValidatesCorrectlyCharsUsingRange()
        {
            var digit = new Choice(
             new Character('5'),
             new Range('0', '9'));
            Assert.True(digit.Match("102").Success());
            Assert.True(digit.Match("102").RemainingText() == "02");
        }

        [Fact]
        public void ValidatesCorretHexCharacter()
        {
            var hex = new Choice(
                new Range('0', '9'),
                new Range('a', 'f'),
                new Range('A', 'F'));
            Assert.True(hex.Match("5bA").Success());
            Assert.True(hex.Match("5bA").RemainingText() == "bA");
        }

        [Fact]
        public void InvalidatesIncorrectHexChar()
        {
            var hex = new Choice(
                new Character('0'),
                new Range('1', '9'),
                new Range('a', 'f'),
                new Range('A', 'F'));
            Assert.False(hex.Match("G301").Success());
            Assert.True(hex.Match("G301").RemainingText() == "G301");
        }

        [Theory]
        [InlineData("09aB", true, "9aB")]
        [InlineData("02bFH", true, "2bFH")]
        [InlineData("05eE4", true, "5eE4")]
        [InlineData("H4X", false, "H4X")]
        [InlineData("/1G", false, "/1G")]
        public void WorksBothWithLiteralsAndDigits(string input, bool expected, string expectedText)
        {
            var digit = new Character('0');
            var hex = new Choice(
                digit, new Choice(
             new Range('1', '9'),
            new Range('a', 'f'),
             new Range('A', 'F')
                )
                );
            bool result = hex.Match(input).Success();
            string remainingTest = hex.Match(input).RemainingText();
            Assert.True(result == expected);
            Assert.True(remainingTest == expectedText);
        }
    }
}

