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
    }
}
