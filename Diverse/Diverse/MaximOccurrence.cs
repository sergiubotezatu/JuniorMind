using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diverse
{
    class MaximOccurrence
    {
        public char GetMaxOccurence(string input)
        {
            var groupedChars = input.GroupBy(x => x);
            return groupedChars.Aggregate(groupedChars.First(), (greatest, current) =>
             current.Count() > greatest.Count() ? greatest = current : greatest).Key;
        }
    }
}
