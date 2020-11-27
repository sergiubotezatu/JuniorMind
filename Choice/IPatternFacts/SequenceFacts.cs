using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ChoiceLibrary;
using InterFace;
using Range = InterFace.Range;

namespace ChoiceFacts
{
    public class SequenceFacts
    {
        [Fact]
        public void InvalidatesNullInputs()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b')
                );

            Assert.False(ab.Match(null).Success());
            Assert.True(ab.Match(null).RemainingText() == null);
        }

        [Fact]
        public void InvalidatesEmptynputs()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b')
                );

            Assert.False(ab.Match("").Success());
            Assert.True(ab.Match("").RemainingText() == "");
        }

        [Fact]
        public void InvalidatesIncorrectSequencePattern()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b')
                );

            Assert.False(ab.Match("def").Success());
            Assert.True(ab.Match("def").RemainingText() == "def");
        }

        [Fact]
        public void ValidatesCorrectlySequenceOfTwoChars()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b')
                );

            Assert.True(ab.Match("ab").Success());
            Assert.True(ab.Match("ab").RemainingText() == "");
        }

        [Fact]
        public void ValidatesCorrectlyInputBiggerThanSequence()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b')
                );

            Assert.True(ab.Match("abcd").Success());
            Assert.True(ab.Match("abcd").RemainingText() == "cd");
        }

        [Fact]
        public void ValidatesCorrectlyJsonHexNumber()
        {
            var hex =  new Choice(
             new Range('0','9'),
            new Range('a', 'f'),
             new Range('A', 'F')
                 );

            var hexSequence = new Sequence(
                new Character('u'),
                new Sequence(
                hex,
                hex
                )
                );

            Assert.True(hexSequence.Match("uB0ab").Success());
            Assert.True(hexSequence.Match("uB0ab").RemainingText() == "ab");
        }

        [Theory]
        [InlineData("uabcdef", true, "ef")]
        [InlineData("uB005 ab", true, " ab")]
        [InlineData("u12g4", false, "g4")]
        [InlineData("abc", false, "abc")]
        [InlineData("u51Af", true, "")]
        public void WorksWithMultipleData(string input, bool expected, string expectedRemainder)
        {
            var hex = new Choice(
             new Range('0', '9'),
            new Range('a', 'f'),
             new Range('A', 'F')
                 );

            var hexSequence = new Sequence(
                new Character('u'),
                new Sequence(
                hex,
                hex,
                hex,
                hex
                )
                );

            bool actualResult = hexSequence.Match(input).Success();
            string actualRemainder = hexSequence.Match(input).RemainingText();
            Assert.True(actualResult == expected);
            Assert.True(actualRemainder == expectedRemainder);
        }
    }
}
