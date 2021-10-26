using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diverse
{
    public class PalindromeValidator
    {
        public IEnumerable<string> GetPalindrome(string input)
        {
            int skipped = 0;
            int selection = 0;
            IEnumerable<string> result = new List<string>();
            while(skipped + selection <= input.Length)
                {
                    result = input.Select(x => x + input.Skip(skipped++).Take(selection).ToString())
                    .Where(x => x.Equals(x.Reverse()));                    
                    skipped = 0;
                    selection++;
                }

            return result;
        }
    }
}
