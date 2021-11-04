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
             return input.SelectMany((x, index) =>
             Enumerable.Range(index + 1, input.Length - 1)
            .SelectMany(i => Enumerable.Range(0, input.Length - i)
            .Select(j => new List<int> { x }
            .Concat(input.Skip(i).Take(j)))))
            .Distinct()
            .Where(subsequence => subsequence.Sum() <= sum);
        }
    }
}
