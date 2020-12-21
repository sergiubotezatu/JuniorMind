using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using InterFace;
using ChoiceLibrary;

namespace ChoiceFacts
{
    public class NumberFacts
    {
        [Fact]
        public void CanBeZero()
        {
            var number = new Number();
            var test = number.Match("0");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == string.Empty);
        }

        [Fact]
        public void DoesNotContainLetters()
        {
            var number = new Number();
            var test = number.Match("a");
            Assert.False(test.Success());
            Assert.True(test.RemainingText() == "a");
        }

        [Fact]
        public void CanHaveASingleDigit()
        {
            var number = new Number();
            var test = number.Match("5");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == string.Empty);
        }

        [Fact]
        public void CanHaveMultipleDigits()
        {
            var number = new Number();
            var test = number.Match("56");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == string.Empty);
        }

        [Fact]
        public void IsNotNull()
        {
            var number = new Number();
            var test = number.Match(null);
            Assert.False(test.Success());
            Assert.True(test.RemainingText() == null);
        }

        [Fact]
        public void IsNotAnEmptyString()
        {
            var number = new Number();
            var test = number.Match("");
            Assert.False(test.Success());
            Assert.True(test.RemainingText() == string.Empty);
        }

        [Fact]
        public void DoesNotStartWithZero()
        {
            var number = new Number();
            var test = number.Match("08");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == "8");
        }

        [Fact]
        public void CanBeNegative()
        {
            var number = new Number();
            var test = number.Match("-11");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == string.Empty);
         }

        [Fact]
        public void CanBeMinusZero()
        {
            var number = new Number();
            var test = number.Match("-0");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == string.Empty);
        }

        [Fact]
        public void CanBeFractional()
        {
            var number = new Number();
            var test = number.Match("12.34");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == string.Empty);
        }

        [Fact]
        public void TheFractionCanHaveLeadingZeros()
        {
            var number = new Number();
            var test = number.Match("10.0000605");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == string.Empty);
        }

        [Fact]
        public void DoesNotEndWithADot()
        {
            var number = new Number();
            var test = number.Match("12.");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == ".");
        }

        [Fact]
        public void DoesNotHaveTwoFractionParts()
        {
            var number = new Number();
            var test = number.Match("12.34.56");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == ".56");
         }

        [Fact]
        public void TheDecimalPartDoesNotAllowLetters()
        {
            var number = new Number();
            var test = number.Match("12.34f");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == "f");
        }

        [Fact]
        public void CanHaveAnExponent()
        {
            var number = new Number();
            var test = number.Match("25e48");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == string.Empty);
        }

        [Fact]
        public void TheExponentCanStartWithCapitalE()
        {
            var number = new Number();
            var test = number.Match("12E3");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == string.Empty);
        }

        [Fact]
        public void TheExponentCanHavePositive()
        {
            var number = new Number();
            var test = number.Match("12E+3");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == string.Empty);
        }

        [Fact]
        public void TheExponentCanBeNegative()
        {
            var number = new Number();
            var test = number.Match("12E-11");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == string.Empty);
        }

        [Fact]
        public void CanHaveFractionAndExponent()
        {
            var number = new Number();
            var test = number.Match("12.4E-11");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == string.Empty);
        }

        [Fact]
        public void TheExponentDoesNotAllowLetters()
        {
            var number = new Number();
            var test = number.Match("22e3x3");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == "x3");
        }

        [Fact]
        public void DoesNotHaveTwoExponents()
        {
            var number = new Number();
            var test = number.Match("22e3e3");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == "e3");
        }

        [Fact]
        public void TheExponentIsAfterTheFraction()
        {
            var number = new Number();
            var test = number.Match("22e3.5");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == ".5");
        }

        [Fact]
        public void TheExponentisNotPreceededByDot()
        {
            var number = new Number();
            var test = number.Match("22.e5");
            Assert.True(test.Success());
            Assert.True(test.RemainingText() == ".e5");
        }

        [Theory]
        [InlineData("22e", true, "e")]
        [InlineData("22e+", true, "e+")]
        [InlineData("22e-", true, "e-")]
        public void TheExponentIsAlwaysComplete(string input, bool expected, string expectedRemainder)
        {
            var number = new Number();
            var test = number.Match(input);
            Assert.True(test.Success() == expected);
            Assert.True(test.RemainingText() == expectedRemainder);
        }
    }
}
