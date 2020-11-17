using System;
using Xunit;
using ChoiceClass;
using Range = ChoiceClass.Range;

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
    }
}
