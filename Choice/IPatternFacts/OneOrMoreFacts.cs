using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Range = InterFace.Range;
using ChoiceLibrary;
using InterFace;

namespace ChoiceFacts
{
    public class OneOrMoreFacts
    {
       [Fact]
        public void InvalidatesNullInputReturnsNull()
        {
            var test = new OneOrMore(new Range('0', '9'));
            var testResult = test.Match(null);
            Assert.False(testResult.Success());
            Assert.True(testResult.RemainingText() == null);
        }

        [Fact]
        public void InvalidatesEmptyStringReturnsEmptyString()
        {
            var test = new OneOrMore(new Range('0', '9'));
            var testResult = test.Match(string.Empty);
            Assert.False(testResult.Success());
            Assert.True(testResult.RemainingText() == string.Empty);
        }

        [Fact]
        public void ValidatesStringWhenAllCharsCorrectReturnsEmptyString()
        {
            var test = new OneOrMore(new Range('0', '9'));
            var testResult = test.Match("123");
            Assert.True(testResult.Success());
            Assert.True(testResult.RemainingText() == string.Empty);
        }

        [Fact]
        public void ValdatesStringWhenOnlyFewCharsAreCorrectReturnsUnmatchdChars()
        {
            var test = new OneOrMore(new Range('0', '9'));
            var testResult = test.Match("15c");
            Assert.True(testResult.Success());
            Assert.True(testResult.RemainingText() == "c");
        }

        [Fact]
        public void InvaldatesStringWithNoMatchFoundReturnsEntireString()
        {
            var test = new OneOrMore(new Range('0', '9'));
            var testResult = test.Match("abc");
            Assert.False(testResult.Success());
            Assert.True(testResult.RemainingText() == "abc");
        }
    }
}
