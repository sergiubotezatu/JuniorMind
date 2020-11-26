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
    }
}
