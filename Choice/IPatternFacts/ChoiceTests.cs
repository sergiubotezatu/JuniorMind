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
             new Range('1', '9')
                                  );
            Assert.False(digit.Match(null).Success());
            Assert.True(digit.Match(null).RemainingText() == null);
        }

        [Fact]
        public void InvalidatesEmptyString()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9')
                                  );
            Assert.False(digit.Match("").Success());
            Assert.True(digit.Match("").RemainingText() == "");
        }

        [Fact]
        public void InValidatesCorrectlyOneCharString()
        {
            var digit = new Choice(
            new Character('0'),
            new Range('1', '9')
                                 );
            Assert.False(digit.Match("a").Success());
            Assert.True(digit.Match("a").RemainingText() == "a");
        }


        [Fact]
        public void ValidatesCorrectString()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9')
                                  );
            Assert.True(digit.Match("02").Success());
            Assert.True(digit.Match("02").RemainingText() == "");
        }

        [Fact]
        public void ValidatesCorrectlyMultipleCharsUsingCharacter()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9')
                                  );
            Assert.True(digit.Match("052").Success());
            Assert.True(digit.Match("052").RemainingText() == "2");

        }

        [Fact]
        public void ValidatesCorrectlyMultipleCharsUsingRange()
        {
            var digit = new Choice(
             new Character('1'),
             new Range('0', '9')
                  );
            Assert.True(digit.Match("102").Success());
            Assert.True(digit.Match("102").RemainingText() == "2");
        }

        [Fact]
        public void ValidatesCorretHexNumber()
        {
            var hex = new Choice(
                new Range('0', '9'),
            new Range('a', 'f'),
             new Range('A', 'F')
                );
            
            Assert.True(hex.Match("5bA").Success());
            Assert.True(hex.Match("5bA").RemainingText() == "");
        }

        [Fact]
        public void InvalidatesIncorrectHexNumber()
        {
            var hex = new Choice(
                new Character('0'),
             new Range('1', '9'),
            new Range('a', 'f'),
             new Range('A', 'F')
                );
            Assert.False(hex.Match("03G1").Success());
            Assert.True(hex.Match("03G1").RemainingText() == "G1");
        }

        [Theory]
        [InlineData("09aB", true, "")]
        [InlineData("02bFH", true, "H")]
        [InlineData("05eE4", true, "4")]
        [InlineData("04X", false, "X")]
        [InlineData("01G", false, "G")]
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

