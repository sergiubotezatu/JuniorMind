using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ChoiceLibrary;
using InterFace;
using DateTime = ChoiceLibrary.DateTime;

namespace ChoiceFacts
{
    public class DateTimeFacts
    {
        [Fact]
        public void InvalidatesFullDayName()
        {
            var dateTime = new DateTime();
            var test = dateTime.Match("Sunday, 20 Jul 1969 20:17:40 +0000");
            Assert.False(test.Success());
            Assert.True(test.RemainingText() == "Sunday, 20 Jul 1969 20:17:40 +0000");
        }

        [Fact]
        public void InvalidatesFullMonthName()
        {
            var dateTime = new DateTime();
            var test = dateTime.Match("Sun, 20 Jully 1969 20:17:40 +0000");
            Assert.False(test.Success());
            Assert.True(test.RemainingText() == "Sun, 20 Jully 1969 20:17:40 +0000");
        }

        [Fact]
        public void InvalidatesNonDigitYear()
        {
            var dateTime = new DateTime();
            var test = dateTime.Match("Sun, 20 Jul 19s9 20:17:40 +0000");
            Assert.False(test.Success());
            Assert.True(test.RemainingText() == "Sun, 20 Jul 19s9 20:17:40 +0000");
        }

        [Fact]
        public void InvalidatesMoreThanFourDigitsYear()
        {
            var dateTime = new DateTime();
            var test = dateTime.Match("Sun, 20 Jul 19690 20:17:40 +0000");
            Assert.False(test.Success());
            Assert.True(test.RemainingText() == "Sun, 20 Jul 19690 20:17:40 +0000");
        }

        [Fact]
        public void InvalidatesNonDigitHour()
        {
            var dateTime = new DateTime();
            var test = dateTime.Match("Sun, 20 Jul 1969 f0:17:40 +0000");
            Assert.False(test.Success());
            Assert.True(test.RemainingText() == "Sun, 20 Jul 1969 f0:17:40 +0000");
        }

        [Fact]
        public void InvalidatesMoreThanTwoDigitsHour()
        {
            var dateTime = new DateTime();
            var test = dateTime.Match("Sun, 20 Jul 1969 120:17:40 +0000");
            Assert.False(test.Success());
            Assert.True(test.RemainingText() == "Sun, 20 Jul 1969 120:17:40 +0000");
        }

        [Fact]
        public void InvalidatesMissingTimeColon()
        {
            var dateTime = new DateTime();
            var test = dateTime.Match("Sun, 20 Jul 1969 2017:40 +0000");
            Assert.False(test.Success());
            Assert.True(test.RemainingText() == "Sun, 20 Jul 1969 2017:40 +0000");
        }

        [Fact]
        public void InvalidatesNonDigitMinutes()
        {
            var dateTime = new DateTime();
            var test = dateTime.Match("Sun, 20 Jul 1969 20:u7:40 +0000");
            Assert.False(test.Success());
            Assert.True(test.RemainingText() == "Sun, 20 Jul 1969 20:u7:40 +0000");
        }

        [Fact]
        public void InvalidatesMoreThanTwoDigitMinutes()
        {
            var dateTime = new DateTime();
            var test = dateTime.Match("Sun, 20 Jul 1969 20:170:40 +0000");
            Assert.False(test.Success());
            Assert.True(test.RemainingText() == "Sun, 20 Jul 1969 20:170:40 +0000");
        }

        [Fact]
        public void InvalidatesNonDigitSeconds()
        {
            var dateTime = new DateTime();
            var test = dateTime.Match("Sun, 20 Jul 1969 20:17:f0 +0000");
            Assert.False(test.Success());
            Assert.True(test.RemainingText() == "Sun, 20 Jul 1969 20:17:f0 +0000");
        }

        [Fact]
        public void ValidatesMissingSeconds()
        {
            var dateTime = new DateTime();
            var test = dateTime.Match("Sun, 20 Jul 1969 20:17 +0000");
            Assert.True(test.Success());
            Assert.True(string.IsNullOrEmpty(test.RemainingText()));
        }

        [Fact]
        public void InvalidatessMissingUTCSign()
        {
            var dateTime = new DateTime();
            var test = dateTime.Match("Sun, 20 Jul 1969 20:17 0000");
            Assert.False(test.Success());
            Assert.True(test.RemainingText() == "Sun, 20 Jul 1969 20:17 0000");
        }
    }
}
