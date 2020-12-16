using System;
using System.Collections.Generic;
using System.Text;
using InterFace;
using Xunit;
using Range = InterFace.Range;
using ChoiceLibrary;

namespace ChoiceFacts
{
    public class ListFacts
    {
        [Fact]
        public void NullStringReturnsNullString()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            Assert.True(a.Match(null).Success());
            Assert.True(a.Match(null).RemainingText() == null);
        }

        [Fact]
        public void EmptyStringReurnsEmptyStringString()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            Assert.True(a.Match(string.Empty).Success());
            Assert.True(a.Match(string.Empty).RemainingText() == string.Empty);
        }

        [Fact]
        public void CorrectListReturnsEmptyString()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            Assert.True(a.Match("1,2,3").Success());
            Assert.True(a.Match("1,2,3").RemainingText() == string.Empty);
        }

        [Fact]
        public void CorrectElementsOfIncompleteListReturnsRestOfTheList()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            Assert.True(a.Match("1a").Success());
            Assert.True(a.Match("1a").RemainingText() == "a");
        }

        [Fact]
        public void IncorrectListReturnsFullList()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            Assert.True(a.Match("abc").Success());
            Assert.True(a.Match("abc").RemainingText() == "abc");
        }

        [Fact]
        public void ListEndingInSeparatorReturnsStringStartingWithSeparator()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            Assert.True(a.Match("1,2,3,").Success());
            Assert.True(a.Match("1,2,3,").RemainingText() == ",");
        }

        [Theory]
        [InlineData("1; 22  ;\n 333 \t; 22", true, "")]
        [InlineData("1 \n;", true, " \n;")]
        [InlineData("abc", true, "abc")]
        public void WorksWithMultipleData(string input, bool expected, string expectedRemainder)
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), whitespace);
            var list = new List(digits, separator);
            IMatch actualResult = list.Match(input);
            Assert.True(actualResult.Success() == expected);
            Assert.True(actualResult.RemainingText() == expectedRemainder);
        }
    }
}
