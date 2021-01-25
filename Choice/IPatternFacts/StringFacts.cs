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
        public void DoesNotContainControlCharacters()
        {
            var test = new String();
            var validator = test.Match(Quoted("a\nb\rc"));
            Assert.True(validator.Success());
            Assert.True(validator.RemainingText() == "\nb\rc\"");
        }

        public static string Quoted(string text)
            => $"\"{text}\"";
    }
}

