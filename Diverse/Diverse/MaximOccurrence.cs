using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diverse
{
    class MaximOccurrence
    {
        public IEnumerable<char> GetMaxOccurence(string input)
        {
            return input.GroupBy(x => x).OrderByDescending(x => x.Count()).GroupBy(x => x.Count()).First().Take(1).Select(x => x.Key);
        }
    }
}
