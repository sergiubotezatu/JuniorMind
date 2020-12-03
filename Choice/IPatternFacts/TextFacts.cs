using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ChoiceLibrary;

namespace ChoiceFacts
{
    public class TextFacts
    {
        [Fact]
        public void InvalidatesNullStrings()
        {
            var a = new Text("a");
            Assert.False(a.Match(null).Success());
            Assert.True(a.Match(null).RemainingText() == null);
        }

        [Fact]
        public void InvalidatesEmptyStrings()
        {
            var a = new Text("a");
            Assert.False(a.Match("").Success());
            Assert.True(a.Match("").RemainingText() == "");
        }

        [Fact]
        public void ValidatesStringIfItIsTheSameAsOneCharString()
        {
            var a = new Text("a");
            Assert.True(a.Match("a").Success());
            Assert.True(a.Match("a").RemainingText() == "");
        }

        [Fact]
        public void ValidatesStringIfItIsSameAsLargerPrefix()
        {
            var pref = new Text("pref");
            Assert.True(pref.Match("pref").Success());
            Assert.True(pref.Match("pref").RemainingText() == "");
        }

        [Fact]
        public void ValidatesStringIfItContainsCharsAfterPrefix()
        {
            var pref = new Text("pref");
            Assert.True(pref.Match("prefix").Success());
            Assert.True(pref.Match("prefix").RemainingText() == "ix");
        }

        [Fact]
        public void InvalidatesStringIfIsSmallerThanPrefix()
        {
            var pref = new Text("pref");
            Assert.False(pref.Match("pr").Success());
            Assert.True(pref.Match("pr").RemainingText() == "pr");
        }

        [Fact]
        public void InvalidatesStringIfFirstCharsAreDifferentThanPrefix()
        {
            var pref = new Text("pref");
            Assert.False(pref.Match("false").Success());
            Assert.True(pref.Match("false").RemainingText() == "false");
        }

        [Fact]
        public void InvalidatesStringIfOnlyOneCharIsDifferentThanPrefix()
        {
            var pref = new Text("pref");
            Assert.False(pref.Match("prif").Success());
            Assert.True(pref.Match("prif").RemainingText() == "prif");
        }
    }
}
