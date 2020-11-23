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
            Assert.False(digit.Match("null").Success());
        }

        [Fact]
        public void InvalidatesEmptyString()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9')
                                  );
            Assert.False(digit.Match("").Success());
        }

        [Fact]
        public void InValidatesCorrectlyOneCharString()
        {
            var digit = new Choice(
            new Character('0'),
            new Range('1', '9')
                                 );
            Assert.False(digit.Match("a").Success());
        }


        [Fact]
        public void ValidatesCorrectString()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9')
                                  );
            Assert.True(digit.Match("02").Success());
        }

        [Fact]
        public void ValidatesCorrectlyMultipleCharsUsingCharacter()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9')
                                  );
            Assert.True(digit.Match("012").Success());
        }

        [Fact]
        public void ValidatesCorrectlyMultipleCharsUsingRange()
        {
            var digit = new Choice(
             new Character('1'),
             new Range('0', '9')
                  );
            Assert.True(digit.Match("102").Success());
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
        }

        [Theory]
        [InlineData("09aB", true)]
        [InlineData("02bF", true)]
        [InlineData("05eE", true)]
        [InlineData("04X", false)]
        [InlineData("01G", false)]
        public void WorksBothWithLiteralsAndDigits(string input, bool expected)
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
            Assert.True(result == expected);
        }
    }
}

