using System;
using System.Collections.Generic;
using System.Text;
using InterFace;
using Xunit;
using ChoiceLibrary;

namespace ChoiceFacts
{
    public class OptionalFacts
    {
        [Fact]
        public void ReturnsNullWhenInputIsNull()
        {
            var a = new Optional(new Character('a'));

            Assert.True(a.Match(null).Success());
            Assert.True(a.Match(null).RemainingText() == null);
        }

        [Fact]
        public void ReturnsEmptyStringWhenInputIsEmpty()
        {
            var a = new Optional(new Character('a'));

            Assert.True(a.Match(string.Empty).Success());
            Assert.True(a.Match(string.Empty).RemainingText() == string.Empty);
        }

        [Fact]
        public void ReturnsInputIfPatternIsNotLocatedAtTheStart()
        {
            var a = new Optional(new Character('a'));

            Assert.True(a.Match("bc").Success());
            Assert.True(a.Match("bc").RemainingText() == "bc");
        }

        [Fact]
        public void ReturnsEmptyStringIfInputAndPatternAreTheSame()
        {
            var a = new Optional(new Character('a'));

            Assert.True(a.Match("a").Success());
            Assert.True(a.Match("a").RemainingText() == string.Empty);
        }

        [Fact]
        public void ReturnsInputExceptIdentifiedPattern()
        {
            var a = new Optional(new Character('a'));

            Assert.True(a.Match("abc").Success());
            Assert.True(a.Match("abc").RemainingText() == "bc");
        }

        [Fact]
        public void ReturnsInputExceptFirstIdentifiedPattern()
        {
            var a = new Optional(new Character('a'));

            Assert.True(a.Match("aaabc").Success());
            Assert.True(a.Match("aaabc").RemainingText() == "aabc");
        }
    }
}
