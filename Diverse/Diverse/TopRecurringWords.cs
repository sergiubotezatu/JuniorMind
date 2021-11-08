using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diverse
{
    class TopRecurringWords
    {
        public IEnumerable<(int, string)> GetMostRecurring(string text)
        {
            return text.Split(".,!?:;'".ToCharArray())
                .GroupBy(word => word)
                .OrderByDescending(criteria => criteria.Count())
                .Select((x, i) => (i + 1, $": {x.Key}"));
        }
    }
}
