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
            IEnumerable<int> values = input.ToList();
            return values.SelectMany((x, index) => 
            Enumerable.Range(index + 1, values.Count() - 1)
            .SelectMany(i => Enumerable.Range(0, values.Count() - i)
            .Select(j => new List<int> { x }
            .Concat(values.Skip(i).Take(j)))))
            .Where(subsequence => subsequence.Sum() <= sum);
        }
    }
}
