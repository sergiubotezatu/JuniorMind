using System;
using Xunit;
using RangeClass;
using Range = RangeClass.Range;

namespace RangeFacts
{
    public class RangeFacts
    {
        [Fact]
        public void ReturnsFalseIfStringIsnull()
        {
            string test = null;
            Range upperCase = new Range('A', 'B');
            Assert.False(upperCase.Match(test));
        }

        [Fact]
        public void ReturnsFalseIfStringIsEmpty()
        {
            string test = "";
            Range upperCase = new Range('A', 'B');
            Assert.False(upperCase.Match(test));
        }
    }
}
