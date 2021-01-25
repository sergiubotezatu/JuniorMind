using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ChoiceLibrary;
using InterFace;
using String = ChoiceLibrary.String;

namespace ChoiceFacts
{
    public class StringFacts
    {
        [Fact]
        public void IsWrappedInDoubleQuotes()
        {
            var test = new String();
            var validator = test.Match(Quoted("abc"));
            Assert.True(validator.Success());
            Assert.True(validator.RemainingText() == "");
        }

        [Fact]
        public void AlwaysStartsWithQuotes()
        {
            var test = new String();
            var validator = test.Match("abc\"");
            Assert.False(validator.Success());
            Assert.True(validator.RemainingText() == "abc\"");
        }

        [Fact]
        public void AlwaysEndsWithQuotes()
        {
            var test = new String();
            var validator = test.Match("\"abc");
            Assert.False(validator.Success());
            Assert.True(validator.RemainingText() == "\"abc");
        }

        [Fact]
        public void IsNotNull()
        {
            var test = new String();
            var validator = test.Match(null);
            Assert.False(validator.Success());
            Assert.True(validator.RemainingText() == null);
        }

        [Fact]
        public void IsNotAnEmptyString()
        {
            var test = new String();
            var validator = test.Match(string.Empty);
            Assert.False(validator.Success());
            Assert.True(validator.RemainingText() == string.Empty);
        }

        [Fact]
        public void IsAnEmptyDoubleQuotedString()
        {
            var test = new String();
            var validator = test.Match(Quoted(string.Empty));
            Assert.True(validator.Success());
            Assert.True(validator.RemainingText() == string.Empty);
        }

        [Fact]
        public void HasStartAndEndQuotes()
        {
            var test = new String();
            var validator = test.Match(("\""));
            Assert.False(validator.Success());
            Assert.True(validator.RemainingText() == "\"");
        }


        [Fact]
        public void DoesNotContainControlCharacters()
        {
            var test = new String();
            var validator = test.Match(Quoted("a\nb\rc"));
            Assert.False(validator.Success());
            Assert.True(validator.RemainingText() == Quoted("a\nb\rc"));
        }

        [Fact]
        public void CanContainLargeUnicodeCharacters()
        {
            var test = new String();
            var validator = test.Match(Quoted("⛅⚾"));
            Assert.True(validator.Success());
            Assert.True(validator.RemainingText() == string.Empty);
        }

        [Fact]
        public void CanContainEscapedQuotationMark()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"\""a\"" b"));
            Assert.True(validator.Success());
            Assert.True(validator.RemainingText() == string.Empty);
        }

        [Fact]
        public void CanContainEscapedReverseSolidus()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"a \\ b"));
            Assert.True(validator.Success());
            Assert.True(validator.RemainingText() == string.Empty);
        }

        [Fact]
        public void CanContainEscapedSolidus()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"a \/ b"));
            Assert.True(validator.Success());
            Assert.True(validator.RemainingText() == string.Empty);
        }

        [Fact]
        public void CanContainEscapedBackspace()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"a \b b"));
            Assert.True(validator.Success());
            Assert.True(validator.RemainingText() == string.Empty);
        }

        [Fact]
        public void CanContainEscapedFormFeed()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"a \f b"));
            Assert.True(validator.Success());
            Assert.True(validator.RemainingText() == string.Empty);
        }

        [Fact]
        public void CanContainEscapedLineFeed()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"a \n b"));
            Assert.True(validator.Success());
            Assert.True(validator.RemainingText() == string.Empty);
        }

        [Fact]
        public void CanContainEscapedCarrigeReturn()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"a \r b"));
            Assert.True(validator.Success());
            Assert.True(validator.RemainingText() == string.Empty);
        }

        [Fact]
        public void CanContainEscapedHorizontalTab()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"a \t b"));
            Assert.True(validator.Success());
            Assert.True(validator.RemainingText() == string.Empty);
        }

        [Fact]
        public void CanContainEscapedUnicodeCharacters()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"a \u26Be b"));
            Assert.True(validator.Success());
            Assert.True(validator.RemainingText() == string.Empty);
        }

        [Fact]
        public void DoesNotContainUnrecognizedExcapceCharacters()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"a\x"));
            Assert.False(validator.Success());
            Assert.True(validator.RemainingText() == Quoted(@"a\x"));
        }

        [Fact]
        public void DoesNotEndWithReverseSolidus()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"a\"));
            Assert.False(validator.Success());
            Assert.True(validator.RemainingText() == Quoted(@"a\"));
        }

        [Fact]
        public void DoesNotEndWithAnUnfinishedHexNumber()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"a\u123"));
            Assert.False(validator.Success());
            Assert.True(validator.RemainingText() == Quoted(@"a\u123"));
        }

        [Fact]
        public void DoesNotContainWrongFormatHexNumber()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"a\u12k4"));
            Assert.False(validator.Success());
            Assert.True(validator.RemainingText() == Quoted(@"a\u12k4"));
        }

        [Fact]
        public void ReturnsFalseIfOneWrongHexNumberAndOneCorrect()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"a\u1234\u15l4"));
            Assert.False(validator.Success());
            Assert.True(validator.RemainingText() == Quoted(@"a\u1234\u15l4"));
        }

        [Fact]
        public void ReturnsFalseIfMultipleCorrectEscapedCharsAndOneControlChar()
        {
            var test = new String();
            var validator = test.Match(Quoted("a\\t\\b\r\\"));
            Assert.False(validator.Success());
            Assert.True(validator.RemainingText() == Quoted("a\\t\\b\r\\"));
        }

        [Fact]
        public void ReturnsFalseIfUnEscapedCharIsTheOnlyChar()
        {
            var test = new String();
            var validator = test.Match(Quoted("\\"));
            Assert.False(validator.Success());
            Assert.True(validator.RemainingText() == Quoted("\\"));
        }

        [Fact]
        public void RetunrnsFalseIfLargerStringContainsUnfinishedHex()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"abcdefg\u1234 ijk\b\\\""d156\u45B \nlmnopqrs-\\⚾tuvxyz"));
            Assert.False(validator.Success());
            Assert.True(validator.RemainingText() == Quoted(@"abcdefg\u1234 ijk\b\\\""d156\u45B \nlmnopqrs-\\⚾tuvxyz"));
         }

        [Fact]
        public void RetunrnsTrueIfLargerStringWithCorrectFormat()
        {
            var test = new String();
            var validator = test.Match(Quoted(@"abcdefg\u1234 ijk\b\\\""d156\u45AB \nlmnopqrs-\\⚾tuvxyz"));
            Assert.True(validator.Success());
            Assert.True(validator.RemainingText() == string.Empty);
        }

        public static string Quoted(string text)
            => $"\"{text}\"";
    }
}
