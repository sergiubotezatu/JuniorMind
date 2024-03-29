﻿using Xunit;
using InterFace;

namespace IPatternFacts
{
    public class CharacterFacts
    {
        [Fact]
        public void ReturnsFalseIfStringIsnull()
        {
            string test = null;
            Character newChar = new Character('0');
            Assert.False(newChar.Match(test).Success());
        }

        [Fact]
        public void ReturnsFalseIfStringIsEmpty()
        {
            string test = "";
            Character newChar = new Character('0');
            Assert.False(newChar.Match(test).Success());
        }

        [Fact]
        public void InvalidatesUnMatchingChars()
        {
            string test = "1";
            Character newChar = new Character('0');
            Assert.False(newChar.Match(test).Success());
        }

        [Fact]
        public void ValidatesMatchingChars()
        {
            string test = "1";
            Character newChar = new Character('1');
            Assert.True(newChar.Match(test).Success());
        }

        [Fact]
        public void ValidateCorrectFirstCharInLongerString()
        {
            string test = "123456";
            Character newChar = new Character('1');
            Assert.True(newChar.Match(test).Success());
        }
    }
}
