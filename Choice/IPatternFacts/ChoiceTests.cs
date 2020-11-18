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
            Assert.False(digit.Match("null"));        
        }

        [Fact]
        public void InvalidatesEmptyString()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9')
                                  );
            Assert.False(digit.Match(""));
        }

        [Fact]
        public void InValidatesCorrectlyOneCharString()
        {
             var digit = new Choice(
             new Character('0'),
             new Range('1', '9')
                                  );
            Assert.False(digit.Match("a"));
        }


        [Fact]
        public void ValidatesCorrectlyOneCharString()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9')
                                  );
            Assert.True(digit.Match("2"));
        }        

        [Fact]
        public void ValidatesCorrectlyMultipleCharsUsingCharacter()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9')
                                  );
            Assert.True(digit.Match("012"));
        }

        [Fact]
        public void ValidatesCorrectlyMultipleCharsUsingRange()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9')
                  );
            Assert.True(digit.Match("102"));
        }

        [Fact]
        public void ValidatesCorretlyOneCharHexNumber()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9')
             );
            var hex = new Choice(
                digit,
             new Choice(
            new Range('a', 'f'),
             new Range('A', 'F')
                )
            );
            Assert.True(hex.Match("A"));
        }

        [Fact]
        public void InvalidatesIncorrectHexNumber()
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9')
             );
            var hex = new Choice(
                digit,
             new Choice(
            new Range('a', 'f'),
             new Range('A', 'F')
                )
            );
            Assert.False(hex.Match("G1"));
        }

        [Theory]
        [InlineData("92", true)]
        [InlineData("a9", true)]
        [InlineData("F8", true)]
        [InlineData("g8", false)]
        [InlineData("G8", false)]
        public void WorksBothWithLiteralsAndDigits(string input, bool expected)
        {
            var digit = new Choice(
             new Character('0'),
             new Range('1', '9')
             );
            var hex = new Choice(
                digit,
             new Choice(
            new Range('a', 'f'),
             new Range('A', 'F')
                )
            );

            bool result = hex.Match(input);
            Assert.True(result == expected);
        }
    }
}
