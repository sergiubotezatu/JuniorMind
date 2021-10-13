using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace Diverse
{
    public class FirstUnique
    {
        private char GetOneOccurence(string input)
        {
            return input.GroupBy(x => x).Where(x => x.Count() == 1).Select(x => x.Key).FirstOrDefault();
        }
    }
}
