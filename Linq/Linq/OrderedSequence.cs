using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linq
{
    public struct Range
    {
        public int lower;
        public int upper;
        public Range(int lowerLimit, int upperLimit)
        {
            this.lower = lowerLimit;
            this.upper = upperLimit;
        }
    }
    class OrderedSequence<TKey, T> : IOrderedEnumerable<T>
    {
        private readonly List<T> ordered;
        private IComparer<T> comparer;
        
        public OrderedSequence(IEnumerable<T> source, IComparer<T> comparer)
        {
            this.ordered = new List<T>(source);
            this.comparer = comparer;
        }

        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            IComparer<T> secondCriteria = new SourceComparer<T, TKey>(comparer, keySelector);
            for (int i = 0; i < this.ordered.Count - 1; i++)
            {
                int newKeyGroup = i + 1;
                bool isSameKey = this.comparer.Compare(this.ordered[i], this.ordered[newKeyGroup]) == 0;
                if (isSameKey)
                {
                    SortGroup<TKey>(secondCriteria, new Range(i, newKeyGroup), descending);
                }                
            }

            this.comparer = secondCriteria;
            return this;
        }

        private void SortGroup<TKey>(IComparer<T> secondCriteria, Range limits, bool descending)
        {
            while (limits.upper < this.ordered.Count && 
                this.comparer.Compare(this.ordered[limits.lower], this.ordered[limits.upper]) == 0)
            {
                limits.upper++;
            }

            
            Sort(secondCriteria, descending, new Range(limits.lower, limits.upper));
        }

        public IEnumerator<T> GetEnumerator()
        {
            Sort(this.comparer, false, new Range(0, this.ordered.Count));
            for (int i = 1; i < this.ordered.Count; i++)
            {
                yield return this.ordered[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Sort(IComparer<T> comparer, bool isDescending, Range limits)
        {
            T toCompare;
            int start;
            for (int i = limits.lower; i < limits.upper; i++)
            {
                toCompare = this.ordered[i];
                start = i - 1;
                while (start >= 0 && Orientation(this.ordered[i], this.ordered[start], comparer, isDescending))
                {
                    this.ordered[start + 1] = this.ordered[start];
                    start -= 1;
                }

                this.ordered[start + 1] = toCompare;
            }
        }

        private bool Orientation(T first, T second, IComparer<T> comparer, bool isDescending)
        {
            if (isDescending)
            {
                return this.comparer.Compare(first, second) >= 0;
            }

            return comparer.Compare(first, second) < 0;
        }
    }
}