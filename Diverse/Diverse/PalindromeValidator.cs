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
            int selection = 1;
            while(selection <= input.Length)
            {
                while(skipped + selection <= input.Length)
                {
                    string result = input.Skip(skipped).Take(selection).ToString();
                    if (result.Equals(result.Reverse()))
                    {
                        yield return result;
                    }

                    skipped++;
                }

                skipped = 0;
                selection++;
            }
        }
    }
}
