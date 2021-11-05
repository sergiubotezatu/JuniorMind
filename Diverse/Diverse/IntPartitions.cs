using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diverse
{ 
    public class IntPartitions
    {
        public IEnumerable<IEnumerable<int>> GetSumPartitions(int[] input, int sum)
        {
            return Enumerable.Range(1, input.Length - 1)
           .SelectMany(i => Enumerable.Range(0, input.Length - i)
           .Select(j => new List<int> { input[i - 1] }
           .Concat(input.Skip(i).Take(j))))
           .Where(subsequence => subsequence.Sum() <= sum);
        }
    }
}
