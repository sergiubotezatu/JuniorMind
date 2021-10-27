using System;
using Xunit;
using Diverse;
using System.Collections.Generic;

namespace PalindromeTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string test = "abac";
            PalindromeValidator testare = new PalindromeValidator();
            IEnumerable<string> result = testare.GetPalindrome(test);
            Assert.True(result.Equals(new List<string>() { "aba" }));
        }
    }
}
