using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diverse
{
    class TopRecurringWords
    {
        public IEnumerable<string> GetMostRecurring(string text)
        {
            char[] notWords = new char[] { ' ', '\n', '\t', '\r', '?', '!', '"','\'', '.', ',', '-', '1', '2','3', '4', '5', '7', '8', '9','0', ')', '(', '\\'};
            string[] wordsOnly = text.Split(notWords);
            return wordsOnly
                .GroupBy(word => word)
                .OrderByDescending(criteria => criteria.Count())
                .Select((x, i) => $"{i + 1} : {x.Key}");
        }
    }
}
