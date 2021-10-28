using System;
using System.Collections.Generic;
using System.Linq;

namespace Diverse
{
    public class PalindromeValidator
    {
        public IEnumerable<string> GetPalindrome(string input)
        {
            return Enumerable.Repeat(input, input.Length - 1)
            .SelectMany((x, i) => x.Select((y, j) => y + input.Skip(j++)
            .Take(i)
            .ToString()).
            Where(partition => partition.Equals(partition.Reverse())));            
        }
    }
}
