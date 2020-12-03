using System;
using System.Collections.Generic;
using System.Text;
using ChoiceLibrary;
using InterFace;
using Xunit;

namespace ChoiceFacts
{
    public class AnyTests
    {
        [Fact]
        public void InvalidatesNullStrings()
        {
            var a = new Any("aA");
            Assert.False(a.Match(null).Success());
            Assert.True(a.Match(null).RemainingText() == null);
        }

        [Fact]
        public void InvalidatesEmptyStrings()
        {
            var a = new Any("aA");
            Assert.False(a.Match("").Success());
            Assert.True(a.Match("").RemainingText() == "");
        }

        [Fact]
        public void ValidatesFirstCharWhenIsTheSameAsFirstInAny()
        {
            var a = new Any("aA");
            Assert.True(a.Match("ab").Success());
            Assert.True(a.Match("").RemainingText() == "b");
        }

        [Fact]
        public void ValidatesFirstCharWhenIsTheSameAsSecondInAny()
        {
            var a = new Any("aA");
            Assert.True(a.Match("Ab").Success());
            Assert.True(a.Match("").RemainingText() == "b");
        }

        [Fact]
        public void InvalidatesFirstCharWhenItIsnotFoundInAny()
        {
            var a = new Any("aA");
            Assert.False(a.Match("bA").Success());
            Assert.True(a.Match("").RemainingText() == "bA");
        }
    }
}
