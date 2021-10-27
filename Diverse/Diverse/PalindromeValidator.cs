using System;
using System.Collections.Generic;
using System.Linq;

namespace Diverse
{
    public class PalindromeValidator
    {
        public IEnumerable<string> GetPalindrome(string input)
        {
            IEnumerable<string> result = new List<string>();
            foreach (int selected in Enumerable.Range(0, input.Length - 1))
            {
                {
                    result = input.Select((x, i) => x + input.Skip(i + 1).Take(selected).ToString())
                    .Where(x => x.Equals(x.Reverse()));
                }
            }

            return result;
        }
    }
}
