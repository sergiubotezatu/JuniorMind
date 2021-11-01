using System;
using System.Collections.Generic;
using System.Linq;

namespace Diverse
{
    public class PalindromeValidator
    {
        int selection = 0;
        int skips = 0;
        public IEnumerable<string> GetPalindromes(string input)
        {
            return Enumerable.Range(0, 10).Select(selection =>
            input.Select((x, i) => x + input
            .Skip(i + 1)
            .Take(selection).ToString())
            .Where(partition => partition.Equals(partition.Reverse())))
            .Cast<string>();             
        }        
    }
}
