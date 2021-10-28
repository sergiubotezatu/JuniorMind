using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diverse
{ 
    public class IntPartitions
    {
        public IEnumerable<IEnumerable<int>> GetSumPartitions(IEnumerable<int> values, int expectedSum)
        {
              var eligible = values.Where(x => x <= expectedSum);
        }
    }
}
