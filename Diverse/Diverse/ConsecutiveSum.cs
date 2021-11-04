using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diverse
{
    class ConsecutiveSum
    {
        public IEnumerable<IEnumerable<int>> GetAllConsecutiveSum(int n, int k)
        {
            IEnumerable<int> sequence = Enumerable.Range(1, n);
            int sequenceSum = sequence.Sum();
            if (sequenceSum < k || sequenceSum - k % 2 != 0)
            {
                return null;
            }
            
            if (sequenceSum == k)
            {
                return new List<IEnumerable<int>> { sequence };
            }

            int tosubstract = sequenceSum - k / 2;
            return GetPartitionsToNegate(sequence, tosubstract)
            .Select(x => sequence.Concat(x)
            .GroupBy(x => x)
            .Select(x => x.Count() > 1 ? GetNegativeOf(x.Key) : x.Key));     
        }

        private int GetNegativeOf(int positive)
        {
            return positive - positive * 2;
        }

        private IEnumerable<IEnumerable<int>> GetPartitionsToNegate(IEnumerable<int> toNegate, int toSubstract)
        {
            return toNegate.SelectMany((x, index) =>
            Enumerable.Range(index + 1, toNegate.Count() - 1)
           .SelectMany(i => Enumerable.Range(0, toNegate.Count() - i)
           .Select(j => new List<int> { x }
           .Concat(toNegate.Skip(i).Take(j)))))
           .Distinct()
           .Where(subsequence => subsequence.Sum() <= toSubstract);
        }        
    }
}
